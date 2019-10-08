using RunpathBEDTest.Models;
using System.Collections.Generic;

namespace RunpathBEDTest.Repos
{
    public interface IPhotoAlbumRepo
    {
        List<Photo> GetPhotos();
        List<Album> GetAlbums();
    }
}