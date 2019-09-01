using System;
using System.Collections.Generic;

namespace Turbo.Plugins.Default
{
    public static class LinqExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var decorator in source)
            {
                action(decorator);
            }
        }
    }
}