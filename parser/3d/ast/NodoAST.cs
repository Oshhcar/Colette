using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser._3d.ast
{
    class NodoAST
    {

        public NodoAST(int linea, int columna) {
            Linea = linea;
            Columna = columna;
        }

        public int Linea { get; set; }
        public int Columna { get; set; }
    }
}
