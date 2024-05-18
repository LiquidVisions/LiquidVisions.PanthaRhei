namespace LiquidVisions.PanthaRhei.Domain.Usecases.NewExpanderUseCase
{
    /// <summary>
    /// A request model for creating a new expander.
    /// </summary>
    public class NewExpander
    {
        /// <summary>
        /// Gets or sets the name of the new expander.
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Gets or sets the type of the new expander.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the path where the expander will be created.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the path where the expander will output its builds.
        /// </summary>
        public string BuildPath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the expander should be built.
        /// </summary>
        public bool Build { get; set; }
    }
}
