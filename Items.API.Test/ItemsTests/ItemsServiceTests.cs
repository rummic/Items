using Items.API.Dtos.ItemsDtos;
using Items.API.Services.ItemsServices;
using Items.Data.Model;
using Items.Data.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Items.API.Test.ItemsTests
{
    [TestFixture]
    public class ItemsServiceTests
    {
        private Mock<IRepository> _repositoryMock;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<IRepository>();
        }

        [Test]
        public async Task EditItem_NonExistingItemId_ReturnsResponseWithError()
        {
            //Arrange
            _repositoryMock.Setup(x => x.GetItem("wrongId")).ReturnsAsync(null as Item);
            var itemsService = new ItemsService(_repositoryMock.Object);
            var editItemDto = new EditItemDto()
            {
                ColorVersionId = Guid.NewGuid(),
                Id = "wrongId",
                Name = "testName",
                Note = "testNote"
            };
            //Act
            var response = await itemsService.EditItem(editItemDto);

            //Assert
            Assert.AreEqual(1, response.Errors.Count);
            Assert.AreEqual($"No {editItemDto.Id} item exists.", response.Errors[0]);
        }

        [Test]
        public async Task EditItem_ValidData_ReturnsResponseWithEditedItem()
        {
            //Arrange
            var color = new Color("TestColor");
            var existingItem = new Item("testName", "testNote", color.VersionId);
            _repositoryMock.Setup(x => x.GetItem(existingItem.Id)).ReturnsAsync(existingItem);

            var itemsService = new ItemsService(_repositoryMock.Object);
            var editItemDto = new EditItemDto()
            {
                ColorVersionId = Guid.NewGuid(),
                Id = existingItem.Id,
                Name = "testNameEdited",
                Note = "testNoteEdited"
            };

            var editedItem = existingItem;
            editedItem.Name = editItemDto.Name;
            editedItem.Note = editItemDto.Note;
            editedItem.ColorVersionId = editItemDto.ColorVersionId;

            _repositoryMock.Setup(x => x.EditItem(existingItem)).ReturnsAsync(editedItem);

            //Act
            var response = await itemsService.EditItem(editItemDto);

            //Assert
            Assert.AreEqual(0, response.Errors.Count);
            Assert.AreEqual(editedItem.Name, response.Value.Name);
            Assert.AreEqual(editedItem.Note, response.Value.Note);
            Assert.AreEqual(editedItem.ColorVersionId, response.Value.ColorVersionId);
        }
    }
}
