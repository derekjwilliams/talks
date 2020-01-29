using System;
using NodaTime;
using System.ComponentModel.DataAnnotations;

namespace FoundObjx.Blog.Data
{
    public class Author
    {
        [Key]
        public Guid PublicKey { get; set; } = Guid.NewGuid();

        public string Name { get; set; }  = null!;
    }
    public class Image
    {
        [Key]
        public Guid PublicKey { get; set; } = Guid.NewGuid();

        public ImageSize Size { get; set; } = ImageSize.Small;

        public string Url { get; set; } = "";

        public Guid PostId { get; set; } = Guid.NewGuid();
    }
    public enum ImageSize
    {
        Small,
        Medium,
        Large
    }

    public class Post
    {
        [Key]
        public Guid PublicKey { get; set; }

        public Guid AuthorId { get; set; } = Guid.NewGuid();

        public string Title { get; set; } = "";

        public string Markdown { get; set; } = "";

        public OffsetDateTime PublishedOn { get; set; }
    }

    public class Comment
    {
        [Key]        
        public Guid PublicKey { get; set; } = Guid.NewGuid();

        public string Text { get; set; } = "";

        public Guid PostId { get; set; } = Guid.NewGuid();

        public OffsetDateTime SubmittedOn { get; set; }
    }
}
