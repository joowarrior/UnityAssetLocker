
using Microsoft.AspNetCore.Mvc;

namespace UnityAssetLockerWebServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UnlockController : ControllerBase
    {

        public class Packet{
            public string Guid { get; set; }
            public string Holder { get; set; }
        }
        // POST: api/Assets
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public ActionResult Unlock(Packet p)
        {
            UnlockResult r = AssetManager.UnlockAsset(p.Guid, p.Holder);
            switch  (r)
            {
                case UnlockResult.Success : return Ok("Success");
                case UnlockResult.AlreadyUnlocked: return Ok("Already unlocked");
                case UnlockResult.NotExist : return NotFound($"{p.Guid} is not founded");
                case UnlockResult.WrongHolder : return BadRequest($"{p.Guid},{p.Holder}, wrong holder");
                default: return BadRequest("${r} is not supported");
            }
        }
    }
}
