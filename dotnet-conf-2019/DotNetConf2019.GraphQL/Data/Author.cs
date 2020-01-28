using System;
using System.ComponentModel.DataAnnotations;
namespace DotNetConf2019.GraphQL.Data
{
    public class Author
    {
        [Key]
        public Guid PublicKey { get; set; } = Guid.NewGuid();

        public string Name { get; set; }  = null!;
    }
}
