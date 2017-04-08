using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace w1Consultorio.Models.ViewModel
{
    public class PerguntaRespostaViewModel
    {
        public int CodPergunta { get; set; }
        public string PerguntaDescricao { get; set; }
        public int PerguntaSequencia { get; set; }
        public int PerguntaTipoResposta { get; set; }


        public int CodResposta { get; set; }
        public int CodProntuario { get; set; }
        public string RespostaValor { get; set; }
        public string RespostaObservacao { get; set; }
    }
}