using Compilador.parser.Colette.ast.entorno;
using Compilador.parser.Colette.ast.expresion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser.Colette.ast.instruccion.ciclos
{
    class For : Instruccion
    {
        public For(LinkedList<Expresion> objetivo, Expresion valor, Bloque bloque, Bloque bloqueElse, int linea, int columna) : base(linea, columna)
        {
            Objetivo = objetivo;
            Valor = valor;
            Bloque = bloque;
            BloqueElse = bloqueElse;
        }

        public LinkedList<Expresion> Objetivo { get; set; }
        public Expresion Valor { get; set; }
        public Bloque Bloque { get; set; }
        public Bloque BloqueElse { get; set; }

        public override Result GetC3D(Ent e, bool funcion, bool ciclo, bool isDeclaracion, bool isObjeto, LinkedList<Error> errores)
        {
            Result result = new Result();

            if (!isDeclaracion)
            {
                if (Objetivo.Count() > 0)
                {
                    Expresion objetivo = Objetivo.ElementAt(0); /*Solo funciona con uno*/
                    Result rsObjetivo = null;

                    Identificador idObjetivo = null;
                    Referencia refObjetivo = null;
                    AccesoLista listObjetivo = null;

                    if (objetivo is Identificador)
                    {
                        idObjetivo = (Identificador)objetivo;
                        idObjetivo.Acceso = false;
                        idObjetivo.IsDeclaracion = isDeclaracion;

                        rsObjetivo = idObjetivo.GetC3D(e, funcion, ciclo, isObjeto, errores);
                    }

                    if (objetivo is Referencia)
                    {
                        refObjetivo = (Referencia)objetivo;
                        refObjetivo.Acceso = false;
                        refObjetivo.ObtenerValor = true;

                        rsObjetivo = refObjetivo.GetC3D(e, funcion, ciclo, isObjeto, errores);

                    }

                    if (objetivo is AccesoLista)
                    {
                        listObjetivo = (AccesoLista)objetivo;
                        listObjetivo.Acceso = false;

                        rsObjetivo = listObjetivo.GetC3D(e, funcion, ciclo, isObjeto, errores);
                    }

                    if (rsObjetivo != null)
                    {
                        if (rsObjetivo.Valor != null)
                        {
                            result.Codigo = rsObjetivo.Codigo;

                            Result rsValor = Valor.GetC3D(e, funcion, ciclo, isObjeto, errores);

                            if (rsValor != null)
                            {
                                if (Valor.GetTipo().IsList())
                                {
                                    if (rsValor != null)
                                    {
                                        result.Codigo += rsValor.Codigo;

                                        string ptr = NuevoTemporal();
                                        result.Codigo += ptr + " = " + rsValor.Valor + ";\n";

                                        result.EtiquetaF = NuevaEtiqueta();
                                        result.EtiquetaV = NuevaEtiqueta();
                                        string etqCiclo = NuevaEtiqueta();

                                        result.Codigo += etqCiclo + ":\n";

                                        result.Codigo += "ifFalse (" + ptr + " >= 0 ) goto " + result.EtiquetaF + ";\n";
                                        result.Codigo += "goto " + result.EtiquetaV + ";\n";
                                        result.Codigo += result.EtiquetaV + ":\n";
                                        result.Codigo += rsObjetivo.Valor + " = heap[" + ptr + "];\n";
                                        result.Codigo += ptr + " = " + ptr + " + 1;\n";
                                        result.Codigo += ptr + " = heap[" + ptr + "];\n";

                                        result.Codigo += Bloque.GetC3D(e, funcion, ciclo, isDeclaracion, isObjeto, errores).Codigo; //arreglar

                                        result.Codigo += "goto " + etqCiclo + ";\n";
                                        result.Codigo += result.EtiquetaF + ":\n";
                                    }
                                    else
                                    {
                                        errores.AddLast(new Error("Semántico", "Error obteniendo el valor a iterar.", Linea, Columna));
                                    }

                                }/*buscar otras*/
                            }
                            else
                            {
                                errores.AddLast(new Error("Semántico", "Error obteniendo el valor a iterar.", Linea, Columna));
                            }

                        }
                        else
                        {
                            errores.AddLast(new Error("Semántico", "Error obteniendo el target.", Linea, Columna));

                        }
                    }
                    else
                    {
                        errores.AddLast(new Error("Semántico", "Error obteniendo el target.", Linea, Columna));
                    }
                }
            }
            else
            {
                if (BloqueElse == null)
                {
                    Ent local = new Ent(e.Ambito + "_for", e);
                    local.Pos = e.Pos;
                    result.Codigo += Bloque.GetC3D(local, funcion, ciclo, isDeclaracion, isObjeto, errores).Codigo;
                    e.Pos = local.Pos;
                }
                else
                {
                    Ent local = new Ent(e.Ambito + "_for", e);
                    local.Pos = e.Pos;
                    result.Codigo += Bloque.GetC3D(local, funcion, ciclo, isDeclaracion, isObjeto, errores).Codigo;
                    e.Pos = local.Pos;
                    Ent local2 = new Ent(e.Ambito + "_else", e);
                    local2.Pos = e.Pos;
                    result.Codigo += BloqueElse.GetC3D(local2, funcion, ciclo, isDeclaracion, isObjeto, errores).Codigo;
                    e.Pos = local.Pos;
                }
            }

            return result;
        }
    }
}
