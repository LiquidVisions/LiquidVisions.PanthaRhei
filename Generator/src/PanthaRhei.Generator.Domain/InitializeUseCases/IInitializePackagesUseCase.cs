using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.InitializeUseCases
{
    internal interface IInitializePackagesUseCase
    {
        void Initialize(IEnumerable<Component> components);

        void DeleteAll();
    }
}
