using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace w1Consultorio.Models
{
    [Table("Risco")]
    public class Risco
    {
        [Key]
        public Int32 CodRisco { get; set; }
        [Required(ErrorMessage = "Informe o risco")]
        [MaxLength(50, ErrorMessage = "O campo suporta 50 caracteres")]
        public string Descricao { get; set; }
        public virtual ICollection<Cargo> Cargos { get; set; }
    }
}