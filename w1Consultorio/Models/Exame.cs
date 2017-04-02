using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace w1Consultorio.Models
{
    public class Exame
    {
        [Key]
        public Int32 CodExame { get; set; }
        [Required(ErrorMessage = "Informe o risco")]
        [MaxLength(100, ErrorMessage = "O campo suporta 50 caracteres")]
        [Display(Name="Exame")]
        public string Descricao { get; set; }
        public string EmiteSolExame { get; set; }
        public string Ativo { get; set; }
        public virtual ICollection<Periodicidade> Periodicidade { get; set; }
        public virtual ICollection<Cargo> Cargos { get; set; }
        [NotMapped]
        public bool EmiteSolExameCheck
        {
            get { return EmiteSolExame == "S"; }
            set { EmiteSolExame = value ? "S" : "N"; }
        }
        [NotMapped]
        public bool AtivoCheck
        {
            get { return Ativo == "S"; }
            set { Ativo = value ? "S" : "N"; }
        }
    }
}