using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;
using Memo.Notes.Config;
using Memo.Notes.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Memo.Notes.Services;

/// <summary>Сервис по работе с JWT</summary>
public sealed class JwtService : IJwtService
{
    private readonly JwtOptions _options;

    public JwtService(IOptions<JwtOptions> options)
    {
        _options = options.Value;
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

    /// <summary>Извлекает полезные данныео пользователе из jwt</summary>
    public ICurrentUserInfoService? GetPayload(string jwt)
    {
        var tokenHandler  = new JwtSecurityTokenHandler();
        
        // Читаем токен
        if (tokenHandler.ReadToken(jwt) is not JwtSecurityToken securityToken)
            return null;
        
        // Читаем нужный claim
        var stringClaimValue  = securityToken.Claims.First(claim  => claim.Type == nameof(ICurrentUserInfoService)).Value;
        
        return JsonSerializer.Deserialize<ICurrentUserInfoService>(stringClaimValue);
    }
}