using System;

namespace DotNetConf2019.GraphQL.Data
{
    public class Posts
    {
        //public Posts()
        //{
        //    //Comments = new HashSet<Comments>();
        //    //Images = new HashSet<Images>();
        //}

        public int Id { get; set; } = 0;

        public int AuthorId { get; set; } = 0;

        public string Title { get; set; } = "";

        public string Markdown { get; set; } = "";

        public DateTime PublishedOn { get; set; }

        //public virtual Authors Author { get; set; } 
        //public virtual ICollection<Comments> Comments { get; set; }
        //public virtual ICollection<Images> Images { get; set; }
    }
}
