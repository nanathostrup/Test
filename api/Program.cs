using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
 
namespace DemoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerSyncController : ControllerBase
    {
        [HttpPost("sync")]
        public async Task<IActionResult> SyncCustomer([FromBody] CustomerRequest request)
        {
            // WRONG: hardcoded endpoint in controller
            var url = "https://thirdparty.example.com/api/customers";
        }
    }
}