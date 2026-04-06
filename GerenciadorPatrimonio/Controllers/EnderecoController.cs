using GerenciadorPatrimonio.Aplications.Services;
using GerenciadorPatrimonio.Domains;
using GerenciadorPatrimonio.DTOs.EnderecoDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly EnderecoService _service;
        public EnderecoController(EnderecoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarEnderecoDTO>> Listar()
        {
            List<ListarEnderecoDTO> listar = _service.Listar();
            return Ok(listar);
        }

        [HttpGet("/{id}")]
        public ActionResult<ListarEnderecoDTO> BuscarPorID(Guid id)
        {
            try
            {
                var buscar = _service.BuscarPorID(id);
                return Ok(buscar);
            }catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/LogradouroENumero")]
        public ActionResult<ListarEnderecoDTO> BuscarPorLogradouroENumero(string logradouro, int? numero, Guid bairroID)
        {
            try
            {
                var buscar = _service.BuscarPorLogradouroENumero(logradouro, numero, bairroID);
                return Ok(buscar);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("/Adicionar")]
        public ActionResult<CriarEnderecoDTO> Adicionar(CriarEnderecoDTO dto)
        {
            try
            {
                _service.Adicionar(dto);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("/Atualizar")]
        public ActionResult<CriarEnderecoDTO> Atualizar(Guid id, CriarEnderecoDTO dto)
        {
            try
            {
                _service.Atualizar(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}
