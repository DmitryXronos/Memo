namespace Memo.Auth.Interfaces;

/// <summary>Интерфейс сервиса по работе с паролями</summary>
public interface IPasswordService
{
    /// <summary>Хэширует предоставленный пароль</summary>
    string HashPassword(string password);

    /// <summary>Сравнивает хэш и предоставленный пароль</summary>
    bool VerifyPassword(string hashedPassword, string providedPassword);
}