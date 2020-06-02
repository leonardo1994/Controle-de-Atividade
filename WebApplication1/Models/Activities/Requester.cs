using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Account;

namespace WebApplication1.Models.Activities
{
    [DisplayName("Requisitante")]
    public class Requester
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Telefone")]
        public string Phones { get; set; }

        [DataType(DataType.EmailAddress)]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayName("Skype")]
        public string Skype { get; set; }

        [DisplayName("Empresa")]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        [Required]
        [DisplayName("Usuário")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Activity> Acitivities { get; set; }
        public virtual ICollection<Evidence> Evidences { get; set; }
    }
}