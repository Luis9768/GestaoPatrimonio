using GerenciadorPatrimonio.Aplications.Services;
using GerenciadorPatrimonio.DTOs.AreaDTO;
using GerenciadorPatrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly AreaService _service;
        public AreaController(AreaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarAreaDTO>> Listar()
        {
            List<ListarAreaDTO> areas = _service.Listar();
            return Ok(areas);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarAreaDTO> BuscarPorId(Guid id)
        {
            try
            {
                ListarAreaDTO area = _service.BuscarPorID(id);
                return Ok(area);
            }
            catch(DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult Adicionar(CriarAreaDTO dto)
        {
            try
            {
                _service.Adicionar(dto);
                return StatusCode(201);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public ActionResult Atualizar(Guid id, CriarAreaDTO dto)
        {
            try
            {
                _service.Atualizar(id,dto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    
    }
}
