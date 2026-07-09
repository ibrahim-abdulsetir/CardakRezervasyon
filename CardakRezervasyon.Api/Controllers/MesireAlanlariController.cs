using CardakRezervasyon.Api.DTOs.MesireAlanlari;
using CardakRezervasyon.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CardakRezervasyon.Api.Controllers
{
    [ApiController]
    [Route("api/mesire-alanlari")]
    public class MesireAlanlariController : ControllerBase
    {
        private readonly IMesireAlaniService _service;

        private readonly ICardakService _cardakService;

        public MesireAlanlariController(IMesireAlaniService service, ICardakService cardakService)
        {
            _service = service;
            _cardakService = cardakService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMesireAlaniDto dto)
        {
            var (result, hataMesaji) = await _service.CreateAsync(dto);

            if (result == null)
            {
                return BadRequest(new { message = hataMesaji });
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        [HttpGet("{id}/bosluk")]
        public async Task<IActionResult> GetBosluk(int id, [FromQuery] DateTime baslangic, [FromQuery] DateTime bitis)
        {
            if (bitis <= baslangic)
            {
                return BadRequest(new { message = "Bitis zamani baslangic zamanindan sonra olmalidir." });
            }

            var result = await _cardakService.GetBoslukAsync(id, baslangic, bitis);

            if (result == null)
            {
                return NotFound($"MesireAlani with id {id} not found.");
            }

            return Ok(result);
        }
    }
}