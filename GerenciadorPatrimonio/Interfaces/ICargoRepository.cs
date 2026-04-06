using GerenciadorPatrimonio.Domains;

namespace GerenciadorPatrimonio.Interfaces
{
    public interface ICargoRepository
    {
        List<Cargo> Listar();
        Cargo? BuscarPorId(Guid id);
        Cargo? BuscarPorNome(string name);
        void Adicionar(Cargo cargo);
        void Atualizar(Guid id, Cargo cargo);
    }
}
