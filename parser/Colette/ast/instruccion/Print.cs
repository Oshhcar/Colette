using Compilador.parser.Colette.ast.entorno;
using Compilador.parser.Colette.ast.expresion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser.Colette.ast.instruccion
{
    class Print : Instruccion
    {
        public Print(Expresion expresion, int linea, int columna) : base(linea, columna)
        {
            Expresion = expresion;
        }

        public Expresion Expresion { get; set; }

        public override Result GetC3D(Ent e)
        {
            Result rsExp = Expresion.GetC3D(e);

            rsExp.Codigo += "print(\"%i\"," + rsExp.Valor + ");\n";

            return rsExp;
        }
    }
}
