using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Account;

namespace WebApplication1.Models.Activities
{
    [DisplayName("Arquivos de acesso remoto")]
    public class AccessRemoteArchive
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Acesso Remoto")]
        public int AccessRemoteId { get; set; }
        public virtual AccessRemote AccessRemote { get; set; }

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