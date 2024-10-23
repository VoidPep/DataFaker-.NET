using DataFaker.Domain.Contas;
using DataFaker.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataFaker.Web.Api;

public class ContasController : BaseApiController
{
    private readonly ISessionService _sessionService;

    public ContasController(ISessionService sessionService) => _sessionService = sessionService;

    [AllowAnonymous, HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequest model)
    {
        if (string.IsNullOrWhiteSpace(model.Senha) || string.IsNullOrWhiteSpace(model.Email))
            return Error("Login e a senha devem estar preenchidos");

        //var usuario = _context.Usuarios
        //    .Include(q => q.Colaboradores)
        //    .ThenInclude(q => q.Instituicao)
        //    .FirstOrDefault(q => q.Email == model.Email);

        //if (usuario == null || !PasswordHelper.VerifyHashedPassword(usuario.Senha, model.Senha))
        //    return Error("Login ou senha incorretos");

        var usuarioDaSessao = new SessionUser
        {
            Nome = "",
            Email = model.Email,
        };

        await _sessionService.LoadIdentity(HttpContext, usuarioDaSessao);

        return Success();
    }

    //[HttpPost, AllowAnonymous, Route("AutoCadastro")]
    //public async Task<IActionResult> AutoCadastro([FromBody] UsuarioRequest model)
    //{
    //    model.AdministradorDoSistema = false;

    //    if (_context.Usuarios.Any(q => q.Email == model.Email))
    //        return Error("E-mail já cadastrado");

    //    var usuario = salvador.Salvar(model);

    //    var usuarioDaSessao = new UsuarioDaSessao
    //    {
    //        Id = usuario.Id,
    //        Nome = usuario.Nome,
    //        Email = usuario.Email,
    //        AdministradorDoSistema = false,
    //    };

    //    await _sessionService.LoadIdentity(HttpContext, usuarioDaSessao);

    //    return Success();
    //}
}