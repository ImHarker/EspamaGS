using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspamaGS_2._0.Models {
    [Table("Reviews")]
    public class Review {
        [Key]
        public int Id { get; set; }
        [StringLength(1024)]
        public string ReviewMessage { get; set; }
        public int Rating { get; set; }
        public int IdJogo { get; set; }
        [StringLength(50)]
        public string IdCliente { get; set; } = null!;

        [ForeignKey("IdJogo")]
        public virtual Jogo IdJogoNavigation { get; set; } = null!;
    }
}
