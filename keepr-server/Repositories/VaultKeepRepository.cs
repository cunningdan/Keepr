using System.Data;

namespace keepr_server.Repositories
{
    public class VaultKeepRepository
    {
        private readonly IDbConnection _db;
        public VaultKeepRepository(IDbConnection db)
        {
            _db = db;
        }
    }
}