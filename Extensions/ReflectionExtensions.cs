using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace Foundation.Extensions
{
    public static class ReflectionExtensions
    {
        public static T GetField<T>(this object instance, string fieldname)
        {
            return (T)AccessTools.Field(instance.GetType(), fieldname).GetValue(instance);
        }
    }
}
