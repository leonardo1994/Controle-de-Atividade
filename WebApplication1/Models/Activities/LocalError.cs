using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.Account;

namespace WebApplication1.Models.Activities
{
    [DisplayName("Local de erro")]
    public class LocalError
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Local error")]
        [MaxLength(50), Index(IsUnique = true)]
        public string Description { get; set; }

        [Required]
        [DisplayName("Usuário")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Evidence> Evidencies { get; set; }
    }
}