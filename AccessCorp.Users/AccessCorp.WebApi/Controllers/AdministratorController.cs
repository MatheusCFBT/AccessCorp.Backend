using AccessCorpUsers.Application.Entities;
using AccessCorpUsers.Application.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Security.Claims;

namespace AccessCorpUsers.WebApi.Controllers;

[ApiVersion("1.0")]
[Route("users/v1/administrator")]

public class AdministratorController : MainController
{
    private readonly IAdministratorService _administratorService;

    public AdministratorController(IAdministratorService administratorService)
    {
        _administratorService = administratorService;
    }

    //TODO
    // Criar para pegar todos os porteiros e residentes com o cep igual ao do Adm que está logado
    [HttpGet("view-all-admin")]
    public async Task<ActionResult<List<AdministratorVM>>> ViewAllAdministrators()
    {
        var userId = GetUserId(HttpContext.User);
        var result = await _administratorService.ViewAllAdministrators(userId.email);

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

    [HttpGet("view-all")]
    public async Task<ActionResult> ViewAllUsersByAdmin()
    {
        var userId = GetUserId(HttpContext.User);
        var result = await _administratorService.GetAdminDoormansResidents(userId.email);

        return CustomResponse(result);
    }
}