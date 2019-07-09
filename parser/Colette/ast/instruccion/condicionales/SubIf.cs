using Compilador.parser.Colette.ast.entorno;
using Compilador.parser.Colette.ast.expresion;
using Compilador.parser.Colette.ast.expresion.operacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser.Colette.ast.instruccion.condicionales
{
    class SubIf : Instruccion
    {
        public SubIf(Expresion condicion, Bloque bloque, int linea, int columna) : base(linea, columna)
        {
            Condicion = condicion;
            Bloque = bloque;
        }

        public Expresion Condicion { get; set; }
        public Bloque Bloque { get; set; }

        public string Salida { get; set; }

        public override Result GetC3D(Ent e, bool funcion, bool ciclo, bool isDeclaracion, LinkedList<Error> errores)
        {
            Result result = new Result();

            if (Condicion != null)
            {
                if (!isDeclaracion)
                {
                    if (Condicion is Relacional)
                        ((Relacional)Condicion).Cortocircuito = true;
                    else if (Condicion is Logica)
                        ((Logica)Condicion).Evaluar = true;

                    Result rsCondicion = Condicion.GetC3D(e, funcion, ciclo, errores);

                    if (Condicion is Literal)
                    {
                        rsCondicion.EtiquetaV = NuevaEtiqueta();
                        rsCondicion.EtiquetaF = NuevaEtiqueta();

                        rsCondicion.Codigo += "ifFalse (" + rsCondicion.Valor + " == 1) goto " + rsCondicion.EtiquetaV + ";\n";
                        rsCondicion.Codigo += "goto " + rsCondicion.EtiquetaF + ";\n";
                        rsCondicion.EtiquetaV += ":\n";
                        rsCondicion.EtiquetaF += ":\n";
                    }

                    if (Condicion is Relacional || Condicion is Literal)
                    {
                        string copy = rsCondicion.EtiquetaF;
                        rsCondicion.EtiquetaF = rsCondicion.EtiquetaV;
                        rsCondicion.EtiquetaV = copy;
                    }

                    result.Codigo += rsCondicion.Codigo;
                    result.Codigo += rsCondicion.EtiquetaV;
                    result.Codigo += Bloque.GetC3D(e, funcion, ciclo, isDeclaracion, errores).Codigo;
                    result.Codigo += "goto " + Salida + ";\n";
                    result.Codigo += rsCondicion.EtiquetaF;
                }
                else
                {
                    result.Codigo += Bloque.GetC3D(e, funcion, ciclo, isDeclaracion, errores).Codigo;
                    result.Codigo += "goto " + Salida + ";\n";
                }
            }
            else
            {

                result.Codigo += Bloque.GetC3D(e, funcion, ciclo, isDeclaracion, errores).Codigo;
                result.Codigo += "goto " + Salida + ";\n";
            }

            return result;
        }
    }
}
