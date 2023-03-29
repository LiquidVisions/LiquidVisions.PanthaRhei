using System;
using System.Collections.Generic;
#region ns-custom-namespaces
#endregion ns-custom-namespaces

namespace LiquidVisions.PanthaRhei.Generated.Domain.Entities
{
	public class Package
	{
		public virtual Guid Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string Version { get; set; }
		public virtual Component Component { get; set; }
	}
}