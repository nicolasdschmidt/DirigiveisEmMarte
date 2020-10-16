using apCaminhosMarte.ClassesRepresentativas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosMarte
{
    class GrafoCaminhos
    {
        LinkedList<Tuple<int, int>>[] grafoAdjacencia;

        public GrafoCaminhos(int qtdVertices)
        {
            grafoAdjacencia = new LinkedList<Tuple<int, int>>[qtdVertices];

            for (int i = 0; i < grafoAdjacencia.Length; ++i)
                grafoAdjacencia[i] = new LinkedList<Tuple<int, int>>();
        }

        public void IncluirNo(int verticeAnte, int verticeProx, int distancia)
        {
            grafoAdjacencia[verticeAnte].AddLast(new Tuple<int, int>(verticeProx, distancia));
        }

        public override string ToString()
        {
            string ret = "";
            
            int i = 0;
            foreach (LinkedList<Tuple<int, int>> list in grafoAdjacencia)
            {
                ret += "grafoAdjacencia[" + i + "]: ";

                foreach (Tuple<int, int> edge in list)
                {
                    ret += edge.Item1 + "(" + edge.Item2 + "), ";
                }

                ++i;
                ret += "\n";
            }

            return ret;
        }
    }
}
