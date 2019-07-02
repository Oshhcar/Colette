using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser.Colette.ast.entorno
{
    class Result
    {
        public Result()
        {
        }

        public string Valor { get; set; }
        public string Codigo { get; set; }
        public string EtiquetaV { get; set; }
        public string EtiquetaF { get; set; }
    }
}
