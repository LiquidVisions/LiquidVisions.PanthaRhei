using LiquidVisions.PanthaRhei.Generator.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generator.Domain.InitializeUseCases
{
    internal interface IInitializeFieldsUseCase
    {
        void Initialize(App app);

        void DeleteAll();
    }

}
