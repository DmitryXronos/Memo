namespace Memo.Mvc.Config;

/// <summary>Параметры для генерации и проверки JWT</summary>
public sealed class JwtOptions
{
    /// <summary>Издатель токена</summary>
    public string Issuer { get; set; } = string.Empty;
    
    /// <summary>Потребитель токена</summary>
    public string Audience { get; set; } = string.Empty;
    
    /// <summary>Ключ для шифрации токена</summary>
    public string Key { get; set; } = string.Empty;
    
    /// <summary>Время жизни токена в минутах</summary>
    public int DurationInMinutes { get; set; }
}