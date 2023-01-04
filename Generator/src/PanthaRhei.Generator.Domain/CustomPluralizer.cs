using Pluralize.NET.Core;

namespace LiquidVisions.PanthaRhei.Generator.Domain
{
    internal class CustomPluralizer : IPluralizer
    {
        private readonly Pluralizer pluralizer = new();

        public string Pluralize(string input) 
            => pluralizer.Pluralize(input);
    }
}
