using GerenciadorPatrimonio.Contexts;
using GerenciadorPatrimonio.Domains;
using GerenciadorPatrimonio.Interfaces;

namespace GerenciadorPatrimonio.Repositorys
{
    public class SolicitacaoTransferenciaRepository : ISolicitacaoTransferenciaRepository
    {
        private readonly GestaoPatrimoniosContext _context;
        public SolicitacaoTransferenciaRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }
        public List<SolicitacaoTransferencia> Listar()
        {
            return _context.SolicitacaoTransferencia
                .OrderByDescending(s => s.DataCriacaoSolicitante)
                .ToList();
        }
        public SolicitacaoTransferencia BuscarPorID(Guid id)
        {
            return _context.SolicitacaoTransferencia.Find(id)!;
        }
        public StatusTransferencia BuscarStatusTransferenciaPorNome(string nomeStatus)
        {
            return _context.StatusTransferencia.FirstOrDefault(s => 
                s.NomeStatus.ToLower() == nomeStatus.ToLower())!;
        }
        public bool ExisteSolicitacaoPendente(Guid id)
        {
            StatusTransferencia statusPendente = BuscarStatusTransferenciaPorNome("Pendente de aprovação");
            if(statusPendente == null)
            {
                return false;
            }
            return _context.SolicitacaoTransferencia.Any(s =>
                s.PatrimonioID == id &&
                s.StatusTransferenciaID == statusPendente.StatusTransferenciaID);
        }
        public bool UsuarioResponsavelDaLocalizacao(Guid usuarioId, Guid LocalizacaoId)
        {
            return _context.Usuario.Any(u =>
            u.UsuarioID == usuarioId &&
            u.Localizacao.Any(l => l.LocalizacaoID == LocalizacaoId));
        }

        public void Adicionar(SolicitacaoTransferencia s)
        {
            _context.SolicitacaoTransferencia.Add(s);
            _context.SaveChanges();
        }
        public bool LocalizacaoExiste(Guid id)
        {
            return _context.Localizacao.Any(l => l.LocalizacaoID == id);
        }
        public Patrimonio BuscarPatrimonioPorId(Guid id)
        {
            return _context.Patrimonio.Find(id)!;
        }
    }
}
