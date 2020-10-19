using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// ENZO FUREGATTI SPINELLA 19168
// NICOLAS DENADAI SCHMIDT 19191
namespace apCaminhosMarte
{
    /// <summary>
    /// Classe que armazena Dados na forma de uma Árvore
    /// Binária de Busca, como pedido na proposta do projeto.
    /// </summary>
    /// <typeparam name="Dado">Tipo</typeparam>
    class ArvoreBusca<Dado> where Dado : IComparable<Dado>
    {
        public NoArvore<Dado> Raiz { get; set; }
        public int Qtd { get; set; }

        public ArvoreBusca()
        {
            Raiz = null;
            Qtd = 0;
        }

        // método público de inclusão
        public void Incluir(Dado info)
        {
            // se a raiz for nula, definimos info como o primeiro Nó
            if (Raiz == null)
            {
                Raiz = new NoArvore<Dado>(info);
                Qtd = 1;
            }
            // senão, chamamos a função interna recursiva Incluir
            else Incluir(Raiz, info);
        }

        // método público de busca
        public Dado Buscar(Dado info)
        {
            // chamamos a função interna recursiva Buscar
            Dado encontrado = Buscar(Raiz, info);
            return encontrado;
        }

        // método interno de inclusão recursiva
        private void Incluir(NoArvore<Dado> noAtual, Dado info)
        {
            // compara o valor armazenado no Nó atual com o valor que queremos armazenar
            int equals = info.CompareTo(noAtual.Info);

            // se for o mesmo
            if (equals == 0) throw new Exception("Nó já existe");

            // senão, se o nosso valor for menor que o do Nó atual, o valor deve 'ir' para a esquerda:
            if (equals < 0)
            {
                // se o Nó da esquerda for null, armazenamos lá
                if (noAtual.Esquerda == null) { noAtual.Esquerda = new NoArvore<Dado>(info); Qtd++; }
                // senão, chamamos o mesmo método no Nó da esquerda
                else Incluir(noAtual.Esquerda, info);
            }
            // mas se o valor for maior do que o do Nó atual, o valor deve 'ir' para a direita:
            else
            {
                // se o Nó da direita for null, armazenamos lá
                if (noAtual.Direita == null)
                    { noAtual.Direita = new NoArvore<Dado>(info); Qtd++; }
                // senão, chamamos o mesmo método no Nó da direita
                else Incluir(noAtual.Direita, info);
            }
        }

        // método interno de busca recursiva, usado, por exemplo, para adquirir os dados completos de uma Cidade sobre a qual só sabemos o id
        private Dado Buscar(NoArvore<Dado> noAtual, Dado info)
        {
            // se chegamos numa das folhas e ainda não encontramos o dado, ele não está nessa Árvore
            if (noAtual == null) throw new Exception("Dado não encontrado");

            // compara o valor armazenado no Nó atual com o valor que estamos procurando
            int equals = info.CompareTo(noAtual.Info);

            // se encontramos, retornamos o Dado
            if (equals == 0) return noAtual.Info;

            // senão, se o nosso valor for menor que o do Nó atual, a busca continua para a esquerda:
            if (equals < 0) return Buscar(noAtual.Esquerda, info);
            // mas se o nosso valor for maior que o do Nó atual, a busca continua para a direita:
            else return Buscar(noAtual.Direita, info);
        }
    }
}
