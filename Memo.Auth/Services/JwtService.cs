using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Memo.Auth.Config;
using Memo.Auth.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Memo.Auth.Services;

/// <summary>Сервис по работе с JWT</summary>
public sealed class JwtService : IJwtService
{
    private readonly JwtOptions _options;

    public JwtService(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    /// <summary>Генерирует Jwt</summary>
    public string GenerateJwt(ICurrentUserInfoService info)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_options.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                // Сериализуем Payload в JSON и записываем в токен
                new Claim(nameof(ICurrentUserInfoService), JsonSerializer.Serialize(info))
            }),
            // Длительность жизни токена
            Expires = DateTime.UtcNow.AddMinutes(_options.DurationInMinutes),
            // Потребитель
            Audience = _options.Audience,
            // Издатель
            Issuer = _options.Issuer,
            // Подпись
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token);
    }

    /// <summary>Проверяет достоверность jwt</summary>
    public bool VerifyJwt(string jwt)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        // Пытаемся валидировать токен
        try
        {
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                // Валидируем подпись
                ValidateIssuerSigningKey = true,
                // Валидируем издателя
                ValidateIssuer = true,
                // Валидируем потребителя
                ValidateAudience = true,
                // Валидируем время жизни токена
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.Key)),
                ValidIssuer = _options.Issuer,
                ValidAudience = _options.Audience
            }, out _);
        }
        catch
        {
            return false;
        }

        return true;
    }

    /// <summary>Извлекает полезные данные из jwt</summary>
    public ICurrentUserInfoService? GetPayload(string jwt)
    {
        var tokenHandler  = new JwtSecurityTokenHandler();
        
        // Читаем токен
        var securityToken  = tokenHandler.ReadToken(jwt) as JwtSecurityToken;
        if (securityToken is null)
            return null;
        
        // Читаем нужный claim
        var stringClaimValue  = securityToken.Claims.First(claim  => claim.Type == nameof(ICurrentUserInfoService)).Value;
        
        return JsonSerializer.Deserialize<ICurrentUserInfoService>(stringClaimValue);
    }
}