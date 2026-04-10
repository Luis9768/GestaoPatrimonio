namespace GerenciadorPatrimonio.DTOs.SolicitacaoTransferenciaDTO
{
    public class CriarSolicitacaoTransferenciaDTO
    {
        public string Justificativa { get; set; } = string.Empty;
        public Guid PatrimonioId {  get; set; }
        public Guid LocalizacaoId { get; set; }

    }
}
