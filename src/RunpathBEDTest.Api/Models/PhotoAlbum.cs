using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunpathBEDTest.Models
{
    public class PhotoAlbum
    {
        public List<Photo> Photos { get; set; }
        public Album Album { get; set; }
    }
}
