using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnFunction.Application.Entities;
using OnFunction.Application.Interfaces;

namespace OnFunction.WebApi.Controllers;

[Route("identity")]
public class AuthController : MainController
{
    private readonly ILogger<AuthController> _logger;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IAuthService _authService;
    private readonly IUserClaimsService _userClaimsService;
    
    public AuthController(SignInManager<IdentityUser> signInManager, 
                          UserManager<IdentityUser> userManager,
                          IAuthService authService,
                          IUserClaimsService userClaimsService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _authService = authService;
        _userClaimsService = userClaimsService;
    }
    
    [HttpPost("register-administrator")]
    public async Task<ActionResult> Register (AdministratorRegisterVM request)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var user = new IdentityUser
        {
            UserName = request.Email,
            Email = request.Email,
            EmailConfirmed = true
        };
        
        var result = await _userManager.CreateAsync(user, request.Senha);

        if (result.Succeeded)
        {
            await _userClaimsService.AddPermissionClaimAsync(user, "FullAccess");
            await _signInManager.SignInAsync(user, false);
            return CustomResponse(await _authService.GenerateJWTAdmin(user.Email));
        }

        foreach (var error in result.Errors)
        {
            AddErrorProcess(error.Description);
        }
        
        return CustomResponse();    
    }
    
    [HttpPost("login-administrator")]
    public async Task<ActionResult> Login(AdministratorLoginVM request)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        if (await _userClaimsService.HasAdmimClaims(request.Email))
        {
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Senha, false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await _authService.GenerateJWTAdmin(request.Email));
            }

            if (result.IsLockedOut)
            {
                AddErrorProcess("Usuário temporariamente bloqueados por tentativas inválidas.");
                return CustomResponse();
            }
        }
        AddErrorProcess("Usuário ou Senha incorretos");
        return CustomResponse(); 
    }
    
    [HttpPost("register-doorman")]
    public async Task<ActionResult> Register (DoormanRegisterVM request)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var user = new IdentityUser
        {
            UserName = request.Email,
            Email = request.Email,
            EmailConfirmed = true
        };
        
        var result = await _userManager.CreateAsync(user, request.Senha);

        if (result.Succeeded)
        {
            await _userClaimsService.AddPermissionClaimAsync(user, "LimitedAccess");
            await _signInManager.SignInAsync(user, false);
            return CustomResponse(await _authService.GenerateJWTDoorman(request.Email));
        }

        foreach (var error in result.Errors)
        {
            AddErrorProcess(error.Description);
        }
        
        return CustomResponse();    
    }
    
    [HttpPost("login-doorman")]
    public async Task<ActionResult> Login(DoormanLoginVM request)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);
        
        if(await _userClaimsService.HasDoormanClaims(request.Email))
        {
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Senha, false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await _authService.GenerateJWTDoorman(request.Email));
            }

            if (result.IsLockedOut)
            {
                AddErrorProcess("Usuário temporariamente bloqueados por tentativas inválidas.");
                return CustomResponse();
            }
        }
        
        AddErrorProcess("Usuário ou Senha incorretos");
        return CustomResponse(); 
    }
   }