using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Memo.Auth.Services;

/// <summary>Сервис по работе с паролями</summary>
public sealed class PasswordService : IPasswordService
{
    /// <summary>Хэширует предоставленный пароль</summary>
    public string HashPassword(string password)
    {
        // 128-битная соль
        var salt = new byte[128 / 8];
        
        // Заполняем соль случайными числами
        using var rng = RandomNumberGenerator.Create();
        rng.GetNonZeroBytes(salt);
        
        // Генерируем 256-битный ключ с помощью HMACSHA256 и 100.000 итераций
        var key = KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8);
        
        // Записываем соль и ключ
        var output = new byte[salt.Length + key.Length];
        Buffer.BlockCopy(salt, 0, output, 0, salt.Length);
        Buffer.BlockCopy(key, 0, output, salt.Length, key.Length);
        
        return Convert.ToBase64String(output);
    }

    /// <summary>Сравнивает хэш и предоставленный пароль</summary>
    public bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        // Декодируем байты хэша из строки
        var decodedHashedPassword = Convert.FromBase64String(hashedPassword);

        // Считываем соль
        var salt = new byte[128 / 8];
        Buffer.BlockCopy(decodedHashedPassword, 0, salt, 0, salt.Length);

        // Считываем ключ
        var expectedKey = new byte[decodedHashedPassword.Length - salt.Length];
        Buffer.BlockCopy(decodedHashedPassword, salt.Length, expectedKey, 0, expectedKey.Length);

        // Хэшируем полученный пароль и сверяем его с настоящим хэшем
        var actualKey = KeyDerivation.Pbkdf2(
            password: providedPassword,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8);
        
        return actualKey.SequenceEqual(expectedKey);
    }
}