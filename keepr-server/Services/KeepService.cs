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
            Keep foundKeep = _repo.GetOne(id);
            foundKeep.Views++;
            this.EditVSK(foundKeep);
            return foundKeep;
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
        public void Edit(Keep keepData, string userId)
        {
            if (userId != keepData.CreatorId)
            {
                throw new System.Exception("Access Denied");
            }
            _repo.Edit(keepData);
        }
        public void EditVSK(Keep keepData)
        {
            _repo.Edit(keepData);
        }
    }
}