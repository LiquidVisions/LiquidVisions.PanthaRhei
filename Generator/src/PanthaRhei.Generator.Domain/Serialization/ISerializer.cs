namespace LiquidVisions.PanthaRhei.Generator.Domain.Serialization
{
    /// <summary>
    /// A generic object that serializes the <typeparamref name="TModel"/>.
    /// </summary>
    /// <typeparam name="TModel">The subjected model.</typeparam>
    public interface ISerializer<in TModel>
        where TModel : new()
    {
        /// <summary>
        /// Serialized <typeparamref name="TModel"/>.
        /// </summary>
        /// <param name="path">the full path to <typeparamref name="TModel"/>.</param>
        /// <param name="model"><typeparamref name="TModel"/>.</param>
        public void Serialize(string path, TModel model);

        /// <summary>
        /// Serialized the <typeparamref name="TModel"/> into a string.
        /// </summary>
        /// <param name="model"><typeparamref name="TModel"/>.</param>
        /// <returns>a string representation of <paramref name="model"/>.</returns>
        public string SerializeToString(TModel model);
    }
}
