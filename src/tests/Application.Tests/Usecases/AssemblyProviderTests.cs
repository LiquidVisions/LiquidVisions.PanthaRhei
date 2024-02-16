using Xunit;
using System.Reflection;
using LiquidVisions.PanthaRhei.Application.Usecases;

namespace LiquidVisions.PanthaRhei.Application.Tests.Usecases
{
    /// <summary>
    /// Tests for <see cref="AssemblyProvider"/>.
    /// </summary>
    public class AssemblyProviderTests
    {
        /// <summary>
        /// Tests for <see cref="AssemblyProvider.EntryAssembly"/>.
        /// </summary>
        [Fact]
        public void EntryAssemblyShouldReturnEntryAssembly()
        {
            // Arrange
            AssemblyProvider assemblyProvider = new();

            // Act
            Assembly result = assemblyProvider.EntryAssembly;

            // Assert
            Assert.Equal(Assembly.GetEntryAssembly(), result);
        }
    }
}
