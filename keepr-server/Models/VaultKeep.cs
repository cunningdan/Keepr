namespace keepr_server.Models
{
    public class VaultKeep
    {
        public int VaultId { get; set; }
        public int KeepId { get; set; }
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public Profile Creator { get; set; }
    }
}