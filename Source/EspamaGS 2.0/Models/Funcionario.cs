using System;
using System.Collections.Generic;

namespace EspamaGS_2._0.Models
{
    public partial class Funcionario
    {
        public Funcionario()
        {
            Jogos = new HashSet<Jogo>();
        }

        public string Telefone { get; set; } = null!;
        public string IdUtilizador { get; set; } = null!;
        public string IdAdmin { get; set; } = null!;
        public DateTime DataRegisto { get; set; }

        public virtual Administrador IdAdminNavigation { get; set; } = null!;
        public virtual ICollection<Jogo> Jogos { get; set; }
    }
}
