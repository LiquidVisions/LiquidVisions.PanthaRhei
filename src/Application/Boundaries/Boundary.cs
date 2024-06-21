using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Application.RequestModels;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Usecases.CreateNewExpander;
using LiquidVisions.PanthaRhei.Domain.Usecases.UpdatePackages;

namespace LiquidVisions.PanthaRhei.Application.Boundaries
{
    /// <summary>
    /// Represents the boundary of the application.
    /// </summary>
    /// <param name="newExpander"><seealso cref="NewExpanderRequestModel"/></param>
    /// <param name="updateCorePackages"><seealso cref="IUpdatePackagesUseCase"/></param>
    internal class Boundary(ICreateNewExpander newExpander, IUpdatePackagesUseCase updateCorePackages) : IBoundary
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

        public Task<Response> UpdatePackages(string root)
            => updateCorePackages.Execute(root);
    }
}
