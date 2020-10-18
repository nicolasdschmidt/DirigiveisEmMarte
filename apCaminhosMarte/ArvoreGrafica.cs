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

        public void DesenharCidades(Graphics g, PictureBox pb, double proporcaoX, double proporcaoY)
        {
            DesenharCidades(Arvore.Raiz, g, pb, proporcaoX, proporcaoY);
        }
        private void DesenharCidades(NoArvore<Cidade> noAtual, Graphics g, PictureBox pb, double proporcaoX, double proporcaoY)
        {
            if (noAtual == null) return;

            Font fonte = new Font("Arial", 12, FontStyle.Bold);

            g.DrawString(noAtual.Info.Nome, fonte, Brushes.Black, (int)Math.Round(noAtual.Info.X * proporcaoX)+10, (int)Math.Round(noAtual.Info.Y * proporcaoY) -5);
            g.DrawEllipse(new Pen(Color.Black, 4f), (int)Math.Round(noAtual.Info.X * proporcaoX), (int)Math.Round(noAtual.Info.Y * proporcaoY), 5, 5);
            g.FillEllipse(new SolidBrush(Color.Black), (int)Math.Round(noAtual.Info.X * proporcaoX), (int)Math.Round(noAtual.Info.Y * proporcaoY), 5, 5);

            DesenharCidades(noAtual.Esquerda, g, pb, proporcaoX, proporcaoY);
            DesenharCidades(noAtual.Direita, g, pb, proporcaoX, proporcaoY);
        }
        public void DesenharCaminhos(MatrizCaminhos matriz, Graphics g, PictureBox pb, double proporcaoX, double proporcaoY)
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
                        int mediaCidadesX = Convert.ToInt32(((cidadeDestino.X + cidadeOrigem.X) / 2) * proporcaoX);
                        int mediaCidadesY = Convert.ToInt32(((cidadeDestino.Y + cidadeOrigem.Y) / 2) * proporcaoY);
                        int variacao = rand.Next(-20, 20);
                        PointF[] vetorDePontosParaAsCurvas = { new PointF((int)Math.Round(cidadeOrigem.X * proporcaoX), (int)Math.Round(cidadeOrigem.Y * proporcaoY)), new PointF(mediaCidadesX+variacao, mediaCidadesY+variacao), new PointF((int)Math.Round(cidadeDestino.X * proporcaoX), (int)Math.Round(cidadeDestino.Y * proporcaoY)) };
                        g.DrawCurve(new Pen(Color.DeepSkyBlue, 2.5f), vetorDePontosParaAsCurvas, 0.5F);

                        g.DrawString(caminhoAtual.Distancia + "", fonte, new SolidBrush(Color.Blue), mediaCidadesX, mediaCidadesY);
                    }
                }
            }
        }
    }
}
