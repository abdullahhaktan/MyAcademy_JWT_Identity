using Microsoft.AspNetCore.Mvc;
using MusicAppUi.DTOs.UserDtos;
using MusicAppUi.Services.AccountServices;

namespace MusicAppUi.Controllers
{
    public class RegisterController(IAccountService accountService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegisterDto registerDto)
        {
            var userResponse = await accountService.RegisterAsync(registerDto);

            if (userResponse != null)
            {
                return RedirectToAction("Index", "Login");
            }

            ModelState.AddModelError("", "Kayıt başarısız, bilgilerinizi kontrol ediniz!");
            return View();
        }
    }
}
