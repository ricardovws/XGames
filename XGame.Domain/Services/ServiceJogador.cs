using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XGame.Domain.Arguments.Base;
using XGame.Domain.Arguments.Jogador;
using XGame.Domain.Entities;
using XGame.Domain.Interfaces.Repositories;
using XGame.Domain.Interfaces.Services;
using XGame.Domain.Resources;
using XGame.Domain.ValueObjects;

namespace XGame.Domain.Services
{
    public class ServiceJogador : Notifiable, IServiceJogador
    {
        private readonly IRepositoryJogador _repositoryJogador;

        public ServiceJogador()
        {
        }

        public ServiceJogador(IRepositoryJogador repositoryJogador)
        {
            _repositoryJogador = repositoryJogador;
        }

        public AdicionarJogadorResponse AdicionarJogador(AdicionarJogadorRequest request)
        {
            //var nome = new Nome("Ricardo", "Vinícius");
            //var email = new Email("ricardo@ricardo.com");

            //Jogador jogador = new Jogador(request.Email, request.Senha);
            //var jogador = new Jogador(nome, email, "123456");

            var nome = new Nome(request.PrimeiroNome, request.UltimoNome);
            var email = new Email(request.Email);

            var jogador = new Jogador(nome, email, request.Senha);

            AddNotifications(nome, email);

            if (_repositoryJogador.Existe(x => x.Email.Endereco == request.Email))
            {
                AddNotification("E-mail", Message.JA_EXISTE_UM_X0_CHAMADO_X1.ToFormat("e-mail", request.Email));
            }
            
            if (this.IsInvalid())
            {
                return null;
            }

            jogador = _repositoryJogador.Adicionar(jogador);
            

            return (AdicionarJogadorResponse)jogador;

        }

        public AlterarJogadorResponse AlterarJogador(AlterarJogadorRequest request)
        {
            if (request == null)
            {
                AddNotification("AlterarJogadorRequest", Message.X0_E_OBRIGATORIO.ToFormat("AlterarJogadorRequest"));
            }

            Jogador jogador = _repositoryJogador.ObterPorId(request.Id);

            if (jogador == null)
            {
                AddNotification("Id", Message.DADOS_NAO_ENCONTRADOS);
                return null;
            }

            var nome = new Nome(request.PrimeiroNome, request.UltimoNome);
            var email = new Email(request.Email);

            jogador.AlterarJogador(nome, email, jogador.Status);

            AddNotifications(jogador);

            if (IsInvalid())
            {
                return null;
            }

            _repositoryJogador.Editar(jogador);

            return (AlterarJogadorResponse)jogador;
        }

        public AutenticarJogadorResponse AutenticarJogador(AutenticarJogadorRequest request)
        {
            if (request == null)
            {
                AddNotification("AutenticarJogadorRequest", Message.X0_E_OBRIGATORIO.ToFormat("AutenticarJogadorRequest"));
            }

            var email = new Email(request.Email);
            
            var jogador = new Jogador(email, request.Senha);

            AddNotifications(jogador, email);

            if (jogador.IsInvalid())
            {
                return null;
            }

            jogador = _repositoryJogador.ObterPor(x => x.Email.Endereco == jogador.Email.Endereco && x.Senha == jogador.Senha);

            //AutenticarJogadorResponse response = new AutenticarJogadorResponse();
            //response.Email = jogador.Email.Endereco;
            //response.PrimeiroNome = jogador.Nome.PrimeiroNome;
            //response.Status = (int)jogador.Status;
            //return response;

            return (AutenticarJogadorResponse)jogador;
        }

        public IEnumerable<JogadorResponse> ListarJogador()
        {
            return _repositoryJogador.Listar().Select(jogador => (JogadorResponse)jogador).ToList();

        }

        public ResponseBase ExcluirJogador(Guid id)
        {
            Jogador jogador = _repositoryJogador.ObterPorId(id);

            if (jogador == null)
            {
                AddNotification("Id", Message.DADOS_NAO_ENCONTRADOS);
                return null;
            }

            _repositoryJogador.Remover(jogador);

            return new ResponseBase();
        }

    }
}
