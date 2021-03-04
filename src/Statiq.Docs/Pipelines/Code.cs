using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Statiq.Common;
using Statiq.Core;

namespace Statiq.Docs.Pipelines
{
    /// <summary>
    /// Loads source files.
    /// </summary>
    public class Code : Pipeline
    {
        public Code()
        {
            InputModules = new ModuleList(
                new ReadFiles(
                    Config.FromSettings(settings
                        => settings.GetList<string>(DocsKeys.SourceFiles).AsEnumerable())));
        }
    }
}
