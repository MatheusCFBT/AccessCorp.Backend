namespace AccessCorp.Domain.Models;

public class Administrator
{
    public int Id { get; set; }
    public string NomeCompleto { get; set; }
    public string Cep { get; set; }
    public Guid IdentityId { get; set; }
}