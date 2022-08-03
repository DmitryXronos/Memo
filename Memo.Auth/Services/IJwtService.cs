using Memo.Auth.Models;

namespace Memo.Auth.Services;

/// <summary>Интерфейс сервиса по работе с jwt</summary>
public interface IJwtService
{
    /// <summary>Генерирует новый jwt</summary>
    string GenerateJwt(User user);
}