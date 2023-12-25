using Xunit;
using LiquidVisions.PanthaRhei.Domain.Models;

namespace LiquidVisions.PanthaRhei.Domain.Tests.Models
{
    /// <summary>
    /// This class contains unit tests for the <seealso cref="RelationshipDto"/> class.
    /// </summary>
    public class RelationshipDtoTests
    {
        /// <summary>
        /// Set and get properties of <see cref="RelationshipDto"/> should return correct values.
        /// </summary>
        [Fact]
        public void RelationshipDtoSetGetPropertiesShouldReturnCorrectValues()
        {
            // Arrange
            var relationshipDto = new RelationshipDto
            {
                Key = "TestKey",
                Entity = "TestEntity",
                Cardinality = "TestCardinality",
                WithForeignEntityKey = "TestForeignEntityKey",
                WithForeignEntity = "TestForeignEntity",
                WithyCardinality = "TestWithyCardinality",
                Required = true
            };

            // Act & Assert
            Assert.Equal("TestKey", relationshipDto.Key);
            Assert.Equal("TestEntity", relationshipDto.Entity);
            Assert.Equal("TestCardinality", relationshipDto.Cardinality);
            Assert.Equal("TestForeignEntityKey", relationshipDto.WithForeignEntityKey);
            Assert.Equal("TestForeignEntity", relationshipDto.WithForeignEntity);
            Assert.Equal("TestWithyCardinality", relationshipDto.WithyCardinality);
            Assert.True(relationshipDto.Required);
        }
    }
}
