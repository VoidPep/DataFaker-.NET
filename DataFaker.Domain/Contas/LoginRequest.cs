namespace DataFaker.Domain.Contas;

public sealed record LoginRequest
{
    public string Email { get; set; }
    public string Senha { get; set; }
}