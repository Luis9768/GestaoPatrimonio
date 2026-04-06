using GerenciadorPatrimonio.Aplications.Services;
using GerenciadorPatrimonio.DTOs.CargoDTO;
using GerenciadorPatrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        private readonly CargoService _service;

        public CargoController(CargoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarCargoDTO>> Listar()
        {
            var listar = _service.Listar();
            return Ok(listar);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarCargoDTO> BuscarPorId(Guid id)
        {
            var busca = _service.BuscarPorId(id);
            return Ok(busca);
        }

        [HttpGet("nome")]
        public ActionResult<ListarCargoDTO> BuscarPorNome([FromQuery] string nome)
        {
            var busca = _service.BuscarPorNome(nome);
            return Ok(busca);
        }

        [HttpPost]
        public ActionResult Adicionar(CriarCargoDTO dto)
        {
            try
            {
                _service.Adicionar(dto);
                return Ok(); // ou Created se quiser melhorar depois
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(Guid id, CriarCargoDTO dto)
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

