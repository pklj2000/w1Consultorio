using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace w1Consultorio.Models.ViewModel
{
    public class ProntuarioViewModel: Prontuario
    {
        //viewmodel que agrega perguntas e respostas por prontuario
        [NotMapped]
        public IList<ViewModel.GrupoViewModel> PerguntasRespostas { get; set; }
    }
}