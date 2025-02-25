﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser.Colette.ast.entorno;

namespace Compilador.parser.Colette.ast.expresion
{
    class Return : Expresion
    {
        public Return(Expresion valor, int linea, int columna) : base(linea, columna)
        {
            Valor = valor;
            Tipo = new Tipo(Tipo.Type.INDEFINIDO);
        }

        public Expresion Valor { get; set; }
        public Tipo Tipo { get; set; }

        public override Result GetC3D(Ent e, bool funcion, bool ciclo, bool isObjeto, LinkedList<Error> errores)
        {
            if (funcion)
            {
                Result result = new Result();
                Sim ret = e.GetGlobal("return");

                if (Valor != null)
                {
                    if (!ret.Tipo.IsVoid())
                    {
                        Result rsValor = Valor.GetC3D(e, funcion, ciclo, isObjeto, errores);

                        if (!Valor.GetTipo().IsIndefinido())
                        {
                            if (rsValor.Valor != null)
                            {
                                if (ret.Tipo.Tip == Valor.GetTipo().Tip)
                                {
                                    result.Valor = NuevoTemporal();
                                    result.Codigo += result.Valor + " = P + " + ret.Pos + ";\n";

                                    result.Codigo += rsValor.Codigo;
                                    result.Codigo += "stack[" + result.Valor + "] = " + rsValor.Valor + ";\n";
                                }
                                else
                                {
                                    errores.AddLast(new Error("Semántico", "El tipo de return no es el mismo de la función: " + ret.Tipo.ToString() + ".", Linea, Columna));
                                    return null;
                                }
                            }
                            else
                            {
                                errores.AddLast(new Error("Semántico", "El valor del return contiene errores.", Linea, Columna));
                                return null;
                            }
                        }
                        else
                        {
                            errores.AddLast(new Error("Semántico", "El valor del return contiene errores.", Linea, Columna));
                            return null;
                        }
                    }
                    else
                    {
                        errores.AddLast(new Error("Semántico", "No se espera valor en return.", Linea, Columna));
                    }
                }
                else
                {
                    if (!ret.Tipo.IsVoid())
                    {
                        errores.AddLast(new Error("Semántico", "Se espera valor en return.", Linea, Columna));
                        return null;
                    }
                }

                result.Codigo += "goto " + e.EtiquetaSalida + ";\n";

                return result;
            }
            else
            {
                errores.AddLast(new Error("Semántico", "El return no se encuentra dentro de una función", Linea, Columna));
                return null;
            }
        }

        public override Tipo GetTipo()
        {
            return Tipo;
        }
    }
}
