using System;
using System.Collections.Generic;
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

        private static TBootstrapper AddDefaultDocsSettings<TBootstrapper>(this TBootstrapper bootstrapper)
            where TBootstrapper : IBootstrapper =>
            bootstrapper
                .AddSettingsIfNonExisting(new Dictionary<string, object>
                {
                    {
                        DocsKeys.SourceFiles,
                        new[]
                        {
                            "src/**/{!.git,!bin,!obj,!packages,!*.Tests,}/**/*.cs",
                            "../src/**/{!.git,!bin,!obj,!packages,!*.Tests,}/**/*.cs"
                        }
                    },
                    { DocsKeys.ApiPath, "api" },
                    { DocsKeys.OutputApiDocuments, true }
                });
    }
}