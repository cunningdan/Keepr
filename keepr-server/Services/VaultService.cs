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
        public string Delete(int id, string userId)
        {
            Vault original = _repo.GetOne(id);
            if (userId != original.CreatorId)
            {
                throw new System.Exception("You are not the user : Access Denied");
            }
            if (original.IsPrivate == true)
            {
                if (_repo.Delete(id))
                {
                    return "Vault deleted";
                }
                return "Not deleted";
            }
            return "Cannot delete public Vault";
        }
        public Vault GetOne(int id)
        {
            Vault foundVault = _repo.GetOne(id);
            if (foundVault == null)
            {
                throw new System.Exception("No found vault");
            }
            if (foundVault.IsPrivate == true)
            {
                throw new System.Exception("No Vault exists by that id");
            }
            return foundVault;
        }
    }
}