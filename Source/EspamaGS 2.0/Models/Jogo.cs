using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int Id { get; set; }
        [Column("NOME")]
        [StringLength(50)]
        [Unicode(false)]
        public string Nome { get; set; } = null!;
        [Column("FOTO")]
        [StringLength(50)]
        [Unicode(false)]
        public string Foto { get; set; } = null!;
        [Column("DESCRICAO")]
        [StringLength(255)]
        [Unicode(false)]
        public string Descricao { get; set; } = null!;
        [Column("PRECO", TypeName = "money")]
        public decimal Preco { get; set; }
        [Column("ID_CATEGORIA")]
        public int IdCategoria { get; set; }
        [Column("ID_DESENVOLVEDORA")]
        public int IdDesenvolvedora { get; set; }
        [Column("ID_PLATAFORMA")]
        public int IdPlataforma { get; set; }
        [Column("ID_FUNCIONARIO")]
        [StringLength(20)]
        [Unicode(false)]
        public string IdFuncionario { get; set; } = null!;
        [Column("DATA_REGISTO", TypeName = "datetime")]
        public DateTime DataRegisto { get; set; }
        [Column("DATA_LANCAMENTO", TypeName = "date")]
        public DateTime DataLancamento { get; set; }

        [ForeignKey("IdCategoria")]
        [InverseProperty("Jogos")]
        public virtual Categoria IdCategoriaNavigation { get; set; } = null!;
        [ForeignKey("IdDesenvolvedora")]
        [InverseProperty("Jogos")]
        public virtual Desenvolvedora IdDesenvolvedoraNavigation { get; set; } = null!;
        [ForeignKey("IdFuncionario")]
        [InverseProperty("Jogos")]
        public virtual Funcionario IdFuncionarioNavigation { get; set; } = null!;
        [ForeignKey("IdPlataforma")]
        [InverseProperty("Jogos")]
        public virtual Plataforma IdPlataformaNavigation { get; set; } = null!;
        [InverseProperty("IdJogoNavigation")]
        public virtual ICollection<Cart> Carts { get; set; }
        [InverseProperty("IdJogoNavigation")]
        public virtual ICollection<Compra> Compras { get; set; }
    }
}
