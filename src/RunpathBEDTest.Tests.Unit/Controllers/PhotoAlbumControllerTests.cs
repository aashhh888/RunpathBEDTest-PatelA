using Moq;
using NUnit.Framework;
using RunpathBEDTest.Api.Controllers;
using RunpathBEDTest.Models;
using RunpathBEDTest.Services;
using System.Collections.Generic;

namespace RunpathBEDTest.Tests.Unit.Controllers
{
    [TestFixture]
    public class PhotoAlbumControllerTests
    {
        [Test]
        public void Should_Call_PhotoAlbumService_To_Get_All_Photo_Albums()
        {
            //give
            Mock<IPhotoAlbumService> photoAlbumServiceMock = new Mock<IPhotoAlbumService>();
            PhotoAlbumController controller = new PhotoAlbumController(photoAlbumServiceMock.Object);

            var photoAlbumList = new List<PhotoAlbum>();
            photoAlbumServiceMock.Setup(x => x.GetAllPhotoAlbums()).Returns(photoAlbumList);

            //when
            var result = controller.GetAllPhotoAlbums();

            //then
            Assert.AreEqual(photoAlbumList, (List<PhotoAlbum>)result.Value);
            photoAlbumServiceMock.Verify(x => x.GetAllPhotoAlbums(), Times.Once);            
        }

        [Test]
        public void Should_Call_PhotoAlbumService_To_Get_All_Photo_Albums_For_UserId()
        {
            //give
            Mock<IPhotoAlbumService> photoAlbumServiceMock = new Mock<IPhotoAlbumService>();
            PhotoAlbumController controller = new PhotoAlbumController(photoAlbumServiceMock.Object);

            var photoAlbumList = new List<PhotoAlbum>();
            photoAlbumServiceMock.Setup(x => x.GetPhotoAlbumsByUserId(It.IsAny<int>())).Returns(photoAlbumList);

            //when
            var result = controller.GetAllPhotoAlbumsForUserId(1);

            //then
            Assert.AreEqual(photoAlbumList, (List<PhotoAlbum>)result.Value);
            photoAlbumServiceMock.Verify(x => x.GetPhotoAlbumsByUserId(1), Times.Once);            
        }
    }
}
