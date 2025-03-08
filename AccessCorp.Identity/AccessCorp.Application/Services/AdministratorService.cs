using AccessCorp.Application.Entities;
using AccessCorp.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AccessCorp.Application.Services;

public class AdministratorService : IAdministratorService
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    
    public AdministratorService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public async Task<Result> EditAdministrator(AdministratorUpdateVM request)
    {
        //TODO fazer validação
        var user = await _userManager.FindByEmailAsync(request.Email);
        
        if (user != null) return Result.Fail("usuário não encontrado");
        
        user.Email = request.Email;
        user.UserName = request.UserName;
        await _userManager.RemovePasswordAsync(user);
        await _userManager.AddPasswordAsync(user, request.Password);

        var result = _userManager.UpdateAsync(user);
        
        return result.IsCompletedSuccessfully ? Result.Ok("Usuário alterado com sucesso") : Result.Fail("erro ao alterar");
    }
}