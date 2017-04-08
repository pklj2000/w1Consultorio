using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace w1Consultorio.Models
{
    public class Resposta
    {
        [Key]
        [Display(Name="Código")]
        public int CodResposta { get; set; }
        
        [Required(ErrorMessage ="Código da] pergunta não informado")]
        public int CodPergunta { get; set; }

        [Required(ErrorMessage = "Código do prontuário não informado")]
        public int CodProntuario { get; set; }

        [Required (ErrorMessage = "Descrição da pergunta não registrado")]
        public string PerguntaDescricao { get; set; }

        [Required(ErrorMessage = "Tipo da resposta não registrado") ]
        public int CodTipoPergunta { get; set; }
        
        [Required (ErrorMessage = "Resposta não informada")]
        [Display(Name = "Resposta")]
        public string RespostaValor { get; set; }

        [Display(Name = "Observação")]
        public string RespostaObservacao { get; set; }

        [Display(Name = "Grupo")]
        public int CodGrupo { get; set; }

        [Display(Name = "Grupo Sequencia")]
        public int GrupoSequencia { get; set; }

        public virtual Prontuario Prontuario { get; set; }
        public virtual Pergunta Pergunta { get; set; }
    }
}