using Memo.Mvc.RequestModels;

namespace Memo.Mvc.Interfaces;

/// <summary>Интерфейс сервиса по авторизации пользователей</summary>
public interface IAuthService
{
    /// <summary>Выполняет вход пользователя через микросервис авторизации</summary>
    Task<string> LoginAsync(LoginRequestModel requestModel);

    /// <summary>Выполняет регистрацию пользователя через микросервис авторизации</summary>
    Task<string> RegisterAsync(RegisterRequestModel requestModel);
}