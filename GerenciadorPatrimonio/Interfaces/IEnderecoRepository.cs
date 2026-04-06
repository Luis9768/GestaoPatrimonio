using GerenciadorPatrimonio.Domains;

namespace GerenciadorPatrimonio.Interfaces
{
    public interface IEnderecoRepository
    {
        List<Endereco> Listar();
        Endereco BuscarPorID(Guid id);
        void Adicionar(Endereco endereco);
        void Atualizar(Endereco endereco);
        bool BairroExiste(Guid bairroID);
        Endereco BuscarPorLogradouroENumero(string logradouro, int? numero, Guid bairroID);
    }
}
