using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace w1Consultorio.Models
{
    [Table("Empresa")]
    public class Empresa
    {
        [Key]
        public Int32 CodEmpresa { get; set; }
        [Display(Name ="Nome da Empresa:")]
        public string Nome { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Ativo { get; set; }
        [NotMapped]
        public bool AtivoCheck 
        { 
            get { return Ativo == "S"; }
            set { Ativo = value ? "S" : "N"; }
        }

        //public virtual ICollection<EmpresaExame> EmpresaExames { get; set; }
    }
}