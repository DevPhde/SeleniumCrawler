using Crawler.Repository;
using Microsoft.AspNetCore.Mvc;

namespace iPrazos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var a = await ProxyDataRepository.GetAllProxyConnection();
                return Ok(a);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return BadRequest(ex.Message);
            }

        }
    }
}