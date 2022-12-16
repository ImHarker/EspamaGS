using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace EspamaGS_2._0.Models {
    public partial class Categoria {
        public Categoria() {
            Jogos = new HashSet<Jogo>();
            Preferencia = new HashSet<Preferencia>();
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

        [InverseProperty("IdCategoriaNavigation")]
        [JsonIgnore]
        public virtual ICollection<Jogo> Jogos { get; set; }
        [InverseProperty("IdCategoriaNavigation")]
        [JsonIgnore]
        public virtual ICollection<Preferencia> Preferencia { get; set; }
    }
}