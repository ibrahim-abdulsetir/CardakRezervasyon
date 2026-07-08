using Microsoft.AspNetCore.Mvc;
using CardakRezervasyon.Api.DTOs.Rezervasyonlar;
using CardakRezervasyon.Api.Services;
using CardakRezervasyon.Api.Models.Entities;

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
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound($"Rezervasyon with id {id} not found.");
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] int? cardakId = null,
            [FromQuery] RezervasyonDurumu? durum = null)
        {
            var result = await _service.GetPagedAsync(page, pageSize, cardakId, durum);
            return Ok(result);
        }
    }
}