using Microsoft.AspNetCore.Mvc;
using RunpathBEDTest.Services;

namespace RunpathBEDTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoAlbumController : ControllerBase
    {
        private readonly IPhotoAlbumService _photoAlbumService;

        public PhotoAlbumController(IPhotoAlbumService photoAlbumService)
        {
            _photoAlbumService = photoAlbumService;
        }
        
        public JsonResult GetAllPhotoAlbums()
        {
            return new JsonResult(_photoAlbumService.GetAllPhotoAlbums());
        }
        
        [Route("{userId}")]
        public JsonResult GetAllPhotoAlbumsForUserId(int userId)
        {
            return new JsonResult(_photoAlbumService.GetPhotoAlbumsByUserId(userId));
        }
    }
}