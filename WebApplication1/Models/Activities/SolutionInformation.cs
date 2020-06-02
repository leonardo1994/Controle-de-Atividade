using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Account;

namespace WebApplication1.Models.Activities
{
    [DisplayName("Informações adicionais da solução")]
    public class SolutionInformation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Solução")]
        public int SolutionId { get; set; }
        public virtual Solution Solution { get; set; }

        [Required]
        [DataType(DataType.Html)]
        [DisplayName("Informações")]
        public string Information { get; set; }

        [Required]
        [DisplayName("Usuário")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
