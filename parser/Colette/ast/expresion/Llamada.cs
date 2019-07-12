using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser.Colette.ast.entorno;
using Compilador.parser.Colette.ast.instruccion;

namespace Compilador.parser.Colette.ast.expresion
{
    class Llamada : Expresion
    {
        public Llamada(string id, LinkedList<Expresion> parametros, int linea, int columna) : base(linea, columna)
        {
            Expresion = null;
            Id = id;
            Parametros = parametros;
            Tipo = new Tipo(Tipo.Type.INDEFINIDO);
            ObtenerReturn = true;
            PtrVariable = null;
        }

        public Llamada(Expresion expresion, LinkedList<Expresion> parametros, int linea, int columna) : base(linea, columna)
        {
            Expresion = expresion;
            Id = null;
            Parametros = parametros;
            Tipo = new Tipo(Tipo.Type.INDEFINIDO);
            ObtenerReturn = true;
            PtrVariable = null;
        }

        public Expresion Expresion { get; set; }
        public string Id { get; set; }
        public LinkedList<Expresion> Parametros { get; set; }
        public Tipo Tipo { get; set; }
        public bool ObtenerReturn { get; set; }
        public String PtrVariable { get; set; }

        public override Result GetC3D(Ent e, bool funcion, bool ciclo, bool isObjeto, LinkedList<Error> errores)
        {
            Result result = new Result();

            if (Expresion == null)
            {
                string firma;
                firma = Id;

                if (Parametros != null)
                {
                    LinkedList<Result> rsParametros = new LinkedList<Result>();
                    foreach (Expresion parametro in Parametros)
                    {
                        Result rsParametro = parametro.GetC3D(e, funcion, ciclo, isObjeto, errores);
                        if (rsParametro != null)
                        {
                            if (!parametro.GetTipo().IsIndefinido())
                            {
                                rsParametros.AddLast(rsParametro);
                                firma += "_" + parametro.GetTipo().ToString();
                                continue;
                            }
                        }

                        errores.AddLast(new Error("Semántico", "El parámetro contiene error.", Linea, Columna));

                        return null;
                    }

                    Sim metodo = e.GetMetodo(firma);
                    
                    if (metodo != null)
                    {
                        Tipo = metodo.Tipo;

                        int pos = 2; /*inicia en dos por return y self*/

                        foreach (Result rsParametro in rsParametros)
                        {
                            string ptrCambio = NuevoTemporal();
                            string tmp = NuevoTemporal();
                            result.Codigo += ptrCambio + " = P + " + e.Size + ";\n"; //Cambio simulado
                            result.Codigo += tmp + " = " + ptrCambio + " + " + pos++ + ";\n";

                            result.Codigo += rsParametro.Codigo;

                            result.Codigo += "stack[" + tmp + "] = " + rsParametro.Valor + ";\n";
                        }

                        result.Codigo += "P = P + " + e.Size + ";\n";
                        result.Codigo += "call " + firma + ";\n";

                        if (ObtenerReturn)/*Si se espera valor*/
                        {
                            string ptrReturn = NuevoTemporal();
                            result.Valor = NuevoTemporal();

                            result.Codigo += ptrReturn + " = P + 0;\n";
                            result.Codigo += result.Valor + " = stack[" + ptrReturn + "];\n";

                        }
                        result.Codigo += "P = P - " + e.Size + ";\n";

                    }
                    else
                    {
                        /*Buscar si es posible clase*/
                        errores.AddLast(new Error("Semántico", "La función: " + Id + " no está declarada.", Linea, Columna));
                        return null;
                    }
                }
                else
                {
                    Sim metodo = e.GetMetodo(firma);

                    if (metodo != null)
                    {
                        Tipo = metodo.Tipo;
                        result.Codigo += "P = P + " + e.Size + ";\n";
                        result.Codigo += "call " + firma + ";\n";

                        if (ObtenerReturn)/*Si se espera valor*/
                        {
                            string ptrReturn = NuevoTemporal();
                            result.Valor = NuevoTemporal();

                            result.Codigo += ptrReturn + " = P + 0;\n";
                            result.Codigo += result.Valor + " = stack[" + ptrReturn + "];\n";

                        }

                        result.Codigo += "P = P - " + e.Size + ";\n";
                    }
                    else
                    {
                        /*Buscar si es posible clase*/
                        Sim clase = e.GetClase(Id);

                        if (clase != null)
                        {
                            if (ObtenerReturn)
                            {
                                if (PtrVariable != null)
                                {
                                    string ptrCambio = NuevoTemporal();
                                    string tmp = NuevoTemporal();
                                    string ptrVariable = NuevoTemporal();
                                    result.Valor = NuevoTemporal();

                                    result.Codigo += ptrCambio + " = P + " + e.Size + ";\n"; //Cambio simulado
                                    result.Codigo += tmp + " = " + ptrCambio + " + 1;\n"; //Posicion slef
                                    result.Codigo += ptrVariable + " = P + " + PtrVariable + " ;\n";
                                    result.Codigo += "stack[" + tmp + "] = " + ptrVariable + ";\n";
                                    result.Codigo += result.Valor + " = H;\n";

                                    result.Codigo += "P = P + " + e.Size + ";\n";
                                    result.Codigo += "call " + Id + "_" + Id + ";\n";
                                    result.Codigo += "P = P - " + e.Size + ";\n";

                                    Tipo = new Tipo(clase.Id);
                                }
                            }
                        }
                        else
                        {
                            errores.AddLast(new Error("Semántico", "La función: " + Id + " no está declarada.", Linea, Columna));
                            return null;
                        }
                    }
                }
            }
            else
            {
                /*acceso a objetos o atributos*/
                if (Expresion is Referencia refExpresion)
                {
                    refExpresion.Acceso = false;

                    Result rsExpresion = refExpresion.GetC3D(e, funcion, ciclo, isObjeto, errores);

                    if (rsExpresion != null)
                    {
                        if (refExpresion.Simbolo != null)
                        {
                            switch (refExpresion.Id)
                            {
                                case "append":
                                    if (Parametros != null)
                                    {
                                        if (Parametros.Count() > 0)
                                        {
                                            if (refExpresion.Simbolo.Tipo.IsList())
                                            {
                                                Expresion valor = Parametros.ElementAt(0);
                                                Result rsValor = valor.GetC3D(e, funcion, ciclo, isObjeto, errores);

                                                if (rsValor != null)
                                                {
                                                    if (!valor.GetTipo().IsIndefinido())
                                                    {
                                                        if (valor.GetTipo().Tip == refExpresion.Simbolo.Tipo.SubTip)
                                                        {
                                                            result.Codigo = rsValor.Codigo;

                                                            string ptrStack = NuevoTemporal();
                                                            result.Codigo += ptrStack + " = P + " + refExpresion.Simbolo.Pos + ";\n";

                                                            string ptrHeap = NuevoTemporal();
                                                            result.Codigo += ptrHeap + " = stack[" + ptrStack + "];\n";

                                                            string ptr = NuevoTemporal();
                                                            result.Codigo += ptr + " = stack[" + ptrStack + "];\n";

                                                            result.EtiquetaF = NuevaEtiqueta();
                                                            result.EtiquetaV = NuevaEtiqueta();
                                                            string etqCiclo = NuevaEtiqueta();

                                                            result.Valor = NuevoTemporal();
                                                            result.Codigo += result.Valor + " = heap[" + ptr + "];\n";

                                                            result.Codigo += etqCiclo + ":\n";
                                                            result.Codigo += "ifFalse (" + ptr + " >= 0 ) goto " + result.EtiquetaF + ";\n";
                                                            result.Codigo += "goto " + result.EtiquetaV + ";\n";
                                                            result.Codigo += result.EtiquetaV + ":\n";

                                                            result.Codigo += result.Valor + " = heap[" + ptr + "];\n";
                                                            result.Codigo += ptrHeap + " = " + ptr + " + 1;\n";
                                                            result.Codigo += ptr + " = heap[" + ptrHeap + "];\n";

                                                            result.Codigo += "goto " + etqCiclo + ";\n";
                                                            result.Codigo += result.EtiquetaF + ":\n";

                                                            result.Codigo += "heap[" + ptrHeap + "] = H;\n";
                                                            result.Codigo += "heap[H] = " + rsValor.Valor + ";\n";
                                                            result.Codigo += "H = H + 1;\n";
                                                            result.Codigo += "heap[H] = 0 - 1;\n";
                                                            result.Codigo += "H = H + 1;\n";

                                                        }
                                                        else
                                                        {
                                                            errores.AddLast(new Error("Semántico", "El valor a agregar no es del mismo tipo.", Linea, Columna));
                                                        }
                                                    }
                                                    else
                                                    {
                                                        errores.AddLast(new Error("Semántico", "Error en valor.", Linea, Columna));
                                                    }
                                                }
                                                else
                                                {
                                                    errores.AddLast(new Error("Semántico", "Error en valor.", Linea, Columna));
                                                }
                                            }
                                            else
                                            {
                                                errores.AddLast(new Error("Semántico", "La variable no es una lista.", Linea, Columna));
                                            }
                                        }
                                    }
                                    break;
                                default:
                                    Sim clase = e.GetClase(refExpresion.Simbolo.Tipo.Objeto);
                                    if (clase != null)
                                    {
                                        //PrintTabla t = new PrintTabla(0, 0);
                                        //t.GetC3D(clase.Entorno, false, false, false, errores);

                                        string firma;
                                        firma = refExpresion.Id;

                                        if (Parametros != null)
                                        {
                                            /*con parametros*/
                                            LinkedList<Result> rsParametros = new LinkedList<Result>();
                                            foreach (Expresion parametro in Parametros)
                                            {
                                                Result rsParametro = parametro.GetC3D(e, funcion, ciclo, isObjeto, errores);
                                                if (rsParametro != null)
                                                {
                                                    if (!parametro.GetTipo().IsIndefinido())
                                                    {
                                                        rsParametros.AddLast(rsParametro);
                                                        firma += "_" + parametro.GetTipo().ToString();
                                                        continue;
                                                    }
                                                }
                                                errores.AddLast(new Error("Semántico", "El parámetro contiene error.", Linea, Columna));
                                                return null;
                                            }

                                            Sim metodo = clase.Entorno.GetMetodo(firma);

                                            if (metodo != null)
                                            {
                                                Tipo = metodo.Tipo;

                                                /*Paso de parametro self(h)*/
                                                string ptrCambioSelf = NuevoTemporal();
                                                string tmpSelf = NuevoTemporal();
                                                result.Codigo += ptrCambioSelf + " = P + " + e.Size + ";\n"; //Cambio simulado
                                                result.Codigo += tmpSelf + " = " + ptrCambioSelf + " + " + 1 + ";\n";

                                                string ptrStackSelf = NuevoTemporal();
                                                string valorSelf = NuevoTemporal();
                                                result.Codigo += ptrStackSelf + " = P + " + refExpresion.Simbolo.Pos + ";\n";
                                                result.Codigo += valorSelf + " = stack[" + ptrStackSelf + "];\n";
                                                result.Codigo += "stack[" + tmpSelf + "] = " + valorSelf + ";\n";

                                                int pos = 2; /*inicia en dos por return y self*/
                                                foreach (Result rsParametro in rsParametros)
                                                {
                                                    string ptrCambio = NuevoTemporal();
                                                    string tmp = NuevoTemporal();
                                                    result.Codigo += ptrCambio + " = P + " + e.Size + ";\n"; //Cambio simulado
                                                    result.Codigo += tmp + " = " + ptrCambio + " + " + pos++ + ";\n";

                                                    result.Codigo += rsParametro.Codigo;

                                                    result.Codigo += "stack[" + tmp + "] = " + rsParametro.Valor + ";\n";
                                                }

                                                result.Codigo += "P = P + " + e.Size + ";\n";
                                                result.Codigo += "call " + firma + ";\n";

                                                if (ObtenerReturn)/*Si se espera valor*/
                                                {
                                                    string ptrReturn = NuevoTemporal();
                                                    result.Valor = NuevoTemporal();

                                                    result.Codigo += ptrReturn + " = P + 0;\n";
                                                    result.Codigo += result.Valor + " = stack[" + ptrReturn + "];\n";

                                                }
                                                result.Codigo += "P = P - " + e.Size + ";\n";

                                            }
                                            else
                                            {
                                                errores.AddLast(new Error("Semántico", "La función: " + refExpresion.Id + " no está declarada.", Linea, Columna));
                                                return null;
                                            }

                                        }
                                        else
                                        {
                                            Sim metodo = clase.Entorno.GetMetodo(firma);

                                            if (metodo != null)
                                            {
                                                Tipo = metodo.Tipo;

                                                /*Paso de parametro self(h)*/
                                                string ptrCambioSelf = NuevoTemporal();
                                                string tmpSelf = NuevoTemporal();
                                                result.Codigo += ptrCambioSelf + " = P + " + e.Size + ";\n"; //Cambio simulado
                                                result.Codigo += tmpSelf + " = " + ptrCambioSelf + " + " + 1 + ";\n";

                                                string ptrStackSelf = NuevoTemporal();
                                                string valorSelf = NuevoTemporal();
                                                result.Codigo += ptrStackSelf + " = P + " + refExpresion.Simbolo.Pos + ";\n";
                                                result.Codigo += valorSelf + " = stack[" + ptrStackSelf + "];\n";
                                                result.Codigo += "stack[" + tmpSelf + "] = " + valorSelf + ";\n";

                                                result.Codigo += "P = P + " + e.Size + ";\n";
                                                result.Codigo += "call " + firma + ";\n";

                                                if (ObtenerReturn)/*Si se espera valor*/
                                                {
                                                    string ptrReturn = NuevoTemporal();
                                                    result.Valor = NuevoTemporal();

                                                    result.Codigo += ptrReturn + " = P + 0;\n";
                                                    result.Codigo += result.Valor + " = stack[" + ptrReturn + "];\n";

                                                }

                                                result.Codigo += "P = P - " + e.Size + ";\n";
                                            }
                                            else
                                            {
                                                errores.AddLast(new Error("Semántico", "La función: " + refExpresion.Id + " no está declarada.", Linea, Columna));
                                                return null;
                                            }
                                        }
                                    }
                                    break;
                            }      
                        }
                    }

                }
                else
                {
                    /*Revisar*/
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
