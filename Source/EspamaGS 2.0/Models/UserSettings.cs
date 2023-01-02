using System.ComponentModel.DataAnnotations;

namespace EspamaGS_2._0.Models {
    public class UserSettings {
        [Key]
        public string Id { get; set; }

        public Boolean EmailNotifications { get; set; }


    }
}
