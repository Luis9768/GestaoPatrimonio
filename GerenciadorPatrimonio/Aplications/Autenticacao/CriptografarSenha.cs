using System.Security.Cryptography;
using System.Text;

namespace GerenciadorPatrimonio.Aplications.Autenticacao
{
    public class CriptografarSenha
    {
        public static byte[] Criptografar(string Senha)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] byteSenha = Encoding.UTF8.GetBytes(Senha);
            byte[] senhaCriptografada = sha256.ComputeHash(byteSenha);

            return senhaCriptografada;
        }

    }
}
