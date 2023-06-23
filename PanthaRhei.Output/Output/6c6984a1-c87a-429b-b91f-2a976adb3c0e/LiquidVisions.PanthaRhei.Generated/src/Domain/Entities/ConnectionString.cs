using System;
using System.Collections.Generic;
#region ns-custom-namespaces
#endregion ns-custom-namespaces

namespace LiquidVisions.PanthaRhei.Generated.Domain.Entities
{
	public class ConnectionString
	{
		public virtual Guid Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string Definition { get; set; }
		public virtual App App { get; set; }

		#region ns-custom-fields
		#endregion ns-custom-fields
	}
}