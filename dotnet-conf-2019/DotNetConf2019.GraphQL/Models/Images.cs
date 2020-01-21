namespace DotNetConf2019.GraphQL.Data
{
    public class Images
    {
        public int Id { get; set; }

        public ImageSize Size { get; set; } = ImageSize.Small;

        public string Url { get; set; } = null!;

        public int PostId { get; set; } = 0;

        //public virtual Posts Post { get; set; }
    }
}
