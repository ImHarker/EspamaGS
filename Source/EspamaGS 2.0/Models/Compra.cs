using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EspamaGS_2._0.Models
{
    [Table("Compra")]
    public partial class Compra
    {
        [Key]
        [Column("ID_CLIENTE")]
        [StringLength(20)]
        [Unicode(false)]
        public string IdCliente { get; set; } = null!;
        [Key]
        [Column("ID_JOGO")]
        public int IdJogo { get; set; }
        [Column("DATA_COMPRA", TypeName = "datetime")]
        public DateTime DataCompra { get; set; }
        [Column("PRECO", TypeName = "money")]
        public decimal Preco { get; set; }

        [ForeignKey("IdJogo")]
        [InverseProperty("Compras")]
        public virtual Jogo IdJogoNavigation { get; set; } = null!;
    }
}
