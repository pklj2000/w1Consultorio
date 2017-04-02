using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace w1Consultorio.Models
{
    [Table("Profissional")]
    public class Profissional
    {
        [Key]
        public Int32 CodProfissional { get; set; }
        [Required(ErrorMessage = "Informe o nome do profissional")]
        public string Nome { get; set; }
        public string CRM { get; set; }
        public string Telefone { get; set; }
        [Required(ErrorMessage = "Informe se o profissional está ativo ou não")]
        public string Ativo { get; set; }
        [NotMapped]
        public string AtivoDescricao
        {
            get
            {
                return Ativo == "S" ? "Sim" : "Não";
            }
        }
        public string Observacao { get; set; }

    }
}