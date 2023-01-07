using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using Pluralize.NET.Core;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture
{
    internal static class Extensions
    {
        private static readonly Pluralizer pluralizer = new();

        public static string GetTemplateFolder(this Expander expander, Parameters parameters, string templateName)
        {
            return Path.Combine(parameters.ExpandersFolder, expander.Name, expander.TemplateFolder, $"{templateName}.template");
        }

        public static string Pluralize(this string str) => pluralizer.Pluralize(str);

        public static string GetComponentNamespace(this Component component, App app, string ns = null)
        {
            string result = $"{app.FullName}.{component.Name}";
            if(ns != null)
            {
                result = $"{result}.{ns}";
            }

            return result;
        }
    }
}
