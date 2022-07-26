using Memo.Auth.RequestModels;

namespace Memo.Auth.Interfaces;

/// <summary>Интерфейс сервиса авторизации</summary>
public interface IAuthService
{
    /// <summary>Выполняет вход за пользователя и возвращает токен</summary>
    Task<string> LoginAsync(LoginRequestModel requestModel);

    /// <summary>Регистрирует пользователя и возвращает токен</summary>
    Task<string> RegisterAsync(RegisterRequestModel requestModel);
}