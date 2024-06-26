﻿using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Application.Boundaries;
using LiquidVisions.PanthaRhei.Application.RequestModels;
using LiquidVisions.PanthaRhei.Domain.Usecases.CreateNewExpander;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Application.Tests.Boundaries
{
    /// <summary>
    /// Unit tests for <see cref="Boundary"/>.
    /// </summary>
    public class BoundaryTests
    {
        private readonly ApplicationFakes fakes = new();
        private readonly Boundary boundary;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundaryTests"/> class.
        /// </summary>
        public BoundaryTests()
        {
            boundary = new(fakes.ICreateNewExpander.Object, fakes.IUpdatePackages.Object);   
        }

        /// <summary>
        /// Unit test for <see cref="Boundary.CreateNewExpander(Application.RequestModels.NewExpanderRequestModel)"/>.
        /// </summary>
        [Fact]
        public async Task CreateNewExpanderShouldExecuteUseCase()
        {
            // arrange
            NewExpanderRequestModel model = new()
            {
                ShortName = "test",
                FullName = "test.test",
                Path = "C:\\test",
                BuildPath = "C:\\build.test"
            };

            // act
            await boundary.CreateNewExpander(model);

            // assert
            fakes.ICreateNewExpander.Verify(x => x.Execute(It.Is<CreateNewExpanderRequestModel>(m => m.Build == model.Build && m.BuildPath == model.BuildPath && m.Path == model.Path && m.FullName == model.FullName && m.ShortName == model.ShortName)), Times.Once);
        }

        /// <summary>
        /// Unit test for <see cref="Boundary.UpdatePackages(string)"/>.
        /// </summary>
        [Fact]
        public void UpdateCorePackagesShouldCallUseCase()
        {
            // arrange
            string path = "C:\\test";

            // act
            boundary.UpdatePackages(path);

            // assert
            fakes.IUpdatePackages.Verify(x => x.Execute(path), Times.Once);
        }
    }
}
