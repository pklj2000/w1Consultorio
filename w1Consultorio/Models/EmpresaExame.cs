using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace w1Consultorio.Models
{
    public class EmpresaExame
    {
        [Key]
        public Int32 CodEmpresa { get; set; }
        public virtual Empresa Empresa { get; set; }
        [Key]
        public Int32 CodExame { get; set; }

        public double Valor { get; set; }
    }
}