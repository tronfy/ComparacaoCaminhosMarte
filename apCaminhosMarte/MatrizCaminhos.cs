﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ENZO FUREGATTI SPINELLA 19168
// NICOLAS DENADAI SCHMIDT 19191
namespace apCaminhosMarte
{
    /// <summary>
    /// Classe que armazena a Matriz de Caminhos usada no programa, por exemplo,
    /// no buscador de arquivos.
    /// </summary>
    class MatrizCaminhos
    {
        public Caminho[,] Matriz { get; }
        public int Tamanho { get; }

        public MatrizCaminhos(int qtd)
        {
            Tamanho = qtd;
            Matriz = new Caminho[qtd, qtd];
        }

        public MatrizCaminhos(MatrizCaminhos outra)
        {
            Tamanho = outra.Tamanho;
            Matriz = (Caminho[,])outra.Matriz.Clone();
        }

        // Método público de inclusão
        public void Incluir(Caminho c)
        {
            // Adiciona o caminho no local da memória da matriz indexado pelo Id da sua cidade de origem e da sua cidade destino
            Matriz[c.Origem.Id, c.Destino.Id] = c;
            // O inverso do caminho indexado pelos Ids, porém trocados. Usado para o programa entender que os caminhos são de duas mãos...
            Caminho inverso = new Caminho(c.Destino, c.Origem, c.Distancia, c.Tempo, c.Custo);
            Matriz[c.Destino.Id, c.Origem.Id] = inverso;
        }

        // Método público de busca (pelos indexadores de linha e de coluna)
        public Caminho BuscarPeloIndice(int linha, int coluna)
        {
            // Se os índices forem válidos
            if (linha > 0 && coluna > 0)
                return Matriz[linha, coluna];
            return null;
        }

        // ToString padrão das matrizes, mostrando ela no formato certo
        public override string ToString()
        {
            string ret = "";

            for (int i = 0; i < Tamanho; i++)
            {
                for (int j = 0; j < Tamanho; j++)
                {
                    if (i == j) ret += "0".PadLeft(6, ' ');
                    else
                    {
                        var atual = Matriz[i, j];
                        if (atual == null) ret += "".PadLeft(6, ' ');
                        else ret += atual.Distancia.ToString().PadLeft(6, ' ');
                    }
                }
                ret += '\n';
            }

            return ret;
        }
    }
}
