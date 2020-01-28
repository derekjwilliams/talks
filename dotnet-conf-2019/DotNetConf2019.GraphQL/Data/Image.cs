using System;
using System.ComponentModel.DataAnnotations;
namespace DotNetConf2019.GraphQL.Data
{
    public class Image
    {
        [Key]
        public Guid PublicKey { get; set; }

        public ImageSize Size { get; set; } = ImageSize.Small;

        public string Url { get; set; } = null!;

        public Guid PostId { get; set; } = Guid.NewGuid();
    }
}
