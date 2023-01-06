using System.Xml.Linq;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Serialization
{
    /// <summary>
    /// A generic object that serializes the <typeparamref name="TModel"/>.
    /// </summary>
    /// <typeparam name="TModel">The subjected model.</typeparam>
    public interface IDeserializer<out TModel>
        where TModel : new()
    {
        /// <summary>
        /// Deserializes to a <seealso cref="XDocument"/>.
        /// </summary>
        /// <param name="path">The full path to <typeparamref name="TModel"/>.</param>
        /// <returns>A model as <see cref="XDocument"/>.</returns>
        XDocument Load(string path);

        /// <summary>
        /// Deserializes to a <typeparamref name="TModel"/>.
        /// </summary>
        /// <param name="xml">the model represented as a <see cref="XDocument"/>.</param>
        /// <returns>A model as <typeparamref name="TModel"/>.</returns>
        TModel Deserialize(XDocument xml);

        /// <summary>
        /// Deserializes to a <typeparamref name="TModel"/>.
        /// </summary>
        /// <param name="path">The full path to <typeparamref name="TModel"/>.</param>
        /// <returns>A model as <typeparamref name="TModel"/>.</returns>
        TModel Deserialize(string path);
    }
}
