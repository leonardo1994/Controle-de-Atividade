using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Account;

namespace WebApplication1.Models.Activities
{
    [DisplayName("Empresa")]
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Empresa")]
        public string CompanyName { get; set; }

        [DisplayName("Telefones")]
        public string Phones { get; set; }

        [DisplayName("Endereços")]
        public string PublicPlace { get; set; }

        [DisplayName("Número")]
        public string Number { get; set; }

        [DisplayName("Bairro")]
        public string Neighborhood { get; set; }

        [DisplayName("Cidade")]
        public string City { get; set; }

        [DisplayName("Estado")]
        public string State { get; set; }

        [DisplayName("Cep")]
        public string ZipCode { get; set; }

        [Required]
        [DisplayName("Usuário")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Activity> Acitivities { get; set; }
        public virtual ICollection<Requester> Requesters { get; set; }
        public virtual ICollection<AccessRemote> AccessRemote { get; set; }
    }
}