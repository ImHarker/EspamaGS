using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EspamaGS_2._0.Models {
    [Index("IdCategoria", Name = "IX_Preferencia_ID_CATEGORIA")]
    public partial class Preferencia {
        [Key]
        [Column("ID_CLIENTE")]
        [StringLength(20)]
        [Unicode(false)]
        public string IdCliente { get; set; } = null!;
        [Key]
        [Column("ID_CATEGORIA")]
        public int IdCategoria { get; set; }

        [ForeignKey("IdCategoria")]
        [InverseProperty("Preferencia")]
        public virtual Categoria IdCategoriaNavigation { get; set; } = null!;
    }
}