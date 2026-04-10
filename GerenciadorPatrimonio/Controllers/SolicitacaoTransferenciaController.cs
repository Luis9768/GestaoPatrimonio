using GerenciadorPatrimonio.Aplications.Services;
using GerenciadorPatrimonio.DTOs.SolicitacaoTransferenciaDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitacaoTransferenciaController : ControllerBase
    {
        private readonly SolicitacaoTransferenciaService _service;
        public SolicitacaoTransferenciaController(SolicitacaoTransferenciaService service)
        {
            _service = service;
        }
        [Authorize]
        [HttpGet]
        public ActionResult<List<ListarSolicitacaoTransferenciaDTO>> Listar()
        {
            List<ListarSolicitacaoTransferenciaDTO> s = _service.Listar();
            return Ok(s);
        }
        [Authorize]
        [HttpGet("{id}")]


    }
}
