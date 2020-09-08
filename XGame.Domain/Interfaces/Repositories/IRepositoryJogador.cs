using System;
using System.Collections;
using System.Collections.Generic;
using XGame.Domain.Arguments.Jogador;
using XGame.Domain.Entities;

namespace XGame.Domain.Interfaces.Repositories
{
    public interface IRepositoryJogador
    {
        Jogador AutenticarJogador(string email, string senha);

        Jogador AdicionarJogador(Jogador jogador);

        IEnumerable<Jogador> ListarJogador();

        Jogador ObterJogadorPorId(Guid id);
        void AlterarJogador(Jogador jogador);
    }
}
