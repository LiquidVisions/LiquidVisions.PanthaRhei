using LiquidVisions.PanthaRhei.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Domain.Usecases
{
    /// <summary>
    /// Represents a contract for a generic Application.
    /// </summary>
    public interface IApplication
    {
        /// <summary>
        /// Adds all the packages to the application.
        /// </summary>
        /// <param name="component"></param>
        void AddPackages(Component component);

        /// <summary>
        /// Gets the configuration file of a component.
        /// </summary>
        /// <param name="component"><seealso cref="Component"/></param>
        /// <returns>Full path the the configuration file</returns>
        string GetComponentConfigurationFile(Component component);

        /// <summary>
        /// Gets the root of a component.
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        string GetComponentRoot(Component component);

        /// <summary>
        /// Materialized the component.
        /// </summary>
        /// <param name="component"></param>
        void MaterializeComponent(Component component);

        /// <summary>
        /// Materializes the project.
        /// </summary>
        void MaterializeProject();
    }
}
