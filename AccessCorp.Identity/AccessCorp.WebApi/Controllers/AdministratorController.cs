using AccessCorp.Application.Entities;
using AccessCorp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccessCorp.WebApi.Controllers;

[Authorize("AdminPolicy"), Route("identity/administrator")]
public class AdministratorController : MainController
{
    private readonly IAuthService _authService;
    private readonly IAdministratorService _administratorService;

    public AdministratorController(IAuthService authService, IAdministratorService administratorService)
    {
        _authService = authService;
        _administratorService = administratorService;
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
    
    [HttpPut("update-administrator")]
    [ProducesResponseType<ActionResult>(400)]
    [ProducesResponseType<ActionResult>(200)]
    public async Task<ActionResult> Update ([FromBody] AdministratorUpdateVM request)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);
    
        var result = await _administratorService.EditAdministrator(request);
        
        if (result.Success) return CustomResponse(result);
        
        foreach (var error in result.Errors)
        {
            AddErrorProcess(error);
        }
            
        return CustomResponse();
    }
}