using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apCaminhosMarte
{
    class ArvoreGrafica
    {
        public ArvoreBusca<Cidade> Arvore { get; set; }

        public ArvoreGrafica(ArvoreBusca<Cidade> arvore)
        {
            Arvore = arvore;
        }

        public void DesenharCidades(Graphics g, PictureBox pb, double proporcao)
        {
            DesenharCidades(Arvore.Raiz, g, pb, proporcao);
        }

        private void DesenharCidades(NoArvore<Cidade> noAtual, Graphics g, PictureBox pb, double proporcao)
        {
            if (noAtual == null) return;

            Font fonte = new Font("Arial", 12, FontStyle.Bold);

            g.DrawString(noAtual.Info.Nome, fonte, Brushes.Black, (int)Math.Round(noAtual.Info.X * proporcao), (int)Math.Round(noAtual.Info.Y * proporcao));

            DesenharCidades(noAtual.Esquerda, g, pb, proporcao);
            DesenharCidades(noAtual.Direita, g, pb, proporcao);
        }
    }
}
