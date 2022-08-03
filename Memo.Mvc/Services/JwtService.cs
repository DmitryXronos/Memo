using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Memo.Mvc.Access;
using Memo.Mvc.Config;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Memo.Mvc.Services;

/// <summary>Сервис по работе с JWT</summary>
public sealed class JwtService : IJwtService
{
    private readonly JwtOptions _options;

    public JwtService(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    /// <summary>Проверяет достоверность jwt</summary>
    public bool VerifyJwt(string token, out ICurrentUserInfoService? info)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        
        // Пытаемся валидировать токен
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
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
            info = null;
            return false;
        }

        // Пытаемся считать инфу о пользователе из токена
        if (tokenHandler.ReadToken(token) is not JwtSecurityToken securityToken)
        {
            info = null;
            return false;
        }
        var userId  = securityToken.Claims.FirstOrDefault(claim  => claim.Type == "UserId");
        var role = securityToken.Claims.FirstOrDefault(claim  => claim.Type == "Role");
        if (userId is null || role is null)
        {
            info = null;
            return false;
        }
        info = new CurrentUserInfoService
        {
            UserId = new Guid(userId.Value),
            Role = (RoleType)int.Parse(role.Value)
        };

        return true;
    }
}