using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace w1Consultorio.Models
{
    public class Modulo
    {
        [Key]
        public int CodModulo { get; set; }
        [Required(ErrorMessage = "Informe o módulo")]
        [Display(Name = "Módulo")]
        public string Nome { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Informe o sistema")]
        public int CodSistema { get; set; }

        public virtual Sistema Sistema { get; set; }

    }
}