using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace w1Consultorio.Models
{
    [Table("Atendimento")]
    public class Atendimento
    {
        [Key]
        public Int32 codAtendimento { get; set; }

        [Required(ErrorMessage = "Informe o empresa"), Display(Name = "Empresa")]
        public Int32 codEmpresa { get; set; }

        [Required(ErrorMessage = "Informe o funcionário"), Display(Name = "Funcionário")]
        public Int32 codFuncionario { get; set; }

        [Required(ErrorMessage = "Informe o tipo do exame"), Display(Name = "Tipo Exame")]
        public Int32 codTipoExame { get; set; }
        
        public bool Ativo { get; set; }

        [Required(ErrorMessage = "Informe a data para atendimento"), Display(Name = "Data Atendimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime dataAtendimento { get; set; }

        public virtual Funcionario Funcionario { get; set; }    
        public virtual TipoExame TipoExame { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}