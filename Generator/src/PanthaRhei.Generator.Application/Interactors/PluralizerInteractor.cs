using Pluralize.NET.Core;

namespace LiquidVisions.PanthaRhei.Generator.Application.Interactors
{
    internal class PluralizerInteractor : IPluralizerInteractor
    {
        private readonly Pluralizer pluralizer = new();

        public string Pluralize(string input)
            => pluralizer.Pluralize(input);
    }
}
