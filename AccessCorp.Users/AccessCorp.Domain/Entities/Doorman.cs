﻿namespace AccessCorpUsers.Domain.Entities;

public class Doorman : Entity
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Cpf { get; set; }
    public string Cep { get; set; }
    public Guid IdentityId { get; set; }
}