using System.Threading.Tasks;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.UpdateCoreUseCase
{
    /// <summary>
    /// Represents the contract for the update core packages use case.
    /// </summary>
    public interface IUpdateCorePackages
    {
        /// <summary>
        /// Executes the update core packages use case.
        /// </summary>
        /// <param name="root">The root directory on where the use case is executed</param>
        /// <returns><seealso cref="Task{Response}"/></returns>
        Task<Response> Execute(string root);
    }
}
