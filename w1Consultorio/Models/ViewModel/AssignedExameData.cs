using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace w1Consultorio.Models.ViewModel
{
    public class AssignedExameData
    {
        public Int32 CodExame { get; set; }
        public string Descricao { get; set; }
        public bool Assigned { get; set; }
    }
}