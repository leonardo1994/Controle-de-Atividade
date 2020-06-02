using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.Account;

namespace WebApplication1.Models.Activities
{
    [DisplayName("Tela")]
    public class Screen
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Nome"), MaxLength(10), Index(IsUnique = true)]
        public string Name { get; set; }

        [Required]
        [DisplayName("Tela")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Modulo")]
        public int ModuleId { get; set; }
        public virtual Module Module { get; set; }

        [Required]
        [DisplayName("Usuário")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Evidence> Evidencies { get; set; }
    }
}