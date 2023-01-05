using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Expanders.MetaCircularSqlScript
{
    public class MetaCircularSqlScriptExpander : AbstractExpander<MetaCircularSqlScriptExpander>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetaCircularSqlScriptExpander"/> class.
        /// </summary>

        public MetaCircularSqlScriptExpander()
        {
        }

        public MetaCircularSqlScriptExpander(IDependencyResolver dependencyResolver)
            : base(dependencyResolver)
        {
        }

        protected override int GetOrder() => 3;
    }
}
