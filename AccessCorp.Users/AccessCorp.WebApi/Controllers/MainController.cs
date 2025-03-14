using AccessCorpUsers.Application.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Security.Claims;

namespace AccessCorpUsers.WebApi.Controllers;

[ApiController]
public abstract class MainController : Controller
{
    protected ICollection<string> Errors = new List<string>();
    protected ActionResult CustomResponse(object result = null)
    {
        if (ValidOperation()) return Ok(result);
        
        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
        {
            { "Messages", Errors.ToArray() }
        }));
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(e => e.Errors);
        foreach (var error in errors)
        {
            AddErrorProcess(error.ErrorMessage);
        }
        
        return CustomResponse();
    }

    protected bool ValidOperation()
    {
        return !Errors.Any();
    }

    protected void AddErrorProcess(string error)
    {
        Errors.Add(error);
    }

    protected void ClearErrorProcess()
    {
        Errors.Clear();
    }
    protected static (Guid userId, string email) GetUserId(ClaimsPrincipal principal)
    {
        AdministratorIdentityRequest userLogin = JsonConvert.DeserializeObject<AdministratorIdentityRequest>(principal?.FindFirst("data").Value);
        return (userId: userLogin.UserId, email: userLogin.Email);
    }
}