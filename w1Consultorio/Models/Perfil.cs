using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace w1Consultorio.Models
{
    [Table("Perfil")]
    public class Perfil
    {
        [Key]
        public int CodPerfil { get; set; }
        [Required(ErrorMessage = "Informe a descrição do perfil"), Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Informe se o perfil está ativa ou não"), Display(Name = "Ativo")]
        public string Ativo { get; set; }
        [Required(ErrorMessage = "Informe o módulo de sistema"), Display(Name = "Módulo")]
        public int CodModulo { get; set; }

        public virtual Modulo Modulo { get; set; }
        public virtual ICollection<Transacao> Transacao { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}