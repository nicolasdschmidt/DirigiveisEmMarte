using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosMarte
{
    public class Caminho
    {
        private int idOrigem;
        private int idDestino;
        private int distancia;
        private int tempo;
        private int custo;

        public Caminho(int idOrigem, int idDestino, int distancia, int tempo, int custo)
        {
            IdCidadeOrigem = idOrigem;
            IdCidadeDestino = idDestino;
            Distancia = distancia;
            Tempo = tempo;
            Custo = custo;
        }

        public int IdCidadeOrigem 
        { 
            get => idOrigem;
            set
            {
                if (value < 0)
                    throw new Exception("Id da Cidade de Origem inválido");
                idOrigem = value;
            }
        }
        public int IdCidadeDestino 
        { 
            get => idDestino;
            set
            {
                if (value < 0)
                    throw new Exception("Id da Cidade Destino inválido");
                idDestino = value;
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
            return new Caminho(idOrigem, idDestino, distancia, tempo, custo);
        }

        public override string ToString()
        {
            return idOrigem + " => " + idDestino;
        }
    }
}
