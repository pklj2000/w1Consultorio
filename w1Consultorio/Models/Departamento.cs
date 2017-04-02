using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace w1Consultorio.Models
{
    [Table("Departamento")] 
    public class Departamento
    {
        [Key]
        public Int32 CodDepartamento { get; set; }

        [Required(ErrorMessage = "Informe o departamento"), Display(Name = "Departamento")]
        public string Descricao { get; set; }

        public string Responsavel { get; set; }

        [Required]
        public Int32 CodEmpresa { get; set; }
        public virtual Empresa Empresa { get; set; }

    }
}