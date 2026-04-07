using GerenciadorPatrimonio.Aplications.Services;
using GerenciadorPatrimonio.DTOs.AutenticacaoDTO;
using GerenciadorPatrimonio.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GerenciadorPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly AutenticacaoService _service;
        public AutenticacaoController(AutenticacaoService service)
        {
            _service = service;
        }
        [HttpPost("login")]
        public ActionResult<TokenDTO> Login(LoginDTO loginDTO)
        {
            try
            {
                TokenDTO token = _service.Login(loginDTO);
                return Ok(token);
            }catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPatch("trocar-senha")]
        public ActionResult TrocarPrimeiraSenha(TrocarPrimeiraSenhaDTO dto)
        {
            try
            {
                string usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrWhiteSpace(usuarioIdClaim))
                {
                    return Unauthorized("Usuário não autorizado.");
                }
                //convertendo string para guid 
                Guid usuarioID = Guid.Parse(usuarioIdClaim);
                _service.TrocarPrimeiraSenha(usuarioID, dto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
