using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Expanders.MetaCircularSqlScript
{
    public class MetaCircularSqlScriptExpander : AbstractExpander<MetaCircularSqlScriptExpander>
    {
        public MetaCircularSqlScriptExpander(IDependencyResolver dependencyResolver)
            : base(dependencyResolver)
        {
        }

        public override string Name => typeof(MetaCircularSqlScriptExpander)
            .Name.Replace("Expander", string.Empty);
    }
}
