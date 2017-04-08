using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace w1Consultorio.Models
{
    public class ResultadoProntuario
    {
        public Dictionary<int, string> ResultadosProntuario { get; set; }

        public ResultadoProntuario()
        {
            ResultadosProntuario = new Dictionary<int, string>()
            {
                {1, "Apto" },
                {2, "Apto com restrições" },
                {3, "Inapto" }
            };
        }
    }
}