using GerenciadorPatrimonio.Domains;

namespace GerenciadorPatrimonio.Interfaces
{
    public interface ILogPatrimonioRepository
    {
        List<LogPatrimonio> Listar();
        List<LogPatrimonio> BuscarPorPatrimonio(Guid patrimonioId);
    }
}
