using System;
//using System.Collections.Generic;

namespace DotNetConf2019.GraphQL.Data
{
    public class Comments
    {
        public int Id { get; set; } = 0;

        public string Text { get; set; } = null!;

        public int PostId { get; set; } = 0;

        public DateTime SubmittedOn { get; set; }

        //public virtual Posts Post { get; set; }
    }
}
