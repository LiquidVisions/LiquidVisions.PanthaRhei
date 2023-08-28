namespace LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Harvesters
{
    /// <summary>
    /// Represents a contract for a generic Harvest Serializer.
    /// </summary>
    public interface IHarvestSerializer
    {
        /// <summary>
        /// Serializes a <see cref="Harvest"/> to a file.
        /// </summary>
        /// <param name="harvest"><seealso cref="Harvest"/></param>
        /// <param name="fullPath">The full path to the save location.</param>
        void Serialize(Harvest harvest, string fullPath);
    }
}
