using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser.Colette.ast.entorno;

namespace Compilador.parser.Colette.ast.instruccion
{
    class Continue : Instruccion
    {
        public Continue(int linea, int columna) : base(linea, columna)
        {

        }

        public override Result GetC3D(Ent e, bool funcion, bool ciclo, bool isDeclaracion, bool isObjeto, LinkedList<Error> errores)
        {
            if (!isDeclaracion)
                Debugger(e, "Continue");

            Result result = new Result();
            if (!isDeclaracion)
            {
                if (ciclo)
                    result.Codigo = "goto " + e.EtiquetaCiclo + ";\n";
                else
                    errores.AddLast(new Error("Semántico", "Sentencia continue no se encuentra dentro de un ciclo.", Linea, Columna));
            }
            return result;
        }
    }
}
