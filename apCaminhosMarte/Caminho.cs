using System;
using apCaminhosMarte;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ENZO FUREGATTI SPINELLA 19168
// NICOLAS DENADAI SCHMIDT 19191

namespace apCaminhosMarte
{
    public class Caminho
    {
        private Cidade origem;
        private Cidade destino;
        private int distancia;
        private int tempo;
        private int custo;

        public Caminho(Cidade origem, Cidade destino, int distancia, int tempo, int custo)
        {
            Origem = origem;
            Destino = destino;
            Distancia = distancia;
            Tempo = tempo;
            Custo = custo;
        }

        public Cidade Origem 
        { 
            get => origem;
            set
            {
                if (value == null)
                    throw new Exception("Origem inválida");
                origem = value;
            }
        }
        public Cidade Destino 
        { 
            get => destino;
            set
            {
                if (value == null)
                    throw new Exception("Destino inválido");
                destino = value;
            }
        }
        public int Distancia 
        { 
            get => distancia;
            set
            {
                if (value < 0)
                    throw new Exception("Valor da distância inválido");
                distancia = value;
            }
        }
        public int Tempo 
        { 
            get => tempo;
            set
            {
                if (value < 0)
                    throw new Exception("Valor do tempo de percurso inválido");
                tempo = value;
            }
        }
        public int Custo 
        {
            get => custo;
            set
            {
                if (value < 0)
                    throw new Exception("Valor do custo de percurso inválido");
                custo = value;
            }
        }

        public Caminho Clone()
        {
            return new Caminho(origem, destino, distancia, tempo, custo);
        }

        public override string ToString()
        {
            return origem.Nome + "(" + origem.Id + ")" + " => " + destino.Nome + "(" + destino.Id + ")";
        }
    }
}
