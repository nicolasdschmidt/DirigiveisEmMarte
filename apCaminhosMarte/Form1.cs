using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apCaminhosMarte
{
    public partial class Form1 : Form
    {
        private ArvoreBusca<Cidade> arvoreCidades;
        private ArvoreGrafica arvoreGrafica;
        private MatrizCaminhos matrizCidades;
        private List<List<Caminho>> todosCaminhos;
        private List<Caminho> melhorCaminho;

        public Form1()
        {
            InitializeComponent();
            dgvMelhorCaminho.RowCount = 1;
            dgvCaminhos.RowCount = 0;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            arvoreCidades = new ArvoreBusca<Cidade>();
            LerArquivoCidades("CidadesMarte.txt");
            arvoreGrafica = new ArvoreGrafica(arvoreCidades);
            matrizCidades = new MatrizCaminhos(arvoreCidades.Qtd);
            LerArquivoCaminhos("CaminhosEntreCidadesMarte.txt");
            Application.DoEvents();
            pbMapa.Refresh();
        }

        private void LerArquivoCidades(string nomeArquivo)
        {
            if (!File.Exists(nomeArquivo))
                throw new Exception($"Arquivo {nomeArquivo} não encontrado");

            var reader = new StreamReader(nomeArquivo);

            string linhaAtual;
            while ((linhaAtual = reader.ReadLine()) != null)
            {
                var id = int.Parse(linhaAtual.Substring(0, 3));
                var nome = linhaAtual.Substring(3, 15);
                var x = int.Parse(linhaAtual.Substring(18, 5));
                var y = int.Parse(linhaAtual.Substring(23, 5));

                var cidadeAtual = new Cidade(id, nome, x, y);
                arvoreCidades.Incluir(cidadeAtual);
            }
        }

        private void LerArquivoCaminhos(string nomeArquivo)
        {
            if (!File.Exists(nomeArquivo))
                throw new Exception($"Arquivo {nomeArquivo} não encontrado");

            var reader = new StreamReader(nomeArquivo);

            string linhaAtual;
            while ((linhaAtual = reader.ReadLine()) != null && linhaAtual.TrimEnd() != "")
            {
                var idOrigem = int.Parse(linhaAtual.Substring(0, 3));
                var idDestino = int.Parse(linhaAtual.Substring(3, 3));
                var distancia = int.Parse(linhaAtual.Substring(6, 5));
                var tempo = int.Parse(linhaAtual.Substring(11, 4));
                var custo = int.Parse(linhaAtual.Substring(15, 5));

                var caminhoAtual = new Caminho(idOrigem, idDestino, distancia, tempo, custo);
                matrizCidades.Incluir(caminhoAtual);
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            dgvCaminhos.Rows.Clear();
            dgvMelhorCaminho.Rows.Clear();
            dgvMelhorCaminho.RowCount = 1;
            int origem = lsbOrigem.SelectedIndex;
            int destino = lsbDestino.SelectedIndex;

            var buscador = new BuscadorDeCaminhos(matrizCidades);


            todosCaminhos = buscador.BuscarCaminho(origem, destino);
            if (todosCaminhos == null) MessageBox.Show("Nenhum caminho encontrado!");
            else
            {
                int menorDistancia = -1;
                melhorCaminho = null;

                for (int i = 0; i < todosCaminhos.Count; i++)
                {
                    dgvCaminhos.RowCount += 1;
                    dgvCaminhos.ColumnCount = todosCaminhos[i].Count;
                    int distanciaAtual = 0;
                    for (int j = 0; j < todosCaminhos[i].Count; j++)
                    {
                        distanciaAtual += todosCaminhos[i][j].Distancia;
                        dgvCaminhos.Rows[i].Cells[j].Value = todosCaminhos[i][j];
                    }

                    if (distanciaAtual < menorDistancia || menorDistancia < 0)
                    {
                        menorDistancia = distanciaAtual;
                        melhorCaminho = todosCaminhos[i];
                    }
                }

                //string melhorCaminhoString = "";
                int contagemDePassosDoCaminho = 0;
                dgvMelhorCaminho.ColumnCount = melhorCaminho.Count;
                foreach (Caminho c in melhorCaminho)
                { 
                    //melhorCaminhoString += c + "; ";
                    dgvMelhorCaminho.Rows[0].Cells[contagemDePassosDoCaminho].Value = c;
                    contagemDePassosDoCaminho++;
                }

                //MessageBox.Show("melhor caminho: " + melhorCaminhoString);
            }
            
        }

        private void pbMapa_Paint(object sender, PaintEventArgs e)
        {
            if (arvoreCidades == null) return;

            var larguraOriginal = 4096;
            var alturaOriginal = 2048;

            var largura = pbMapa.Width;
            var altura = pbMapa.Height;

            var proporcaoX = (double)largura / larguraOriginal;
            var proporcaoY = (double)altura / alturaOriginal;

            arvoreGrafica.DesenharCaminhos(matrizCidades, e.Graphics, pbMapa, proporcaoX, proporcaoY);
            arvoreGrafica.DesenharCidades(e.Graphics, pbMapa, proporcaoX, proporcaoY);
            
        }

        private void tpArvore_Paint(object sender, PaintEventArgs e)
        {
            //arvoreMarte.DesenharArvore(true, arvoreMarte.Raiz, (int)tpArvore.Width / 2, 0, Math.PI / 2, Math.PI / 2.5, 300, g);
            arvoreGrafica.DesenharArvore(e.Graphics, (int)tpArvore.Width / 2, 0, Math.PI / 2, Math.PI / 2.5, 300);
        }
    }
}
;