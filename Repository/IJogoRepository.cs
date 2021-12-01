using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiCatalogoJogos.Entities;
using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.ViewModel;

namespace ApiCatalogoJogos.Repository
{
    public interface IJogoRepository
    {
        Task<List<Jogo>> Obter(int pagina, int quantidade);

        Task<Jogo> Obter(Guid id);
        Task<List<Jogo>> Obter(string nome, string produtora);

        //Task<Jogo> Inserir(Jogo jogo);
        Task Inserir(Jogo jogo);
        Task Atualizar(Jogo jogo);
        Task Remover(Guid id);
    }
}