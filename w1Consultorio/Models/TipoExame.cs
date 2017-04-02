using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace w1Consultorio.Models
{
    [Table("TipoExame")]
    public class TipoExame
    {
        [Key]
        public Int32 CodTipoExame { get; set; }
        [Required(ErrorMessage = "Digite o tipo do exame.")]
        [StringLength(50, ErrorMessage = "O tamanho máximo são 50 caracteres.")]
        [Display(Name = "Tipo do Exame")]
        public string Descricao { get; set; }
    }
}