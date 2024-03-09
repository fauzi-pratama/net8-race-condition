
using apps.Models.Request;
using apps.Services;
using Microsoft.AspNetCore.Mvc;

namespace apps.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController(ITransactionService transactionService) : ControllerBase
    {
        [HttpPost("add_transaction")]
        public async Task<IActionResult> AddTransaction([FromBody] List<AddTransactionRequest> request)
        {
            var result = await transactionService.AddTransaction(request);

            if (!result.status)
                return BadRequest(result.message);

            return Ok(result.message);
        }
    }
}
