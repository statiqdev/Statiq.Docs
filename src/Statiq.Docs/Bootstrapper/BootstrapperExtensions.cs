using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Statiq.Common;
using Statiq.Core;

namespace Statiq.Docs
{
    public static class BootstrapperExtensions
    {
        /// <summary>
        /// Adds Statiq Docs functionality to an existing bootstrapper.
        /// </summary>
        /// <remarks>
        /// This method is useful when you want to add Statiq Docs support to an existing bootstrapper,
        /// for example because you created the bootstrapper without certain default functionality
        /// or don't want to add Statiq Web functionality.
        /// </remarks>
        /// <param name="boostrapper">The bootstrapper to add Statiq Docs functionality to.</param>
        /// <returns>The bootstrapper.</returns>
        public static TBootstrapper AddDocs<TBootstrapper>(this TBootstrapper boostrapper)
            where TBootstrapper : IBootstrapper =>
            boostrapper
                .AddPipelines(typeof(BootstrapperExtensions).Assembly)
                .AddDocsServices()
                .AddDefaultDocsSettings()
                .ConfigureEngine(e => e.LogAndCheckVersion(typeof(BootstrapperExtensions).Assembly, "Statiq Docs", DocsKeys.MinimumStatiqDocsVersion));

        private static TBootstrapper AddDocsServices<TBootstrapper>(this TBootstrapper bootstrapper)
            where TBootstrapper : IBootstrapper =>
            bootstrapper
                .ConfigureServices(services => services
                    .AddSingleton(new TypeNameLinks()));

        private static readonly string[] DefaultSourceFiles = new[]
        {
            "src/**/{!.git,!bin,!obj,!packages,!*.Tests,}/**/*.cs",
            "../src/**/{!.git,!bin,!obj,!packages,!*.Tests,}/**/*.cs"
        };

        private static TBootstrapper AddDefaultDocsSettings<TBootstrapper>(this TBootstrapper bootstrapper)
            where TBootstrapper : IBootstrapper =>
            bootstrapper
                .AddSettingsIfNonExisting(new Dictionary<string, object>
                {
                    {
                        DocsKeys.SourceFiles,
                        DefaultSourceFiles
                    },
                    { DocsKeys.ApiPath, "api" },
                    { DocsKeys.OutputApiDocuments, true }
                });

        public static TBootstrapper AddSourceFiles<TBootstrapper>(
            this TBootstrapper bootstrapper,
            params string[] sourceFiles)
            where TBootstrapper : IBootstrapper =>
            bootstrapper.AddCodeFiles(DocsKeys.SourceFiles, DefaultSourceFiles, sourceFiles);

        public static TBootstrapper AddProjectFiles<TBootstrapper>(
            this TBootstrapper bootstrapper,
            params string[] sourceFiles)
            where TBootstrapper : IBootstrapper =>
            bootstrapper.AddCodeFiles(DocsKeys.ProjectFiles, null, sourceFiles);

        public static TBootstrapper AddSolutionFiles<TBootstrapper>(
            this TBootstrapper bootstrapper,
            params string[] sourceFiles)
            where TBootstrapper : IBootstrapper =>
            bootstrapper.AddCodeFiles(DocsKeys.SolutionFiles, null, sourceFiles);

        public static TBootstrapper AddAssemblyFiles<TBootstrapper>(
            this TBootstrapper bootstrapper,
            params string[] sourceFiles)
            where TBootstrapper : IBootstrapper =>
            bootstrapper.AddCodeFiles(DocsKeys.AssemblyFiles, null, sourceFiles);

        private static TBootstrapper AddCodeFiles<TBootstrapper>(
            this TBootstrapper bootstrapper,
            string key,
            string[] defaultValues,
            string[] values)
            where TBootstrapper : IBootstrapper =>
            bootstrapper
                .ConfigureSettings(settings =>
                {
                    if (values?.Length > 0)
                    {
                        HashSet<string> aggregateValues = new HashSet<string>();
                        if (settings.TryGetValue(key, out object valueObject)
                            && (defaultValues is null || valueObject != defaultValues))
                        {
                            if (valueObject is IEnumerable<string> currentValues)
                            {
                                aggregateValues.AddRange(currentValues);
                            }

                            if (valueObject is string currentValue)
                            {
                                aggregateValues.Add(currentValue);
                            }
                        }

                        aggregateValues.AddRange(values);
                        settings[key] = aggregateValues;
                    }
                });
    }
}