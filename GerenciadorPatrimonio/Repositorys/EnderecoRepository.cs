using GerenciadorPatrimonio.Contexts;
using GerenciadorPatrimonio.Domains;
using GerenciadorPatrimonio.Interfaces;

namespace GerenciadorPatrimonio.Repositorys
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly GestaoPatrimoniosContext _context;
        public EnderecoRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }
        public List<Endereco> Listar()
        {
            return _context.Endereco.ToList();
        }

        public Endereco BuscarPorID(Guid id)
        {
            return _context.Endereco.Find(id)!;
        }
        public void Adicionar(Endereco endereco)
        {
            _context.Endereco.Add(endereco);
            _context.SaveChanges();
        }
        public void Atualizar(Endereco endereco)
        {
            if (endereco == null)
            {
                return;
            }
            Endereco enderecoBanco = _context.Endereco.Find(endereco.EnderecoID)!;
            if (enderecoBanco == null)
            {
                return;
            }
            enderecoBanco.Numero = endereco.Numero;
            enderecoBanco.Logradouro = endereco.Logradouro;
            enderecoBanco.Bairro = endereco.Bairro;

            _context.SaveChanges();
        }
        public bool BairroExiste(Guid bairroID)
        {
            return _context.Bairro.Any(b => b.BairroID == bairroID);
        }
        public Endereco BuscarPorLogradouroENumero(string logradouro, int? numero, Guid bairroID)
        {
            return _context.Endereco.FirstOrDefault(e => e.Logradouro .ToLower() == logradouro.ToLower() && e.Numero == numero && e.BairroID == bairroID)!;
        }
    }
}
