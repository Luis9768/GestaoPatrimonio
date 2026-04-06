using GerenciadorPatrimonio.Domains;
using GerenciadorPatrimonio.DTOs.EnderecoDTO;
using GerenciadorPatrimonio.Exceptions;
using GerenciadorPatrimonio.Interfaces;

namespace GerenciadorPatrimonio.Aplications.Services
{
    public class EnderecoService
    {
        private readonly IEnderecoRepository _repository;
        public EnderecoService(IEnderecoRepository repository)
        {
            _repository = repository;
        }

        public List<ListarEnderecoDTO> Listar()
        {
            List<Endereco> endereco = _repository.Listar();
            List<ListarEnderecoDTO> listarDTO = endereco.Select(e => new ListarEnderecoDTO
            {
                BairroID = e.BairroID,
                Numero = e.Numero,
                Logradouro = e.Logradouro,
                CEP = e.CEP,
                Complemento = e.Complemento,
                EnderecoID = e.EnderecoID
            }).ToList();
            return listarDTO;
        }

        public ListarEnderecoDTO BuscarPorID(Guid id)
        {
            Endereco enderecoBanco = _repository.BuscarPorID(id);
            if (enderecoBanco == null)
            {
                throw new DomainException("Endereço não encontrado!");
            }
            return new ListarEnderecoDTO
            {
                BairroID = enderecoBanco.BairroID,
                Numero = enderecoBanco.Numero,
                Logradouro = enderecoBanco.Logradouro,
                CEP = enderecoBanco.CEP,
                Complemento = enderecoBanco.Complemento,
                EnderecoID = enderecoBanco.EnderecoID
            };
        }

        public ListarEnderecoDTO BuscarPorLogradouroENumero(string logradouro, int? numero, Guid bairroID)
        {
            if(logradouro == null)
            {
                throw new DomainException("O campo logradouro não pode estar vazio!");
            }
            if(bairroID == null)
            {
                throw new DomainException("O campo bairro não pode estar vazio!");
            }

            Endereco endereco = _repository.BuscarPorLogradouroENumero(logradouro, numero, bairroID);
            return new ListarEnderecoDTO
            {
                BairroID = endereco.BairroID,
                Numero = endereco.Numero,
                Logradouro = endereco.Logradouro,
                CEP = endereco.CEP,
                Complemento = endereco.Complemento,
                EnderecoID = endereco.EnderecoID
            };
        }

        public void Adicionar(CriarEnderecoDTO endereco)
        {
           var guardarBairro = _repository.BairroExiste(endereco.BairroID);

            if(guardarBairro == null)
            {
                throw new DomainException("O bairro não existe!");
            }
           
            Endereco enderecoParaBanco = new Endereco
            {
               BairroID = endereco.BairroID,
               Logradouro = endereco.Logradouro,
               Numero = endereco.Numero,
               CEP = endereco.CEP,
               Complemento = endereco.Complemento
            };
            _repository.Adicionar(enderecoParaBanco);
        }
        public void Atualizar(Guid id, CriarEnderecoDTO endereco)
        {
            Endereco? enderecoBanco = _repository.BuscarPorID(id);

            if (enderecoBanco == null)
            {
                throw new DomainException("Endereço não encontrado!");
            }
            
            enderecoBanco.BairroID = endereco.BairroID;
            enderecoBanco.CEP = endereco.CEP;
            enderecoBanco.Logradouro = endereco.Logradouro;
            enderecoBanco.Numero = endereco.Numero;
            enderecoBanco.Complemento = endereco.Complemento;
            
            _repository.Atualizar(enderecoBanco);
        }



    }
}
