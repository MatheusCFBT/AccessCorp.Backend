using AccessCorp.Application.Entities;
using AccessCorp.Application.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccessCorp.WebApi.Controllers;

[ApiVersion("1.0")]
[Authorize("AdminPolicy"), Route("identity/v1/doorman")]
public class DoormanController : MainController
{
    private readonly IDoormanService _doormanService;
    
    public DoormanController(IDoormanService doormanService)
    {
        _doormanService = doormanService;
    }

    [HttpGet("view-doorman")]
    [ProducesResponseType<ActionResult>(400)]
    [ProducesResponseType<ActionResult>(401)]
    [ProducesResponseType<ActionResult>(200)]
    public async Task<ActionResult> View ([FromQuery] string id)
    {
        var result = await _doormanService.ViewDoorman(id);
        
        if (result.Success) return CustomResponse(result);
        
        foreach (var error in result.Errors)
        {
            AddErrorProcess(error);
        }
           
        return CustomResponse();
    }
    
    [HttpPost("register-doorman")]
    [ProducesResponseType<ActionResult>(400)]
    [ProducesResponseType<ActionResult>(401)]
    [ProducesResponseType<ActionResult>(200)]
    public async Task<ActionResult> Register ([FromBody] DoormanRegisterVM request)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);
    
        var result = await _doormanService.RegisterDoorman(request);
        
        if (result.Success) return CustomResponse(result);
        
        foreach (var error in result.Errors)
        {
            AddErrorProcess(error);
        }
            
        return CustomResponse();
    }
    
    [HttpPut("update-doorman")]
    [ProducesResponseType<ActionResult>(400)]
    [ProducesResponseType<ActionResult>(401)]
    [ProducesResponseType<ActionResult>(200)]
    public async Task<ActionResult> Update ([FromQuery] string email ,[FromBody] DoormanUpdateVM request)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);
    
        var result = await _doormanService.EditDoorman(email, request);
        
        if (result.Success) return CustomResponse(result);
        
        foreach (var error in result.Errors)
        {
            AddErrorProcess(error);
        }
            
        return CustomResponse();
    }
    
    [HttpDelete("exclude-doorman")]
    [ProducesResponseType<ActionResult>(400)]
    [ProducesResponseType<ActionResult>(401)]
    [ProducesResponseType<ActionResult>(200)]
    public async Task<ActionResult> Exclude ([FromQuery] string email)
    {
        var result = await _doormanService.ExcludeDoorman(email);
        
        if (result.Success) return CustomResponse(result);
        
        foreach (var error in result.Errors)
        {
            AddErrorProcess(error);
        }
            
        return CustomResponse();
    }

}