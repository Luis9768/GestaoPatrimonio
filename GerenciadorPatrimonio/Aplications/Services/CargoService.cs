using GerenciadorPatrimonio.Aplications.Regras;
using GerenciadorPatrimonio.Domains;
using GerenciadorPatrimonio.DTOs.CargoDTO;
using GerenciadorPatrimonio.Exceptions;
using GerenciadorPatrimonio.Interfaces;

namespace GerenciadorPatrimonio.Aplications.Services
{
    public class CargoService
    {
        private readonly ICargoRepository _repository;
        public CargoService(ICargoRepository repository)
        {
            _repository = repository;
        }
        public List<ListarCargoDTO> Listar()
        {
            List<Cargo> cargos = _repository.Listar();
            List<ListarCargoDTO> cargoDto = cargos.Select(cargo => new ListarCargoDTO
            {
                CargoId = cargo.CargoID,
                NomeCargo = cargo.NomeCargo
            }).ToList();
            return cargoDto;
        }
        public ListarCargoDTO BuscarPorId(Guid id)
        {
            Cargo cargo = _repository.BuscarPorId(id)!;
            if (cargo == null)
            {
                throw new DomainException("Cargo não encontrado.");
            }
            ListarCargoDTO dto = new ListarCargoDTO
            {
                CargoId = cargo.CargoID,
                NomeCargo = cargo.NomeCargo
            };
            return dto;
        }
        public ListarCargoDTO BuscarPorNome(string nome)
        {
            Cargo cargo = _repository.BuscarPorNome(nome)!;
            if (cargo == null)
            {
                throw new DomainException("Cargo não encontrado.");
            }
            ListarCargoDTO dto = new ListarCargoDTO
            {
                NomeCargo = cargo.NomeCargo
            };
            return dto;
        }
        public void Adicionar(CriarCargoDTO dto)
        {
            Cargo cargoExiste = _repository.BuscarPorNome(dto.NomeCargo)!;
            Validar.ValidarNome(dto.NomeCargo);
            if (cargoExiste != null)
            {
                throw new DomainException("Cargo já existente.");
            }
            Cargo cargoNovo = new Cargo
            {
                NomeCargo = dto.NomeCargo
            };
            _repository.Adicionar(cargoNovo);
        }
        public void Atualizar(Guid id, CriarCargoDTO dto)
        {
            Cargo? cargoBanco = _repository.BuscarPorId(id)!;
            if(cargoBanco == null)
            {
                throw new DomainException("Cargo para atualizar não encontrado.");
            }
            if(cargoBanco.NomeCargo == dto.NomeCargo)
            {
                throw new DomainException("Já existe um cargo com esse nome.");
            }
            Validar.ValidarNome(dto.NomeCargo);

            cargoBanco.NomeCargo = dto.NomeCargo;

            _repository.Atualizar(id,cargoBanco);
        }
    }
}
