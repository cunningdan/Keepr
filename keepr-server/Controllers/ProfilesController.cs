using System.Collections.Generic;
using keepr_server.Models;
using keepr_server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly KeepService _kserv;
        private readonly VaultService _vserv;
        private readonly ProfileService _ps;
        public ProfilesController(KeepService ks, VaultService vs, ProfileService ps)
        {
            _ps = ps;
            _vserv = vs;
            _kserv = ks;
        }
        [HttpGet("{creatorId}/keeps")]
        [Authorize]
        public ActionResult<IEnumerable<Keep>> GetKeepByProfile(string creatorId)
        {
            try
            {
                return Ok(_kserv.GetByProfile(creatorId));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{creatorId}/vaults")]
        [Authorize]
        public ActionResult<IEnumerable<Vault>> GetVaultByProfile(string creatorId)
        {
            try
            {
                return Ok(_vserv.GetByProfile(creatorId));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{name}")]
        public ActionResult<Profile> GetProfileByName(string Name)
        {
            try
            {
                return Ok(_ps.GetByName(Name));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}