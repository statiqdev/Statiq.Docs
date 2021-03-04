using System;
using System.Collections.Generic;
using System.Text;
using Statiq.App;
using Statiq.Common;
using Statiq.Core;
using Statiq.Web;

namespace Statiq.Docs
{
    public static class BootstrapperFactoryExtensions
    {
        /// <summary>
        /// Creates a bootstrapper with all functionality for Statiq Docs.
        /// </summary>
        /// <remarks>
        /// This includes Statiq Web functionality so
        /// <see cref="Statiq.Web.BootstrapperFactoryExtensions.CreateWeb(BootstrapperFactory, string[])"/>
        /// does not need to be called in addition to this method.
        /// </remarks>
        /// <param name="factory">The bootstrapper factory.</param>
        /// <param name="args">The command line arguments.</param>
        /// <returns>A bootstrapper.</returns>
        public static Bootstrapper CreateDocs(this BootstrapperFactory factory, string[] args) =>
            factory.CreateWeb(args).AddDocs();
    }
}
