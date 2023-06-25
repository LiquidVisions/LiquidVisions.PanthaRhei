using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;
using Pluralize.NET.Core;
using Scriban.Runtime;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Templates
{
    public class CustomScripts : ScriptObject
    {
        private static readonly Pluralizer pluralizer = new();
        private static readonly Dictionary<string, IElementTemplateParameters> elementTemplateParameters = new();

        public CustomScripts(IEnumerable<IElementTemplateParameters> parameters)
        {
            foreach (var par in parameters)
            {
                if (!elementTemplateParameters.ContainsKey(par.ElementType))
                {
                    elementTemplateParameters.Add(par.ElementType, par);
                }
            }
        }

        public static string GetPostfix(string elementType)
            => elementTemplateParameters[elementType].NamePostfix;

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
