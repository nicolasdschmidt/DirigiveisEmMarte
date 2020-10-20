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
        private List<Caminho> caminhoSelecionado;

        private double proporcaoX, proporcaoY;

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

                Cidade cidadeOrigem = new Cidade(idOrigem);
                Cidade cidadeDestino = new Cidade(idDestino);
                var caminhoAtual = new Caminho(cidadeOrigem, cidadeDestino, distancia, tempo, custo);
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

            todosCaminhos = new List<List<Caminho>>();

            var todosCaminhosTemp = buscador.BuscarCaminho(origem, destino);

            if (todosCaminhosTemp == null) MessageBox.Show("Nenhum caminho encontrado!");
            else
            {
                foreach (List<Caminho> lista in todosCaminhosTemp)
                {
                    var listaTemp = new List<Caminho>();
                    foreach (Caminho c in lista)
                    {
                        var cidadeOrigemTemp = arvoreCidades.Buscar(c.Origem);
                        var cidadeDestinoTemp = arvoreCidades.Buscar(c.Destino);
                        var caminhoTemp = new Caminho(cidadeOrigemTemp, cidadeDestinoTemp, c.Distancia, c.Tempo, c.Custo);
                        listaTemp.Add(caminhoTemp);
                    }
                    todosCaminhos.Add(listaTemp);
                }

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
                    }
                    int distanciaAtual = 0;
                    int j;
                    for (j = 0; j < todosCaminhos[i].Count; j++)
                    {
                        distanciaAtual += todosCaminhos[i][j].Distancia;
                        dgvCaminhos.Rows[i].Cells[j].Value = todosCaminhos[i][j].Origem.Nome + " (" + todosCaminhos[i][j].Origem.Id + ")";
                    }
                    dgvCaminhos.Rows[i].Cells[j].Value = todosCaminhos[i][j - 1].Destino.Nome + " (" + todosCaminhos[i][j - 1].Destino.Id + ")";

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
                    dgvMelhorCaminho.Rows[0].Cells[k].Value = melhorCaminho[k].Origem.Nome + " (" + melhorCaminho[k].Origem.Id + ")";
                }
                dgvMelhorCaminho.Rows[0].Cells[k].Value = melhorCaminho[k - 1].Destino.Nome + " (" + melhorCaminho[k - 1].Destino.Id + ")";
            }

        }

        private void pbMapa_Paint(object sender, PaintEventArgs e)
        {
            if (arvoreCidades == null) return;

            var larguraOriginal = 4096;
            var alturaOriginal = 2048;

            var largura = pbMapa.Width;
            var altura = pbMapa.Height;

            proporcaoX = (double)largura / larguraOriginal;
            proporcaoY = (double)altura / alturaOriginal;

            arvoreGrafica.DesenharCaminhos(matrizCidades, e.Graphics, pbMapa, proporcaoX, proporcaoY);
            arvoreGrafica.DesenharCidades(e.Graphics, pbMapa, proporcaoX, proporcaoY);

            if (caminhoSelecionado != null)
                arvoreGrafica.DesenharCaminho(caminhoSelecionado, e.Graphics, pbArvore, proporcaoX, proporcaoY);
        }

        private void pbArvore_Paint(object sender, PaintEventArgs e)
        {
            if (arvoreCidades == null) return;

            arvoreGrafica.DesenharArvore(true, e.Graphics, pbArvore.Width / 2, 0, Math.PI / 2, Math.PI / 2.2, 300);
        }

        private void dgvMelhorCaminho_SelectionChanged(object sender, EventArgs e)
        {
            if (todosCaminhos == null) return;
            try
            {
                caminhoSelecionado = melhorCaminho;
                pbMapa.Refresh();
            }
            catch (Exception) { }
        }

        private void dgvCaminhos_SelectionChanged(object sender, EventArgs e)
        {
            if (todosCaminhos == null) return;
            try
            {
                caminhoSelecionado = todosCaminhos[dgvCaminhos.CurrentCell.RowIndex];
                pbMapa.Refresh();
            }
            catch (Exception) { }
        }
    }
}
;