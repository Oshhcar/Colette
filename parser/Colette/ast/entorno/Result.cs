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
        public int PtrStack { get; set; } /*Uso en asignaciones*/
        public Sim Simbolo { get; set; } /*Uso en asignaciones*/
        public Tipo Tipo { get; set; }  /*Uso en asignaciones*/
    }
}
