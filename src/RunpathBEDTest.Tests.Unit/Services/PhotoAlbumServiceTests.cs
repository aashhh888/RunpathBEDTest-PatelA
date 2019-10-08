using Moq;
using NUnit.Framework;
using RunpathBEDTest.Models;
using RunpathBEDTest.Repos;
using RunpathBEDTest.Services;
using System.Collections.Generic;

namespace RunpathBEDTest.Tests.Unit.Services
{
    [TestFixture]
    public class PhotoAlbumServiceTests
    {
        private Mock<IPhotoAlbumRepo> _photoAlbumRepoMock;
        private IPhotoAlbumService _service;

        [SetUp]
        public void SetUp()
        {
            _photoAlbumRepoMock = new Mock<IPhotoAlbumRepo>();
            _service = new PhotoAlbumService(_photoAlbumRepoMock.Object);
        }

        [Test]
        public void Should_Get_Photos_And_Albums_From_Repo_And_Rerturn_List_Of_PhotoAlbums()
        {
            //given
            var photo = new Photo { Id = 1, AlbumId = 1, Title = "phototitle" };
            _photoAlbumRepoMock.Setup(x => x.GetPhotos()).Returns(new List<Photo> { photo });
            var album = new Album { Id = 1, Title = "albumtitle" };
            _photoAlbumRepoMock.Setup(x => x.GetAlbums()).Returns(new List<Album> { album });

            //when
            var result = _service.GetAllPhotoAlbums();

            //then
            Assert.NotNull(result);
            Assert.IsNotEmpty(result);
            Assert.AreEqual(1, result.Count);
            Assert.NotNull(result[0].Album);
            Assert.AreEqual(album, result[0].Album);
            Assert.NotNull(result[0].Photos);
            Assert.IsNotEmpty(result[0].Photos);
            Assert.AreEqual(1, result[0].Photos.Count);
            Assert.AreEqual(photo, result[0].Photos[0]);
            _photoAlbumRepoMock.Verify(x => x.GetPhotos(), Times.Once);
            _photoAlbumRepoMock.Verify(x => x.GetAlbums(), Times.Once);
        }

        [Test]
        public void Should_Get_Photos_And_Albums_From_Repo_And_Return_List_Of_PhotoAlbums_For_Specified_UserId()
        {
            //given
            var photo = new Photo { Id = 1, AlbumId = 1, Title = "phototitle" };
            var photo2 = new Photo { Id = 2, AlbumId = 1, Title = "phototitle2" };
            var photo3 = new Photo { Id = 3, AlbumId = 2, Title = "phototitle3" };
            _photoAlbumRepoMock.Setup(x => x.GetPhotos()).Returns(new List<Photo> { photo, photo2, photo3 });
            var album = new Album { Id = 1, Title = "albumtitle1", UserId = 1 };
            var album2 = new Album { Id = 2, Title = "albumtitle2", UserId = 2 };
            _photoAlbumRepoMock.Setup(x => x.GetAlbums()).Returns(new List<Album> { album, album2 });

            //when
            var result = _service.GetPhotoAlbumsByUserId(1);

            //then
            Assert.NotNull(result);
            Assert.IsNotEmpty(result);
            Assert.AreEqual(1, result.Count);
            Assert.NotNull(result[0].Album);
            Assert.AreEqual(album, result[0].Album);
            Assert.NotNull(result[0].Photos);
            Assert.IsNotEmpty(result[0].Photos);
            Assert.AreEqual(2, result[0].Photos.Count);
            Assert.AreEqual(photo, result[0].Photos[0]);
            Assert.AreEqual(photo2, result[0].Photos[1]);
            _photoAlbumRepoMock.Verify(x => x.GetPhotos(), Times.Once);
            _photoAlbumRepoMock.Verify(x => x.GetAlbums(), Times.Once);
        }
    }
}
