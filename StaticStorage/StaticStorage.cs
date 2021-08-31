using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.StaticStorage
{
    public static class StaticStorage
    {
        private static ConcurrentDictionary<string, object> keyToObjectDict = new ConcurrentDictionary<string, object>();

        public static void SetValue(string key, object value)
        {
            StaticStorage.keyToObjectDict[key] = value;
        }

        public static T GetValue<T>(string key, T @default = default)
        {
            if (!StaticStorage.keyToObjectDict.TryGetValue(key, out var value))
                return @default;

            return (T)value;
        }

        public static void Add(string key, int modifier = 1)
        {
            keyToObjectDict.AddOrUpdate(key, modifier, (dictKey, dictValue) => dictValue = ((int)dictValue) + modifier);
        }

        public static void Subtract(string key, int modifier = 1)
        {
            keyToObjectDict.AddOrUpdate(key, modifier, (dictKey, dictValue) => dictValue = ((int)dictValue) - modifier);
        }
    }
}
