using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;
using Pluralize.NET.Core;
using Scriban.Runtime;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Templates
{
    public class SqlScriptObject : ScriptObject
    {
        private static Pluralizer pluralizer = new Pluralizer();

        public static string RenderDeleteStatements(App app)
        {
            StringBuilder sb = new();

            string[] all = GetComplexTypes(app.GetType().BaseType, new List<Type>())
                .Select(x => pluralizer.Pluralize(x.Name))
                .ToArray();

            foreach (string str in all)
            {
                sb.Append($"DELETE FROM {str}");
                if(all.Last() != str)
                {
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        public static string GetAllFields(DataType dataType)
        {
            Type type = dataType.GetType().BaseType;

            var result = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => x.PropertyType != null && !x.PropertyType.IsGenericType)
                .Select(x => x.Name);

            return $"( {string.Join(", ", result)} )";
        }

        private static IEnumerable<Type> GetComplexTypes(Type obj, List<Type> all)
        {
            if (all.Contains(obj))
            {
                // The obj type has already been processed, so we don't need to process it again
                yield return obj;
            }

            var nestedTypes = obj.GetProperties(BindingFlags.Instance | BindingFlags.Public)
               .Where(x => x.PropertyType.IsClass && x.PropertyType != typeof(string))
               .Select(x => x.PropertyType.IsGenericType ? x.PropertyType.GetGenericArguments().Single() : x.PropertyType) // Gets the generic type in case of generics
               .Where(x => !all.Contains(x))
               .ToList();

            all.AddRange(nestedTypes);

            foreach (Type type in nestedTypes)
            {
                foreach (var nested in GetComplexTypes(type, all))
                {
                    yield return nested;
                }
            }
        }
    }
}
