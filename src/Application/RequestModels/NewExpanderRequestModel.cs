using System;

namespace LiquidVisions.PanthaRhei.Application.RequestModels
{
    /// <summary>
    /// A request model for creating a new expander.
    /// </summary>
    public class NewExpanderRequestModel
    {
        /// <summary>
        /// Gets or sets the name of the new expander.
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Gets or sets the type of the new expander.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the path where the expander will be created.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the path where the expander will output its builds.
        /// </summary>
        public string BuildPath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the expander should be built.
        /// </summary>
        public bool Build { get; set; }

        /// <summary>
        /// Gets or sets the application ID of the expander.
        /// </summary>
        public Guid AppId { get; set; }
    }
}
