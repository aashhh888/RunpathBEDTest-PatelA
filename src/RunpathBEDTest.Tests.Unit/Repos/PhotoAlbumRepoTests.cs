using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using RunpathBEDTest.Configs;
using RunpathBEDTest.Repos;
using RunpathBEDTest.Wrappers;
using System;

namespace RunpathBEDTest.Tests.Unit.Repos
{
    [TestFixture]
    public class PhotoAlbumRepoTests
    {
        private Mock<IHttpClientWrapper> _httpClientWrapperMock;
        private Mock<IOptions<PhotoAlbumConfig>> _photoAlbumConfigMock;
        private IPhotoAlbumRepo _repo;

        [SetUp]
        public void SetUp()
        {
            _httpClientWrapperMock = new Mock<IHttpClientWrapper>();
            _photoAlbumConfigMock = new Mock<IOptions<PhotoAlbumConfig>>();
            _repo = new PhotoAlbumRepo(_photoAlbumConfigMock.Object, _httpClientWrapperMock.Object);

            var photoAlbumConfig = new PhotoAlbumConfig { PhotoAlbumBaseAddress = "baseAddress" };
            _photoAlbumConfigMock.SetupGet(x => x.Value).Returns(photoAlbumConfig);
        }

        [Test]
        public void Should_Call_Photos_Endpoint_And_Return_Empty_Collection_On_Empty_Result()
        {
            //given
            _httpClientWrapperMock.Setup(x => x.Get(It.IsAny<string>())).Returns("[]");

            //when
            var result = _repo.GetPhotos();

            //then
            Assert.NotNull(result);
            Assert.IsEmpty(result);
            _httpClientWrapperMock.Verify(x => x.Get($"{_photoAlbumConfigMock.Object.Value.PhotoAlbumBaseAddress}/photos"), Times.Once);
        }

        [Test]
        public void Should_Call_Photos_Endpoint_And_Map_Result_To_Collection()
        {
            //given
            _httpClientWrapperMock.Setup(x => x.Get(It.IsAny<string>())).Returns("[{\"albumId\": 1, \"id\": 2, \"title\": \"title\", \"url\": \"url\", \"thumbnailUrl\": \"thumbnailUrl\" }]");

            //when
            var result = _repo.GetPhotos();

            //then
            Assert.NotNull(result);
            Assert.IsNotEmpty(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].AlbumId);
            Assert.AreEqual(2, result[0].Id);
            Assert.AreEqual("thumbnailUrl", result[0].ThumbnailUrl);
            Assert.AreEqual("title", result[0].Title);
            Assert.AreEqual("url", result[0].Url);
            _httpClientWrapperMock.Verify(x => x.Get($"{_photoAlbumConfigMock.Object.Value.PhotoAlbumBaseAddress}/photos"), Times.Once);
        }

        [Test]
        public void Should_Call_Photos_Endpoint_And_Return_Empty_Result_On_Exception()
        {
            //given
            _httpClientWrapperMock.Setup(x => x.Get(It.IsAny<string>())).Throws(new Exception());

            //when
            var result = _repo.GetPhotos();

            //then
            Assert.NotNull(result);
            Assert.IsEmpty(result);
            _httpClientWrapperMock.Verify(x => x.Get($"{_photoAlbumConfigMock.Object.Value.PhotoAlbumBaseAddress}/photos"), Times.Once);
        }



        [Test]
        public void Should_Call_Albums_Endpoint_And_Return_Empty_Collection_On_Empty_Result()
        {
            //given
            _httpClientWrapperMock.Setup(x => x.Get(It.IsAny<string>())).Returns("[]");

            //when
            var result = _repo.GetAlbums();

            //then
            Assert.NotNull(result);
            Assert.IsEmpty(result);
            _httpClientWrapperMock.Verify(x => x.Get($"{_photoAlbumConfigMock.Object.Value.PhotoAlbumBaseAddress}/albums"), Times.Once);
        }

        [Test]
        public void Should_Call_Albums_Endpoint_And_Map_Result_To_Collection()
        {
            //given
            _httpClientWrapperMock.Setup(x => x.Get(It.IsAny<string>())).Returns("[{\"userId\": 1, \"id\": 2, \"title\": \"title\" }]");

            //when
            var result = _repo.GetAlbums();

            //then
            Assert.NotNull(result);
            Assert.IsNotEmpty(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].UserId);
            Assert.AreEqual(2, result[0].Id);
            Assert.AreEqual("title", result[0].Title);
            _httpClientWrapperMock.Verify(x => x.Get($"{_photoAlbumConfigMock.Object.Value.PhotoAlbumBaseAddress}/albums"), Times.Once);
        }

        [Test]
        public void Should_Call_Albums_Endpoint_And_Return_Empty_Result_On_Exception()
        {
            //given
            _httpClientWrapperMock.Setup(x => x.Get(It.IsAny<string>())).Throws(new Exception());

            //when
            var result = _repo.GetAlbums();

            //then
            Assert.NotNull(result);
            Assert.IsEmpty(result);
            _httpClientWrapperMock.Verify(x => x.Get($"{_photoAlbumConfigMock.Object.Value.PhotoAlbumBaseAddress}/albums"), Times.Once);
        }
    }
}
