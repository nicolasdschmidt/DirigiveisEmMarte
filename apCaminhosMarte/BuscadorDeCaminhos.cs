using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apCaminhosMarte
{
    class BuscadorDeCaminhos
    {
        private MatrizCaminhos Matriz { get; set; }
        private List<List<Caminho>> todosCaminhos;
        private List<int> jaVisitados;

        public BuscadorDeCaminhos(MatrizCaminhos matriz)
        {
            Matriz = matriz;
        }

        public List<List<Caminho>> BuscarCaminho(int idOrigem, int idDestino)
        {
            todosCaminhos = new List<List<Caminho>>();
            jaVisitados = new List<int>();

            jaVisitados.Add(idOrigem);

            BuscarCaminho(new List<Caminho>(), idOrigem, idDestino);

            if (todosCaminhos.Count() == 0) return null;

            return todosCaminhos;
        }

        private void BuscarCaminho(List<Caminho> caminhoAtual, int cidadeAtual, int cidadeDestino)
        {
            for(int i = 0; i < Matriz.Tamanho; i++)
            {
                Caminho caminhoTeste = Matriz.BuscarPeloIndice(cidadeAtual, i);
                if (caminhoTeste != null && caminhoTeste.Distancia > 0 && !jaVisitados.Any(item => item == i))
                {
                    caminhoAtual.Add(caminhoTeste);

                    if (i == cidadeDestino)
                        todosCaminhos.Add(caminhoAtual.Select(c => c.Clone()).ToList());
                    else
                    {
                        jaVisitados.Add(i);
                        BuscarCaminho(caminhoAtual, i, cidadeDestino);
                        jaVisitados.Remove(i);
                    }
                    caminhoAtual.RemoveAt(caminhoAtual.Count - 1);
                }
            }
        }
    }
}
