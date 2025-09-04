using ASPNETChallenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETChallenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusteriController : ControllerBase
    {
        private readonly MusteriService _service;

        public MusteriController(MusteriService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetMusterilerAsync();
            return Ok(result);
        }

        [HttpGet("{id}/bakiye")]
        public async Task<IActionResult> GetBakiye(int id)
        {
            var result = await _service.GetBakiyeSeyriAsync(id);
            return Ok(result);
        }
    }
}
