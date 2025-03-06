﻿using AccessCorp.Application.Entities;
using AccessCorp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AccessCorp.WebApi.Controllers;

[Route("identity")]
public class AuthController : MainController
{
    private readonly ILogger<AuthController> _logger;
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("register-administrator")]
    [ProducesResponseType<ActionResult>(400)]
    [ProducesResponseType<ActionResult>(200)]
    public async Task<ActionResult> Register ([FromBody] AdministratorRegisterVM request)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);
    
        var result = await _authService.RegisterAdministrator(request);
        
        if (result.Success) return CustomResponse(result);
        
        foreach (var error in result.Errors)
        {
            AddErrorProcess(error);
        }
            
        return CustomResponse();
    }
    
    [HttpPost("login-administrator")]
    [ProducesResponseType<ActionResult>(400)]
    [ProducesResponseType<ActionResult>(200)]
    public async Task<ActionResult> Login([FromBody] AdministratorLoginVM request)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);
        
        var result = await _authService.LoginAdministrator(request);

        if (result.Success) return CustomResponse(result);
        
        foreach (var error in result.Errors)
        {
            AddErrorProcess(error);
        }
            
        return CustomResponse();

    }
    
    [HttpPost("login-doorman")]
    [ProducesResponseType<ActionResult>(400)]
    [ProducesResponseType<ActionResult>(200)]
    public async Task<ActionResult> Login([FromBody] DoormanLoginVM request)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);
        
        var result = await _authService.LoginDoorman(request);

        if (result.Success) return CustomResponse(result);
        
        foreach (var error in result.Errors)
        {
            AddErrorProcess(error);
        }
            
        return CustomResponse();
    }
   }