using System;
using System.Collections.Generic;

namespace EspamaGS_2._0.Models {
    public partial class Administrador {
        public Administrador() {
            Funcionarios = new HashSet<Funcionario>();
            InverseIdAdminNavigation = new HashSet<Administrador>();
        }

        public int IdUtilizador { get; set; }
        public int IdAdmin { get; set; }
        public DateTime DataRegisto { get; set; }

        public virtual Administrador IdAdminNavigation { get; set; } = null!;
        public virtual Utilizador IdUtilizadorNavigation { get; set; } = null!;
        public virtual ICollection<Funcionario> Funcionarios { get; set; }
        public virtual ICollection<Administrador> InverseIdAdminNavigation { get; set; }
    }
}
