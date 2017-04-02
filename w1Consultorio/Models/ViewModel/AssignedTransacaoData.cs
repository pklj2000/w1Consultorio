using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace w1Consultorio.Models.ViewModel
{
    public class AssignedTransacaoData
    {
        public Int32 CodTransacao { get; set; }
        public string Descricao { get; set; }
        public bool Assigned { get; set; }
    }
}