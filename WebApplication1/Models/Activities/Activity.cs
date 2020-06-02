using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.Account;

namespace WebApplication1.Models.Activities
{
    [DisplayName("Atividade")]
    public class Activity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Título da atividade")]
        public string Title { get; set; }

        [Required]
        [DisplayName("Meio de solicitação")]
        public int RequestMeansId { get; set; }
        public virtual RequestMeans RequestMeans { get; set; }

        [DisplayName("Número do chamado")]
        public string CalledNumber { get; set; }

        [Required]
        [DisplayName("Empresa")]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        [Required]
        [DisplayName("Requisitante")]
        public int RequesterId { get; set; }
        public virtual Requester Requester { get; set; }

        [Required]
        [DisplayName("Status")]
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        [Required]
        [DisplayName("Data de Requisição")]
        //[Column(TypeName = "datetime2")]
        [DataType(DataType.Date)]
        public DateTime RequestDate { get; set; }

        [DisplayName("Data de Finalização")]
        //[Column(TypeName = "datetime2")]
        [DataType(DataType.Date)]
        public DateTime? FinalDate { get; set; }

        [Required]
        [DisplayName("Usuário")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Evidence> Evidencies { get; set; }
    }
}