using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using keepr_server.Services;
using keepr_server.Models;

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
    }
}