namespace keepr_server.Models
{
    public class Keep
    {
        public string Name { get; set; }
        public string Img { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public Profile Creator { get; set; }
        // public string[] Tags {get;set;}
    }
    public class KeepTagsViewModel : Keep
    {
        public string KeepId { get; set; }
    }
}