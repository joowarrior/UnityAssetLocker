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
    public class AddController : ControllerBase
    {

        // POST: api/Assets
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public ActionResult Add(Asset asset)
        {
            AddResult result = AssetManager.AddAsset(asset);
            switch(result)
            {
                case AddResult.ExistGUID: return BadRequest("Asset already exist");
                case AddResult.Success: return Ok("Success");
                default : return BadRequest($"{result} is Notsupported");
            }
        }
    }
}
