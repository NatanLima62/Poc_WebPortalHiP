using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Poc_WebPortalHiP.Api.Application.Contracts;
using Poc_WebPortalHiP.Api.Application.DTOs.Usuario;
using Poc_WebPortalHiP.Api.Application.Notifications;
using Poc_WebPortalHiP.Api.Domain.Contracts.Repositories;
using Poc_WebPortalHiP.Api.Domain.Entities;

namespace Poc_WebPortalHiP.Api.Application.Services;

public class UsuarioService : BaseServices, IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordHasher<Usuario> _passwordHasher;

    public UsuarioService(IMapper mapper, INotificator notificator, IUsuarioRepository usuarioRepository,
        IPasswordHasher<Usuario> passwordHasher) : base(mapper, notificator)
    {
        _usuarioRepository = usuarioRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UsuarioDto?> Adicionar(AdicionarUsuarioDto usuarioDto)
    {
        var usuario = Mapper.Map<Usuario>(usuarioDto);
        if (!await Validar(usuario))
        {
            return null;
        }

        usuario.Senha = _passwordHasher.HashPassword(usuario, usuario.Senha);
        _usuarioRepository.Cadastrar(usuario);
        if (await _usuarioRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<UsuarioDto>(usuario);
        }

        Notificator.Handle("Não foi possível cadastrar o usuário");
        return null;
    }

    public async Task<UsuarioDto?> ObterPorId(int id)
    {
        var usuario = await _usuarioRepository.ObterPorId(id);
        if (usuario != null)
            return Mapper.Map<UsuarioDto>(usuario);

        Notificator.HandleNotFoundResourse();
        return null;
    }

    public async Task<List<UsuarioDto>?> ObterTodos()
    {
        var usuarios = await _usuarioRepository.ObterTodos();
        if (usuarios != null)
            return Mapper.Map<List<UsuarioDto>>(usuarios);

        Notificator.Handle("Não existe usuário cadastrado");
        return null;
    }

    private async Task<bool> Validar(Usuario usuario)
    {
        if (!usuario.Validar(out var validationResult))
        {
            Notificator.Handle(validationResult.Errors);
        }

        var usuarioExistente = await _usuarioRepository.FirstOrDefault(c =>
            c.Id != usuario.Id && (c.Email == usuario.Email));
        if (usuarioExistente != null)
        {
            Notificator.Handle("Já existe um usuário cadastrado com essas idenficações");
        }

        return !Notificator.HasNotification;
    }
}