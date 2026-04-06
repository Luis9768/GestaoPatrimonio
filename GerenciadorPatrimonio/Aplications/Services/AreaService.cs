using GerenciadorPatrimonio.Aplications.Regras;
using GerenciadorPatrimonio.Domains;
using GerenciadorPatrimonio.DTOs.AreaDTO;
using GerenciadorPatrimonio.Exceptions;
using GerenciadorPatrimonio.Interfaces;
using System.Globalization;

namespace GerenciadorPatrimonio.Aplications.Services
{
    public class AreaService
    {
        private readonly IAreaRepository _repository;
        public AreaService(IAreaRepository repository)
        {
            _repository = repository;
        }
        public List<ListarAreaDTO> Listar()
        {
            List<Area> areas = _repository.Listar();
            List<ListarAreaDTO> areasDto = areas.Select(area => new ListarAreaDTO
            {
                AreaID = area.AreaID,
                NomeArea = area.NomeArea,
            }).ToList();
            return areasDto;
        }
        public ListarAreaDTO BuscarPorID(Guid areaID)
        {
            Area area = _repository.BuscarPorId(areaID)!;
            if(area == null)
            {
                throw new DomainException("Área não encontrada.");
            }
            ListarAreaDTO areaDTO = new ListarAreaDTO
            {
                AreaID = area.AreaID,
                NomeArea = area.NomeArea
            };
            return areaDTO;
        }
        public ListarAreaDTO BuscarPorNome(string nomeArea)
        {
            Area? area = _repository.BuscarPorNome(nomeArea);
            if (area == null)
            {
                throw new DomainException("Área não encontrada.");
            }
            ListarAreaDTO areaDTO = new ListarAreaDTO
            {
                AreaID = area.AreaID,
                NomeArea = area.NomeArea
            };
            return areaDTO;
        }
        public void Adicionar(CriarAreaDTO dto)
        {
            Validar.ValidarNome(dto.NomeArea);

            Area? areaExistente = _repository.BuscarPorNome(dto.NomeArea);

            if(areaExistente != null)
            {
                new DomainException("Já existe uma área cadastrada com esse nome!");
            }

            Area area = new Area
            {
                NomeArea = dto.NomeArea,
            };
            _repository.Adicionar(area);
        }
        public void Atualizar(Guid areaID, CriarAreaDTO dto)
        {
            Validar.ValidarNome(dto.NomeArea);

            Area? areaBanco = _repository.BuscarPorId(areaID);

            if (areaBanco != null)
            {
                throw new DomainException("Área não encontrada!");
            }

            Area? areaExiste = _repository.BuscarPorNome(dto.NomeArea);

            if(areaExiste != null)
            {
                throw new DomainException("Já existe uma área cadastrada com esse nome!");
            }
            areaBanco.NomeArea = dto.NomeArea;
            _repository.Atualizar(areaBanco);
        }
    }
}
