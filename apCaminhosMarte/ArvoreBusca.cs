using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apCaminhosMarte
{
    class ArvoreBusca<Dado> where Dado : IComparable<Dado>
    {
        public NoArvore<Dado> Raiz { get; set; }
        public int Qtd { get; set; }

        public ArvoreBusca()
        {
            Raiz = null;
            Qtd = 0;
        }

        public void Incluir(Dado info)
        {
            if (Raiz == null)
            {
                Raiz = new NoArvore<Dado>(info);
                Qtd = 1;
            }
            else Incluir(Raiz, info);
        }

        public Dado Buscar(Dado info)
        {
            Dado encontrado = Buscar(Raiz, info);
            return encontrado;
        }

        private void Incluir(NoArvore<Dado> noAtual, Dado info)
        {
            int equals = info.CompareTo(noAtual.Info);

            if (equals == 0) throw new Exception("Nó já existe");

            if (equals < 0)
                if (noAtual.Esquerda == null) { noAtual.Esquerda = new NoArvore<Dado>(info); Qtd++; }
                else Incluir(noAtual.Esquerda, info);
            else
                if (noAtual.Direita == null)
            {
                noAtual.Direita = new NoArvore<Dado>(info); Qtd++;
            }
            else Incluir(noAtual.Direita, info);
        }

        private Dado Buscar(NoArvore<Dado> noAtual, Dado info)
        {
            if (noAtual == null) throw new Exception("Dado não encontrado");

            int equals = info.CompareTo(noAtual.Info);

            if (equals == 0) return noAtual.Info;

            if (equals < 0) return Buscar(noAtual.Esquerda, info);
            else return Buscar(noAtual.Direita, info);
        }
    }
}
