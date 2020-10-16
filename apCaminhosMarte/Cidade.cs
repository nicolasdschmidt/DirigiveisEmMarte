using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosMarte
{
    class Cidade : IComparable<Cidade>
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Cidade (int id, string nome, int x, int y)
        {
            Id = id;
            Nome = nome;
            X = x;
            Y = y;
        }

        public int CompareTo(Cidade outra)
        {
            return Id.CompareTo(outra.Id);
        }
    }
}
