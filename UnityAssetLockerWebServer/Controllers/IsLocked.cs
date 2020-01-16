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
    public class IsLockedController : ControllerBase
    {

        public class Packet
        {
            public string Guid { get; set; }
            public string Holder { get; set; }
        }
        // POST: api/Assets
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public ActionResult IsLocked(Packet p)
        {
            if (string.IsNullOrEmpty(p.Guid))
                return BadRequest("guid is null");

            Asset founded = null;
            if (!AssetManager.TryGetAsset(p.Guid, out founded))
                return NotFound($"{p.Guid} not founded");

            if (founded.Holder == p.Holder
            || !founded.Locked)
            {
                return Ok("Me");
            }
            else
            {
                return Ok(p.Holder);
            }
        }
    }
}
