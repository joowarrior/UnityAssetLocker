using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UnityAssetLockerWebServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RemoveController : ControllerBase
    {

        public class Packet{
            public string Guid { get; set; }
        }
        // POST: api/Assets
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public ActionResult Remove(Packet p)
        {
            switch(AssetManager.Remove(p.Guid))
            {
                case RemoveResult.NotExist: return NotFound($"{p.Guid} not exist");
                case RemoveResult.Success : return Ok("Success");
                default: return BadRequest("Wrong request");
            }
        }
    }
}
