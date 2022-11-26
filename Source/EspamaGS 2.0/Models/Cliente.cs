using System;
using System.Collections.Generic;

namespace EspamaGS_2._0.Models {
    public partial class Cliente {
        public Cliente() {
            Compras = new HashSet<Compra>();
            IdCategoria = new HashSet<Categoria>();
        }

        public bool Estado { get; set; }
        public int IdUtilizador { get; set; }

        public virtual Utilizador IdUtilizadorNavigation { get; set; } = null!;
        public virtual ICollection<Compra> Compras { get; set; }

        public virtual ICollection<Categoria> IdCategoria { get; set; }
    }
}
