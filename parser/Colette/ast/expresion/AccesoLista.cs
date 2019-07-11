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
            Acceso = true;
        }

        public Expresion Objetivo { get; set; }
        public Expresion Posicion { get; set; }
        public Tipo Tipo { get; set; }
        public bool Acceso { get; set; }


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
                            //Codigo de la posicion
                            result.Codigo += rsPosicion.Codigo;
                            string pos = NuevoTemporal();
                            result.Codigo += pos + " = " + rsPosicion.Valor + ";\n";


                            result.Codigo += rsObjetivo.Codigo;

                            /*Verificar lista vacía -1*/
                            string ptr = NuevoTemporal();
                            result.Codigo += ptr + " = " + rsObjetivo.Valor + ";\n";

                            result.Valor = NuevoTemporal();

                            if (Acceso)
                                result.Codigo += result.Valor + " = heap[" + rsObjetivo.Valor + "];\n";
                            else
                                result.Valor = "heap[" + rsObjetivo.Valor + "]";


                            result.EtiquetaV = NuevaEtiqueta();
                            result.EtiquetaF = NuevaEtiqueta();
                            string etqCiclo = NuevaEtiqueta();
                            string tmpCiclo = NuevoTemporal();

                            result.Codigo += tmpCiclo + " = 0;\n";
                            result.Codigo += etqCiclo + ":\n";
                            result.Codigo += "if (" + tmpCiclo + " == " + pos + ") goto " + result.EtiquetaV + ";\n";
                            result.Codigo += "goto " + result.EtiquetaF + ";\n";
                            result.Codigo += result.EtiquetaF + ":\n";

                            result.Codigo += ptr + " = " + ptr + " + 1;\n";
                            result.Codigo += ptr + " = heap[" + ptr + "];\n";

                            result.Codigo += "if (" + ptr + " < 0) goto " + result.EtiquetaV + ";\n";

                            if (Acceso)
                                result.Codigo += result.Valor + " = heap[" + ptr + "];\n";
                            else
                                result.Valor = "heap[" + ptr + "]";

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
                    errores.AddLast(new Error("Semántico", "No es una lista.", Linea, Columna));
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
