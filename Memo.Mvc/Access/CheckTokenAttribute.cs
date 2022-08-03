using Memo.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Memo.Mvc.Access;

/// <summary>Проверяет токен, пришедший в заголовках запроса</summary>
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public sealed class CheckTokenAttribute : Attribute, IActionFilter
{
    /// <summary>Вызывается до выполнения действия</summary>
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Если токена нет в куки, то редиректим пользака, чтобы авторизовался
        if (!context.HttpContext.Request.Cookies.ContainsKey("token"))
        {
            context.Result = new RedirectToActionResult("Login", "Auth", null);
            return;
        }

        // Извлекаем токен
        var token = context.HttpContext.Request.Cookies["token"];
        if (string.IsNullOrEmpty(token))
        {
            context.Result = new RedirectToActionResult("Login", "Auth", null);
            return;
        }

        // Валидируем токен
        var jwtService = context.HttpContext.RequestServices.GetRequiredService<IJwtService>();
        if (!jwtService.VerifyJwt(token, out var info))
        {
            context.Result = new RedirectToActionResult("Login", "Auth", null);
            return;
        }
        
        if (info is null)
        {
            context.Result = new StatusCodeResult(500);
            return;
        }

        // Добавляем информацию о пользователе в DI
        var infoService = context.HttpContext.RequestServices.GetRequiredService<ICurrentUserInfoService>();
        infoService.UserId = info.UserId;
        infoService.Role = info.Role;
    }

    /// <summary>Вызывается после выполнения действия</summary>
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}
