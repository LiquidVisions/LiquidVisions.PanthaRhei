// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using LiquidVisions.PanthaRhei.Domain.Entities;
using Xunit;

namespace LiquidVisions.PanthaRhei.Domain.Tests
{
    /// <summary>
    /// Tests for <see cref="GenerationOptions"/>.
    /// </summary>
    public class GenerationOptionsTests
    {

        /// <summary>
        /// The <see cref="GenerationOptions"/> instance.
        /// </summary>
        [Fact]
        public void DefaultOptionsShouldBeValid()
        {
            GenerationOptions options = new();
            Guid appIdValue = Guid.NewGuid();

            options.AppId = appIdValue;
            options.Clean = true;
            options.Migrate = true;
            options.Seed = true;
            options.ConnectionString = "connectionString";
            options.Modes = GenerationModes.Run;
            options.Root = "C:\\Root";
            options.ExpandersFolder = "E";
            options.HarvestFolder = "H";
            options.OutputFolder = "O";

            // act
            string result = options.ToString();

            // assert
            Assert.Equal(appIdValue, options.AppId);
            Assert.True(options.Clean);
            Assert.True(options.Migrate);
            Assert.True(options.Seed);
            Assert.Equal("connectionString", options.ConnectionString);
            Assert.Equal(GenerationModes.Run, options.Modes);
            Assert.Equal("C:\\Root", options.Root);
            Assert.Equal($"C:\\Root\\E", options.ExpandersFolder);
            Assert.Equal($"C:\\Root\\H", options.HarvestFolder);
            Assert.Equal($"C:\\Root\\O\\{appIdValue}", options.OutputFolder);
            Assert.Equal($"CommandParameters {{ \r\n " +
                $"\"AppId\": \"{appIdValue}\", \r\n " +
                $"\"Clean\": \"True\", \r\n " +
                $"\"Migrate\": \"True\", \r\n " +
                $"\"Seed\": \"True\", \r\n " +
                $"\"ConnectionString\": \"connectionString\", \r\n " +
                $"\"Modes\": \"Run\", \r\n " +
                $"\"Root\": \"C:\\Root\", \r\n " +
                $"\"ExpandersFolder\": \"C:\\Root\\E\", " +
                $"\r\n \"HarvestFolder\": \"C:\\Root\\H\", \r\n " +
                $"\"OutputFolder\": \"C:\\Root\\O\\{appIdValue}\", \r\n}}\r\n", result);
        }
    }
}
