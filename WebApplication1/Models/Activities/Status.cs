using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Account;

namespace WebApplication1.Models.Activities
{
    [DisplayName("Status")]
    public class Status
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Código")]
        public string Code { get; set; }

        [Required]
        [DisplayName("Descrição")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Usuário")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Activity> Acitivities { get; set; }
    }
}