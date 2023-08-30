using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Initializers
{
    /// <summary>
    /// Specifies a contract of an object that loads expanders as a plugin.
    /// </summary>
    public interface IExpanderPluginLoader
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
        /// <returns>A list of <seealso cref="IExpander"/>.</returns>
        ICollection<IExpander> ShallowLoadAllExpanders(string path);
    }
}
