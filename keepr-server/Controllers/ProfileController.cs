using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CodeWorks.Auth0Provider;
using keepr_server.Services;
using keepr_server.Models;

namespace keepr_server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileService _ps;
        private readonly VaultService _vs;
        private readonly KeepService _ks;
        private readonly VaultKeepService _vks;
        public ProfileController(ProfileService ps, VaultService vs, KeepService ks, VaultKeepService vks)
        {
            _ks = ks;
            _vs = vs;
            _ps = ps;
            _vks = vks;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Profile>>> Get()
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_ps.GetOrCreateProfile(userInfo));
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
                return Ok(_vs.GetByProfile(creatorId));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{creatorId}/keeps")]
        [Authorize]
        public ActionResult<IEnumerable<Keep>> GetKeepByProfile(string creatorId)
        {
            try
            {
                return Ok(_ks.GetByProfile(creatorId));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // [HttpGet("{creatorId}/vaultkeeps")]
        // [Authorize]
        // public ActionResult<IEnumerable<VaultKeep>> GetVaultKeepsByProfile(string creatorId)
        // {
        //     try
        //     {
        //         return Ok(_vks.GetByProfile(creatorId));
        //     }
        //     catch (System.Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }
    }
}