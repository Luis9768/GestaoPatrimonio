using GerenciadorPatrimonio.Contexts;
using GerenciadorPatrimonio.Domains;
using GerenciadorPatrimonio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorPatrimonio.Repositorys
{
    public class LogPatrimonioRepository : ILogPatrimonioRepository
    {
        private readonly GestaoPatrimoniosContext _context;
        public LogPatrimonioRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }
        public List<LogPatrimonio> Listar()
        {
            return _context.LogPatrimonio
                .Include(log => log.Usuario)
                .Include(log => log.Localizacao)
                .Include(log => log.TipoAlteracao)
                .Include(log => log.StatusPatrimonio)
                .Include(log => log.Patrimonio)
                .OrderByDescending(log => log.DataTransferencia)
                .ToList();
        }

        public List<LogPatrimonio> BuscarPorPatrimonio(Guid patrimonioId)
        {
            return _context.LogPatrimonio
                .Include(log => log.Usuario)
                .Include(log => log.Localizacao)
                .Include(log => log.TipoAlteracao)
                .Include(log => log.StatusPatrimonio)
                .Include(log => log.Patrimonio)
                .Include(log => log.PatrimonioID == patrimonioId)
                .OrderByDescending(log => log.DataTransferencia)
                .ToList();
        }
    }
}
