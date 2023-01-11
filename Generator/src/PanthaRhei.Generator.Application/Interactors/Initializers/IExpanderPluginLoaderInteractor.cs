using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Generator.Application.Interactors.Initializers
{
    /// <summary>
    /// Specifies a contract of an object that loads expanders as a plugin.
    /// </summary>
    public interface IExpanderPluginLoaderInteractor
    {
        /// <summary>
        /// Executes the loading command.
        /// </summary>
        /// <param name="app"><seealso cref="App"/></param>
        void LoadAllRegisteredPluginsAndBootstrap(App app);

        /// <summary>
        /// Executes the loading command.
        /// </summary>
        /// <param name="path">Path to the expander folder.</param>
        /// <returns>A list of <seealso cref="IExpanderInteractor"/>.</returns>
        List<IExpanderInteractor> ShallowLoadAllExpanders(string path);
    }
}
