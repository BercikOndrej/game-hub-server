using GameHub.Application.Interfaces;
using GameHub.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GameHub.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly ILogger<PlatformsController> _logger;
        private readonly IPlatformService _service;

        public PlatformsController(ILogger<PlatformsController> logger, IPlatformService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Platform>>> GetPlatformsAsync() =>
            Ok(await _service.GetPlatformsAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Platform>> GetPlatformAsync(int id) =>
            Ok(await _service.GetPlatformAsync(id));
    }
}
