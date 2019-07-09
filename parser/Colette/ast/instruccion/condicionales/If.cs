using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser.Colette.ast.entorno;

namespace Compilador.parser.Colette.ast.instruccion.condicionales
{
    class If : Instruccion
    {
        public If(LinkedList<SubIf> subIfs, int linea, int columna) : base(linea, columna)
        {
            SubIfs = subIfs;
        }

        public LinkedList<SubIf> SubIfs { get; set; }

        public override Result GetC3D(Ent e, bool funcion, bool ciclo, bool isDeclaracion, LinkedList<Error> errores)
        {
            Result result = new Result();
            string etqSalida = NuevaEtiqueta();

            foreach (SubIf subif in SubIfs)
            {
                subif.Salida = etqSalida;
                result.Codigo += subif.GetC3D(e, funcion, ciclo, isDeclaracion, errores).Codigo;
            }

            result.Codigo += etqSalida + ":\n";

            return result;
        }
    }
}
