using Memo.Auth.Models;
using Memo.Auth.RequestModels;
using Microsoft.EntityFrameworkCore;

namespace Memo.Auth.Services;

/// <summary>Сервис авторизации</summary>
public sealed class AuthService : IAuthService
{
    private readonly ApplicationContext _context;
    private readonly IJwtService _jwtService;
    private readonly IPasswordService _passwordService;
    private readonly ICurrentUserInfoService _info;

    public AuthService(ApplicationContext context, IJwtService jwtService, IPasswordService passwordService,
        ICurrentUserInfoService info)
    {
        _context = context;
        _jwtService = jwtService;
        _passwordService = passwordService;
        _info = info;
    }
    
    /// <summary>Выполняет вход за пользователя и возвращает токен</summary>
    public async Task<string> LoginAsync(LoginRequestModel requestModel)
    {
        var user = await _context.Users.FirstOrDefaultAsync(p => p.Email == requestModel.Email);
        
        // Если пользователь не найден, то токен не генерируем
        if (user is null)
            return string.Empty;

        // Если пароль не верен, то токен не генерируем
        if (!_passwordService.VerifyPassword(user.PasswordHash, requestModel.Password))
            return string.Empty;

            // Генерируем и возвращаем токен
        var token = _jwtService.GenerateJwt(new CurrentUserInfoService
        {
            UserId = user.Id,
            Role = user.Role
        });

        return token;
    }

    /// <summary>Регистрирует пользователя и возвращает токен</summary>
    public async Task<string> RegisterAsync(RegisterRequestModel requestModel)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(p => p.Email == requestModel.Email || p.Name == requestModel.Name);
        
        // Если пользак с таким именем или email уже существует
        if (user is not null)
            return string.Empty;

        // Хэшируем пароль
        var passwordHash = _passwordService.HashPassword(requestModel.Password);
        
        // Создаем пользака
        user = new User
        {
            Id = Guid.NewGuid(),
            Email = requestModel.Email,
            Name = requestModel.Name,
            PasswordHash = passwordHash
        };

        // Сохраняем его в базу
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        // Генерируем и возвращаем токен
        var token = _jwtService.GenerateJwt(new CurrentUserInfoService
        {
            UserId = user.Id,
            Role = user.Role
        });

        return token;
    }

    /// <summary>Меняет пароль пользователя и возвращает true в случае успеха</summary>
    public async Task<bool> ChangePasswordAsync(ChangePasswordRequestModel requestModel)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(p => p.Id == _info.UserId);

        if (user is null)
            return false;
        
        // Если старый пароль не верен, то ничего не меняем
        if (! _passwordService.VerifyPassword(user.PasswordHash, requestModel.OldPassword))
            return false;
        
        // Хэшируем новый пароль
        var hash = _passwordService.HashPassword(requestModel.NewPassword);
        
        // Меняем пароль
        user.PasswordHash = hash;
        await _context.SaveChangesAsync();

        return true;
    }
}