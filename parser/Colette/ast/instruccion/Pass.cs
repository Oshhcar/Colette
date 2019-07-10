using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser.Colette.ast.entorno;

namespace Compilador.parser.Colette.ast.instruccion
{
    class Pass : Instruccion
    {
        public Pass(int linea, int columna) : base(linea, columna)
        {}

        public override Result GetC3D(Ent e, bool funcion, bool ciclo, bool isDeclaracion, bool isObjeto, LinkedList<Error> errores)
        {
            return new Result();
        }
    }
}
