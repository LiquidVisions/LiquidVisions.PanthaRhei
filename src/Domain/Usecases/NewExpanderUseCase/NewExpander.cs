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
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the new expander.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the path where the expander will be created.
        /// </summary>
        public string Path { get; set; }
    }
}
