
using apps.Models.Request;
using apps.Services;
using Microsoft.AspNetCore.Mvc;

namespace apps.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController(IMasterService masterService) : ControllerBase
    {
        [HttpPost("add_master")]
        public async Task<IActionResult> AddMaster(List<AddMasterRequest> request)
        {
            var result = await masterService.AddMaster(request);

            if(!result.status)
                return NotFound(result.message);

            return Ok(result.message);
        }
    }
}
