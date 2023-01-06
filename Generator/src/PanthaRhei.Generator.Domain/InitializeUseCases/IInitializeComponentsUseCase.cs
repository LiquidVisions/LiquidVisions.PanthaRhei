using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generator.Domain.InitializeUseCases
{
    internal interface IInitializeComponentsUseCase
    {
        void DeleteAll();

        IEnumerable<Component> Initialize(IEnumerable<Expander> expanders);
    }
}
