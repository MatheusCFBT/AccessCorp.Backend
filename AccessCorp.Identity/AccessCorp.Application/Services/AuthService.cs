using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AccessCorp.Application.Entities;
using AccessCorp.Application.Interfaces;
using AccessCorp.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace AccessCorp.Application.Services;

public class AuthService : IAuthService
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly AppSettings _appSettings;
    private readonly ICepValidationService _cepValidationService;
    private readonly IJwtService _jwtService;
    private readonly IUserClaimsService _userClaimsService;
    
    public AuthService(SignInManager<IdentityUser> signInManager, 
                       UserManager<IdentityUser> userManager,
                       IOptions<AppSettings> appSettings,
                       ICepValidationService cepValidationService,
                       IJwtService jwtService,
                       IUserClaimsService userClaimsService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _appSettings = appSettings.Value;
        _cepValidationService = cepValidationService;
        _jwtService = jwtService;
        _userClaimsService = userClaimsService;
    }

    public async Task<Result> LoginAdministrator(AdministratorLoginVM request)
    {
        if (!ValidateCep(request.Cep))
            return Result.Fail("Invalid Cep");

        if (await _userClaimsService.HasAdmimClaims(request.Email))
        {
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Senha, false, true);

            if (result.Succeeded)
                return Result.Ok(await _jwtService.GenerateJWTAdmin(request.Email));

            if (result.IsLockedOut)
                return Result.Fail("Usuário temporariamente bloqueados por tentativas inválidas.");
        }

        return Result.Fail("Usuário ou Senha incorretos");
    }

    public async Task<Result> RegisterAdministrator(AdministratorRegisterVM request)
    {
        if (!ValidateCep(request.Cep))
            return Result.Fail("Invalid Cep");
        
        var user = new IdentityUser
        {
            UserName = request.Email,
            Email = request.Email,
            EmailConfirmed = true
        };
        
        var result = await _userManager.CreateAsync(user, request.Senha);
    
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return Result.Fail(errors);
        }
        
        await _userClaimsService.AddPermissionClaimAsync(user, "FullAccess");
        await _signInManager.SignInAsync(user, false);
        
        return Result.Ok(await _jwtService.GenerateJWTAdmin(user.Email));
    }

    public async Task<Result> LoginDoorman(DoormanLoginVM request)
    {
        if (!ValidateCep(request.Cep))
            return Result.Fail("Invalid Cep");
        
        if(await _userClaimsService.HasDoormanClaims(request.Email))
        {
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Senha, false, true);
    
            if (result.Succeeded)
                return Result.Ok(await _jwtService.GenerateJWTDoorman(request.Email));
            
            if (result.IsLockedOut)
                return Result.Fail("Usuário temporariamente bloqueados por tentativas inválidas.");
        }
        
        return Result.Fail("Usuário ou Senha incorretos"); 
    }


    private bool ValidateCep(string cep)
    {
        if (!_cepValidationService.CepIsValid(cep)) return false;

        return true;
    }
}