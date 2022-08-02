using Memo.Notes.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Memo.Notes.Access;

/// <summary>Проверяет токен, пришедший в заголовках запроса</summary>
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public sealed class CheckTokenAttribute : Attribute, IActionFilter
{
    /// <summary>Вызывается до выполнения действия</summary>
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Ищем токен из заголовков запроса
        var token = (string)context.HttpContext.Request.Headers
            .Where(p => p.Key == "token")
            .Select(p => p.Value)
            .FirstOrDefault();
        if (token is null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // Валидируем токен
        var jwtService = context.HttpContext.RequestServices.GetRequiredService<IJwtService>();
        if (!jwtService.VerifyJwt(token, out var info))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (info is null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // Добавляем информацию о пользователе в DI
        var payloadService = context.HttpContext.RequestServices.GetRequiredService<ICurrentUserInfoService>();
        payloadService.UserId = info.UserId;
    }

    /// <summary>Вызывается после выполнения действия</summary>
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}

