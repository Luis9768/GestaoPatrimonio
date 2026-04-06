using System.Globalization;

namespace GerenciadorPatrimonio.DTOs.EnderecoDTO
{
    public class CriarEnderecoDTO
    {
        public Guid BairroID { get; set; }
        public string Logradouro { get; set; } = null!;
        public string? CEP {  get; set; }
        public int Numero { get; set; }
        public string? Complemento { get; set; }

    }
}
