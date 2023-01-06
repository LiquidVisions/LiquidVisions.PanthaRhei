using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases
{
    /// <summary>
    /// Represents a model that contains the information needed tot persist the Harvested infomration.
    /// </summary>
    public class Harvest
    {
        /// <summary>
        /// Gets or sets the path of the file that is the source of the Harvest.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the list of <seealso cref="HarvestItem">HarvestItems</seealso>.
        /// </summary>
        public List<HarvestItem> Items { get; set; }
            = new List<HarvestItem>();
    }
}
