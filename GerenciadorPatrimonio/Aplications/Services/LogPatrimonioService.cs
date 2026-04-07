using GerenciadorPatrimonio.Domains;
using GerenciadorPatrimonio.DTOs.LogPatrimonioDTO;
using GerenciadorPatrimonio.Exceptions;
using GerenciadorPatrimonio.Interfaces;

namespace GerenciadorPatrimonio.Aplications.Services
{
    public class LogPatrimonioService
    {
        private readonly ILogPatrimonioRepository _repository;
        public LogPatrimonioService(ILogPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarLogPatrimonioDTO> Listar()
        {
            List<LogPatrimonio> logs = _repository.Listar();
            List<ListarLogPatrimonioDTO> logsDto = logs.Select(log => new ListarLogPatrimonioDTO
            {
                LogPatrimonioID = log.PatrimonioID,
                DataTransferencia = log.DataTransferencia,
                PatrimonioID = log.PatrimonioID,
                DenominacaoPatrimonio = log.Patrimonio.Denominacao,
                TipoAlteracao = log.TipoAlteracao.NomeTipo,
                StatusPatrimonio = log.StatusPatrimonio.NomeStatus,
                Usuario = log.Usuario.Nome,
                Localizacao = log.Localizacao.NomeLocal
            }).ToList();
            return logsDto;
        }
        public List<ListarLogPatrimonioDTO> BuscarPorPatrimonio(Guid patrimonioId)
        {
            List<LogPatrimonio> logs = _repository.BuscarPorPatrimonio(patrimonioId);
            if(logs == null)
            {
                throw new DomainException("Patrimonio não encontrado.");
            }
            List<ListarLogPatrimonioDTO> logsDto = logs.Select(log => new ListarLogPatrimonioDTO
            {
                LogPatrimonioID = log.PatrimonioID,
                DataTransferencia = log.DataTransferencia,
                PatrimonioID = log.PatrimonioID,
                DenominacaoPatrimonio = log.Patrimonio.Denominacao,
                TipoAlteracao = log.TipoAlteracao.NomeTipo,
                StatusPatrimonio = log.StatusPatrimonio.NomeStatus,
                Usuario = log.Usuario.Nome,
                Localizacao = log.Localizacao.NomeLocal
            }).ToList();
            return logsDto;
        }
    }
}
