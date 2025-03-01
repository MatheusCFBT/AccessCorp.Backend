using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnFunction.Application.Entities;

namespace OnFunction.WebApi.Controllers;

[ApiController]
[Route("identity")]
public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    
    [HttpPost("register-administrator")]
    public async Task<ActionResult> Register (AdministratorRegisterVM request)
    {
        if (!ModelState.IsValid) return BadRequest(request);

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
            return Ok();
        }
        
        return BadRequest(request);    
    }
    
    [HttpPost("login-administrator")]
    public async Task<ActionResult> Login(AdministratorLoginVM request)
    {
        if (!ModelState.IsValid) return BadRequest(request);
        
        var result = await _signInManager.PasswordSignInAsync(request.Email, request.Senha, false, true);

        if (result.Succeeded)
        {
            return Ok(request);
        }
        
        return BadRequest(request); 
    }
}