using System.Collections.Generic;
using System.Reflection;
using LiquidVisions.PanthaRhei.Generator.Domain.Generators.Expanders;
using LiquidVisions.PanthaRhei.Generator.Domain.Generators.Handlers;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Initializers
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
        List<IExpander> ShallowLoadAllExpanders(string path);
    }
}
