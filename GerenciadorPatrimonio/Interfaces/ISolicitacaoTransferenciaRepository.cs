using GerenciadorPatrimonio.Domains;

namespace GerenciadorPatrimonio.Interfaces
{
    public interface ISolicitacaoTransferenciaRepository
    {
        List<SolicitacaoTransferencia> Listar();
        SolicitacaoTransferencia BuscarPorID(Guid id);
        bool ExisteSolicitacaoPendente(Guid id);
        bool UsuarioResponsavelDaLocalizacao(Guid id, Guid lozalizacao);
        StatusTransferencia BuscarStatusTransferenciaPorNome(string nomeStatus);
        void Adicionar(SolicitacaoTransferencia solicitacaoTransferencia);
        bool LocalizacaoExiste(Guid id);
        Patrimonio BuscarPatrimonioPorId(Guid id);
    }
}
