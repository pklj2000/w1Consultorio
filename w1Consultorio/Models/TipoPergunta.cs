using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace w1Consultorio.Models
{
    public class TipoPergunta
    {
        public Dictionary<int, string> TiposPergunta { get; set; }

        public TipoPergunta()
        {
            TiposPergunta = new Dictionary<int, string>()
            {
                { 1, "Texto Livre"},
                { 2, "Numérico"},
                { 3, "Lista"}
            };
        }
    }
}