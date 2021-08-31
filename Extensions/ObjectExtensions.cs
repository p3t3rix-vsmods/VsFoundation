using System.Collections.Generic;

namespace Foundation.Extensions
{
    public static class ObjectExtensions
    { 
        public static List<T> AsList<T> (this T obj) => new List<T> {obj};
    }
}
