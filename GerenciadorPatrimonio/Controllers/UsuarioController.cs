using GerenciadorPatrimonio.Aplications.Services;
using GerenciadorPatrimonio.DTOs.UsuarioDTO;
using GerenciadorPatrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;
        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<ListarUsuarioDTO> Listar()
        {
            List<ListarUsuarioDTO> lista = _service.Listar();
            return Ok(lista);
        }
        [HttpGet("/{id}")]
        public ActionResult<ListarUsuarioDTO> BuscarPorID(Guid id)
        {
            try
            {
                ListarUsuarioDTO usuario = _service.BuscarPorID(id);
                return Ok(usuario);
            }catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<CriarUsuarioDTO> Adicionar(CriarUsuarioDTO dto)
        {
            try
            {
                _service.Adicionar(dto);
                return StatusCode(201, dto);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
