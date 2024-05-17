// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Application.Usecases.Templates;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Application.Tests.Usecases
{
    /// <summary>
    /// Tests for <see cref="CustomScripts"/>.
    /// </summary>
    public class CustomScriptsTests
    {
        /// <summary>
        /// Tests for <see cref="CustomScripts.GetPostfix(string)"/>.
        /// </summary>
        [Fact]
        public void TestGetPostfix()
        {
            // Arrange
            string input = "Hello World";
            string expectedOutput = "World";

            Mock<IElementTemplateParameters> mockedElements = new();
            mockedElements.Setup(x => x.ElementType).Returns(input);
            mockedElements.Setup(x => x.NamePostfix).Returns(expectedOutput);
            _ = new CustomScripts([mockedElements.Object]);

            // Act
            string actualOutput = CustomScripts.GetPostfix(input);

            // Assert
            Assert.Equal(expectedOutput, actualOutput);
        }

        /// <summary>
        /// Tests for <see cref="CustomScripts.Pluralize(string)"/>.
        /// </summary>
        [Fact]
        public void TestPluralize()
        {
            // Arrange
            string input = "apple";
            string expectedOutput = "apples";

            // Act
            string actualOutput = CustomScripts.Pluralize(input);

            // Assert
            Assert.Equal(expectedOutput, actualOutput);
        }

        /// <summary>
        /// Tests for <see cref="CustomScripts.ComponentFullName(Component, string[])"/>.
        /// </summary>
        [Fact]
        public void TestComponentFullName()
        {
            // Arrange
            var componentMock = new Mock<Component>();
            var expanderMock = new Mock<Expander>();
            var appMock = new Mock<App>();

            componentMock.Setup(c => c.Expander).Returns(expanderMock.Object);
            componentMock.Setup(c => c.Name).Returns("ComponentName");
            expanderMock.Setup(e => e.Apps).Returns([appMock.Object]);
            appMock.Setup(a => a.FullName).Returns("MockedFullName");

            Component component = componentMock.Object;

            string[] segments = ["Part1", "Part2"];

            // Act
            string actualOutput = CustomScripts.ComponentFullName(component, segments);

            // Assert
            Assert.Equal($"MockedFullName.ComponentName.{segments[0]}.{segments[1]}", actualOutput);
        }

        /// <summary>
        /// Tests for <see cref="CustomScripts.ComponentFullName(Component, string[])"/>.
        /// </summary>
        [Fact]
        public void TestArgumentNullExceptionOnCustomScriptsComponentFullName()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => CustomScripts.ComponentFullName(null, []));
        }

        /// <summary>
        /// Tests for <see cref="CustomScripts.AppFullName(Component, string[])"/>.
        /// </summary>
        [Fact]
        public void TestAppFullName()
        {
            // Arrange
            var componentMock = new Mock<Component>();
            var expanderMock = new Mock<Expander>();
            var appMock = new Mock<App>();

            componentMock.Setup(c => c.Expander).Returns(expanderMock.Object);
            expanderMock.Setup(e => e.Apps).Returns([appMock.Object]);
            appMock.Setup(a => a.FullName).Returns("MockedFullName");

            Component component = componentMock.Object;

            string[] segments = ["Part1", "Part2"];

            // Act
            string actualOutput = CustomScripts.AppFullName(component, segments);

            // Assert
            Assert.Equal($"MockedFullName.{segments[0]}.{segments[1]}", actualOutput);
        }


    }
}
