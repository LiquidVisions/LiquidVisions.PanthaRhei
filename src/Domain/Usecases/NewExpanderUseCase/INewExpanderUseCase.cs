using System.Threading.Tasks;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.NewExpanderUseCase
{
    /// <summary>
    /// Represents the contract for the new expander use case.
    /// </summary>
    public interface INewExpanderUseCase
    {
        /// <summary>
        /// Executes the new expander use case.
        /// </summary>
        /// <param name="model"><seealso cref="NewExpander"/></param>
        /// <returns><seealso cref="Task{Response}"/></returns>
        Task<Response> Execute(NewExpander model);
    }
}
