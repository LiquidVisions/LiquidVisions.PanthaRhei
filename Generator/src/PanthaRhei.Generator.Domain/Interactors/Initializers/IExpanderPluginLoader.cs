﻿using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Initializers
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