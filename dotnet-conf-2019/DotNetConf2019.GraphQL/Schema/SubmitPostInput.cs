using System;

namespace FoundObjx.Blog.Schema
{
    public class SubmitPostInput
    {
        public Guid AuthorId { get; set; }

        public string Title { get; set; } = "";

        public string Markdown { get; set; } = "";
    }
}
