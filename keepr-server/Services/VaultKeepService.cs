using keepr_server.Repositories;

namespace keepr_server.Services
{
    public class VaultKeepService
    {
        private readonly VaultKeepRepository _repo;
        public VaultKeepService(VaultKeepRepository repo)
        {
            _repo = repo;
        }
    }
}