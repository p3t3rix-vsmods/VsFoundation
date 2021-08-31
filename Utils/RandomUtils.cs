using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.Utils
{
    public static class RandomUtils
    {
        [ThreadStatic]
        private static Random __random;

        public static Random Random => __random ?? (__random = new Random((int)((1 + System.Threading.Thread.CurrentThread.ManagedThreadId) * DateTime.UtcNow.Ticks)));
    }
}
