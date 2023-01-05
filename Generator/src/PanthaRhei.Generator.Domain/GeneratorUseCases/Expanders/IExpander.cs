using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Generators.Expanders
{
    /// <summary>
    /// Specifies the interface of an expander.
    /// </summary>
    public interface IExpander
    {
        /// <summary>
        /// Gets the Name of the <see cref="IExpander"/>.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the Order in which the <seealso cref="IExpander"/> should be executed.
        /// </summary>
        int Order { get; }

        /// <summary>
        /// Gets the <seealso cref="Expand">Model</seealso>.
        /// </summary>
        Expander Model { get; }

        /// <summary>
        /// Executes the <see cref="IExpander"/>.
        /// </summary>
        void Expand();

        /// <summary>
        /// Executes the expander harvesting.
        /// </summary>
        void Harvest();

        /// <summary>
        /// Executes the expander Rejuvination.
        /// </summary>
        void Rejuvenate();

        /// <summary>
        /// Executes the expander PostProcessing.
        /// </summary>
        void PostProcess();

        /// <summary>
        /// Executes the expander PreProcessing.
        /// </summary>
        void PreProcess();
    }
}
