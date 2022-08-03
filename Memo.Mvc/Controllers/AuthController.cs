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
    public async Task<IActionResult> Login(LoginRequestModel model)
    {
        if (! ModelState.IsValid) 
            return View(model);
        
        var token = await _authService.LoginAsync(model);
        
        if (string.IsNullOrWhiteSpace(token))
            return View(model);
        
        HttpContext.Response.Cookies.Append("token", token);
        
        return RedirectToAction("Index", "Home");
    }

    /// <summary>Возвращает представление со страницей регистрации</summary>
    [HttpGet]
    public IActionResult Register() => View();

    
    /// <summary>Регистрирует пользователя</summary>
    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequestModel model)
    {
        if (! ModelState.IsValid) 
            return View(model);
        
        var token = await _authService.RegisterAsync(model);
        
        if (string.IsNullOrWhiteSpace(token))
            return View(model);
        
        HttpContext.Response.Cookies.Append("token", token);
        
        return RedirectToAction("Index", "Home");
    }
}