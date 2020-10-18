using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosMarte
{
    class Cidade : IComparable<Cidade>
    {
        private int idCidade;
        private string nomeCidade;
        private int coordenadaX;
        private int coordenadaY;

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
        public int Id
        {
            get => idCidade;
            set
            {
                if (value < 0)
                    throw new Exception("Id inválido");
                idCidade = value;
            }
        }
        public string Nome
        {
            get => nomeCidade;
            set
            {
                if (value.Equals("") || value == null)
                    throw new Exception("Nome inválido");
                nomeCidade = value;
            }
        }
        public int X
        {
            get => coordenadaX;
            set
            {
                if (value < 0)
                    throw new Exception("Coordenada X inválida");
                coordenadaX = value;
            }
        }
        public int Y
        {
            get => coordenadaY;
            set
            {
                if (value < 0)
                    throw new Exception("Coordenada Y inválida");
                coordenadaY = value;
            }
        }

        override
        public String ToString()
        {
            string ret = "";
            ret += $"{Id},{Nome},{X},{Y}|";
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
    }
}
