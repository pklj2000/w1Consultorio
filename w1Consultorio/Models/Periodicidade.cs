using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace w1Consultorio.Models
{
    public class Periodicidade
    {
        [Key]
        public Int32 CodPeriodicidade { get; set; }
        [Required(ErrorMessage = "Informe a periodicidade")]
        [MaxLength(50, ErrorMessage = "O campo suporta 50 caracteres")]
        [Display(Name = "Periodicidade")]
        public string Descricao { get; set; }
        [Required]
        [Range(0, 1000)]
        public Int32 Dias { get; set; }
        public virtual ICollection<Exame> Exames { get; set; }
    }
}