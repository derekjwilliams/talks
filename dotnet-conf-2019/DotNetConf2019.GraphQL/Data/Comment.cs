using NodaTime;
using System;
using System.ComponentModel.DataAnnotations;

namespace FoundObjx.Blog.Data
{
    public class Comment
    {
        [Key]        
        public Guid PublicKey { get; set; } = Guid.NewGuid();

        public string Text { get; set; } = null!;

        public Guid PostId { get; set; } = Guid.NewGuid();

        public OffsetDateTime SubmittedOn { get; set; }
    }
}
