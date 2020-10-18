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

            g.DrawString(noAtual.Info.Nome, fonte, Brushes.Black, (int)Math.Round(noAtual.Info.X * proporcao)+10, (int)Math.Round(noAtual.Info.Y * proporcao)-5);
            g.DrawEllipse(new Pen(Color.Black, 4f), (int)Math.Round(noAtual.Info.X * proporcao), (int)Math.Round(noAtual.Info.Y * proporcao), 5, 5);
            g.FillEllipse(new SolidBrush(Color.Black), (int)Math.Round(noAtual.Info.X * proporcao), (int)Math.Round(noAtual.Info.Y * proporcao), 5, 5);

            DesenharCidades(noAtual.Esquerda, g, pb, proporcao);
            DesenharCidades(noAtual.Direita, g, pb, proporcao);
        }
        public void DesenharCaminhos(MatrizCaminhos matriz, Graphics g, PictureBox pb, double proporcao)
        {
            Caminho caminhoAtual;
            Font fonte = new Font("Arial", 12, FontStyle.Bold);
            for (int i = 0; i < Arvore.Qtd; i++)
            {
                for(int j = 0; j < Arvore.Qtd; j++)
                {
                    if ((caminhoAtual = matriz.BuscarPeloIndice(i, j)) != null)
                    {
                        Cidade cidadeOrigem = new Cidade(caminhoAtual.IdCidadeOrigem);
                        Cidade cidadeDestino = new Cidade(caminhoAtual.IdCidadeDestino);

                        cidadeOrigem = Arvore.Buscar(cidadeOrigem);
                        cidadeDestino = Arvore.Buscar(cidadeDestino);
                        Random rand = new Random();
                        //g.DrawLine(new Pen(Color.AliceBlue), (int)Math.Round(cidadeOrigem.X * proporcao), (int)Math.Round(cidadeOrigem.Y * proporcao), (int)Math.Round(cidadeDestino.X * proporcao), (int)Math.Round(cidadeDestino.Y * proporcao));
                        int mediaCidadesX = Convert.ToInt32(((cidadeDestino.X + cidadeOrigem.X) / 2) * proporcao);
                        int mediaCidadesY = Convert.ToInt32(((cidadeDestino.Y + cidadeOrigem.Y) / 2) * proporcao);
                        int variacao = rand.Next(-20, 20);
                        PointF[] vetorDePontosParaAsCurvas = { new PointF((int)Math.Round(cidadeOrigem.X * proporcao), (int)Math.Round(cidadeOrigem.Y * proporcao)), new PointF(mediaCidadesX+variacao, mediaCidadesY+variacao), new PointF((int)Math.Round(cidadeDestino.X * proporcao), (int)Math.Round(cidadeDestino.Y * proporcao)) };
                        g.DrawCurve(new Pen(Color.DeepSkyBlue, 2.5f), vetorDePontosParaAsCurvas, 0.5F);

                        g.DrawString(caminhoAtual.Distancia + "", fonte, new SolidBrush(Color.Blue), mediaCidadesX, mediaCidadesY);
                    }
                }
            }
        }
    }
}
