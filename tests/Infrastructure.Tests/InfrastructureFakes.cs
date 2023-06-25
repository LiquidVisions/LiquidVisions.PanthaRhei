﻿using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Infrastructure.Serialization;
using LiquidVisions.PanthaRhei.Tests;
using Moq;

namespace LiquidVisions.PanthaRhei.Infrastructure.Tests
{
    internal class InfrastructureFakes : Fakes
    {
        public Mock<IDeserializerInteractor<Harvest>> IHarvestDeserializerInteractor { get; } = new();

        public Mock<ISerializerInteractor<Harvest>> ISerializerInteractor { get; } = new();

        public Mock<IHarvestSerializer> IHarvestSerializerInteractor { get; } = new();

        /// <summary>
        /// Gets a mock of an empy class file as a <see cref="string"/>.
        /// </summary>
        /// <returns><seealso cref="string"/></returns>
        public static string[] GetEmptyClass()
        {
            return new string[]
            {
                "using System;",
                "using System.Collections.Generic;",
                "using System.Linq;",
                "using System.Text;",
                "using System.Threading.Tasks;",
                string.Empty,
                "namespace LiquidVisions.Jafar.Tests.Domain",
                "{",
                "   public class Class1",
                "   {",
                "   }",
                "}",
            };
        }

        /// <summary>
        /// Gets a mock of an empy class file as a <see cref="string"/>.
        /// </summary>
        /// <returns><seealso cref="string"/></returns>
        public static string[] GetEmptyClassWithEmptyMethod()
        {
            return new string[]
            {
                "using System;",
                "using System.Collections.Generic;",
                "using System.Linq;",
                "using System.Text;",
                "using System.Threading.Tasks;",
                string.Empty,
                "namespace LiquidVisions.Jafar.Tests.Domain",
                "{",
                "   public class Class1",
                "   {",
                string.Empty,
                "       public void Test()",
                "       {",
                "           // empty",
                "       }",
                "   }",
                "}",
            };
        }

        public override void ConfigureIDependencyFactoryInteractor()
        {
            base.ConfigureIDependencyFactoryInteractor();

            IDependencyFactoryInteractor.Setup(x => x.Get<IHarvestSerializer>()).Returns(IHarvestSerializerInteractor.Object);
            IDependencyFactoryInteractor.Setup(x => x.Get<IDeserializerInteractor<Harvest>>()).Returns(IHarvestDeserializerInteractor.Object);
            IDependencyFactoryInteractor.Setup(x => x.Get<ISerializerInteractor<Harvest>>()).Returns(ISerializerInteractor.Object);
        }
    }
}
