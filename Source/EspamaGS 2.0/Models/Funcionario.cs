using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EspamaGS_2._0.Models
{
    [Table("Funcionario")]
    public partial class Funcionario
    {
        public Funcionario()
        {
            Jogos = new HashSet<Jogo>();
        }

        [Column("TELEFONE")]
        [StringLength(9)]
        [Unicode(false)]
        public string Telefone { get; set; } = null!;
        [Key]
        [Column("ID_UTILIZADOR")]
        [StringLength(20)]
        [Unicode(false)]
        public string IdUtilizador { get; set; } = null!;
        [Column("ID_ADMIN")]
        [StringLength(20)]
        [Unicode(false)]
        public string IdAdmin { get; set; } = null!;
        [Column("DATA_REGISTO", TypeName = "datetime")]
        public DateTime DataRegisto { get; set; }

        [ForeignKey("IdAdmin")]
        [InverseProperty("Funcionarios")]
        public virtual Administrador IdAdminNavigation { get; set; } = null!;
        [InverseProperty("IdFuncionarioNavigation")]
        public virtual ICollection<Jogo> Jogos { get; set; }
    }
}
