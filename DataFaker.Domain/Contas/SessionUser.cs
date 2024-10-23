namespace DataFaker.Domain.Contas;

public interface ISessionUser
{
    public string Nome { get; set; }
    public string Email { get; set; }
}

public class SessionUser : ISessionUser
{
    public string Nome { get; set; }
    public string Email { get; set; }
}