using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace w1Consultorio.Models
{
    [Table("Transacao")]
    public class Transacao
    {
        [Key]
        public int CodTransacao { get; set; }
        [Required(ErrorMessage = "Informe a descrição da transação"), Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Informe a descrição da janela"), Display(Name = "Janela")]
        public string Janela { get; set; }
        [Display(Name = "Objeto")]
        public string Objeto { get; set; }
        [Required(ErrorMessage = "Informe se a transação está ativa ou não"), Display(Name = "Ativo")]
        public string Ativo { get; set; }
        [Required(ErrorMessage = "Informe o módulo de sistema"), Display(Name = "Módulo")]
        public int CodModulo { get; set; }

        public virtual Modulo Modulo { get; set; }
        public virtual ICollection<Perfil> Perfil { get; set; }
    }
}