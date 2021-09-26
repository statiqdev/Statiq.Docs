using System;
using System.Collections.Generic;
using System.Text;
using Statiq.Common;

namespace Statiq.Docs
{
    public static class DocsKeys
    {
        ////////// Global

        /// <summary>
        /// Sets a semantic version range of Statiq Docs that must be used.
        /// Particularly useful for themes to set a supported version range.
        /// </summary>
        public const string MinimumStatiqDocsVersion = nameof(MinimumStatiqDocsVersion);

        /// <summary>
        /// Indicates where to locate source files for the API documentation.
        /// By default the globbing pattern "src/**/{!bin,!obj,!packages,!*.Tests,}/**/*.cs"
        /// is used which searches for all "*.cs" files at any depth under a "src" folder
        /// but not under "bin", "obj", "packages" or "Tests" folders. You can specify
        /// your own globbing pattern (or more than one globbing pattern) if your source
        /// files are found elsewhere.
        /// </summary>
        /// <type><see cref="string"/> or <c>IEnumerable&lt;string&gt;</c></type>
        public const string SourceFiles = nameof(SourceFiles);

        /// <summary>
        /// Indicates where to locate project files for the API documentation.
        /// </summary>
        /// <type><see cref="string"/> or <c>IEnumerable&lt;string&gt;</c></type>
        public const string ProjectFiles = nameof(ProjectFiles);

        /// <summary>
        /// Indicates where to locate solution files for the API documentation.
        /// </summary>
        /// <type><see cref="string"/> or <c>IEnumerable&lt;string&gt;</c></type>
        public const string SolutionFiles = nameof(SolutionFiles);

        /// <summary>
        /// Indicates where to locate assemblies for the API documentation. You can specify
        /// one (or more) globbing pattern(s).
        /// </summary>
        /// <type><see cref="string"/> or <c>IEnumerable&lt;string&gt;</c></type>
        public const string AssemblyFiles = nameof(AssemblyFiles);

        /// <summary>
        /// Controls whether the global namespace is included in your API
        /// documentation.
        /// </summary>
        /// <type><see cref="bool"/></type>
        public const string IncludeGlobalNamespace = nameof(IncludeGlobalNamespace);

        /// <summary>
        /// Controls the parent path where API docs are placed. The default is "api".
        /// </summary>
        /// <type><see cref="NormalizedPath"/> or <see cref="string"/></type>
        public const string ApiPath = nameof(ApiPath);

        /// <summary>
        /// The path to a layout file for use with API documents. Themes can use this
        /// to set a consistent layout that works even when <see cref="ApiPath"/> changes.
        /// If undefined, normal layout file searching rules will apply.
        /// </summary>
        /// <type><see cref="NormalizedPath"/> or <see cref="string"/></type>
        public const string ApiLayout = nameof(ApiLayout);

        /// <summary>
        /// Setting this to <c>true</c> will assume <c>inheritdoc</c> for all API symbols
        /// that don't provide their own documentation comments.
        /// </summary>
        /// <type><see cref="bool"/></type>
        public const string ImplicitInheritDoc = nameof(ImplicitInheritDoc);

        /// <summary>
        /// Controls whether API symbol documents should be output or treated only as data.
        /// The default is <c>true</c> which outputs pages for each symbol.
        /// </summary>
        public const string OutputApiDocuments = nameof(OutputApiDocuments);
    }
}