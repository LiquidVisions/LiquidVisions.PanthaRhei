using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;
using System;

namespace LiquidVisions.PanthaRhei.Expanders.PROJECT_TITLE
{
    /// <summary>
    /// The <seealso cref="PROJECT_TITLEExpander"/> expanders. PROJECT_NAME
    /// </summary>
    public class PROJECT_TITLEExpander : AbstractExpander<PROJECT_TITLEExpander>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PROJECT_TITLEExpander"/> class.
        /// </summary>
        public PROJECT_TITLEExpander()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PROJECT_TITLEExpander"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public PROJECT_TITLEExpander(IDependencyFactory dependencyFactory)
            : base(dependencyFactory)
        {
        }

        public override void Clean()
        {
            throw new NotImplementedException();
        }

        protected override int GetOrder()
        {
            throw new NotImplementedException();
        }
    }
}