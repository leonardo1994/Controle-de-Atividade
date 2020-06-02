using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Activities
{
    [DisplayName("Situação")]
    public class Situation
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Código")]
        public string Code { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        public virtual ICollection<Solution> Solutions { get; set; }
    }
}
