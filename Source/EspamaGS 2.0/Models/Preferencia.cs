using System;
using System.Collections.Generic;

namespace EspamaGS_2._0.Models
{
    public partial class Preferencia
    {
        public string IdCliente { get; set; } = null!;
        public int IdCategoria { get; set; }

        public virtual Categoria IdCategoriaNavigation { get; set; } = null!;
    }
}
