using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosMarte
{
    class MatrizCaminhos
    {
        Caminho[,] matriz;
        int tamanho;

        public MatrizCaminhos(int qtd)
        {
            tamanho = qtd;
            matriz = new Caminho[qtd, qtd];
        }

        public void Incluir(Caminho c)
        {
            matriz[c.IdCidadeOrigem, c.IdCidadeDestino] = matriz[c.IdCidadeDestino, c.IdCidadeOrigem] = c;
        }

        public override string ToString()
        {
            string ret = "";

            for (int i = 0; i < tamanho; i++)
            {
                for (int j = 0; j < tamanho; j++)
                {
                    if (i == j) ret += "0".PadLeft(6, ' ');
                    else
                    {
                        var atual = matriz[i, j];
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
