using GerenciadorPatrimonio.Domains;

namespace GerenciadorPatrimonio.Interfaces
{
    public interface IBairroRepository
    {
        List<Bairro> Listar();
        Bairro BuscarPorID(Guid id);
        void Adicionar(Bairro bairro);
        void Atualizar(Bairro bairro);
        Bairro BuscarPorNome(string nome, Guid cidadeID);
        bool CidadeExiste(Guid cidadeID);
    }
}
