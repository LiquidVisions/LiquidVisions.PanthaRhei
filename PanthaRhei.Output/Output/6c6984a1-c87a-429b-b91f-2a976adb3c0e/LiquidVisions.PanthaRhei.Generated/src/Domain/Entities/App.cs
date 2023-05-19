using System;
using System.Collections.Generic;
#region ns-custom-namespaces
// Dit is een test
#endregion ns-custom-namespaces

namespace LiquidVisions.PanthaRhei.Generated.Domain.Entities
{
	public class App
	{
		public virtual Guid Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string FullName { get; set; }
		public virtual List<Expander> Expanders { get; set; }
		public virtual List<Entity> Entities { get; set; }
		public virtual List<ConnectionString> ConnectionStrings { get; set; }

		#region ns-custom-fields
        public string DitIsEenNaam { get; set; }
		#endregion ns-custom-fields
	}
}
