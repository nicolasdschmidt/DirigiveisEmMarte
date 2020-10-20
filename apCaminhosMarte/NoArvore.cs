using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
// ENZO FUREGATTI SPINELLA 19168
// NICOLAS DENADAI SCHMIDT 19191
namespace apCaminhosMarte
{
    /// <summary>
    /// Classe que representa um nó da árvore binaria (ArvoreBusca)
    /// usada no programa.
    /// </summary>
    class NoArvore<Dado>
    {
        NoArvore<Dado> esquerda, direita;

        public Dado Info { get; set; }
        public NoArvore<Dado> Esquerda { get => esquerda; set => esquerda = value; }
        public NoArvore<Dado> Direita { get => direita; set => direita = value; }

        public NoArvore(Dado info)
        {
            Info = info;
            esquerda = null;
            direita = null;
        }
    }
}
