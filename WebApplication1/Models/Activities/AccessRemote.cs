using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Account;

namespace WebApplication1.Models.Activities
{
    [DisplayName("Acesso Remoto")]
    public class AccessRemote
    {
        [Key]
        public int Id { get; set; }

        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        [Required]
        [DisplayName("Tipo de acesso")]
        public TypeAccess TypeAccess { get; set; }

        [DisplayName("Dominio")]
        public string Domain { get; set; }

        [Required]
        [DisplayName("Acesso")]
        public string Acesso { get; set; }

        [DisplayName("Usuário")]
        public string User { get; set; }

        [DisplayName("Senha")]
        public string Password { get; set; }

        [DisplayName("Caminho do sistema")]
        public string DirSystem { get; set; }

        [DisplayName("String de Conexão")]
        public string ConnectionString { get; set; }

        [DisplayName("Tipo de banco")]
        public TypeBase TypeBase { get; set; }

        [DisplayName("Tipo de ambiente")]
        public Ambiente Ambiente { get; set; }

        [DisplayName("Nome da base de dados")]
        public string DataBaseName { get; set; }

        [DisplayName("Usuário para acesso ao Starsoft")]
        public string UserSsa { get; set; }

        [DisplayName("Senha para acesso ao Starsoft")]
        public string PasswordSsa { get; set; }

        [DisplayName("Endereço Físico uso para BrowsfishUkey")]
        public string PhysicalAddress { get; set; }

        [Required]
        [DisplayName("Usuário")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<AccessRemoteArchive> AccessRemoteArchives { get; set; }
    }

    public enum TypeAccess
    {
        [Display(Name = "Terminal Service")]
        Ts,
        [Display(Name = "Team Viewer")]
        Tv,
        [Display(Name = "Amazon - AWS")]
        Aws,
        [Display(Name = "Virtual Private Networking - VPN")]
        Vpn,
        [Display(Name = "Servidor de banco de dados")]
        DataBaseServer
    }

    public enum TypeBase
    {
        Sql,
        Oracle,
        Hana
    }

    public enum Ambiente
    {
        Producao,
        Homologacao
    }
}
