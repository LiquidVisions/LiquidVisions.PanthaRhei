using System;

namespace LiquidVisions.PanthaRhei.Domain.Entities
{
    /// <summary>
    /// Specifies the different options when generating.
    /// </summary>
    [Flags]
    public enum GenerationModes
    {
        /// <summary>
        /// Not set.
        /// </summary>
        None = 0,

        /// <summary>
        /// The default generation mode.
        /// </summary>
        Default = 1,

        /// <summary>
        /// Extend generation mode.
        /// </summary>
        Extend = 2,

        /// <summary>
        /// Deploy generation mode.
        /// </summary>
        Deploy = 4,

        /// <summary>
        /// Migrate generation mode.
        /// </summary>
        Migrate = 8,

        /// <summary>
        /// Harvest generation mode.
        /// </summary>
        Harvest = 16,

        /// <summary>
        /// Rejuvenate mode.
        /// </summary>
        Rejuvenate = 32,

        /// <summary>
        /// Runs the generated project
        /// </summary>
        Run = 64,
    }
}
