using Items.API.Services.ColorsService;
using Items.Data.Model;
using Items.Data.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Items.API.Test.ColorsTests
{
    [TestFixture]
    public class ColorsServiceTests
    {
        private Mock<IRepository> _repositoryMock;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<IRepository>();
        }

        [Test]
        public async Task EditColor_ValidInput_ReturnsNewColorWithOldColorId()
        {
            //Arrange
            var oldColor = new Color("Red");
            var versionId = oldColor.VersionId;
            Func<Color, bool> condition = x => x.VersionId == versionId;
            _repositoryMock.Setup(x => x.GetColors(It.IsAny<Func<Color, bool>>())).ReturnsAsync(new List<Color>() { oldColor });

            var newColor = new Color(oldColor.ColorId, "Green");
            _repositoryMock.Setup(x => x.EditColor(It.IsAny<Color>(), It.IsAny<Color>())).ReturnsAsync(newColor);

            var colorsService = new ColorsService(_repositoryMock.Object);

            //Act
            var response = await colorsService.EditColor(oldColor.VersionId, "Green");

            //Assert
            Assert.AreEqual(0, response.Errors.Count);
            Assert.AreEqual(oldColor.ColorId, response.Value.ColorId);
            Assert.AreEqual("Green", response.Value.Name);
        }

        [Test]
        public async Task EditColor_NonExistingVersionId_ReturnsResponseWithError()
        {
            //Arrange
            var oldColor = new Color("Red");
            _repositoryMock.Setup(x => x.GetColors(It.IsAny<Func<Color, bool>>())).ReturnsAsync(new List<Color>());
            var colorsService = new ColorsService(_repositoryMock.Object);

            //Act
            var response = await colorsService.GetColors(x => x.VersionId == Guid.NewGuid());

            //Assert
            Assert.AreEqual(1, response.Errors.Count);
            Assert.AreEqual("There are no colors.", response.Errors[0]);
        }
    }
}
