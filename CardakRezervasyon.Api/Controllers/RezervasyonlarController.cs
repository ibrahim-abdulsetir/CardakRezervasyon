using Microsoft.AspNetCore.Mvc;
using CardakRezervasyon.Api.DTOs.Rezervasyonlar;
using CardakRezervasyon.Api.Services;

namespace CardakRezervasyon.Api.Controllers
{
    [ApiController]
    [Route("api/rezervasyonlar")]
    public class RezervasyonlarController : ControllerBase
    {
        private readonly IRezervasyonService _service;

        public RezervasyonlarController(IRezervasyonService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRezervasyonDto dto)
        {
            var (result, hataMesaji) = await _service.CreateAsync(dto);

            if (result == null)
            {
                return BadRequest(new { message = hataMesaji });
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Placeholder for now — we'll properly implement this in the next step (15.7, List/Detail)
            return Ok();
        }
    }
}