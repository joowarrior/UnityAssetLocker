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
    public class GetInfoController : ControllerBase
    {

        public class Packet{
            public string Guid { get; set; }
        }
        // POST: api/Assets
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public ActionResult GetInfo(Packet p)
        {
            Asset founded = null;
            if(!AssetManager.TryGetAsset(p.Guid, out founded))
                return NotFound($"{p.Guid} is not exist");

            return Ok(founded);
        }
    }
}
