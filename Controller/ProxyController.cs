using Microsoft.AspNetCore.Mvc;
using TeiasProxy.Services;

namespace TeiasProxy.Controllers
{
    [ApiController]
    [Route("teias/dgp")]
    public class ProxyController : ControllerBase
    {
        private readonly TeiasProxyService _proxyService;

        public ProxyController(TeiasProxyService proxyService)
        {
            _proxyService = proxyService;
        }

        [HttpPost]
        [Produces("application/xml")]
        [Consumes("text/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post()
        {
            using var reader = new StreamReader(Request.Body);
            var firmXml = await reader.ReadToEndAsync();

            try
            {
                var teiasResponse = await _proxyService.ForwardToTeiasAndLogAsync(firmXml);
                return Content(teiasResponse, "text/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hata: {ex.Message}");
            }
        }
    }
}
