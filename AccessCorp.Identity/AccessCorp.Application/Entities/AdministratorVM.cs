using System.ComponentModel.DataAnnotations;

namespace OnFunction.Application.Entities;

public class AdministratorRegisterVM
{
    [Required(ErrorMessage = "O campo {0} é obrigatório ")]
    [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório ")]
    [StringLength(8, MinimumLength = 8, ErrorMessage = "O campo {0} precisa ter {1} digitos")]
    public string Cep { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório ")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
    public string Senha { get; set; }
    
    [Compare("Senha", ErrorMessage = "As senhas não conferem")]
    public string SenhaConfirmacao { get; set; }
}

public class AdministratorLoginVM
{
    [Required(ErrorMessage = "O campo {0} é obrigatório ")]
    [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório ")]
    [StringLength(8, MinimumLength = 8, ErrorMessage = "O campo {0} precisa ter {1} digitos")]
    public string Cep { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório ")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
    public string Senha { get; set; } 
}