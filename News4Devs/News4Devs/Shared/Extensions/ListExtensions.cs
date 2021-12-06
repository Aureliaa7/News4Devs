using System;
using System.Collections.Generic;
using System.Linq;

namespace News4Devs.Shared.Extensions
{
    public static class ListExtensions
    {
        public static List<T> Randomize<T>(this List<T> source)
        {
            Random rnd = new();
            return source.OrderBy((_) => rnd.Next()).ToList();
        }
    }
}
