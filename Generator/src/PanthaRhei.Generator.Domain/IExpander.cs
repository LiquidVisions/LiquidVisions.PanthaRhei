namespace LiquidVisions.PanthaRhei.Generator.Domain
{
    /// <summary>
    /// Specifies a a contract for an expander.
    /// </summary>
    public interface IExpander
    {
        /// <summary>
        /// Gets the name of the expander.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///  Executes the harvesting process.
        /// </summary>
        public void Harvest();

        /// <summary>
        ///  Executes the preparation process.
        /// </summary>
        public void Prepare();

        /// <summary>
        /// Executes the expanding process.
        /// </summary>
        public void Expand();

        /// <summary>
        /// Execute the rejuvenation process.
        /// </summary>
        public void Rejuvenate();

        /// <summary>
        /// Executes the finalizing process.
        /// </summary>
        public void Finish();
    }
}
