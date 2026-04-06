using GerenciadorPatrimonio.Aplications.Services;
using GerenciadorPatrimonio.DTOs.CidadeDTO;
using GerenciadorPatrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private readonly CidadeService _service;

        public CidadeController(CidadeService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarCidadeDTO>> Listar()
        {
            List<ListarCidadeDTO> cidades = _service.Listar();
            return Ok(cidades);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarCidadeDTO> BuscarPorId(Guid id)
        {
            try
            {
                ListarCidadeDTO cidade = _service.BuscarPorId(id);
                return Ok(cidade);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarCidadeDTO dto)
        {
            try
            {
                _service.Adicionar(dto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(Guid id, CriarCidadeDTO dto)
        {
            try
            {
                _service.Atualizar(id, dto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
