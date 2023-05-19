using System;
using System.Collections.Generic;
#region ns-custom-namespaces
#endregion ns-custom-namespaces

namespace LiquidVisions.PanthaRhei.Generated.Domain.Entities
{
	public class Relationship
	{
		public virtual Guid Id { get; set; }
		public virtual Field Key { get; set; }
		public virtual Entity Entity { get; set; }
		public virtual string Cardinality { get; set; }
		public virtual Field WithForeignEntityKey { get; set; }
		public virtual Entity WithForeignEntity { get; set; }
		public virtual string WithCardinality { get; set; }
		public virtual bool Required { get; set; }

		#region ns-custom-fields
		#endregion ns-custom-fields
	}
}