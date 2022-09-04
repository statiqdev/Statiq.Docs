using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Statiq.CodeAnalysis;
using Statiq.Common;
using Statiq.Core;
using Statiq.Web;

namespace Statiq.Docs.Pipelines
{
    /// <summary>
    /// Uses Roslyn to analyze any source files loaded in the previous
    /// pipeline along with any specified assemblies. This pipeline
    /// results in documents that represent Roslyn symbols.
    /// </summary>
    public class Api : Pipeline
    {
        public Api()
        {
            Dependencies.Add(nameof(Code));

            // Will only get processed by the Content pipeline if OutputApiDocuments is true which sets
            // ContentType to ContentType.Content, otherwise pipeline will ignore output documents
            DependencyOf.Add(nameof(Web.Pipelines.Content));

            ProcessModules = new ModuleList(
                new ExecuteIf(
                    Config.FromContext(ctx =>
                        ctx.Outputs.FromPipeline(nameof(Code)).Any()
                        || ctx.Settings.GetList<string>(DocsKeys.AssemblyFiles)?.Count > 0
                        || ctx.Settings.GetList<string>(DocsKeys.ProjectFiles)?.Count > 0
                        || ctx.Settings.GetList<string>(DocsKeys.SolutionFiles)?.Count > 0),
                    new ConcatDocuments(nameof(Code)),
                    new CacheDocuments(
                        new ExecuteConfig(Config.FromContext(ctx =>
                            new AnalyzeCSharp()
                                .WhereNamespaces(ctx.Settings.GetBool(DocsKeys.IncludeGlobalNamespace))
                                .WherePublic()
                                .WithCssClasses("code", "language-csharp")
                                .WithDestinationPrefix(ctx.GetPath(DocsKeys.ApiPath))
                                .WithAssemblies(Config.FromContext<IEnumerable<string>>(ctx => ctx.GetList<string>(DocsKeys.AssemblyFiles)))
                                .WithProjects(Config.FromContext<IEnumerable<string>>(ctx => ctx.GetList<string>(DocsKeys.ProjectFiles)))
                                .WithSolutions(Config.FromContext<IEnumerable<string>>(ctx => ctx.GetList<string>(DocsKeys.SolutionFiles)))
                                .WithAssemblySymbols()
                                .WithImplicitInheritDoc(ctx.GetBool(DocsKeys.ImplicitInheritDoc)))),
                        new ExecuteConfig(Config.FromDocument((doc, ctx) =>
                        {
                            // Calculate a type name to link lookup for auto linking
                            string name = null;
                            string kind = doc.GetString(CodeAnalysisKeys.Kind);
                            if (kind == "NamedType")
                            {
                                name = doc.GetString(CodeAnalysisKeys.DisplayName);
                            }
                            else if (kind == "Property" || kind == "Method")
                            {
                                IDocument containingType = doc.GetDocument(CodeAnalysisKeys.ContainingType);
                                if (containingType != null)
                                {
                                    name = $"{containingType.GetString(CodeAnalysisKeys.DisplayName)}.{doc.GetString(CodeAnalysisKeys.DisplayName)}";
                                }
                            }
                            if (name != null)
                            {
                                TypeNameLinks typeNameLinks = ctx.GetService<TypeNameLinks>();
                                typeNameLinks.Links.AddOrUpdate(WebUtility.HtmlEncode(name), ctx.GetLink(doc), (x, y) => string.Empty);
                            }

                            // Add metadata
                            MetadataItems metadataItems = new MetadataItems();

                            // Calculate an xref that includes a "api-" prefix to avoid collisions
                            metadataItems.Add(WebKeys.Xref, "api-" + doc.GetString(CodeAnalysisKeys.QualifiedName));

                            // Add the layout path if one was defined
                            NormalizedPath apiLayout = ctx.GetPath(DocsKeys.ApiLayout);
                            if (!apiLayout.IsNullOrEmpty)
                            {
                                metadataItems.Add(WebKeys.Layout, apiLayout);
                            }

                            // Change the content provider if needed
                            IContentProvider contentProvider = doc.ContentProvider;
                            if (ctx.GetBool(DocsKeys.OutputApiDocuments))
                            {
                                contentProvider = doc.ContentProvider.CloneWithMediaType(MediaTypes.Html);
                                metadataItems.Add(WebKeys.ContentType, ContentType.Content);
                            }

                            return doc.Clone(metadataItems, contentProvider);
                        })))
                        .WithoutSourceMapping()));
        }
    }
}