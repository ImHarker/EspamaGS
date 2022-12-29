using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace EspamaGS_2._0.Models {
    [Table("Plataforma")]
    public partial class Plataforma {
        public Plataforma() {
            Jogos = new HashSet<Jogo>();
        }

        [Key]
        [Column("ID")]
        [JsonInclude]
        public int Id { get; set; }
        [Column("NOME")]
        [StringLength(20)]
        [Unicode(false)]
        [JsonInclude]
        public string Nome { get; set; } = null!;

        [InverseProperty("IdPlataformaNavigation")]
        [JsonIgnore]
        public virtual ICollection<Jogo> Jogos { get; set; }
    }
}
