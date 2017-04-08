using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace w1Consultorio.Models
{
    public class ResultadoClinico
    {
        public Dictionary<int, string> ResultadosClinicos { get; set; }

        public ResultadoClinico()
        {
            ResultadosClinicos = new Dictionary<int, string>()
            {
                {1, "Normal" },
                {2, "Anormal" }
            };
        }
    }

}