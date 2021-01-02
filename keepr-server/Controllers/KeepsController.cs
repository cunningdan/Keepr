using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using keepr_server.Services;
using keepr_server.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;

namespace keepr_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeepsController : ControllerBase
    {
        private readonly KeepService _ks;
        public KeepsController(KeepService ks)
        {
            _ks = ks;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Keep>> GetAll()
        {
            try
            {
                return Ok(_ks.GetAll());
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Keep>> Create(Keep newKeep)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newKeep.CreatorId = userInfo.Id;
                Keep created = _ks.Create(newKeep);
                created.Creator = userInfo;
                return created;
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}