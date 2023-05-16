using System;
using System.Collections.Generic;
#region ns-custom-namespaces
#endregion ns-custom-namespaces

namespace LiquidVisions.PanthaRhei.Generated.Domain.Entities
{
	public class Expander
	{
		public virtual Guid Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string TemplateFolder { get; set; }
		public virtual int Order { get; set; }
		public virtual List<App> Apps { get; set; }
		public virtual List<Component> Components { get; set; }
	}
}