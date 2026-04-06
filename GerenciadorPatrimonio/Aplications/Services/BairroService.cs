using GerenciadorPatrimonio.Aplications.Regras;
using GerenciadorPatrimonio.Domains;
using GerenciadorPatrimonio.DTOs.BairroDTO;
using GerenciadorPatrimonio.Exceptions;
using GerenciadorPatrimonio.Interfaces;

namespace GerenciadorPatrimonio.Aplications.Services
{
    public class BairroService
    {
        private readonly IBairroRepository _repository;

        public BairroService(IBairroRepository repository)
        {
            _repository = repository;
        }

        public List<ListarBairroDTO> Listar()
        {
            List<Bairro> bairros = _repository.Listar();

            List<ListarBairroDTO> bairrosDto = bairros.Select(bairro => new ListarBairroDTO
            {
                BairroID = bairro.BairroID,
                NomeBairro = bairro.NomeBairro,
                CidadeID = bairro.CidadeID
            }).ToList();

            return bairrosDto;
        }

        public ListarBairroDTO BuscarPorId(Guid bairroId)
        {
            Bairro bairro = _repository.BuscarPorID(bairroId);

            if (bairro == null)
            {
                throw new DomainException("Bairro não encontrado.");
            }

            return new ListarBairroDTO
            {
                BairroID = bairro.BairroID,
                NomeBairro = bairro.NomeBairro,
                CidadeID = bairro.CidadeID
            };
        }

        public void Adicionar(CriarBairroDTO dto)
        {
            Validar.ValidarNome(dto.NomeBairro);

            Bairro bairroExistente = _repository.BuscarPorNome(dto.NomeBairro, dto.CidadeID);

            if (bairroExistente != null)
            {
                throw new DomainException("Já existe um bairro com esse nome nessa cidade.");
            }

            if (!_repository.CidadeExiste(dto.CidadeID))
            {
                throw new DomainException("Cidade informada não existe.");
            }

            Bairro bairro = new Bairro
            {
                NomeBairro = dto.NomeBairro,
                CidadeID = dto.CidadeID
            };

            _repository.Adicionar(bairro);
        }

        public void Atualizar(Guid bairroId, CriarBairroDTO dto)
        {
            Validar.ValidarNome(dto.NomeBairro);

            Bairro bairroBanco = _repository.BuscarPorID(bairroId);

            if (bairroBanco == null)
            {
                throw new DomainException("Bairro não encontrado.");
            }

            Bairro bairroExistente = _repository.BuscarPorNome(dto.NomeBairro, dto.CidadeID);

            if (bairroExistente != null && bairroExistente.BairroID != bairroId)
            {
                throw new DomainException("Já existe um bairro com esse nome nessa cidade.");
            }

            if (!_repository.CidadeExiste(dto.CidadeID))
            {
                throw new DomainException("Cidade informada não existe.");
            }

            bairroBanco.NomeBairro = dto.NomeBairro;
            bairroBanco.CidadeID = dto.CidadeID;

            _repository.Atualizar(bairroBanco);
        }
    }
}
