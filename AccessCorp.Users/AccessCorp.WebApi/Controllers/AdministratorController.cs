using AccessCorpUsers.Application.Entities;
using AccessCorpUsers.Application.Interfaces;
using AccessCorpUsers.Application.Services;
using AccessCorpUsers.WebApi.Extensions;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccessCorpUsers.WebApi.Controllers;

[ApiVersion("1.0")]
[ClaimsAuthorize("Permission", "FullAccess"), Route("users/v1/administrator")]
public class AdministratorController : MainController
{
    private readonly IResidentService _residentService;
    private readonly IAdministratorService _administratorService;

    public AdministratorController(IAdministratorService administratorService, IResidentService residentService)
    {
        _administratorService = administratorService;
        _residentService = residentService;
    }

    //TODO
    // Criar para pegar todos os porteiros e residentes com o cep igual ao do Adm que está logado
    [HttpGet("view-all-admin")]
    public async Task<ActionResult<List<AdministratorVM>>> ViewAllAdministrators()
    {
        var userId = GetUserId(HttpContext.User);
        var result = await _administratorService.ViewAllAdministrators(userId.email);

        if (result == null)
            return CustomResponse();

        return CustomResponse(result);
    }

    //TODO
    // Criar para pegar os porteiros ou residentes
    [HttpGet("{id}")]
    public async Task<ActionResult<AdministratorVM>> ViewAdministratorById(Guid id)
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

        if (result.Success) return CustomResponse(result);

        foreach (var error in result.Errors)
        {
            AddErrorProcess(error);
        }

        return CustomResponse();
    }

    [HttpPut("update")]
    public async Task<ActionResult> UpdateAdministrator([FromQuery] string email, AdministratorVM request)
    {
        //if (id != request.Id)
        //    return CustomResponse();

        if (!ModelState.IsValid)
            return CustomResponse(ModelState);

        var result = await _administratorService.UpdateAdministrator(email, request);

        if (result.Success) return CustomResponse(result);

        foreach (var error in result.Errors)
        {
            AddErrorProcess(error);
        }

        return CustomResponse();
    }

    [HttpDelete("{email}")]
    public async Task<ActionResult> ExcludeAdministrator(string email)
    {
        var result = await _administratorService.ExcludeAdministrator(email);

        if (result.Success) return CustomResponse(result);

        foreach (var error in result.Errors)
        {
            AddErrorProcess(error);
        }

        return CustomResponse();
    }

    [HttpGet("view-all-residents")]
    public async Task<ActionResult<List<ResidentVM>>> ViewAllResidents()
    {
        var userId = GetUserId(HttpContext.User);
        var result = await _residentService.ViewAllResidents(userId.email);

        if (result == null)
            return CustomResponse();

        return CustomResponse(result);
    }

    [HttpGet("view-resident/{id}")]
    public async Task<ActionResult<ResidentVM>> ViewResidentById(Guid id)
    {
        var result = await _residentService.ViewResidentById(id);

        if (result == null)
            return CustomResponse();

        return CustomResponse(result);
    }

    [HttpPost("register-resident")]
    public async Task<ActionResult<ResidentVM>> RegisterResident(ResidentVM request)
    {
        if (!ModelState.IsValid)
            return CustomResponse(ModelState);

        var result = await _residentService.RegisterResident(request);

        if (result.Success) return CustomResponse(result);

        foreach (var error in result.Errors)
        {
            AddErrorProcess(error);
        }

        return CustomResponse();
    }

    [HttpPut("update-resident/{email}")]
    public async Task<ActionResult<List<ResidentVM>>> UpdateResident(string email, ResidentVM request)
    {
        if (!ModelState.IsValid)
            return CustomResponse(ModelState);

        var result = await _residentService.UpdateResident(email, request);

        if (result.Success) return CustomResponse(result);

        foreach (var error in result.Errors)
        {
            AddErrorProcess(error);
        }

        return CustomResponse();
    }

    [HttpDelete("exclude-resident/{email}")]
    public async Task<ActionResult<List<ResidentVM>>> ExcludeResidents(string email)
    {
        var result = await _residentService.ExcludeResident(email);

        if (result.Success) return CustomResponse(result);

        foreach (var error in result.Errors)
        {
            AddErrorProcess(error);
        }

        return CustomResponse();
    }

    [HttpGet("view-all")]
    public async Task<ActionResult> ViewAllUsersByAdmin()
    {
        var userId = GetUserId(HttpContext.User);

        if (userId.email != null && userId.userId != null)
        {
            var result = await _administratorService.GetAdminDoormansResidents(userId.email);

            return CustomResponse(result);
        }

        return CustomResponse(ModelState);
    }
}