using GerenciadorPatrimonio.Aplications.Autenticacao;
using GerenciadorPatrimonio.Aplications.Regras;
using GerenciadorPatrimonio.Domains;
using GerenciadorPatrimonio.DTOs.UsuarioDTO;
using GerenciadorPatrimonio.Exceptions;
using GerenciadorPatrimonio.Interfaces;

namespace GerenciadorPatrimonio.Aplications.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;
        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarUsuarioDTO> Listar()
        {
            List<Usuario> listar = _repository.Listar();
            List<ListarUsuarioDTO> usuariosDto = listar.Select(usuario => new ListarUsuarioDTO
            {
                UsuarioID = usuario.UsuarioID,
                NIF = usuario.NIF,
                Nome = usuario.Nome,
                CargoID = usuario.CargoID,
                RG = usuario.RG,
                CPF = usuario.CPF,
                CarteiraTrabalho = usuario.CarteiraTrabalho,
                Email = usuario.Email,
                Ativo = usuario.Ativo,
                PrimeiroAcesso = usuario.PrimeiroAcesso,
                EnderecoID = usuario.EnderecoID,
                TipoUsuarioID = usuario.TipoUsuarioID

            }).ToList();
            return usuariosDto;
        }
        public ListarUsuarioDTO BuscarPorID(Guid usuarioID)
        {
            Usuario usuario = _repository.BuscarPorID(usuarioID)!;
            if(usuario == null)
            {
                throw new DomainException("Usuário não encontrado!");
            }
            ListarUsuarioDTO usuarioDTO = new ListarUsuarioDTO
            {
                UsuarioID = usuario.UsuarioID,
                NIF = usuario.NIF,
                Nome = usuario.Nome,
                CargoID = usuario.CargoID,
                RG = usuario.RG,
                CPF = usuario.CPF,
                CarteiraTrabalho = usuario.CarteiraTrabalho,
                Email = usuario.Email,
                Ativo = usuario.Ativo,
                PrimeiroAcesso = usuario.PrimeiroAcesso,
                EnderecoID = usuario.EnderecoID,
                TipoUsuarioID = usuario.TipoUsuarioID
            };
            return usuarioDTO;
        }
        public void Adicionar(CriarUsuarioDTO dto)
        {
            Validar.ValidarNome(dto.Nome);
            Validar.ValidarNIF(dto.NIF);
            Validar.ValidarEmail(dto.Email);
            Validar.ValidarCPF(dto.CPF);


            Usuario usuarioDuplicado = _repository.BuscarDuplicado(dto.NIF, dto.CPF, dto.Email)!;

            if(usuarioDuplicado != null)
            {
                if (usuarioDuplicado.NIF == dto.NIF)
                {
                    throw new DomainException("Já existe um usuario cadastrado com esse NIF");
                }
                if (usuarioDuplicado.CPF == dto.CPF)
                {
                    throw new DomainException("Já existe um usuario cadastrado com esse CPF");

                }
                if (usuarioDuplicado.Email.ToLower() == dto.Email.ToLower(){
                    throw new DomainException("Já existe um usuario cadastrado com esse Email");

                }
            }
            if (!_repository.CargoExiste(dto.CargoID))
            {
                throw new DomainException("Cargo informado não existe");
            }
            if (!_repository.TipoUsuarioExiste(dto.TipoUsuarioID))
            {
                throw new DomainException("Tipo usuário informado não existe");
            }
            Usuario usuario = new Usuario
            {
                NIF = dto.NIF,
                Nome = dto.Nome,
                CargoID = dto.CargoID,
                RG = dto.RG,
                CPF = dto.CPF,
                CarteiraTrabalho = dto.CarteiraTrabalho,
                Email = dto.Email,
                Senha = CriptografarSenha.Criptografar(dto.Senha),
                Ativo = true,
                PrimeiroAcesso = true,
                EnderecoID = dto.EnderecoID,
                TipoUsuarioID = dto.TipoUsuarioID
            };
            return usuario;
        }
        
    }
}
