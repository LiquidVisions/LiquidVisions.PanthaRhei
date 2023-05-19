using System;
using System.Collections.Generic;
#region ns-custom-namespaces
#endregion ns-custom-namespaces

namespace LiquidVisions.PanthaRhei.Generated.Domain.Entities
{
	public class Field
	{
		public virtual Guid Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string ReturnType { get; set; }
		public virtual bool IsCollection { get; set; }
		public virtual string Modifier { get; set; }
		public virtual string GetModifier { get; set; }
		public virtual string SetModifier { get; set; }
		public virtual string Behaviour { get; set; }
		public virtual int Order { get; set; }
		public virtual int? Size { get; set; }
		public virtual bool Required { get; set; }
		public virtual Entity Reference { get; set; }
		public virtual Entity Entity { get; set; }
		public bool IsKey { get; set; }
		public bool IsIndex { get; set; }
		public virtual List<Relationship> RelationshipKeys { get; set; }
		public virtual List<Relationship> IsForeignEntityKeyOf { get; set; }

		#region ns-custom-fields
		#endregion ns-custom-fields
	}
}