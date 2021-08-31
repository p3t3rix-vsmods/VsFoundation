using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.Extensions
{
    public static class ObjectExtensions
    {
        public static List<T> AsList<T>(this T obj) => new List<T> { obj };

        public static dynamic ToDynamic(this object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
                expando.Add(property.Name, property.GetValue(value));
            foreach (FieldInfo field in value.GetType().GetFields())
                expando.Add(field.Name, field.GetValue(value));

            return expando as ExpandoObject;
        }
    }
}
