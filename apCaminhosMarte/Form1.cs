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

// ENZO FUREGATTI SPINELLA 19168
// NICOLAS DENADAI SCHMIDT 19191

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
            tpArvore.BackColor = Color.LightGreen;
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
            pbArvore.Refresh();
        }

        private void LerArquivoCidades(string nomeArquivo)
        {
            if (!File.Exists(nomeArquivo))
                throw new Exception($"Arquivo {nomeArquivo} não encontrado");

            var reader = new StreamReader(nomeArquivo);

            string linhaAtual;
            while ((linhaAtual = reader.ReadLine()) != null)
            {
                var id = int.Parse(linhaAtual.Substring(0, 3).Trim());
                var nome = linhaAtual.Substring(3, 15).Trim();
                var x = int.Parse(linhaAtual.Substring(18, 5).Trim());
                var y = int.Parse(linhaAtual.Substring(23, 5).Trim());

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
                var idOrigem = int.Parse(linhaAtual.Substring(0, 3).Trim());
                var idDestino = int.Parse(linhaAtual.Substring(3, 3).Trim());
                var distancia = int.Parse(linhaAtual.Substring(6, 5).Trim());
                var tempo = int.Parse(linhaAtual.Substring(11, 4).Trim());
                var custo = int.Parse(linhaAtual.Substring(15, 5).Trim());

                Cidade cidadeExemplo = new Cidade(idOrigem);
                Cidade cidadeExemplo2 = new Cidade(idDestino);
                var caminhoAtual = new Caminho(idOrigem, idDestino, distancia, tempo, custo, arvoreCidades.Buscar(cidadeExemplo).Nome, arvoreCidades.Buscar(cidadeExemplo2).Nome);
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

                int colunas = 0;
                for (int i = 0; i < todosCaminhos.Count; i++)
                {
                    dgvCaminhos.RowCount += 1;
                    int novasColunas = todosCaminhos[i].Count + 1;
                    if (novasColunas > colunas)
                    {
                        dgvCaminhos.ColumnCount = todosCaminhos[i].Count + 1;
                        colunas = novasColunas;
                        foreach (DataGridViewColumn c in dgvCaminhos.Columns)
                        {
                            c.Width = 80;
                        }
                    }
                    int distanciaAtual = 0;
                    int j;
                    for (j = 0; j < todosCaminhos[i].Count; j++)
                    {
                        distanciaAtual += todosCaminhos[i][j].Distancia;
                        dgvCaminhos.Rows[i].Cells[j].Value = todosCaminhos[i][j].NomeCidadeOrigem + "(" + todosCaminhos[i][j].IdCidadeOrigem + ")";
                        //dgvCaminhos.Rows[i].Cells[j].Value = todosCaminhos[i][j].IdCidadeOrigem;
                        //dgvCaminhos.Rows[i].Cells[j].Value = todosCaminhos[i][j].ToString();
                    }
                    //dgvCaminhos.Rows[i].Cells[j].Value = todosCaminhos[i][j - 1].IdCidadeDestino;
                    dgvCaminhos.Rows[i].Cells[j].Value = todosCaminhos[i][j - 1].NomeCidadeDestino + "(" + todosCaminhos[i][j - 1].IdCidadeDestino + ")";

                    if (distanciaAtual < menorDistancia || menorDistancia < 0)
                    {
                        menorDistancia = distanciaAtual;
                        melhorCaminho = todosCaminhos[i];
                    }
                }

                dgvMelhorCaminho.ColumnCount = melhorCaminho.Count + 1;
                int k;
                for (k = 0; k < melhorCaminho.Count; k++)
                {
                    dgvMelhorCaminho.Rows[0].Cells[k].Value = melhorCaminho[k].NomeCidadeOrigem + "(" + melhorCaminho[k].IdCidadeOrigem + ")";
                    //dgvMelhorCaminho.Rows[0].Cells[k].Value = melhorCaminho[k].IdCidadeOrigem;
                    //dgvMelhorCaminho.Rows[0].Cells[k].Value = melhorCaminho[k].ToString();
                }
                //dgvMelhorCaminho.Rows[0].Cells[k].Value = melhorCaminho[k - 1].IdCidadeDestino;
                dgvMelhorCaminho.Rows[0].Cells[k].Value = melhorCaminho[k - 1].NomeCidadeDestino + "(" + melhorCaminho[k - 1].IdCidadeDestino + ")";
                foreach (DataGridViewColumn c in dgvMelhorCaminho.Columns)
                {
                    c.Width = 80;
                }
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

        private void pbArvore_Paint(object sender, PaintEventArgs e)
        {
            if (arvoreCidades == null) return;

            arvoreGrafica.DesenharArvore(true, e.Graphics, (int)pbArvore.Width / 2, 0, Math.PI / 2, Math.PI / 2.2, 300);
        }
    }
}
;