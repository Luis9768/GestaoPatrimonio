namespace GerenciadorPatrimonio.DTOs.CidadeDTO
{
    public class ListarCidadeDTO
    {
        public Guid CidadeID { get; set; }
        public string NomeCidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}
