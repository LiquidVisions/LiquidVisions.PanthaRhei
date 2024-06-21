using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Application.RequestModels;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Usecases.CreateNewExpander;
using LiquidVisions.PanthaRhei.Domain.Usecases.UpdatePackages;
using LiquidVisions.PanthaRhei.Domain.Usecases.UpdateCore;

namespace LiquidVisions.PanthaRhei.Application.Boundaries
{
    /// <summary>
    /// Represents the boundary of the application.
    /// </summary>
    /// <param name="newExpander"><seealso cref="NewExpanderRequestModel"/></param>
    /// <param name="updateCorePackages"><seealso cref="IUpdateCoreUseCase"/></param>
    /// <param name="updateCore"><seealso cref="IUpdateCoreUseCase"/></param>
    internal class Boundary(ICreateNewExpander newExpander, IUpdatePackagesUseCase updateCorePackages, IUpdateCoreUseCase updateCore) : IBoundary
    {
        public async Task<Response> CreateNewExpander(NewExpanderRequestModel model)
        {
            CreateNewExpanderRequestModel dto = new()
            {
                ShortName = model.ShortName,
                FullName = model.FullName,
                Path = model.Path,
                BuildPath = model.BuildPath,
                Build = model.Build,
            };

            Response response = await newExpander.Execute(dto)
                .ConfigureAwait(false);

            return response;
        }

        public Task<Response> UpdateCore()
            => updateCore.Update();

        public Task<Response> UpdatePackages(string root)
            => updateCorePackages.Execute(root);
    }
}
