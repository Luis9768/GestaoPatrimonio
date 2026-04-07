using GerenciadorPatrimonio.Domains;

namespace GerenciadorPatrimonio.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> Listar();
        Usuario? BuscarPorID(Guid id);
        Usuario? BuscarDuplicado(string nif, string cpf, string email, Guid? usuarioID = null);
        bool EnderecoExiste(Guid enderecoID);
        bool CargoExiste(Guid cargoID);
        bool TipoUsuarioExiste(Guid usuarioID);
        void Adicionar(Usuario usuario);
        void Atualizar(Usuario usuario);
        void AtualizarStaus(Usuario usuario);
        Usuario? ObterPorNIFComTipoUsuario(string nif);
        void AtualizarSenha(Usuario usuario);
        void AtualizarPrimeiroAcesso(Usuario usuario);
    }
}