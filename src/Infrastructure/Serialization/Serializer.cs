using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace LiquidVisions.PanthaRhei.Infrastructure.Serialization
{
    /// <summary>
    /// Handles serialization and deserialization for <typeparamref name="TModel"/>.
    /// </summary>
    /// <typeparam name="TModel">The model.</typeparam>
    [ExcludeFromCodeCoverage]
    internal sealed class Serializer<TModel> : IDeserializer<TModel>, ISerializer<TModel>
        where TModel : class
    {
        private readonly XmlSerializer _serializer = new(typeof(TModel));

        /// <inheritdoc/>
        public TModel Deserialize(XDocument xml)
        {
            TModel result = (TModel)_serializer.Deserialize(xml.Root.CreateReader());

            return result;
        }

        /// <inheritdoc/>
        public TModel Deserialize(string path)
        {
            TModel model = Deserialize(Load(path));

            return model;
        }

        /// <inheritdoc/>
        public XDocument Load(string path)
        {
            XDocument xml = XDocument.Load(path);

            return xml;
        }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public void Serialize(string path, TModel model)
        {
            using TextWriter writer = new StreamWriter(path);
            _serializer.Serialize(writer, model);
        }

        /// <inheritdoc/>
        public string SerializeToString(TModel model)
        {
            using StringWriter textWriter = new();
            _serializer.Serialize(textWriter, model);
            return textWriter.ToString();
        }
    }
}
