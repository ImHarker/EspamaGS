using System;
using System.Collections.Generic;

namespace EspamaGS_2._0.Models {
    public partial class Compra {
        public int IdCliente { get; set; }
        public int IdJogo { get; set; }
        public DateTime DataCompra { get; set; }
        public decimal Preco { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; } = null!;
        public virtual Jogo IdJogoNavigation { get; set; } = null!;
    }
}
