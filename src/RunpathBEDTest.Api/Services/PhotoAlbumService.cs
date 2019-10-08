using RunpathBEDTest.Models;
using RunpathBEDTest.Repos;
using System.Collections.Generic;
using System.Linq;

namespace RunpathBEDTest.Services
{
    public class PhotoAlbumService : IPhotoAlbumService
    {
        private readonly IPhotoAlbumRepo _photoAlbumRepo;

        public PhotoAlbumService(IPhotoAlbumRepo photoAlbumRepo)
        {
            _photoAlbumRepo = photoAlbumRepo;
        }

        public List<PhotoAlbum> GetAllPhotoAlbums()
        {
            return BuildPhotoAlbums();
        }

        public List<PhotoAlbum> GetPhotoAlbumsByUserId(int userId)
        {
            return BuildPhotoAlbums(userId);
        }

        private List<PhotoAlbum> BuildPhotoAlbums(int? userId = null)
        {
            var albums = _photoAlbumRepo.GetAlbums();
            var photos = _photoAlbumRepo.GetPhotos();
            return albums.Where(x => (!userId.HasValue) | x.UserId == userId).Select(x => new PhotoAlbum { Album = x, Photos = photos.Where(y => y.AlbumId == x.Id).ToList() }).ToList();
        }
    }
}
