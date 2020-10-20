using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// ENZO FUREGATTI SPINELLA 19168
// NICOLAS DENADAI SCHMIDT 19191

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
        public void DesenharArvore(bool primeiraVez, Graphics g, int x, int y, double angulo, double incremento, double comprimento)
        {
            DesenharArvore(primeiraVez, Arvore.Raiz, g, x, y, angulo, incremento, comprimento);
        }

        public void DesenharCaminho(List<Caminho> caminhoAtual, Graphics g, PictureBox pb, double proporcaoX, double proporcaoY)
        {
            Pen pen = new Pen(Color.DarkBlue, 3.0f);
            foreach (Caminho c in caminhoAtual)
            {
                g.DrawLine(pen, (int)Math.Round(c.Origem.X * proporcaoX), (int)Math.Round(c.Origem.Y * proporcaoY), (int)Math.Round(c.Destino.X * proporcaoX), (int)Math.Round(c.Destino.Y * proporcaoY));
            }
        }

        private void DesenharCidades(NoArvore<Cidade> noAtual, Graphics g, PictureBox pb, double proporcaoX, double proporcaoY)
        {
            if (noAtual == null) return;

            Font fonte = new Font("Arial", 12, FontStyle.Bold);

            g.DrawString(noAtual.Info.Nome, fonte, Brushes.Black, (int)Math.Round(noAtual.Info.X * proporcaoX) + 10, (int)Math.Round(noAtual.Info.Y * proporcaoY) - 5);
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
                for (int j = 0; j < Arvore.Qtd; j++)
                {
                    if ((caminhoAtual = matriz.BuscarPeloIndice(i, j)) != null)
                    {
                        Cidade cidadeOrigem = Arvore.Buscar(caminhoAtual.Origem);
                        Cidade cidadeDestino = Arvore.Buscar(caminhoAtual.Destino);

                        Random rand = new Random();
                        int mediaCidadesX = Convert.ToInt32(((cidadeDestino.X + cidadeOrigem.X) / 2) * proporcaoX);
                        int mediaCidadesY = Convert.ToInt32(((cidadeDestino.Y + cidadeOrigem.Y) / 2) * proporcaoY);
                        int variacao = rand.Next(-20, 20);

                        PointF[] vetorDePontosParaAsCurvas = { new PointF((int)Math.Round(cidadeOrigem.X * proporcaoX),(int)Math.Round(cidadeOrigem.Y * proporcaoY)),
                                                               new PointF(mediaCidadesX+variacao, mediaCidadesY+variacao),
                                                               new PointF((int)Math.Round(cidadeDestino.X * proporcaoX), (int)Math.Round(cidadeDestino.Y * proporcaoY)) };
                        g.DrawCurve(new Pen(Color.DeepSkyBlue, 2.5f), vetorDePontosParaAsCurvas, 0.5F);
                        g.DrawString(caminhoAtual.Distancia + "", fonte, new SolidBrush(Color.Blue), mediaCidadesX, mediaCidadesY);
                    }
                }
            }
        }
        private void DesenharArvore(bool primeiraVez, NoArvore<Cidade> noAtual, Graphics g, int x, int y, double angulo, double incremento, double comprimento)
        {
            int xf, yf;
            if (noAtual != null)
            {
                Pen caneta = new Pen(Color.Brown);
                xf = (int)Math.Round(x + Math.Cos(angulo) * (comprimento + 10));
                yf = (int)Math.Round(y + Math.Sin(angulo) * (comprimento + 10));
                if (primeiraVez)
                    yf = 25;
                g.DrawLine(caneta, x, y, xf, yf);

                DesenharArvore(false, noAtual.Esquerda, g, xf, yf, Math.PI / 2 + incremento,
                                       incremento * 0.55, comprimento * 0.77);
                DesenharArvore(false, noAtual.Direita, g, xf, yf, Math.PI / 2 - incremento,
                                       incremento * 0.55, comprimento * 0.77);

                SolidBrush preenchimento = new SolidBrush(Color.ForestGreen);
                g.FillEllipse(preenchimento, xf - 35, yf - 25, 72, 45);
                g.DrawString(Convert.ToString(noAtual.Info.ToString()), new Font("Times New Roman", 10),
                    new SolidBrush(Color.Black), xf - 35, yf - 7);

            }
        }
    }
}