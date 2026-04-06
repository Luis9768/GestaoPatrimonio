using GerenciadorPatrimonio.Contexts;
using GerenciadorPatrimonio.Domains;
using GerenciadorPatrimonio.Interfaces;

namespace GerenciadorPatrimonio.Repositorys
{
    public class CargoRepository : ICargoRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public CargoRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }
        public List<Cargo> Listar()
        {
            return _context.Cargo.OrderBy(a => a.NomeCargo).ToList();
        }
        public Cargo BuscarPorId(Guid id)
        {
            return _context.Cargo.Find(id)!;
        }
        public Cargo BuscarPorNome(string nome)
        {
            return _context.Cargo.FirstOrDefault(a => a.NomeCargo == nome)!;
        }
        public void Adicionar(Cargo cargo)
        {
            _context.Cargo.Add(cargo);
            _context.SaveChanges();
        }
        public void Atualizar(Guid id, Cargo cargo)
        {
            if(cargo == null)
            {
                return;
            }
            Cargo cargoBanco = _context.Cargo.Find(id)!;
            if(cargoBanco == null)
            {
                return;
            }
            cargoBanco.NomeCargo = cargo.NomeCargo;
            _context.SaveChanges();
        }
       
    }
}
