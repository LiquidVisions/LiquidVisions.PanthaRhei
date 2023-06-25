using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Domain.Interactors.Generators.Harvesters
{
    /// <summary>
    /// Represents a model that contains the information needed tot persist the Harvested infomration.
    /// </summary>
    public class Harvest
    {
        private readonly string type;

        public Harvest()
        {
        }

        public Harvest(string type)
        {
            this.type = type;
        }

        /// <summary>
        /// Gets or sets the path of the file that is the source of the Harvest.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets the harvest of harvest.
        /// </summary>
        public string HarvestType => type;

        /// <summary>
        /// Gets or sets the list of <seealso cref="HarvestItem">HarvestItems</seealso>.
        /// </summary>
        public List<HarvestItem> Items { get; set; }
            = new List<HarvestItem>();
    }
}
