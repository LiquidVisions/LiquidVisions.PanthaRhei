using System.Threading.Tasks;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.CreateNewExpander
{
    /// <summary>
    /// Represents the contract for the new expander use case.
    /// </summary>
    public interface ICreateNewExpander
    {
        /// <summary>
        /// Executes the new expander use case.
        /// </summary>
        /// <param name="model"><seealso cref="CreateNewExpanderRequestModel"/></param>
        /// <returns><seealso cref="Task{Response}"/></returns>
        Task<Response> Execute(CreateNewExpanderRequestModel model);
    }
}
