using System.Collections.Generic;
using keepr_server.Models;
using keepr_server.Repositories;

namespace keepr_server.Services
{
    public class VaultService
    {
        private readonly VaultRepository _repo;
        public VaultService(VaultRepository repo)
        {
            _repo = repo;
        }
        public IEnumerable<Vault> GetByProfile(string creatorId)
        {
            return _repo.GetByProfile(creatorId);
        }
        public Vault Create(Vault newVault)
        {
            newVault.Id = _repo.Create(newVault);
            return newVault;
        }
    }
}