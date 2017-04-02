using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace w1Consultorio.Models
{
    [Table("Sistema")]
    public class Sistema
    {
        [Key]
        public int CodSistema { get; set; }
        [Required(ErrorMessage = "Informe o código do sistema")]
        [Display(Name = "Código do sistema")]
        [MaxLength(10, ErrorMessage= "Informe até 10 caracteres")]
        public string CodRefSistema {get; set;}
        [Required(ErrorMessage = "Informe o nome do sistema")]
        [Display(Name = "Nome do sistema")]
        public string Nome { get; set; }
        [Display(Name = "Descrição do sistema")]
        public string Descricao { get; set; }
    }
}