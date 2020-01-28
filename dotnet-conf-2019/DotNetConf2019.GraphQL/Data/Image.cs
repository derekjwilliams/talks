using System;
using System.ComponentModel.DataAnnotations;
namespace FoundObjx.Blog.Data
{
    public class Image
    {
        [Key]
        public Guid PublicKey { get; set; } = Guid.NewGuid();

        public ImageSize Size { get; set; } = ImageSize.Small;

        public string Url { get; set; } = null!;

        public Guid PostId { get; set; } = Guid.NewGuid();
    }
}
