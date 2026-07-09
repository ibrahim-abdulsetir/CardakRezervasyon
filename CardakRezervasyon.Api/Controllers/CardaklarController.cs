using Microsoft.AspNetCore.Mvc;
using CardakRezervasyon.Api.DTOs.Cardaklar;
using CardakRezervasyon.Api.Services;
using CardakRezervasyon.Api.DTOs.Cardaklar;
namespace CardakRezervasyon.Api.Controllers
{
    [ApiController]
    [Route("api/mesire-alanlari/{mesireAlaniId}/cardaklar")]
    public class CardaklarController : ControllerBase
    {
        private readonly ICardakService _service;

        public CardaklarController(ICardakService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged(
            int mesireAlaniId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? blok = null,
            [FromQuery] int? minKapasite = null)
        {
            var result = await _service.GetPagedByParkAsync(mesireAlaniId, page, pageSize, blok, minKapasite);

            if (result == null)
            {
                return NotFound($"MesireAlani with id {mesireAlaniId} not found.");
            }

            return Ok(result);
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Create(int mesireAlaniId, [FromBody] CreateCardakDto dto)
        {
            var (result, hataMesaji) = await _service.CreateAsync(mesireAlaniId, dto);

            if (result == null)
            {
                return BadRequest(new { message = hataMesaji });
            }

            return CreatedAtAction(nameof(GetPaged), new { mesireAlaniId }, result);
        }
    }
}