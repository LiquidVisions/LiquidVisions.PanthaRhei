namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates
{
    /// <summary>
    /// Loads and renders Templates.
    /// </summary>
    public interface ITemplateService
    {
        /// <summary>
        /// Renders the template to an actual instance.
        /// </summary>
        /// <param name="fullTemplatePath">The full path to the template.</param>
        /// <param name="model">The template parameters.</param>
        /// <returns>The rendered instance as a string.</returns>
        string Render(string fullTemplatePath, object model);

        /// <summary>
        /// Creates the generated file and the folder if needed.
        /// </summary>
        /// <param name="fullPathToTemplate">The template folder path.</param>
        /// <param name="parameters">The template parameters.</param>
        /// <param name="fullPathToOutput">Full path of the file that should be generated.</param>
        void RenderAndSave(string fullPathToTemplate, object parameters, string fullPathToOutput);
    }
}
