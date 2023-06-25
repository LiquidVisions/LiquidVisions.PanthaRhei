using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Infrastructure.Serialization;
using LiquidVisions.PanthaRhei.Tests;
using Moq;

namespace LiquidVisions.PanthaRhei.Infrastructure.Tests
{
    internal class InfrastructureFakes : Fakes
    {
        public Mock<IDeserializer<Harvest>> IHarvestDeserializer { get; } = new();

        public Mock<ISerializer<Harvest>> ISerializer { get; } = new();

        public Mock<IHarvestSerializer> IHarvestSerializer { get; } = new();

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

        public override void ConfigureIDependencyFactory()
        {
            base.ConfigureIDependencyFactory();

            IDependencyFactory.Setup(x => x.Get<IHarvestSerializer>()).Returns(IHarvestSerializer.Object);
            IDependencyFactory.Setup(x => x.Get<IDeserializer<Harvest>>()).Returns(IHarvestDeserializer.Object);
            IDependencyFactory.Setup(x => x.Get<ISerializer<Harvest>>()).Returns(ISerializer.Object);
        }
    }
}
