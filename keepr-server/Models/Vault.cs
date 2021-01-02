namespace keepr_server.Models
{
    public class Vault
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IsPrivate { get; set; }
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public Profile Creator { get; set; }
    }
    // public class VaultKeepViewModel : Vault
    // {
    //     public int KeepId {get;set;}
    // }
}