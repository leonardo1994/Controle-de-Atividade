using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Account;

namespace WebApplication1.Models.Activities
{
    [DisplayName("Código Ajuda")]
    public class HelperCode
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Origem")]
        public Tipo Tipo { get; set; }

        [Required]
        [DisplayName("Título")]
        public string Title { get; set; }

        [Required]
        [DisplayName("Descricao"), DataType(DataType.Html)]
        public string Description { get; set; }

        [Required]
        [DisplayName("Usuário")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<HelperCodeArchive> HelperCodeArchives { get; set; }
        public virtual ICollection<SolutionHelperCode> SolutionHelperCodes { get; set; }
    }

    public enum Tipo
    {
        [Display(Name = "VGO_GEN")]
        VGO_GEN,
        [Display(Name = "LPACK")]
        LPACK,
        [Display(Name = "ULIB")]
        ULIB,
        [Display(Name = "DB")]
        DB,
        [Display(Name = "VGO_CUSTOM")]
        VGO_CUSTOM,
        [Display(Name = "VGO_ACC")]
        VGO_ACC,
        [Display(Name = "SQL SERVER")]
        SQL_SERVER,
        [Display(Name = "ORACLE")]
        ORACLE,
        [Display(Name = "VISUAL FOX PRO")]
        VISUAL_FOX_PRO,
        [Display(Name = "MENU DO USUÁRIO")]
        MENU_USUARIO,
        [Display(Name = "ATALHO")]
        ATALHO,
        [Display(Name = "DYNAMSOFT")]
        DYNAMSOFT,
        [Display(Name = "DLL")]
        DLL,
        [Display(Name = "LICENÇA")]
        LICENCA
    }
}