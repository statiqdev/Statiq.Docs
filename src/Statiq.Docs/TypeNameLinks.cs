using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Statiq.Common;

namespace Statiq.Docs
{
    public class TypeNameLinks
    {
        public ConcurrentDictionary<string, string> Links { get; } = new ConcurrentDictionary<string, string>();
    }
}
