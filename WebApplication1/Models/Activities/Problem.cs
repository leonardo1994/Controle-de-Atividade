using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Account;

namespace WebApplication1.Models.Activities
{
    [DisplayName("Problema")]
    public class Problem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Problema")]
        public string Title { get; set; }

        [DisplayName("Descricao")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Usuário")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Evidence> Evidences { get; set; }
        public virtual ICollection<Solution> Solutions { get; set; }
    }
}