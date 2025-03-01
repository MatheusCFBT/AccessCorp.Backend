using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnFunction.Application.Entities;
using OnFunction.WebApi.Extensions;

namespace OnFunction.WebApi.Controllers;

[Route("identity")]
public class AuthController : MainController
{
    private readonly ILogger<AuthController> _logger;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly AppSettings _appSettings;
    
    public AuthController(SignInManager<IdentityUser> signInManager, 
                          UserManager<IdentityUser> userManager,
                          IOptions<AppSettings> appSettings)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _appSettings = appSettings.Value;
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
            await _signInManager.SignInAsync(user, false);
            return CustomResponse(await GerarJwt(request.Email));
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
        
        var result = await _signInManager.PasswordSignInAsync(request.Email, request.Senha, false, true);

        if (result.Succeeded)
        {
            return CustomResponse(await GerarJwt(request.Email));
        }

        if (result.IsLockedOut)
        {
            AddErrorProcess("Usuário temporariamente bloqueados por tentativas inválidas.");
            return CustomResponse();
        }
        
        AddErrorProcess("Usuário ou Senha incorretos");
        return CustomResponse(); 
    }

    private async Task<AdministratorResponseVM> GerarJwt(string email)
    {
         var user = await _userManager.FindByEmailAsync(email);
         var claims = await _userManager.GetClaimsAsync(user);
         var roles = await _userManager.GetRolesAsync(user);
        
         // Claims para o JWT
         claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
         claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
         claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
         claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
         claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

         foreach (var userRole in roles)
         {
             claims.Add(new Claim(ClaimTypes.Role, userRole));
         }
         
         var identityClaims = new ClaimsIdentity();
         identityClaims.AddClaims(claims);
         
         var tokenHandler = new JwtSecurityTokenHandler();
         var tokenKey = Encoding.ASCII.GetBytes(_appSettings.Secret);

         var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
         {
             Issuer = _appSettings.Emissor,
             Audience = _appSettings.ValidoEm,
             Subject = identityClaims,
             Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
             SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                 SecurityAlgorithms.HmacSha256Signature)
         }); 
         
         var encodedToken = tokenHandler.WriteToken(token);

         var response = new AdministratorResponseVM
         {
             AccessToken = encodedToken,
             ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
             AdministratorToken = new AdministratorToken
             {
                 Id = user.Id,
                 Email = user.Email,
                 Claims = claims.Select(c => new AdministratorClaim { Type = c.Type, Value = c.Value })
             }
         };
         
         return response;
    }
    
    private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
}