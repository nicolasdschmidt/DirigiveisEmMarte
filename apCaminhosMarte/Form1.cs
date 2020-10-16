using apCaminhosMarte.ClassesRepresentativas;
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

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            arvoreCidades = new ArvoreBusca<Cidade>();
            LerArquivoCidades("CidadesMarte.txt");
            LerArquivoCaminhos("CaminhosEntreCidadesMarte.txt");
            arvoreGrafica = new ArvoreGrafica(arvoreCidades);
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
            while ((linhaAtual = reader.ReadLine()) != null)
            {
                var idOrigem = int.Parse(linhaAtual.Substring(0, 3));
                var idDestino = int.Parse(linhaAtual.Substring(3, 3));
                var distancia = int.Parse(linhaAtual.Substring(6, 5));
                var tempo = int.Parse(linhaAtual.Substring(11, 4));
                var custo = int.Parse(linhaAtual.Substring(15, 5));

                var caminhoAtual = new CaminhosEntreCidadesMarte(idOrigem, idDestino, distancia, tempo, custo);
            }
        }

        private void TxtCaminhos_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Buscar caminhos entre cidades selecionadas");
        }

        private void pbMapa_Paint(object sender, PaintEventArgs e)
        {
            if (arvoreCidades == null) return;

            var larguraOriginal = 4096;
            var largura = pbMapa.Width;

            var proporcao = (double)largura / larguraOriginal;

            arvoreGrafica.DesenharCidades(e.Graphics, pbMapa, proporcao);
        }
    }
}
