using GerenciadorPatrimonio.Contexts;
using GerenciadorPatrimonio.Domains;
using GerenciadorPatrimonio.Interfaces;
using System.Globalization;

namespace GerenciadorPatrimonio.Repositorys
{
    public class BairroRepository : IBairroRepository
    {
        private readonly GestaoPatrimoniosContext _context;
        public BairroRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }
        public List<Bairro> Listar()
        {
            return _context.Bairro.ToList();
        }
        public Bairro BuscarPorID(Guid bairroID)
        {
            return _context.Bairro.Find(bairroID)!;
        }
        public Bairro BuscarPorNome(string nomeBairro, Guid cidadeID)
        {
            return _context.Bairro.FirstOrDefault(bairro =>
            bairro.NomeBairro.ToLower() == nomeBairro.ToLower()
            && 
            bairro.CidadeID == cidadeID)!;
        }
        public void Adicionar(Bairro bairro)
        {
            _context.Add(bairro);
            _context.SaveChanges();
        }
        public void Atualizar(Bairro bairro)
        {
            if (bairro == null)
            {
                return;
            }

            Bairro? bairroBanco = _context.Bairro.Find(bairro.BairroID);

            if (bairroBanco == null)
            {
                return;
            }

            bairroBanco.NomeBairro = bairro.NomeBairro;
            bairroBanco.CidadeID = bairro.CidadeID;

            _context.SaveChanges();
        }

        public bool CidadeExiste(Guid cidadeID)
        {
            return _context.Bairro.Any(bairro => bairro.CidadeID == cidadeID);
        }

    }
}
