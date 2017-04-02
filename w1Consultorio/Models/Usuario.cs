using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace w1Consultorio.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [Required(ErrorMessage = "Informe o código do usuário")]
        [StringLength(10, ErrorMessage = "O tamanho máximo são 10 caracteres.")]
        [Display(Name = "Código do usuário")]
        public string CodUsuario { get; set; }
        [Required(ErrorMessage = "Informe o nome do usuário")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Informe o usuário está ativo ou não")]
        public string Ativo { get; set; }
        public string Email { get; set; }
        [Display(Name = "Último acesso")]
        public DateTime DataUltAcesso { get; set; }
        public string Senha { get; set; }

        public virtual ICollection<Perfil> Perfil { get; set; }

    }
}