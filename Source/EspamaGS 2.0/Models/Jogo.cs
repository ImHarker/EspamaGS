using System;
using System.Collections.Generic;

namespace EspamaGS_2._0.Models {
    public partial class Jogo {
        public Jogo() {
            Compras = new HashSet<Compra>();
        }

        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Foto { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public decimal Preco { get; set; }
        public int IdCategoria { get; set; }
        public int IdDesenvolvedora { get; set; }
        public int IdPlataforma { get; set; }
        public int IdFuncionario { get; set; }
        public DateTime DataRegisto { get; set; }
        public DateTime DataLancamento { get; set; }

        public virtual Categoria IdCategoriaNavigation { get; set; } = null!;
        public virtual Desenvolvedora IdDesenvolvedoraNavigation { get; set; } = null!;
        public virtual Funcionario IdFuncionarioNavigation { get; set; } = null!;
        public virtual Plataforma IdPlataformaNavigation { get; set; } = null!;
        public virtual ICollection<Compra> Compras { get; set; }
    }
}
