using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.ModelInitializers
{
    internal interface IInitializeComponentsUseCase
    {
        void DeleteAll();

        IEnumerable<Component> Initialize(IEnumerable<Expander> expanders);
    }
}
