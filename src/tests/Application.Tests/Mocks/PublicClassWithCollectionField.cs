using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Application.Tests.Mocks
{
    /// <summary>
    /// Class used for mocking.
    /// </summary>
    public class PublicClassWithCollectionField
    {
        /// <summary>
        /// A collection field.
        /// </summary>
        public ICollection<string> CollectionField { get; set; }
    }
}
