using Microsoft.AspNetCore.Identity;

namespace AccessCorp.Application.Interfaces;

public interface IUserClaimsService
{
    public Task AddPermissionClaimAsync(IdentityUser user, string permission);
    public Task<bool> HasAdmimClaims(string email);
    public Task<bool> HasDoormanClaims(string email);
}