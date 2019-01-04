using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            registerUserDto.name = registerUserDto.name.ToLower();

            if(await _authRepository.UserExists(registerUserDto.name))
                return BadRequest("User already exists!");

            var newUser = new User()
            {
                UserName = registerUserDto.name
            };

            var createdUser = await _authRepository.Register(newUser, registerUserDto.password);

            return StatusCode(201);
        }
    }
}