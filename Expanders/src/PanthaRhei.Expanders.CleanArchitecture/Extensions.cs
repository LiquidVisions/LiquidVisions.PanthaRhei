using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using Pluralize.NET.Core;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture
{
    internal static class Extensions
    {
        private static readonly Pluralizer pluralizer = new();

        internal static string GetPathToTemplate(this Expander expander, GenerationOptions expandRequestModel, string templateName)
        {
            return Path.Combine(expandRequestModel.ExpandersFolder, expander.Name, expander.TemplateFolder, $"{templateName}.template");
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

        internal static bool CanExecuteDefaultAndExtend(this GenerationOptions expandRequestModel)
        {
            return expandRequestModel.GenerationMode.HasFlag(GenerationModes.Default)
                || expandRequestModel.GenerationMode.HasFlag(GenerationModes.Extend);
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
