using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace EspamaGS_2._0.Models {
    [Table("Jogo")]
    [Index("IdCategoria", Name = "IX_Jogo_ID_CATEGORIA")]
    [Index("IdDesenvolvedora", Name = "IX_Jogo_ID_DESENVOLVEDORA")]
    [Index("IdFuncionario", Name = "IX_Jogo_ID_FUNCIONARIO")]
    [Index("IdPlataforma", Name = "IX_Jogo_ID_PLATAFORMA")]
    public partial class Jogo {
        public Jogo() {
            Carts = new HashSet<Cart>();
            Compras = new HashSet<Compra>();
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
        [Column("FOTO")]
        [StringLength(50)]
        [Unicode(false)]
        [JsonInclude]
        public string Foto { get; set; } = null!;
        [Column("DESCRICAO")]
        [StringLength(1024)]
        [Unicode(false)]
        [JsonInclude]
        public string Descricao { get; set; } = null!;
        [Column("PRECO", TypeName = "money")]
        [JsonInclude]
        public decimal Preco { get; set; }
        [Column("ID_CATEGORIA")]
        [JsonInclude]
        public int IdCategoria { get; set; }
        [Column("ID_DESENVOLVEDORA")]
        [JsonInclude]
        public int IdDesenvolvedora { get; set; }
        [Column("ID_PLATAFORMA")]
        [JsonInclude]
        public int IdPlataforma { get; set; }
        [Column("ID_FUNCIONARIO")]
        [StringLength(20)]
        [Unicode(false)]
        [JsonIgnore]
        public string IdFuncionario { get; set; } = null!;
        [Column("DATA_REGISTO", TypeName = "datetime")]
        [JsonIgnore]
        public DateTime DataRegisto { get; set; }
        [Column("DATA_LANCAMENTO", TypeName = "date")]
        [JsonInclude]
        public DateTime DataLancamento { get; set; }

        [ForeignKey("IdCategoria")]
        [InverseProperty("Jogos")]
        [JsonInclude]
        public virtual Categoria? IdCategoriaNavigation { get; set; } = null!;
        [ForeignKey("IdDesenvolvedora")]
        [InverseProperty("Jogos")]
        [JsonInclude]
        public virtual Desenvolvedora? IdDesenvolvedoraNavigation { get; set; } = null!;
        [ForeignKey("IdFuncionario")]
        [InverseProperty("Jogos")]
        [JsonIgnore]
        public virtual Funcionario? IdFuncionarioNavigation { get; set; } = null!;
        [ForeignKey("IdPlataforma")]
        [InverseProperty("Jogos")]
        [JsonInclude]
        public virtual Plataforma? IdPlataformaNavigation { get; set; } = null!;
        [InverseProperty("IdJogoNavigation")]
        [JsonIgnore]
        public virtual ICollection<Cart>? Carts { get; set; }
        [InverseProperty("IdJogoNavigation")]
        [JsonIgnore]
        public virtual ICollection<Compra>? Compras { get; set; }
    }
}
