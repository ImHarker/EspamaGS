using System;
using System.Collections.Generic;

namespace EspamaGS_2._0.Models
{
    public partial class Plataforma
    {
        public Plataforma()
        {
            Jogos = new HashSet<Jogo>();
        }

        public int Id { get; set; }
        public string Nome { get; set; } = null!;

        public virtual ICollection<Jogo> Jogos { get; set; }
    }
}
