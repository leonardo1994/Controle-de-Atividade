using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Account;

namespace WebApplication1.Models.Activities
{
    [DisplayName("Evidência Solução")]
    public class EvidenceSolution
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Evidência")]
        public int EvidenceId { get; set; }
        public virtual Evidence Evidence { get; set; }

        [Required]
        [DisplayName("Solução")]
        public int SolutionId { get; set; }
        public virtual Solution Solution { get; set; }

        [Required]
        [DisplayName("Usuário")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
