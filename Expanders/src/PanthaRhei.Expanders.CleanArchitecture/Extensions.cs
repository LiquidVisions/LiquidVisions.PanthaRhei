using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using Pluralize.NET.Core;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture
{
    internal static class Extensions
    {
        private static readonly Pluralizer pluralizer = new();

        internal static string GetTemplateFolder(this Expander expander, Parameters parameters, string templateName)
        {
            return Path.Combine(parameters.ExpandersFolder, expander.Name, expander.TemplateFolder, $"{templateName}.template");
        }

        internal static string Pluralize(this string str) => pluralizer.Pluralize(str);

        internal static string GetComponentNamespace(this Component component, App app, string ns = null)
        {
            string result = $"{app.FullName}.{component.Name}";
            if(ns != null)
            {
                result = $"{result}.{ns}";
            }

            return result;
        }

        internal static bool CanExecuteDefaultAndExtend(this Parameters parameters)
        {
            return parameters.GenerationMode.HasFlag(GenerationModes.Default)
                || parameters.GenerationMode.HasFlag(GenerationModes.Extend);
        }

        internal static string ToFileName(this Entity entity, string action, string postfix) =>
            action switch
            {
                "Get" => $"Get{entity.Name.Pluralize()}{postfix}",
                "GetById" => $"Get{entity.Name}ById{postfix}",
                _ => $"{action}{entity.Name}{postfix}"
            };
    }
}
