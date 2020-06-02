using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Account;

namespace WebApplication1.Models.Activities
{
    [DisplayName("Solução")]
    public class Solution
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Problema")]
        public int ProblemId { get; set; }
        public virtual Problem Problem { get; set; }

        [Required]
        [DataType(DataType.Html)]
        [DisplayName("Solução")]
        public string Description { get; set; }

        [DisplayName("Solução base")]
        public int? SolutionBaseId { get; set; }
        public virtual Solution SolutionBase { get; set; }

        [DisplayName("Solução Antorior | Usar para aplicar solução atual como melhoria")]
        public int? SolutionOldId { get; set; }
        public virtual Solution SolutionOld { get; set; }

        [DisplayName("Situação")]
        public int SituationId { get; set; }
        public virtual Situation Situation { get; set; }

        [Required]
        [DisplayName("Status da solução")]
        public SolutionStatus Status { get; set; }

        [Required]
        [DisplayName("Usuário")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<SolutionArchive> SolutionArchives { get; set; }
        public virtual ICollection<Solution> Solutions { get; set; }
        public virtual ICollection<SolutionInformation> SolutionInformations { get; set; }
        public virtual ICollection<EvidenceSolution> EvidenceSolutions { get; set; }
        public virtual ICollection<SolutionHelperCode> SolutionHelperCodes { get; set; }
        public virtual ICollection<Evidence> Evidences { get; set; }
    }

    public enum SolutionStatus
    {
        [Display(Name = "Sucesso", Description = "Solução funcionou sem erros")]
        Success,
        [Display(Name = "Alerta", Description = "Solução funcionou, mas necessário realizar ajustes")]
        Alert,
        [Display(Name = "Error", Description = "Solução não funcionou")]
        Error,
        [Display(Name = "Aguardando", Description = "Aguardando aprovação")]
        Waiting
    }
}
