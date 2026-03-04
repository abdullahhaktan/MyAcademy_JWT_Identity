using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyAcademy_JWT_Identity.Dtos.UserDtos;
using MyAcademy_JWT_Identity.Entities;
using MyAcademy_JWT_Identity.Services.JwtServices;

namespace MyAcademy_JWT_Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtService jwtService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Kullanıcı Kaydı Başarılı");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            if (!result.Succeeded)
            {
                return BadRequest("kullanıcı veya şfire hatal");
            }

            var user = await userManager.FindByNameAsync(model.UserName);

            var token = await jwtService.GenerateTokenAsync(user);

            return Ok(new { token = token });
        }
    }
}
