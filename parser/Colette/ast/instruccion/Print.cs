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
            Tipo tipoExp = Expresion.GetTipo(e);

            /*Si es literal funciona*/
            switch (tipoExp)
            {
                case Tipo.INT:
                    string tmpInt = NuevoTemporal();
                    rsExp.Codigo += tmpInt + " = " + rsExp.Valor + ";\n";
                    rsExp.Codigo += "print(\"%i\"," + tmpInt + ");\n";
                    break;
                case Tipo.DOUBLE:
                    string tmpDouble = NuevoTemporal();
                    rsExp.Codigo += tmpDouble + " = " + rsExp.Valor + ";\n";
                    rsExp.Codigo += "print(\"%f\"," + tmpDouble + ");\n";
                    break;
                case Tipo.STRING:
                    rsExp.EtiquetaV = NuevaEtiqueta();
                    rsExp.EtiquetaF = NuevaEtiqueta();
                    string etqCiclo = NuevaEtiqueta();
                    string tmpCiclo = NuevoTemporal();

                    rsExp.Codigo += etqCiclo + ":\n";
                    rsExp.Codigo += tmpCiclo + " = heap[" + rsExp.Valor + "];\n";
                    rsExp.Codigo += "if (" + tmpCiclo + " == 0) goto " + rsExp.EtiquetaV + ";\n";
                    rsExp.Codigo += "goto " + rsExp.EtiquetaF + ";\n";
                    rsExp.Codigo += rsExp.EtiquetaF + ":\n";
                    rsExp.Codigo += "print(\"%c\"," + tmpCiclo + ");\n";
                    rsExp.Codigo += rsExp.Valor + " = " + rsExp.Valor + " + 1;\n";
                    rsExp.Codigo += "goto " + etqCiclo + ";\n";
                    rsExp.Codigo += rsExp.EtiquetaV + ":\n";

                    string tmpSalto = NuevoTemporal();
                    rsExp.Codigo += tmpSalto + "= 10;\n";
                    rsExp.Codigo += "print(\"%c\"," + tmpSalto + ");\n";
                    break;
            }
           

            return rsExp;
        }
    }
}
