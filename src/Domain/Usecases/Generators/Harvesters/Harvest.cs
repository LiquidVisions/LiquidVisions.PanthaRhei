﻿using System.Collections.ObjectModel;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Harvesters
{
    /// <summary>
    /// Represents a model that contains the information needed tot persist the Harvested information.
    /// </summary>
    public class Harvest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Harvest"/> class.
        /// </summary>
        public Harvest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Harvest"/> class.
        /// </summary>
        /// <param name="type"></param>
        public Harvest(string type)
        {
            HarvestType = type;
        }

        /// <summary>
        /// Gets or sets the path of the file that is the source of the Harvest.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets the harvest of harvest.
        /// </summary>
        public string HarvestType { get; }

        /// <summary>
        /// Gets or sets the list of <seealso cref="HarvestItem">HarvestItems</seealso>.
        /// </summary>
        public Collection<HarvestItem> Items { get; set; } = [];
    }
}
