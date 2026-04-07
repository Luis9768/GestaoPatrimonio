using GerenciadorPatrimonio.Aplications.Autenticacao;
using GerenciadorPatrimonio.Aplications.Regras;
using GerenciadorPatrimonio.Domains;
using GerenciadorPatrimonio.DTOs.AutenticacaoDTO;
using GerenciadorPatrimonio.Exceptions;
using GerenciadorPatrimonio.Interfaces;

namespace GerenciadorPatrimonio.Aplications.Services
{
    public class AutenticacaoService
    {
        private readonly IUsuarioRepository _repository;
        private readonly GerarTokenJWT _jwt;

        public AutenticacaoService(IUsuarioRepository repository, GerarTokenJWT jwt)
        {
            _jwt = jwt;
            _repository = repository;
        }
        private static bool VerificarSenha(string senhaDigitada, byte[] senhaHashBanco)
        {
            var hashDigitado = CriptografarSenha.Criptografar(senhaDigitada);
            return hashDigitado.SequenceEqual(senhaHashBanco);
        }
        public TokenDTO Login(LoginDTO loginDTO)
        {
            Usuario usuario = _repository.ObterPorNIFComTipoUsuario(loginDTO.NIF)!;
            if(usuario == null)
            {
                throw new DomainException("NIF ou senha inválidos!");
            }
            if(usuario.Ativo == false)
            {
                throw new DomainException("Usuário inátivo.");
            }
            if(!VerificarSenha(loginDTO.Senha, usuario.Senha))
            {
                throw new DomainException("NIF ou senha inválidos!");
            }
            string token = _jwt.GerarToken(usuario);
            TokenDTO novoToken = new TokenDTO
            {
                Token = token,
                PrimeiroAcesso = usuario.PrimeiroAcesso,
                TipoUsuario = usuario.TipoUsuario.NomeTipo
            };
            return novoToken;
        }
        public void TrocarPrimeiraSenha(Guid usuarioID, TrocarPrimeiraSenhaDTO dto)
        {
            Validar.ValidarSenha(dto.SenhaAtual);
            Validar.ValidarSenha(dto.NovaSenha);

            Usuario usuario = _repository.BuscarPorID(usuarioID)!;
            if(usuario == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }
            if(!VerificarSenha(dto.SenhaAtual, usuario.Senha))
            {
                throw new DomainException("Senha atual inválida!");
            }
            if(dto.NovaSenha == dto.SenhaAtual)
            {
                throw new DomainException("A nova senha deve ser diferente da senha atual.");
            }
            usuario.Senha = CriptografarSenha.Criptografar(dto.NovaSenha);
            usuario.PrimeiroAcesso = false;
            _repository.AtualizarSenha(usuario);
            _repository.AtualizarPrimeiroAcesso(usuario);
        }   
    }
}
