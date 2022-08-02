using Memo.Mvc.Services;
using Memo.Mvc.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace Memo.Mvc.Controllers;

/// <summary>Контроллер авторизации</summary>
public sealed class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    /// <summary>Возвращает представление со страницей входа</summary>
    [HttpGet]
    public IActionResult Login() => View();

    /// <summary>Выполняет вход пользователя</summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginRequestModel model)
    {
        if (ModelState.IsValid)
        {
            var token = await _authService.LoginAsync(model);
        }
        return View(model);
    }

    /// <summary>Возвращает представление со страницей регистрации</summary>
    [HttpGet]
    public IActionResult Register() => View();

    /*/// <summary>Регистрирует пользователя</summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterRequestModel model)
    {
        if (ModelState.IsValid)
        {
            User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
            {
                // добавляем пользователя в бд
                db.Users.Add(new User { Email = model.Email, Password = model.Password });
                await db.SaveChangesAsync();
 
                await Authenticate(model.Email); // аутентификация
 
                return RedirectToAction("Index", "Home");
            }
            else
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
        }
        return View(model);
    }
 
        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
 
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }*/
}