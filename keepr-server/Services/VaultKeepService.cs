using System.Collections.Generic;
using keepr_server.Models;
using keepr_server.Repositories;

namespace keepr_server.Services
{
    public class VaultKeepService
    {
        private readonly VaultKeepRepository _repo;
        private readonly KeepService _ks;
        private readonly VaultRepository _vrepo;
        private readonly KeepRepository _krepo;
        public VaultKeepService(VaultKeepRepository repo, KeepService ks, VaultRepository vrepo, KeepRepository krepo)
        {
            _krepo = krepo;
            _vrepo = vrepo;
            _ks = ks;
            _repo = repo;
        }
        public IEnumerable<Keep> GetKeepsByVault(int vaultId)
        {
            return _repo.GetKeepsByVaultId(vaultId);
        }
        public VaultKeep Create(VaultKeep newVaultKeep)
        {
            Vault foundVault = _vrepo.GetOne(newVaultKeep.VaultId);
            if (foundVault.CreatorId != newVaultKeep.CreatorId)
            {
                throw new System.Exception("There is no vault by that Id : Access Denied");
            }
            Keep foundKeep = _ks.GetOne(newVaultKeep.KeepId);
            foundKeep.Keeps++;
            _krepo.Edit(foundKeep);
            newVaultKeep.Id = _repo.Create(newVaultKeep);
            return newVaultKeep;
        }
        public string Delete(int id, string userId)
        {
            VaultKeep original = _repo.GetOne(id);
            if (original == null)
            {
                throw new System.Exception("No keeps in that vault");
            }
            if (original.CreatorId != userId)
            {
                throw new System.Exception("Access is denied");
            }
            if (_repo.Delete(id))
            {
                return "Successfully deleted";
            }
            return "Not deleted";
        }
        public VaultKeep GetOne(int id, string userId)
        {
            VaultKeep original = _repo.GetOne(id);
            if (original == null)
            {
                throw new System.Exception("No keeps in that vault");
            }
            if (original.CreatorId != userId)
            {
                throw new System.Exception("Access Denied");
            }
            return original;
        }
    }
}