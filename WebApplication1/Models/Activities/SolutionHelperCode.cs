using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models.Account;

namespace WebApplication1.Models.Activities
{
    [DisplayName("Solução e Código ajudas")]
    public class SolutionHelperCode
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Solução")]
        public int SolutionId { get; set; }
        public virtual Solution Solution { get; set; }

        [Required]
        [DisplayName("Código Ajuda")]
        public int HelperCodeId { get; set; }
        public virtual HelperCode HelperCode { get; set; }

        [Required]
        [DisplayName("Usuário")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
