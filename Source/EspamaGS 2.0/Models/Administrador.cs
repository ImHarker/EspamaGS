using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EspamaGS_2._0.Models
{
    [Table("Administrador")]
    [Index("IdAdmin", Name = "IX_Administrador_ID_ADMIN")]
    public partial class Administrador
    {
        public Administrador()
        {
            Funcionarios = new HashSet<Funcionario>();
            InverseIdAdminNavigation = new HashSet<Administrador>();
        }

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
        [InverseProperty("InverseIdAdminNavigation")]
        public virtual Administrador IdAdminNavigation { get; set; } = null!;
        [InverseProperty("IdAdminNavigation")]
        public virtual ICollection<Funcionario> Funcionarios { get; set; }
        [InverseProperty("IdAdminNavigation")]
        public virtual ICollection<Administrador> InverseIdAdminNavigation { get; set; }
    }
}
