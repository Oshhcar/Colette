using Compilador.parser.Colette.ast.entorno;
using Compilador.parser.Colette.ast.expresion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser.Colette.ast.instruccion
{
    class Del : Instruccion
    {
        public Del(LinkedList<Expresion> objetivo, int linea, int columna) : base(linea, columna)
        { }

        public LinkedList<Expresion> Objetivo { get; set; }

        public override Result GetC3D(Ent e, bool funcion, bool ciclo, bool isDeclaracion, bool isObjeto, LinkedList<Error> errores)
        {
            if (!isDeclaracion)
                Debugger(e, "Del");

            Result result = new Result();
            return result;
        }
    }
}
