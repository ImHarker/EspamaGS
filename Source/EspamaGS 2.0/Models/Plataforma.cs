using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EspamaGS_2._0.Models
{
    [Table("Plataforma")]
    public partial class Plataforma
    {
        public Plataforma()
        {
            Jogos = new HashSet<Jogo>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("NOME")]
        [StringLength(10)]
        [Unicode(false)]
        public string Nome { get; set; } = null!;

        [InverseProperty("IdPlataformaNavigation")]
        public virtual ICollection<Jogo> Jogos { get; set; }
    }
}
