using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using Pluralize.NET.Core;
using Scriban.Runtime;

namespace LiquidVisions.PanthaRhei.Generator.Application.Interactors.Templates
{
    public class CustomScripts : ScriptObject
    {
        private static readonly Pluralizer pluralizer = new();

        public static string Pluralize(string name)
            => pluralizer.Pluralize(name);

        public static string ComponentFullname(Component component, params string[] str)
        {
            string result = $"{component.Expander.Apps.Single().FullName}.{component.Name}";
            if (str != null && str.Length > 0)
            {
                result = $"{result}.{string.Join('.', str)}";
            }

            return result;
        }

        public static string AppFullname(Component component, params string[] str)
        {
            string result = $"{component.Expander.Apps.Single().FullName}";
            if (str != null)
            {
                result = $"{result}.{string.Join('.', str)}";
            }

            return result;
        }
    }
}
