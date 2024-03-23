using IdentityServiceApi.Contrat;
using IdentityServiceApi.Dto;

using Microsoft.AspNetCore.Mvc;

namespace IdentityServiceApi.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private ResponseDto responseDto;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Register(RegistrationDto registrationDto)
        {
            var result = await authService.RegisterAsync(registrationDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto user)
        {
            var result = await authService.LoginAsync(user);
            if (result.Token != string.Empty && result.User is not null)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
