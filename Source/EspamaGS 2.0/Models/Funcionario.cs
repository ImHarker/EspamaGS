using System;
using System.Collections.Generic;

namespace EspamaGS_2._0.Models {
    public partial class Funcionario {
        public Funcionario() {
            Jogos = new HashSet<Jogo>();
        }

        public string Telefone { get; set; } = null!;
        public int IdUtilizador { get; set; }
        public int IdAdmin { get; set; }
        public DateTime DataRegisto { get; set; }

        public virtual Administrador IdAdminNavigation { get; set; } = null!;
        public virtual Utilizador IdUtilizadorNavigation { get; set; } = null!;
        public virtual ICollection<Jogo> Jogos { get; set; }
    }
}
