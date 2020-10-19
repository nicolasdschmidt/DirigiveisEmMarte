using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
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
        private MatrizCaminhos Matriz { get; set; }
        private List<List<Caminho>> todosCaminhos;
        private List<int> jaVisitados;

        public BuscadorDeCaminhos(MatrizCaminhos matriz)
        {
            Matriz = matriz;
        }

        public List<List<Caminho>> BuscarCaminho(int idOrigem, int idDestino)
        {
            // inicializar as listas necessárias para execução
            todosCaminhos = new List<List<Caminho>>();
            jaVisitados = new List<int>();

            // como estamos na cidade de origem, adicioná-la à lista de cidades visitadas
            jaVisitados.Add(idOrigem);

            // chamar o método interno BuscarCaminho(), passando uma lista de Caminhos vazia para representar o conjunto de Caminhos atual
            BuscarCaminho(new List<Caminho>(), idOrigem, idDestino);

            // se não há caminhos entre as cidades selecionadas, retorna null
            if (todosCaminhos.Count() == 0) return null;

            // senão, retorna os caminhos
            return todosCaminhos;
        }

        private void BuscarCaminho(List<Caminho> caminhoAtual, int cidadeAtual, int cidadeDestino)
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
                        BuscarCaminho(caminhoAtual, i, cidadeDestino);
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
    }
}
