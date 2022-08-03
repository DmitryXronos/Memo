using Memo.Mvc.Access;

namespace Memo.Mvc.Services;

/// <summary>Информация о текущем пользователе</summary>
public interface ICurrentUserInfoService
{
    /// <summary>Id пользователя</summary>
    Guid UserId { get; set; }
    
    RoleType Role { get; set; }
}