using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace w1Consultorio.Models
{
    public class Pergunta
    {
        [Key]
        public int CodPergunta { get; set; }
        [Required(ErrorMessage = "Informe a pergunta")]
        [Display(Name = "Pergunta")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Informe a sequencia")]
        public int Sequencia { get; set; }
        [Required(ErrorMessage = "Informe se a pergunta está ou não ativa")]
        [MaxLength(1)]
        public string Ativo { get; set; }
        [Required (ErrorMessage = "Informe o grupo de pergunta")]
        public int CodPerguntaGrupo { get; set; }
        [Display(Name = "Tipo da resposta")]
        [Required(ErrorMessage = "Informe o tipo da resposta")]
        public int CodTipoResposta { get; set; }
        [Display(Name = "Resposta obrigatória")]
        [Required(ErrorMessage = "Informe se a resposta é obrigatória")]
        public int RespostaObrigatoria { get; set; }
        [Display(Name = "Respostas possíveis")]
        public string RespostaItem { get; set; }

        public virtual PerguntaGrupo PerguntaGrupo { get; set; }

        [NotMapped]
        public Dictionary<int, string> TipoResposta { get; set; }

        [NotMapped]
        public string AtivoDescricao
        {
            get
            {
                string _ativo = "Não";
                if (Ativo == "1")
                    _ativo = "Sim";
                return _ativo;
            }
        }
    }
}