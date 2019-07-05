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
                else if (tipoExp.IsString())
                {
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
                }

                string tmpSalto = NuevoTemporal();
                rsExp.Codigo += tmpSalto + "= 10;\n";
                rsExp.Codigo += "print(\"%c\"," + tmpSalto + ");\n";
            }

            return result;
        }
    }
}
