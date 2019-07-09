using Compilador.parser.Colette.ast.entorno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser.Colette.ast.instruccion
{
    abstract class Instruccion : Nodo
    {
        public Instruccion(int linea, int columna) : base(linea, columna)
        { }

        public abstract Result GetC3D(Ent e, bool funcion, bool ciclo, bool isDeclaracion, LinkedList<Error> errores);
    }
}
