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
    public class LockController : ControllerBase
    {
        public class Packet{
            public string Guid{get;set;}
            public string Holder{get;set;}
        }

        // POST: api/Assets
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public ActionResult Lock(Packet p)
        {
            LockResult result = AssetManager.LockAsset(p.Guid, p.Holder);
            switch(result)
            {
                case LockResult.AlreadyLocked: return Ok("Already Locked");
                case LockResult.NotExist: return NotFound($"guid : {p.Guid} is not exist");
                case LockResult.Success: return Ok("Success");
                default: return BadRequest($"{result} is not supported");
            }
        }
    }
}