using RunpathBEDTest.Models;
using System.Collections.Generic;

namespace RunpathBEDTest.Services
{
    public interface IPhotoAlbumService
    {
        List<PhotoAlbum> GetAllPhotoAlbums();
        List<PhotoAlbum> GetPhotoAlbumsByUserId(int userId);
    }
}