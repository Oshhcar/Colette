using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser.Colette.ast.entorno;

namespace Compilador.parser.Colette.ast.instruccion
{
    class Global : Instruccion
    {
        public Global(LinkedList<string> ids, Tipo tipo, int linea, int columna) : base(linea, columna)
        {
            Ids = ids;
            Tipo = tipo;
        }

        public LinkedList<string> Ids { get; set; }
        public Tipo Tipo { get; set; }

        public override Result GetC3D(Ent e, bool funcion, bool ciclo, bool isDeclaracion, bool isObjeto, LinkedList<Error> errores)
        {
            if(!isDeclaracion)
                Debugger(e, "Global");

            Result result = new Result();

            foreach (string id in Ids)
            {
                if (!isDeclaracion)
                {
                    e.Add(new Sim(id, Tipo, Rol.GLOBAL, 1, H++, e.Ambito, -1, -1));
                }
            }

            return result;
        }
    }
}
