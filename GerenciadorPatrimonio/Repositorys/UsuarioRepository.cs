using GerenciadorPatrimonio.Contexts;
using GerenciadorPatrimonio.Domains;
using GerenciadorPatrimonio.DTOs.UsuarioDTO;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorPatrimonio.Repositorys
{
    public class UsuarioRepository
    {
        private readonly GestaoPatrimoniosContext _context;
        public UsuarioRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<Usuario> Listar()
        {
            return _context.Usuario.OrderBy(c => c.Nome).ToList();
        }
        public Usuario BuscarPorID(Guid id)
        {
            return _context.Usuario.Find(id)!;
        }
        public Usuario BuscarDuplicado(string nif, string cpf, string email, Guid? usuarioID = null)
        {
            var consulta = _context.Usuario.AsQueryable();
            if (usuarioID.HasValue)
            {
                consulta = consulta.Where(u => u.UsuarioID != usuarioID.Value);
            }

            return consulta.FirstOrDefault(u =>
            u.NIF == nif ||
            u.CPF == cpf ||
            u.Email.ToLower() == email.ToLower()
            )!;
        }

        public bool EnderecoExiste(Guid id)
        {
            return _context.Endereco.Any(e => e.EnderecoID == id);
        }
        public bool CargoExiste(Guid cargoID)
        {
            return _context.Cargo.Any(c => c.CargoID == cargoID);
        }

        public bool TipoUsuarioExiste(Guid tipoUsuarioID)
        {
            return _context.TipoUsuario.Any(t => t.TipoUsuarioID == tipoUsuarioID);
        }
        public void Adicionar(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            _context.SaveChanges();
        }
        public void Atualizar(Usuario usuario)
        {
            var usuarioBanco = _context.Usuario.Find(usuario.UsuarioID)!;
            if (usuarioBanco == null)
            {
                return;
            }
            usuarioBanco.NIF = usuario.NIF;
            usuarioBanco.Email = usuario.Email;
            usuarioBanco.Nome = usuario.Nome;
            usuarioBanco.CPF = usuario.CPF;
            usuarioBanco.RG = usuario.RG;
            usuarioBanco.CarteiraTrabalho = usuario.CarteiraTrabalho;
            usuarioBanco.EnderecoID = usuario.EnderecoID;
            usuarioBanco.CargoID = usuario.CargoID;
            usuarioBanco.TipoUsuarioID = usuario.TipoUsuarioID;

            _context.SaveChanges();
        }
        public void AtualizarStatus(Usuario usuario)
        {
            if(usuario == null)
            {
                return;
            }
            Usuario usuarioBanco = _context.Usuario.Find(usuario.UsuarioID)!;

            if(usuarioBanco == null)
            {
                return; 
            }

            usuarioBanco.Ativo = usuario.Ativo;
            _context.SaveChanges();
        }
        public Usuario ObterPorNIFComTipoUsuario(string nif)
        {
            return _context.Usuario.Include(u => u.TipoUsuario).FirstOrDefault(u => u.NIF == nif)!;
        }
        public void AtualizarSenha(Usuario usuario)
        {
            if(usuario == null)
            {
                return;
            }
            Usuario usuarioBanco = _context.Usuario.Find(usuario.UsuarioID)!;
            if(usuarioBanco == null)
            {
                return;
            }
            usuarioBanco.Senha = usuario.Senha;
            _context.SaveChanges();
        }
        public void AtualizarPrimeiroAcesso(Usuario usuario)
        {
            if (usuario == null)
            {
                return;
            }
            Usuario usuarioBanco = _context.Usuario.Find(usuario.UsuarioID)!;
            if (usuarioBanco == null)
            {
                return;
            }
            usuarioBanco.PrimeiroAcesso = usuario.PrimeiroAcesso;
            _context.SaveChanges();
        }
    }
}
