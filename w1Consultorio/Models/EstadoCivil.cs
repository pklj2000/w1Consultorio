using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace w1Consultorio.Models
{
    [Table("EstadoCivil")]
    public class EstadoCivil
    {
        [Key]
        public Int32 CodEstCivil { get; set; }
        [Required(ErrorMessage = "Informe o estado civil"), Display(Name = "Estado Civil")]
        [MaxLength(50, ErrorMessage = "O campo suporta 50 caracteres")]
        public string Descricao { get; set; }
    }
}