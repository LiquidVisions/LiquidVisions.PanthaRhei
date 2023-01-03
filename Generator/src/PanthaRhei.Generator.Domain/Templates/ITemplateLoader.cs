namespace LiquidVisions.PanthaRhei.Generator.Domain.Templates
{
    /// <summary>
    /// Interface for static template loader.
    /// </summary>
    public interface ITemplateLoader
    {
        /// <summary>
        /// Loads the static template.
        /// </summary>
        /// <param name="fullPathToTemplateFile">The full path to the template file.</param>
        /// <returns>Retruns a string.</returns>
        string Load(string fullPathToTemplateFile);
    }
}
