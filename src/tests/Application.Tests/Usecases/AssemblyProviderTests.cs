using Xunit;
using System.Reflection;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Tests
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
            var assemblyProvider = new AssemblyProvider();

            // Act
            var result = assemblyProvider.EntryAssembly;

            // Assert
            Assert.Equal(Assembly.GetEntryAssembly(), result);
        }
    }
}
