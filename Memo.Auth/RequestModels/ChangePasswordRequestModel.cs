using System.ComponentModel.DataAnnotations;

namespace Memo.Auth.RequestModels;

/// <summary>Модель, использующаяся для смены пароля</summary>
public sealed class ChangePasswordRequestModel
{
    /// <summary>Старый пароль</summary>
    [StringLength(20, MinimumLength = 4)]
    public string OldPassword { get; set; } = string.Empty;
    
    /// <summary>Новый пароль</summary>
    [StringLength(20, MinimumLength = 4)]
    public string NewPassword { get; set; } = string.Empty;
}