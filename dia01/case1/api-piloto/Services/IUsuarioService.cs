using api_piloto.DTOs;
namespace api_piloto.Services
{
    public interface IUsuarioService
    {
        bool CriarUsuario(UsuarioCreateDto usuarioDto);
    }
}