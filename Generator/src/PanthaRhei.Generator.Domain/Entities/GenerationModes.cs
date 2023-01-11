using System;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Entities
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
        /// Mograte generation mode.
        /// </summary>
        Migrate = 8,
    }
}
