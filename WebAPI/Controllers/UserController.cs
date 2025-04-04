using Application.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _userService;

        public UserController(IAuthService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userDto)
        {
            var result = await _userService.RegisterAsync(userDto.Username, userDto.Password, userDto.Role);

            if (result == null)
                return BadRequest("Kullanıcı kaydedilirken bir hata oluştu.");

            if (result == "Bu kullanıcı adı zaten alınmış!")
                return Conflict(result);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var token = await _userService.Authenticate(request.Username, request.Password);

            if (token == null)
                return Unauthorized("Geçersiz kullanıcı adı veya şifre!");

            return Ok(new { Token = token });
        }
    }
}

