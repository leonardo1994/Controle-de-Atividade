using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.Account;

namespace WebApplication1.Models.Activities
{
    [DisplayName("Calendário/Agenda")]
    public class Calendar
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Evidência")]
        public int EvidenceId { get; set; }
        public virtual Evidence Evidence { get; set; }

        [DisplayName("Data Inicial"), DataType(DataType.Date)]
        //[Column(TypeName = "datetime2")]
        public DateTime DateInitial { get; set; }

        [DisplayName("Hora Inicial"), DataType(DataType.Time)]
        //[Column(TypeName = "datetime2")]
        public DateTime TimeInitial { get; set; }

        [DisplayName("Data Inicial"), DataType(DataType.Date)]
        //[Column(TypeName = "datetime2")]
        public DateTime DateFinal { get; set; }

        [DisplayName("Hora Inicial"), DataType(DataType.Time)]
        //[Column(TypeName = "datetime2")]
        public DateTime TimeFinal { get; set; }

        [DisplayName("Controle do horário")]
        public ControlTime ControlTime { get; set; }

        public bool Send { get; set; }

        [Required]
        [DisplayName("Usuário")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }

    public enum ControlTime
    {
        [Display(Name = "Agenda Aberta")]
        Open,
        [Display(Name = "Agenda Fechada")]
        Closed,
        [Display(Name = "Agenda Cancelada")]
        Cancel
    }
}