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
        public Keep GetOne(int id)
        {
            return _repo.GetOne(id);
        }
        public string Delete(int id, string userId)
        {
            Keep original = _repo.GetOne(id);
            if (original == null)
            {
                throw new System.Exception("Bad Id");
            }
            if (original.CreatorId != userId)
            {
                throw new System.Exception("Not yours. Access Denied");
            }
            if (_repo.Delete(id))
            {
                return "Deleted successfully";
            }
            return "Not Deleted";
        }
    }
}