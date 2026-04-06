using GerenciadorPatrimonio.Aplications.Regras;
using GerenciadorPatrimonio.Domains;
using GerenciadorPatrimonio.DTOs.CidadeDTO;
using GerenciadorPatrimonio.Exceptions;
using GerenciadorPatrimonio.Interfaces;

namespace GerenciadorPatrimonio.Aplications.Services
{
    public class CidadeService
    {
        private readonly ICidadeRepository _repository;

        public CidadeService(ICidadeRepository repository)
        {
            _repository = repository;
        }

        public List<ListarCidadeDTO> Listar()
        {
            List<Cidade> cidades = _repository.Listar();

            List<ListarCidadeDTO> cidadesDto = cidades.Select(cidade => new ListarCidadeDTO
            {
                CidadeID = cidade.CidadeID,
                NomeCidade = cidade.NomeCidade,
                Estado = cidade.Estado
            }).ToList();

            return cidadesDto;
        }

        public ListarCidadeDTO BuscarPorId(Guid cidadeId)
        {
            Cidade? cidade = _repository.BuscarPorId(cidadeId);

            if (cidade == null)
            {
                throw new DomainException("Cidade não encontrada.");
            }

            ListarCidadeDTO cidadeDto = new ListarCidadeDTO
            {
                CidadeID = cidade.CidadeID,
                NomeCidade = cidade.NomeCidade,
                Estado = cidade.Estado
            };

            return cidadeDto;
        }

        public void Adicionar(CriarCidadeDTO dto)
        {
            Validar.ValidarNome(dto.NomeCidade);
            Validar.ValidarEstado(dto.Estado);

            Cidade? cidadeExistente = _repository.BuscarPorNomeEEstado(dto.NomeCidade, dto.Estado);

            if (cidadeExistente != null)
            {
                throw new DomainException("Já existe uma cidade cadastrada com esse nome nesse estado.");
            }

            Cidade cidade = new Cidade
            {
                NomeCidade = dto.NomeCidade,
                Estado = dto.Estado
            };

            _repository.Adicionar(cidade);
        }

        public void Atualizar(Guid cidadeId, CriarCidadeDTO dto)
        {
            Validar.ValidarNome(dto.NomeCidade);
            Validar.ValidarEstado(dto.Estado);

            Cidade? cidadeBanco = _repository.BuscarPorId(cidadeId);

            if (cidadeBanco == null)
            {
                throw new DomainException("Cidade não encontrada.");
            }

            Cidade? cidadeExistente = _repository.BuscarPorNomeEEstado(dto.NomeCidade, dto.Estado);

            if (cidadeExistente != null && cidadeExistente.CidadeID != cidadeId)
            {
                throw new DomainException("Já existe uma cidade cadastrada com esse nome nesse estado.");
            }

            cidadeBanco.NomeCidade = dto.NomeCidade;
            cidadeBanco.Estado = dto.Estado;

            _repository.Atualizar(cidadeBanco);
        }
    }
}
