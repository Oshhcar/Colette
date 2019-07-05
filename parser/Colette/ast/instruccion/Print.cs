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
            Result result = new Result
            {
                Codigo = ""
            };

            Result rsExp = Expresion.GetC3D(e);
            Tipo tipoExp = Expresion.GetTipo();

            if (!tipoExp.IsIndefinido())
            {
                /*verificar objeto y todas las demas*/
                result.Codigo += rsExp.Codigo;

                if (tipoExp.IsInt())
                {
                    if (Expresion is Literal)
                    {
                        string tmp = NuevoTemporal();
                        result.Codigo += tmp + " = " + rsExp.Valor + ";\n";
                        result.Codigo += "print(\"%i\"," + tmp + ");\n";
                    }
                    else
                    {
                        result.Codigo += "print(\"%i\"," + rsExp.Valor + ");\n";
                    }

                }
                else if (tipoExp.IsDouble())
                {
                    if (Expresion is Literal)
                    {
                        string tmp = NuevoTemporal();
                        result.Codigo += tmp + " = " + rsExp.Valor + ";\n";
                        result.Codigo += "print(\"%f\"," + tmp + ");\n";
                    }
                    else
                    {
                        result.Codigo += "print(\"%f\"," + rsExp.Valor + ");\n";
                    }
                }
                else if (tipoExp.IsBoolean())
                {
                    result.EtiquetaV = NuevaEtiqueta();
                    result.EtiquetaF = NuevaEtiqueta();
                    string etqSalida = NuevaEtiqueta();
                    string tmpCiclo = NuevoTemporal();

                    result.Codigo += "if (" + rsExp.Valor + " == 0) goto " + result.EtiquetaV + ";\n";
                    result.Codigo += "goto " + result.EtiquetaF + ";\n";
                    result.Codigo += result.EtiquetaV + ":\n";
                    result.Codigo += tmpCiclo + " = 70;\n";
                    result.Codigo += "print(\"%c\", " + tmpCiclo + ");\n";
                    result.Codigo += tmpCiclo + " = 97;\n";
                    result.Codigo += "print(\"%c\", " + tmpCiclo + ");\n";
                    result.Codigo += tmpCiclo + " = 108;\n";
                    result.Codigo += "print(\"%c\", " + tmpCiclo + ");\n";
                    result.Codigo += tmpCiclo + " = 115;\n";
                    result.Codigo += "print(\"%c\", " + tmpCiclo + ");\n";
                    result.Codigo += tmpCiclo + " = 101;\n";
                    result.Codigo += "print(\"%c\", " + tmpCiclo + ");\n";
                    result.Codigo += "goto " + etqSalida + ";\n";
                    result.Codigo += result.EtiquetaF + ":\n";
                    result.Codigo += tmpCiclo + " = 84;\n";
                    result.Codigo += "print(\"%c\", " + tmpCiclo + ");\n";
                    result.Codigo += tmpCiclo + " = 114;\n";
                    result.Codigo += "print(\"%c\", " + tmpCiclo + ");\n";
                    result.Codigo += tmpCiclo + " = 117;\n";
                    result.Codigo += "print(\"%c\", " + tmpCiclo + ");\n";
                    result.Codigo += tmpCiclo + " = 101;\n";
                    result.Codigo += "print(\"%c\", " + tmpCiclo + ");\n";
                    result.Codigo += etqSalida + ":\n";

                }
                else if (tipoExp.IsNone())
                {
                    string tmp = NuevoTemporal();
                    result.Codigo += tmp + " = 78;\n";
                    result.Codigo += "print(\"%c\", " + tmp + ");\n";
                    result.Codigo += tmp + " = 111;\n";
                    result.Codigo += "print(\"%c\", " + tmp + ");\n";
                    result.Codigo += tmp + " = 110;\n";
                    result.Codigo += "print(\"%c\", " + tmp + ");\n";
                    result.Codigo += tmp + " = 101;\n";
                    result.Codigo += "print(\"%c\", " + tmp + ");\n";
                }
                else if (tipoExp.IsString())
                {
                    result.EtiquetaV = NuevaEtiqueta();
                    result.EtiquetaF = NuevaEtiqueta();
                    string etqCiclo = NuevaEtiqueta();
                    string tmpCiclo = NuevoTemporal();

                    result.Codigo += etqCiclo + ":\n";
                    result.Codigo += tmpCiclo + " = heap[" + rsExp.Valor + "];\n";
                    result.Codigo += "if (" + tmpCiclo + " == 0) goto " + result.EtiquetaV + ";\n";
                    result.Codigo += "goto " + result.EtiquetaF + ";\n";
                    result.Codigo += result.EtiquetaF + ":\n";
                    result.Codigo += "print(\"%c\"," + tmpCiclo + ");\n";
                    result.Codigo += rsExp.Valor + " = " + rsExp.Valor + " + 1;\n";
                    result.Codigo += "goto " + etqCiclo + ";\n";
                    result.Codigo += result.EtiquetaV + ":\n";
                }

                string tmpSalto = NuevoTemporal();
                result.Codigo += tmpSalto + " = 10;\n";
                result.Codigo += "print(\"%c\"," + tmpSalto + ");\n";
            }

            return result;
        }
    }
}
