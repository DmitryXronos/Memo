namespace Memo.Auth.Interfaces;

/// <summary>Интерфейс сервиса по работе с jwt</summary>
public interface IJwtService
{
    /// <summary>Генерирует новый jwt</summary>
    string GenerateJwt(ICurrentUserInfoService info);
    
    /// <summary>Проверяет достоверность jwt</summary>
    bool VerifyJwt(string token, out ICurrentUserInfoService? info);
}