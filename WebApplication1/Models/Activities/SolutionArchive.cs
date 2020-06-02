using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Account;

namespace WebApplication1.Models.Activities
{
    [DisplayName("Arquivos da solução")]
    public class SolutionArchive
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Solução")]
        public int SolutionId { get; set; }
        public virtual Solution Solution { get; set; }

        [Required]
        [DisplayName("Nome do arquivo")]
        public string FileName { get; set; }

        [Required]
        [DisplayName("Tipo do arquivo")]
        public string ContentType { get; set; }

        [Required]
        [DisplayName("Arquivo")]
        public byte[] Content { get; set; }

        [Required]
        [DisplayName("Usuário")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
