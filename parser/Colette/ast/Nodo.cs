using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser.Colette.ast
{
    class Nodo
    {
        public Nodo(int linea, int columna)
        {
            Linea = linea;
            Columna = columna;
        }

        public int Linea { get; set; }
        public int Columna { get; set; }

        public static int Etiquetas { get; set; }
        public static int Temporales { get; set; }

        public int NuevaEtiqueta() { return ++Etiquetas; }
        public int NuevoTemporal() { return ++Temporales; }
    }
}
