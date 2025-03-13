using AccessCorpUsers.Application.Entities;
using AccessCorpUsers.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AccessCorpUsers.WebApi.Controllers;

[ApiController, Route("users/v1/administrator")]
public class AdministratorController : MainController
{
    private readonly IAdministratorService _administratorService;

    public AdministratorController(IAdministratorService administratorService)
    {
        _administratorService = administratorService;
    }

    [HttpGet("view-all")]
    public async Task<ActionResult> ViewAllAdministrators()
    {
        var result = await _administratorService.ViewAllAdministrators();

        return CustomResponse(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> ViewAdministratorById(Guid id)
    {
        var result = await _administratorService.ViewAdministratorById(id);

        if (result == null)
            return CustomResponse();

        return CustomResponse(result);
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterAdministrator(AdministratorVM request)
    {
        if (!ModelState.IsValid) 
            return CustomResponse(ModelState);

        var result = await _administratorService.RegisterAdministrator(request);

        return CustomResponse(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAdministrator(Guid id, AdministratorVM request)
    {
        if (id != request.Id)
            return CustomResponse();

        if (!ModelState.IsValid)
            return CustomResponse(ModelState);
        
        var result = await _administratorService.UpdateAdministrator(id, request);

        return CustomResponse(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> ExcludeAdministrator(Guid id)
    {
        var result = await _administratorService.ExcludeAdministrator(id);

        if (result == null)
            return CustomResponse();

        return CustomResponse(result);
    }
}