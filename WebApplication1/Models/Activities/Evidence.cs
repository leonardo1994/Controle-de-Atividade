using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.Account;

namespace WebApplication1.Models.Activities
{
    [DisplayName("Evidência")]
    public class Evidence
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Atividade")]
        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }

        [Required]
        [DisplayName("Título")]
        public string Title { get; set; }

        [Required]
        [DisplayName("Relator")]
        public int RequesterId { get; set; }
        public virtual Requester Requester { get; set; }

        [Required]
        [DataType(DataType.Html)]
        [DisplayName("Evidência")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Local erro")]
        public int LocalErrorId { get; set; }
        public virtual LocalError LocalError { get; set; }

        [Required]
        [DisplayName("Tela")]
        public int ScreenId { get; set; }
        public virtual Screen Screen { get; set; }

        [Required]
        [DisplayName("Problema")]
        public int ProblemId { get; set; }
        public virtual Problem Problem { get; set; }

        [Required]
        [DisplayName("Data inicial")]
        [DataType(DataType.Date)]
        //[Column(TypeName = "datetime2")]
        public DateTime InitalDate { get; set; }

        //[Column(TypeName = "datetime2")]
        [DisplayName("Data final")]
        [DataType(DataType.Date)]
        public DateTime? FinalDate { get; set; }

        /// <summary>
        /// Utilizar está opção, serve para informar uma evidência anterior que fez surgir está evidência.
        /// </summary>
        [DisplayName("Evidência anterior")]
        public int? EvidenceOldId { get; set; }
        public virtual Evidence EvidenceOld { get; set; }

        [DisplayName("Solução")]
        public int? SolutionId { get; set; }
        public virtual Solution Solution { get; set; }

        [Required]
        [DisplayName("Usuário")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<EvidenceArchive> EvidenceArchives { get; set; }
        public virtual ICollection<Evidence> Evidencies { get; set; }
        public virtual ICollection<Calendar> Calendars { get; set; }
        public virtual ICollection<EvidenceSolution> EvidenceSolutions { get; set; }
    }

    public enum Priority
    {
        [Display(Name = "Alta")]
        Hight,
        [Display(Name = "Média")]
        Medium,
        [Display(Name = "Baixa")]
        Down
    }
}
