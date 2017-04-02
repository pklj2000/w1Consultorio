using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace w1Consultorio.Models
{
    [Table("Cargo")]
    public class Cargo
    {
        [Key]
        public Int32 CodCargo { get; set; }
        [Required(ErrorMessage = "Informe o cargo"), Display(Name = "Cargo") ]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Informe o departamento"), Range(1, Int32.MaxValue,ErrorMessage="Informe o departamento")]
        public Int32 codDepartamento { get; set; }
        public virtual Departamento Departamento { get; set; }
        [Required]
        public Int32 codPeriodicidade { get; set; }
        public virtual Periodicidade Periodicidade { get; set; }

        public virtual ICollection<Exame> Exames { get; set; }
        public virtual ICollection<Risco> Riscos { get; set; }
    }
}