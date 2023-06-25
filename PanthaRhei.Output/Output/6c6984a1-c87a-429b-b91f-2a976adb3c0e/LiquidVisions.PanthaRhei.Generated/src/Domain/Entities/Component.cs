using System;
using System.Collections.Generic;
#region ns-custom-namespaces
#endregion ns-custom-namespaces

namespace LiquidVisions.PanthaRhei.Generated.Domain.Entities
{
	public class Component
	{
		public virtual Guid Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string Description { get; set; }
		public virtual List<Package> Packages { get; set; }
		public virtual Expander Expander { get; set; }

		#region ns-custom-fields
		#endregion ns-custom-fields
	}
}