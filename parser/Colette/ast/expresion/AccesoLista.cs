using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser.Colette.ast.entorno;

namespace Compilador.parser.Colette.ast.expresion
{
    class AccesoLista : Expresion
    {
        public AccesoLista(Expresion objetivo, Expresion posicion, int linea, int columna) : base(linea, columna)
        {
            Objetivo = objetivo;
            Posicion = posicion;
            Tipo = new Tipo(Tipo.Type.INDEFINIDO);
        }

        public Expresion Objetivo { get; set; }
        public Expresion Posicion { get; set; }
        public Tipo Tipo { get; set; }

        public override Result GetC3D(Ent e, bool funcion, bool ciclo, bool isObjeto, LinkedList<Error> errores)
        {
            Result result = new Result();

            Result rsObjetivo = Objetivo.GetC3D(e, funcion, ciclo, isObjeto, errores);

            if (rsObjetivo != null)
            {
                if (Objetivo.GetTipo().IsList())
                {
                    Result rsPosicion = Posicion.GetC3D(e, funcion, ciclo, isObjeto, errores);
                    if (rsPosicion != null)
                    {
                        if (Posicion.GetTipo().IsInt())
                        {
                            result.Codigo = rsObjetivo.Codigo;
                            string ptr = NuevoTemporal();
                            result.Codigo += ptr + "= heap[" + rsObjetivo.Valor + "];\n";

                            result.Codigo += rsPosicion.Codigo;
                            string pos = NuevoTemporal();
                            result.Codigo += pos + " = " + rsPosicion.Valor + ";\n";

                            /*Verificar lista vacía -1*/
                            string ptrValor = NuevoTemporal();
                            result.Codigo += ptrValor + " = " + ptr + "+ 1;\n";

                            result.Valor = NuevoTemporal();
                            result.Codigo += result.Valor + " = heap[" + ptrValor + "];\n";


                            result.EtiquetaV = NuevaEtiqueta();
                            result.EtiquetaF = NuevaEtiqueta();
                            string etqCiclo = NuevaEtiqueta();
                            string tmpCiclo = NuevoTemporal();

                            result.Codigo += tmpCiclo + " = 0;\n";
                            result.Codigo += etqCiclo + ":\n";
                            result.Codigo += "if (" + tmpCiclo + " == " + pos + ") goto " + result.EtiquetaV + ";\n";
                            result.Codigo += "goto " + result.EtiquetaF + ";\n";
                            result.Codigo += result.EtiquetaF + ":\n";
                            result.Codigo += ptrValor + " = " + ptrValor + "+ 2;\n";
                            result.Codigo += result.Valor + " = heap[" + ptrValor + "];\n";
                            result.Codigo += tmpCiclo + " = " + tmpCiclo + " + 1;\n";
                            result.Codigo += "goto " + etqCiclo + ";\n";
                            result.Codigo += result.EtiquetaV + ":\n";


                            Tipo.Tip = Objetivo.GetTipo().SubTip;
                        }
                        else
                        {
                            errores.AddLast(new Error("Semántico", "La posición debe ser entero.", Linea, Columna));
                            return null;
                        }
                    }
                }
                else
                {
                    errores.AddLast(new Error("Semántico","No es una lista.", Linea, Columna));
                    return null;
                }
            }

            return result;
        }

        public override Tipo GetTipo()
        {
            return Tipo;
        }
    }
}
