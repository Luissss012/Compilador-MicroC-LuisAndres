using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroC_PreCompilador
{
    public class Token
    {
        public int Linea { get; set; }

        public int Codigo { get; set; }

        public string Lexema { get; set; }

        public string Tipo { get; set; }
    }
}
