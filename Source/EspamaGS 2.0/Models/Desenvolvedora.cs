using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
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
        [JsonInclude]
        public int Id { get; set; }
        [Column("NOME")]
        [StringLength(50)]
        [Unicode(false)]
        [JsonInclude]
        public string Nome { get; set; } = null!;

        [InverseProperty("IdDesenvolvedoraNavigation")]
        [JsonIgnore]
        public virtual ICollection<Jogo> Jogos { get; set; }
    }
}
