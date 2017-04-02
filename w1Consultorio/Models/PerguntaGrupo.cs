using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace w1Consultorio.Models
{
    [Table("PerguntaGrupo")]
    public class PerguntaGrupo
    {
        [Key]
        public int CodPerguntaGrupo { get; set; }
        [Required(ErrorMessage="Informe a descrição do grupo")]
        [Display(Name="Descrição")]
        public string Descricao { get; set; }
        [Required(ErrorMessage="Informe a sequencia")]
        public int Sequencia { get; set; }
        [Required(ErrorMessage = "Informe se o grupo está ou não ativo")]
        [MaxLength(1)]
        public string Ativo { get; set; }

        [NotMapped]
        public string AtivoDescricao
        {
            get
            {
                string _ativo = "Não";
                if (Ativo == "S")
                    _ativo = "Sim";
                return _ativo;
            }
        }
    }
}