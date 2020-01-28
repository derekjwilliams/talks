using NodaTime;
using System;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using System.ComponentModel.DataAnnotations;

namespace FoundObjx.Blog.Data
{
    public class Post
    {
        [Key]
        public Guid PublicKey { get; set; } = Guid.NewGuid();

        public Guid AuthorId { get; set; } = Guid.NewGuid();

        public string Title { get; set; } = "";

        public string Markdown { get; set; } = "";

        public OffsetDateTime PublishedOn { get; set; }
    }
}
