using System;
using System.Collections.Generic;

namespace EspamaGS_2._0.Models
{
    public partial class Administrador
    {
        public Administrador()
        {
            Funcionarios = new HashSet<Funcionario>();
            InverseIdAdminNavigation = new HashSet<Administrador>();
        }

        public string IdUtilizador { get; set; } = null!;
        public string IdAdmin { get; set; } = null!;
        public DateTime DataRegisto { get; set; }

        public virtual Administrador IdAdminNavigation { get; set; } = null!;
        public virtual ICollection<Funcionario> Funcionarios { get; set; }
        public virtual ICollection<Administrador> InverseIdAdminNavigation { get; set; }
    }
}
