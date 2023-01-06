﻿using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Expanders;

namespace LiquidVisions.PanthaRhei.Expanders.MetaCircular
{
    public class MetaCircularExpander : AbstractExpander<MetaCircularExpander>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetaCircularExpander"/> class.
        /// </summary>
        public MetaCircularExpander()
        {
        }

        public MetaCircularExpander(IDependencyResolver dependencyResolver)
            : base(dependencyResolver)
        {
        }

        protected override int GetOrder() => 2;
    }
}