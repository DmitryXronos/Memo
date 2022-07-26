using Memo.Auth.Enums;
using Memo.Auth.Interfaces;

namespace Memo.Auth.Services;

/// <summary>Информация о текущем пользователе</summary>
public class CurrentUserInfoService : ICurrentUserInfoService
{
    /// <summary>Id пользователя</summary>
    public Guid UserId { get; set; }
    
    public RoleType Role { get; set; }
}