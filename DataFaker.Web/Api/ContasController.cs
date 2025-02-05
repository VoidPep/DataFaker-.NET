﻿using DataFaker.Context;
using DataFaker.Domain.Contas;
using DataFaker.Domain.Usuarios;
using DataFaker.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using DataFaker.Domain.Helpers;

namespace DataFaker.Web.Api;

public class ContasController : BaseApiController
{
    private readonly ISessionService _sessionService;
    private readonly IDataFakerContext _context;

    public ContasController(IDataFakerContext context, ISessionService sessionService)
    {
        _sessionService = sessionService;
        _context = context;
    }

    [AllowAnonymous, HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        if (string.IsNullOrWhiteSpace(model.Senha) || string.IsNullOrWhiteSpace(model.Email))
            return Error("Login e a senha devem estar preenchidos");

        var usuario = _context.Usuarios
            .FirstOrDefault(q => q.Email == model.Email);

        if (usuario == null || !PasswordHelper.VerifyPassword(usuario.Senha, model.Senha))
            return Error("Login ou senha incorretos");

        var usuarioDaSessao = new SessionUser
        {
            Nome = usuario.Nome,
            Email = model.Email,
        };

        await _sessionService.LoadIdentity(HttpContext, usuarioDaSessao);

        return Ok(new
        {
            RedirectUrl = Url.Action("Index", "Home")
        });
    }

    [HttpPost, AllowAnonymous, Route("AutoCadastro")]
    public async Task<IActionResult> AutoCadastro([FromBody] UsuarioRequest model)
    {
        if (_context.Usuarios.Any(q => q.Email == model.Email))
            return Error("E-mail já cadastrado");

        var usuario = new Usuario
        {
            Nome = model.Nome,
            Email = model.Email,
        };

        usuario.Senha = PasswordHelper.HashPassword(model.Senha);

        _context.Usuarios.Add(usuario);
        _context.SaveChanges();

        var usuarioDaSessao = new SessionUser
        {
            Nome = usuario.Nome,
            Email = usuario.Email,
        };

        await _sessionService.LoadIdentity(HttpContext, usuarioDaSessao);

        return Success(new
        {
            RedirectUrl = Url.Action("Index", "Home")
        });
    }
}