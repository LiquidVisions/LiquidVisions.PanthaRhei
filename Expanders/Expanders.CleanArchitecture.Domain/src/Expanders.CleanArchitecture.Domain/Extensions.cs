using System.IO;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using Pluralize.NET.Core;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Domain
{
    internal static class Extensions
    {
        private static readonly Pluralizer pluralizer = new();

        internal static string GetPathToTemplate(this Expander expander, GenerationOptions options, string templateName)
        {
            return Path.Combine(options.ExpandersFolder, expander.Name, LiquidVisions.PanthaRhei.Domain.Resources.TemplatesFolder, $"{templateName}.template");
        }

        internal static string Pluralize(this string str) => pluralizer.Pluralize(str);

        internal static string GetComponentNamespace(this Component component, App app, string ns = null)
        {
            string result = $"{app.FullName}.{component.Name}";
            if (ns != null)
            {
                result = $"{result}.{ns}";
            }

            return result;
        }

        internal static bool CanExecuteDefaultAndExtend(this GenerationOptions expandRequestModel)
        {
            return expandRequestModel.Modes.HasFlag(GenerationModes.Default)
                || expandRequestModel.Modes.HasFlag(GenerationModes.Extend);
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
