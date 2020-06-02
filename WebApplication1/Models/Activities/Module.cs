using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.Account;

namespace WebApplication1.Models.Activities
{
    [DisplayName("Modulo")]
    public class Module
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Nome (Exemp.: SMA)")]
        [MaxLength(10)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [Required]
        [DisplayName("Descrição")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Usuário")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Screen> Screens { get; set; }
    }
}