using CardakRezervasyon.Api.DTOs.MesireAlanlari;
using CardakRezervasyon.Api.Services;
using Microsoft.AspNetCore.Mvc;
using CardakRezervasyon.Api.DTOs.MesireAlanlari;

namespace CardakRezervasyon.Api.Controllers
{
    [ApiController]
    [Route("api/mesire-alanlari")]
    public class MesireAlanlariController : ControllerBase
    {
        private readonly IMesireAlaniService _service;

        public MesireAlanlariController(IMesireAlaniService service)
        {
            _service = service;
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
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
    }
}