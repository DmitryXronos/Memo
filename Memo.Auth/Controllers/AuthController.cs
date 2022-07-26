﻿using Memo.Auth.Services;
using Memo.Auth.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace Memo.Auth.Controllers;

/// <summary>Контроллер авторизации</summary>
[Route("api/[controller]/[action]")]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    /// <summary>Выполнение входа пользователя</summary>
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequestModel requestModel)
    {
        var token = await _authService.LoginAsync(requestModel);
        
        if (! string.IsNullOrWhiteSpace(token))
            return Ok(token);

        return Unauthorized();
    }
    
    /// <summary>Регистрация ползователя</summary>
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterRequestModel requestModel)
    {
        var token = await _authService.RegisterAsync(requestModel);
        
        if (! string.IsNullOrWhiteSpace(token))
            return Ok(token);

        return BadRequest();
    }

    /// <summary>Смена пароля</summary>
    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequestModel requestModel)
    {
        var success = await _authService.ChangePasswordAsync(requestModel);

        return success ? Ok() : BadRequest();
    }
}