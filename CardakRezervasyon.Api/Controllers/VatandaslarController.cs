using Microsoft.AspNetCore.Mvc;
using CardakRezervasyon.Api.DTOs.Vatandaslar;
using CardakRezervasyon.Api.Services;

namespace CardakRezervasyon.Api.Controllers
{
    [ApiController]
    [Route("api/vatandaslar")]
    public class VatandaslarController : ControllerBase
    {
        private readonly IVatandasService _service;

        public VatandaslarController(IVatandasService service)
        {
            _service = service;
        }

        [HttpPost("kayit")]
        public async Task<IActionResult> Register([FromBody] RegisterVatandasDto dto)
        {
            var (result, hataMesaji) = await _service.RegisterAsync(dto);

            if (result == null)
            {
                return BadRequest(new { message = hataMesaji });
            }

            return CreatedAtAction(nameof(Register), result);
        }
        [HttpPost("giris")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var (basarili, mesaj) = await _service.LoginAsync(dto);

            if (!basarili)
            {
                return BadRequest(new { message = mesaj });
            }

            return Ok(new { message = mesaj });
        }
        [HttpPost("dogrula")]
        public async Task<IActionResult> VerifyKodu([FromBody] VerifyKoduDto dto)
        {
            var (token, hataMesaji) = await _service.VerifyKoduAsync(dto);

            if (token == null)
            {
                return BadRequest(new { message = hataMesaji });
            }

            return Ok(new TokenDto { Token = token });
        }
    }
}