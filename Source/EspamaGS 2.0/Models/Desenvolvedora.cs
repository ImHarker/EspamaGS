using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EspamaGS_2._0.Models
{
    [Table("Desenvolvedora")]
    public partial class Desenvolvedora
    {
        public Desenvolvedora()
        {
            Jogos = new HashSet<Jogo>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("NOME")]
        [StringLength(50)]
        [Unicode(false)]
        public string Nome { get; set; } = null!;

        [InverseProperty("IdDesenvolvedoraNavigation")]
        public virtual ICollection<Jogo> Jogos { get; set; }
    }
}
