namespace Memo.Notes.Interfaces;

/// <summary>Интерфейс сервиса по работе с jwt</summary>
public interface IJwtService
{
    /// <summary>Проверяет достоверность jwt</summary>
    bool VerifyJwt(string jwt, out ICurrentUserInfoService? info);
}