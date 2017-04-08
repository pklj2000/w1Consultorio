using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace w1Consultorio.Models.ViewModel
{
    public class GrupoViewModel
    {
        public int CodGrupo { get; set; }
        public int GrupoSequencia { get; set; }
        public string GrupoDescricao { get; set; }
        [NotMapped]
        public IList<PerguntaRespostaViewModel> Perguntas { get; set; }
    }
}