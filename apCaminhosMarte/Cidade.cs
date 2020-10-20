using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ENZO FUREGATTI SPINELLA 19168
// NICOLAS DENADAI SCHMIDT 19191

namespace apCaminhosMarte
{
    public class Cidade : IComparable<Cidade>
    {
        private int id;
        private string nome;
        private int x;
        private int y;

        public Cidade (int id, string nome, int x, int y)
        {
            Id = id;
            Nome = nome;
            X = x;
            Y = y;
        }

        public Cidade(int id)
        {
            Id = id;
        }

        public Cidade(Cidade c)
        {
            Id = c.Id;
            Nome = c.Nome;
            X = c.X;
            Y = c.Y;
        }

        public int Id
        {
            get => id;
            set
            {
                if (value < 0)
                    throw new Exception("Id inválido");
                id = value;
            }
        }

        public string Nome
        {
            get => nome;
            set {
                if (value == null)
                    throw new Exception("Nome inválido");
                nome = value;
            }
        }

        public int X
        {
            get => x;
            set
            {
                if (value < 0)
                    throw new Exception("Coordenada X inválida");
                x = value;
            }
        }
        public int Y
        {
            get => y;
            set
            {
                if (value < 0)
                    throw new Exception("Coordenada Y inválida");
                y = value;
            }
        }

        override
        public String ToString()
        {
            string ret = "";
            ret += $"{Id},{Nome}";
            return ret;
        }
        public int CompareTo(Cidade outra)
        {
            if (this.Id > outra.Id)
                return 777;
            if (this.Id < outra.Id)
                return -777;

            return 0;
        }
        /*public int CompareTo(Cidade outra)
        {
            return Id.CompareTo(outra.Id);
        }*/

        public Cidade Clone()
        {
            return new Cidade(this);
        }
    }
}
