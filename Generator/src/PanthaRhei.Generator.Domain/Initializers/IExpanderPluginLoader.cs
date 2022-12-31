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
        void Load(App app);
    }
}
