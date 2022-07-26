namespace Memo.Mvc.Interfaces;

public interface IAuthService
{
    Task<string> LoginAsync();
}