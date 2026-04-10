using GerenciadorPatrimonio.Domains;
using GerenciadorPatrimonio.DTOs.SolicitacaoTransferenciaDTO;
using GerenciadorPatrimonio.Exceptions;
using GerenciadorPatrimonio.Interfaces;

namespace GerenciadorPatrimonio.Aplications.Services
{
    public class SolicitacaoTransferenciaService
    {
        private readonly ISolicitacaoTransferenciaRepository _repository;
        private readonly IUsuarioRepository _usuarioRepository;
        public SolicitacaoTransferenciaService(ISolicitacaoTransferenciaRepository repository,  IUsuarioRepository usuarioRepository)
        {
            _repository = repository;
            _usuarioRepository = usuarioRepository;
        }
        public List<ListarSolicitacaoTransferenciaDTO> Listar()
        {
            List<SolicitacaoTransferencia> s = _repository.Listar();
            List<ListarSolicitacaoTransferenciaDTO> dto = s.Select(ss => new ListarSolicitacaoTransferenciaDTO
            {
                TransferenciaID = ss.TransferenciaID,
                DataCriacaoSolicitante = ss.DataCriacaoSolicitante,
                DataResposta = ss.DataResposta,
                Justificativa = ss.Justificativa,
                StatusTransferenciaID = ss.StatusTransferenciaID,
                UsuarioIDSolicitacao = ss.UsuarioIDSolicitacao,
                UsuarioIDAprovacao = ss.UsuarioIDAprovacao,
                PatrimonioID = ss.PatrimonioID,
                LocalizacaoID = ss.LocalizacaoID
            }).ToList();
            return dto;
        }
        public ListarSolicitacaoTransferenciaDTO BuscarPorID(Guid id)
        {
            SolicitacaoTransferencia s = _repository.BuscarPorID(id);
            if(s == null)
            {
                throw new DomainException("Solicitação de transferencia não encontrada.");
            }
            ListarSolicitacaoTransferenciaDTO ss = new ListarSolicitacaoTransferenciaDTO
            {
                TransferenciaID = s.TransferenciaID,
                DataCriacaoSolicitante = s.DataCriacaoSolicitante,
                DataResposta = s.DataResposta,
                Justificativa = s.Justificativa,
                StatusTransferenciaID = s.StatusTransferenciaID,
                UsuarioIDSolicitacao = s.UsuarioIDSolicitacao,
                UsuarioIDAprovacao = s.UsuarioIDAprovacao,
                PatrimonioID = s.PatrimonioID,
                LocalizacaoID = s.LocalizacaoID
            };
            return ss;
        }
    }
}
