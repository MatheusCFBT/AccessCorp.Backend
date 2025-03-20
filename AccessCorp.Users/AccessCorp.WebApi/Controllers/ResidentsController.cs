using AccessCorpUsers.Application.Entities;
using AccessCorpUsers.Application.Interfaces;
using AccessCorpUsers.WebApi.Extensions;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace AccessCorpUsers.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [ClaimsAuthorize("Permission", "FullAccess"), Route("users/v1/residents")]
    public class ResidentsController : MainController
    {
        private readonly IResidentService _residentService;

        public ResidentsController(IResidentService residentService)
        {
            _residentService = residentService;
        }

        [HttpGet("view-all")]
        public async Task<ActionResult<List<ResidentVM>>> GetAllResidents()
        {
            var userId = GetUserId(HttpContext.User);
            var result = await _residentService.ViewAllResidents(userId.email);

            if (result == null)
                return CustomResponse();

            return CustomResponse(result);
        }

        [HttpGet("view/{id}")]
        public async Task<ActionResult<ResidentVM>> GetResidentById(Guid id)
        {
            var result = await _residentService.ViewResidentById(id);

            if (result == null)
                return CustomResponse();

            return CustomResponse(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResidentVM>> PostResident([FromBody] ResidentVM request)
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

        [HttpPut("update/{email}")]
        public async Task<ActionResult<ResidentVM>> PutResident(string email, [FromBody] ResidentVM request)
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

        [HttpDelete("exclude/{email}")]
        public async Task<ActionResult<ResidentVM>> DeleteResident(string email)
        {
            var result = await _residentService.ExcludeResident(email);

            if (result.Success) return CustomResponse(result);

            foreach (var error in result.Errors)
            {
                AddErrorProcess(error);
            }

            return CustomResponse();
        }
    }
}
