using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosMarte.ClassesRepresentativas
{
    class CaminhosEntreCidadesMarte
    {
        private int idCidadeOrigem;
        private int idCidadeDestino;
        private int distancia;
        private int tempo;
        private int custo;

        public CaminhosEntreCidadesMarte(int idOrigem, int idDestino, int distancia, int tempo, int custo)
        {
            IdCidadeOrigem = idOrigem;
            IdCidadeDestino = idDestino;
            Distancia = distancia;
            Tempo = tempo;
            Custo = custo;
        }

        public int IdCidadeOrigem 
        { 
            get => idCidadeOrigem;
            set
            {
                if (value < 0)
                    throw new Exception("Id da Cidade de Origem inválido");
                idCidadeOrigem = value;
            }
        }
        public int IdCidadeDestino 
        { 
            get => idCidadeDestino;
            set
            {
                if (value < 0)
                    throw new Exception("Id da Cidade Destino inválido");
                idCidadeDestino = value;
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
    }
}
