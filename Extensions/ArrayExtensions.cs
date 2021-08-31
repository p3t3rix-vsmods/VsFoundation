using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.Extensions
{
    public static class ArrayExtensions
    {
        private static Random Rand = new Random();

        public static T RandomElement<T>(this T[] items)
        {
            if (items.Length == 0)
                return default;

            return items[Rand.Next(0, items.Length)];
        }

        public static T RandomElement<T>(this List<T> items)
        {
            if (items.Count == 0)
                return default;

            return items[Rand.Next(0, items.Count)];
        }
    }
}
