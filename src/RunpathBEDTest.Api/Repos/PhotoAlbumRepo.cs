using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RunpathBEDTest.Configs;
using RunpathBEDTest.Models;
using RunpathBEDTest.Wrappers;
using System;
using System.Collections.Generic;

namespace RunpathBEDTest.Repos
{
    public class PhotoAlbumRepo : IPhotoAlbumRepo
    {
        private readonly IOptions<PhotoAlbumConfig> _config;
        private readonly IHttpClientWrapper _httpClientWrapper;

        public PhotoAlbumRepo(IOptions<PhotoAlbumConfig> config, IHttpClientWrapper httpClientWrapper)
        {
            _config = config;
            _httpClientWrapper = httpClientWrapper;
        }

        public List<Photo> GetPhotos()
        {
            try
            {
                var result = _httpClientWrapper.Get($"{_config.Value.PhotoAlbumBaseAddress}/photos");
                return JsonConvert.DeserializeObject<List<Photo>>(result);
            }
            catch (Exception)
            {
                return new List<Photo>();
            }
        }

        public List<Album> GetAlbums()
        {
            try
            {
                var result = _httpClientWrapper.Get($"{_config.Value.PhotoAlbumBaseAddress}/albums");
                return JsonConvert.DeserializeObject<List<Album>>(result);
            }
            catch (Exception)
            {
                return new List<Album>();
            }
        }
    }
}
