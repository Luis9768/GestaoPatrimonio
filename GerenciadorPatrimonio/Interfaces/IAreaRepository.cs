using GerenciadorPatrimonio.Domains;

namespace GerenciadorPatrimonio.Interfaces
{
    public interface IAreaRepository
    {
        List<Area> Listar();
        Area? BuscarPorId(Guid areaID);
        Area? BuscarPorNome(string nomeArea);
        void Adicionar(Area area);
        void Atualizar(Area area);

    }
}
