﻿using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// ENZO FUREGATTI SPINELLA 19168
// NICOLAS DENADAI SCHMIDT 19191
namespace apCaminhosMarte
{
    /// <summary>
    /// Classe que utiliza da técnica do backtracking recursivo
    /// para encontrar Caminhos entre as Cidades
    /// </summary>
    class BuscadorDeCaminhos
    {
        private ArvoreAVL<Cidade> Arvore { get; set; }
        private MatrizCaminhos Matriz { get; set; }
        private List<List<Caminho>> todosCaminhos;
        private List<int> jaVisitados;

        public BuscadorDeCaminhos(ArvoreAVL<Cidade> arvore, MatrizCaminhos matriz)
        {
            Arvore = arvore;
            Matriz = matriz;
        }

        public List<List<Caminho>> BuscarCaminhoBackTracking(int idOrigem, int idDestino)
        {
            // inicializar as listas necessárias para execução
            todosCaminhos = new List<List<Caminho>>();
            jaVisitados = new List<int>();

            // como estamos na cidade de origem, adicioná-la à lista de cidades visitadas
            jaVisitados.Add(idOrigem);

            // chamar o método interno BuscarCaminho(), passando uma lista de Caminhos vazia para representar o conjunto de Caminhos atual
            BuscarCaminhoBackTracking(new List<Caminho>(), idOrigem, idDestino);

            // se não há caminhos entre as cidades selecionadas, retorna null
            if (todosCaminhos.Count() == 0) return null;

            // senão, retorna os caminhos
            return todosCaminhos;
        }

        public List<List<Caminho>> BuscarCaminhoPilha(Cidade origem, Cidade destino)
        {
            BuscadorPilha buscador = new BuscadorPilha(Matriz, origem, destino);
            buscador.Solucionar();

            // se não há caminhos entre as cidades selecionadas, retorna null
            if (buscador.Solucoes.Count == 0) return null;

            // senão, retorna os caminhos
            return buscador.Solucoes;
        }

        public List<Caminho> BuscarCaminhoDijkstra(Cidade origem, Cidade destino, string criterio)
        {
            BuscadorDijkstra buscador = new BuscadorDijkstra(Arvore, Matriz, origem, destino, criterio);
            List<Caminho> caminho = buscador.Solucionar();

            return caminho;
        }

        private void BuscarCaminhoBackTracking(List<Caminho> caminhoAtual, int cidadeAtual, int cidadeDestino)
        {
            // para todas as cidades que existem,
            for(int i = 0; i < Matriz.Tamanho; i++)
            {
                // gerar um caminho entre a cidade atual e a nova cidade
                Caminho caminhoTeste = Matriz.BuscarPeloIndice(cidadeAtual, i);
                // se esse caminho existir e não fomos pra essa cidade nesse 'braço' da execução,
                if (caminhoTeste != null && caminhoTeste.Distancia > 0 && !jaVisitados.Any(item => item == i))
                {
                    // vamos para a nova cidade, e a adicionamos ao caminho atual
                    caminhoAtual.Add(caminhoTeste);

                    // se chegamos no destino do usuário, salvamos um clone do caminho atual como um dos caminhos possíveis
                    if (i == cidadeDestino)
                        todosCaminhos.Add(caminhoAtual.Select(c => c.Clone()).ToList());
                    else
                    {
                        // senão, usamos recursão:
                        // adicionamos a cidade atual à lista de visitadas
                        jaVisitados.Add(i);
                        // chamamos o mesmo método, passando o caminho atual e começando da cidade em que estamos
                        BuscarCaminhoBackTracking(caminhoAtual, i, cidadeDestino);
                        /* ao finalizar o método, significa que todas as cidades de 'níveis' mais distantes do que essa
                         * ja foram verificadas, então podemos removê-la da lista de cidades visitadas, para que outras
                         * iterações do backtracking possam passar por ela
                         */
                        jaVisitados.Remove(i);
                    }
                    // no fim, temos que remover a ultima etapa do caminho atual, seguindo a lógica de backtracking
                    caminhoAtual.RemoveAt(caminhoAtual.Count - 1);
                }
            }
        }

        private void BuscarCaminhoDijkstra()
        {

        }
    }
}
