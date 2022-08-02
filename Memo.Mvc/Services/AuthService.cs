using Memo.Mvc.Extensions;
using Memo.Mvc.RequestModels;

namespace Memo.Mvc.Services;

/// <summary>Сервис по авторизации пользователей</summary>
public sealed class AuthService : IAuthService
{
    /// <summary>Выполняет вход пользователя через сервис авторизации</summary>
    public async Task<string> LoginAsync(LoginRequestModel requestModel)
    {
        // Шлем запрос к сервису авторизации
        using var httpClient = new HttpClient();
        var queryString = $"http://memo_authy:80/Auth/Login?{requestModel.ToQueryString()}";
        var result = await httpClient.PostAsync(queryString, null);

        // Читаем токен из ответа
        var token = await result.Content.ReadAsStringAsync();

        return token;
    }

    /// <summary>Выполняет регистрацию пользователя через микросервис авторизации</summary>
    public async Task<string> RegisterAsync(RegisterRequestModel requestModel)
    {
        // Шлем запрос к сервису авторизации
        using var httpClient = new HttpClient();
        var queryString = $"http://memo_auth:80/Auth/Register?{requestModel.ToQueryString()}";
        var result = await httpClient.PostAsync(queryString, null);

        // Читаем токен из ответа
        var token = await result.Content.ReadAsStringAsync();

        return token;
    }
}