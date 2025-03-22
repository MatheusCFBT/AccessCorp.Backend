﻿using System.ComponentModel.DataAnnotations;

namespace AccessCorp.Application.Entities;

public class DoormanRegisterVM
{
    [Key]
    public string Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório ")]
    [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório ")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
    public string Password { get; set; }
    
    [Compare("Password", ErrorMessage = "As senhas não conferem")]
    public string PasswordConfirmed { get; set; }
}

public class DoormanLoginVM
{
    [Required(ErrorMessage = "O campo {0} é obrigatório ")]
    [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório ")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
    public string Senha { get; set; } 
}
public class DoormanUpdateVM
{
    [Required(ErrorMessage = "O campo {0} é obrigatório ")]
    [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório ")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
    public string Password { get; set; }
}