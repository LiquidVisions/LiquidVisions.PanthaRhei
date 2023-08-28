namespace LiquidVisions.PanthaRhei.Domain.Usecases.Templates
{
    /// <summary>
    /// Represents a contract for a Element Template Parameters.
    /// </summary>
    public interface IElementTemplateParameters
    {
        /// <summary>
        /// Gets the type of the element.
        /// </summary>
        string ElementType { get; }

        /// <summary>
        /// Gets the name postfix of the element.
        /// </summary>
        string NamePostfix { get; }
    }
}
