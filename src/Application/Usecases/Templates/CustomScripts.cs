using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;
using Pluralize.NET.Core;
using Scriban.Runtime;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Templates
{
    /// <summary>
    /// Custom <seealso cref="ScriptObject"/> for Scriban."/>
    /// </summary>
    public class CustomScripts : ScriptObject
    {
        private static readonly Pluralizer s_pluralizer = new();
        private static readonly Dictionary<string, IElementTemplateParameters> s_elementTemplateParameters = new();

        /// <summary>
        /// default constructor.
        /// </summary>
        /// <param name="parameters"><seealso cref="IEnumerable{IElementTemplateParameters}"/></param>
        public CustomScripts(IEnumerable<IElementTemplateParameters> parameters)
        {
            foreach (IElementTemplateParameters par in parameters)
            {
                if (!s_elementTemplateParameters.ContainsKey(par.ElementType))
                {
                    s_elementTemplateParameters.Add(par.ElementType, par);
                }
            }
        }

        /// <summary>
        /// Gets the prefix for the element type.
        /// </summary>
        /// <param name="elementType"></param>
        /// <returns></returns>
        public static string GetPostfix(string elementType)
            => s_elementTemplateParameters[elementType].NamePostfix;

        /// <summary>
        /// Gets the prefix for the element type.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string Pluralize(string name)
            => s_pluralizer.Pluralize(name);

        /// <summary>
        /// Gets the prefix for the element type.
        /// </summary>
        /// <param name="component">The <seealso cref="Component"/>.</param>
        /// <param name="segments">The segments.</param>
        /// <returns></returns>
        public static string ComponentFullname(Component component, params string[] segments)
        {
            string result = $"{component.Expander.Apps.Single().FullName}.{component.Name}";
            if (segments != null && segments.Length > 0)
            {
                result = $"{result}.{string.Join('.', segments)}";
            }

            return result;
        }

        /// <summary>
        /// Appends the <paramref name="segments"/> to the <seealso cref="App.FullName"/>.
        /// </summary>
        /// <param name="component">The <seealso cref="Component"/>.</param>
        /// <param name="segments"></param>
        /// <returns></returns>
        public static string AppFullname(Component component, params string[] segments)
        {
            string result = $"{component.Expander.Apps.Single().FullName}";
            if (segments != null)
            {
                result = $"{result}.{string.Join('.', segments)}";
            }

            return result;
        }
    }
}
