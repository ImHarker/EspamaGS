using System;
using System.Collections.Generic;

namespace EspamaGS_2._0.Models {
    public partial class Utilizador {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Passwd { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Nome { get; set; } = null!;

        public virtual Administrador? Administrador { get; set; }
        public virtual Cliente? Cliente { get; set; }
        public virtual Funcionario? Funcionario { get; set; }
    }
}
