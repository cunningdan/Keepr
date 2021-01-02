using System.Collections.Generic;
using keepr_server.Models;
using keepr_server.Repositories;

namespace keepr_server.Services
{
    public class KeepService
    {
        private readonly KeepRepository _repo;
        public KeepService(KeepRepository repo)
        {
            _repo = repo;
        }
        public IEnumerable<Keep> GetAll()
        {
            return _repo.GetAll();
        }
        public IEnumerable<Keep> GetByProfile(string creatorId)
        {
            return _repo.GetByProfile(creatorId);
        }
        public Keep Create(Keep newKeep)
        {
            newKeep.Id = _repo.Create(newKeep);
            return newKeep;
        }
    }
}