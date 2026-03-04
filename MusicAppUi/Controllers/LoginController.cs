using Microsoft.AspNetCore.Mvc;
using MusicAppUi.DTOs.UserDtos;
using MusicAppUi.Services.AccountServices;

namespace MusicAppUi.Controllers
{
    public class LoginController(IAccountService accountService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginDto loginDto)
        {
            var userResponse = await accountService.LoginAsync(loginDto);

            if (userResponse != null && !string.IsNullOrEmpty(userResponse.Token))
            {
                HttpContext.Session.SetString("JwtToken", userResponse.Token);

                HttpContext.Session.SetString("UserFullName", userResponse.FullName);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Giriş başarısız, bilgilerinizi kontrol ediniz!");
            return View();
        }
    }
}
