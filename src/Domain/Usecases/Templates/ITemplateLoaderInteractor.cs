namespace LiquidVisions.PanthaRhei.Domain.Usecases.Templates
{
    /// <summary>
    /// Interface for static template loader.
    /// </summary>
    public interface ITemplateLoaderInteractor
    {
        /// <summary>
        /// Loads the static template.
        /// </summary>
        /// <param name="fullPathToTemplateFile">The full path to the template file.</param>
        /// <returns>Retruns a string.</returns>
        string Load(string fullPathToTemplateFile);
    }
}
