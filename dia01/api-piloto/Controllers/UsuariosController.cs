using Microsoft.AspNetCore.Mvc;
using api_piloto.DTOs;
using System.Security.Cryptography.X509Certificates;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    [HttpPost]
    public IActionResult CriarUsuario([FromBody] UsuarioCreateDto usuarioDto)
    {
        return Ok("Usuário válido!");
    }
}