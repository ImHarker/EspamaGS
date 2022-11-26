using System;
using System.Collections.Generic;

namespace EspamaGS_2._0.Models {
    public partial class Categoria {
        public Categoria() {
            Jogos = new HashSet<Jogo>();
            IdClientes = new HashSet<Cliente>();
        }

        public int Id { get; set; }
        public string Nome { get; set; } = null!;

        public virtual ICollection<Jogo> Jogos { get; set; }

        public virtual ICollection<Cliente> IdClientes { get; set; }
    }
}
