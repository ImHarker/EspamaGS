using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EspamaGS_2._0.Models
{
    [Table("Cart")]
    public partial class Cart
    {
        [Key]
        public int Id { get; set; }
        public int IdJogo { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string IdCliente { get; set; } = null!;

        [ForeignKey("IdJogo")]
        [InverseProperty("Carts")]
        public virtual Jogo IdJogoNavigation { get; set; } = null!;
    }
}
