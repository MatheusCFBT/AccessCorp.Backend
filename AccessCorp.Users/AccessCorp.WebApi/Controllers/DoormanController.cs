using AccessCorpUsers.Application.Entities;
using AccessCorpUsers.Application.Interfaces;
using AccessCorpUsers.WebApi.Extensions;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace AccessCorpUsers.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [ClaimsAuthorize("Permission", "LimitedAccess"), Route("users/v1/doorman")]
    public class DoormanController : MainController
    {
        private readonly IDoormanService _doormanService;
        public DoormanController(IDoormanService doormanService)
        {
            _doormanService = doormanService;
        }

        [HttpGet("view-all")]
        public async Task<ActionResult<List<DoormanVM>>> ViewAllDoorman()
        {
            var result = await _doormanService.ViewAllDoorman();

            if (result == null)
                return CustomResponse();

            return CustomResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoormanVM>> ViewDoormanById(Guid id)
        {
            var result = await _doormanService.ViewDoormanById(id);

            if (result == null)
                return CustomResponse();

            return CustomResponse(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterDoorman(DoormanVM request)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var result = await _doormanService.RegisterDoorman(request);

            return CustomResponse(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDoorman(Guid id,DoormanVM request)
        {
            if (id != request.Id)
                return CustomResponse();

            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var result = await _doormanService.UpdateDoorman(id, request);

            return CustomResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDoorman(Guid id)
        {
            var result = await _doormanService.ExcludeDoorman(id);

            if (result == null)
                return CustomResponse();

            return CustomResponse(result);
        }

       
    }
}
