using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using keepr_server.Services;
using keepr_server.Models;

namespace keepr_server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KeepsController : ControllerBase
    {
        [HttpGet]
        public Keep GetAll()
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