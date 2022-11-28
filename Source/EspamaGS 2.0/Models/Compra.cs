using System;
using System.Collections.Generic;

namespace EspamaGS_2._0.Models
{
    public partial class Compra
    {
        public string IdCliente { get; set; } = null!;
        public int IdJogo { get; set; }
        public DateTime DataCompra { get; set; }
        public decimal Preco { get; set; }

        public virtual Jogo IdJogoNavigation { get; set; } = null!;
    }
}
