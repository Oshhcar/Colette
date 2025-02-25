﻿using Compilador.parser.Colette.ast.entorno;
using Compilador.parser.Colette.ast.expresion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser.Colette.ast.instruccion
{
    class Asignacion : Instruccion
    {
        public Asignacion(Tipo tipo, LinkedList<Expresion> objetivo, LinkedList<LinkedList<Expresion>> valor,
            int linea, int columna) : base(linea, columna)
        {
            Objetivo = objetivo;
            Valor = valor;
            Tipo = tipo;
        }

        public LinkedList<Expresion> Objetivo { get; set; }
        public LinkedList<LinkedList<Expresion>> Valor { get; set; }
        public Tipo Tipo { get; set; }

        public override Result GetC3D(Ent e, bool funcion, bool ciclo, bool isDeclaracion, bool isObjeto, LinkedList<Error> errores)
        {
            if (!isDeclaracion)
                Debugger(e, "Asignacion");

            Result result = new Result();

            foreach (LinkedList<Expresion> valList in Valor)
            {
                if (Objetivo.Count() != valList.Count())
                {
                    if(isDeclaracion)
                        errores.AddLast(new Error("Semántico", "La lista de ids debe ser simétrica", Linea, Columna));
                    return null;
                }
            }

            for (int i = 0; i < Objetivo.Count(); i++)
            {
                Expresion obj = Objetivo.ElementAt(i);
                if (obj is Identificador || obj is Referencia || obj is AccesoLista) //verifico que sea id o referencia a atributo
                {
                    Identificador idObjetivo = null;
                    Referencia refObjetivo = null;
                    AccesoLista listObjetivo = null;

                    Result rsObj = null;

                    if (obj is Identificador)
                    {
                        idObjetivo = (Identificador)obj;
                        idObjetivo.Acceso = false;
                        idObjetivo.IsDeclaracion = isDeclaracion;

                        if (!Tipo.IsIndefinido())
                            idObjetivo.GetLocal = true;
                        rsObj = idObjetivo.GetC3D(e, funcion, ciclo, isObjeto, errores);
                    }

                    if (obj is Referencia)
                    {
                        refObjetivo = (Referencia)obj;
                        refObjetivo.Acceso = false;
                        refObjetivo.ObtenerValor = true;
                        if (!isDeclaracion)
                            rsObj = refObjetivo.GetC3D(e, funcion, ciclo, isObjeto, errores);
                        else
                            rsObj = null;
                    }

                    if (obj is AccesoLista)
                    {
                        listObjetivo = (AccesoLista)obj;
                        listObjetivo.Acceso = false;
                        //listObjetivo.IsDeclaracion = isDeclaracion;
                        if (!isDeclaracion)
                            rsObj = listObjetivo.GetC3D(e, funcion, ciclo, isObjeto, errores);
                        else
                            rsObj = null;
                    }

                    if (rsObj == null && obj is Identificador) //si no existe, creo la variable si viene con tipo
                    {
                        if (!Tipo.IsIndefinido())
                        {
                            idObjetivo.Tipo = Tipo;

                            rsObj = new Result();

                            Sim s = new Sim(((Identificador)obj).Id, Tipo, Rol.LOCAL, 1, e.GetPos(), e.Ambito, -1, -1);
                            rsObj.Simbolo = s;

                            if (isObjeto)
                                s.IsAtributo = true;

                            e.Add(s);

                            if (!isDeclaracion)
                            {
                                idObjetivo.PtrVariable = s.Pos+"";

                                string ptrStack = NuevoTemporal();
                                if (!isObjeto)
                                {
                                    rsObj.Codigo = ptrStack + " = P + " + s.Pos + ";\n";
                                    rsObj.Valor = "stack[" + ptrStack + "]";
                                }
                                else
                                {
                                    string ptrHeap = NuevoTemporal();
                                    string valorHeap = NuevoTemporal();

                                    rsObj.Codigo = ptrStack + " = P + " + 1 + ";\n";
                                    rsObj.Codigo += valorHeap + " = stack[" + ptrStack + "];\n";
                                    rsObj.Codigo += ptrHeap + " = " + valorHeap + " + " + s.Pos + ";\n";
                                    rsObj.Valor = "heap[" + ptrHeap + "]";
                                }
                            }
                        }
                        else
                        {
                            if(!isDeclaracion)
                                errores.AddLast(new Error("Semántico", "No se pudo encontrar la variable: "+idObjetivo.Id+".", Linea, Columna));
                            return null;
                        }
                    }
                    else
                    {
                        if (!Tipo.IsIndefinido() && obj is Identificador)
                        {
                            if(!isDeclaracion)
                                errores.AddLast(new Error("Semántico", "Ya se declaró una variable con el id: "+ idObjetivo.Id+".", Linea, Columna));
                            return null;
                        }
                    }

                    LinkedList<Result> rsList = new LinkedList<Result>();
                    for (int j = 0; j < Valor.Count(); j++)
                    {
                        LinkedList<Expresion> valList = Valor.ElementAt(j);
                        Expresion expI = valList.ElementAt(i);

                        if (j + 1 == Valor.Count())
                        {
                            if (!isDeclaracion)
                            {
                                if (expI is Llamada)
                                    ((Llamada)expI).PtrVariable = idObjetivo.PtrVariable;

                                Result rsTemp = expI.GetC3D(e, funcion, ciclo, isObjeto, errores);
                                if (rsTemp != null)
                                {
                                    if (obj is Identificador)
                                    {
                                        if (expI.GetTipo().Tip != idObjetivo.Tipo.Tip)
                                        {
                                            errores.AddLast(new Error("Semántico", "El valor a asignar no es del mismo tipo.", Linea, Columna));
                                            return null;
                                        }
                                    }
                                    else if (obj is Referencia)
                                    {
                                        if (expI.GetTipo().Tip != refObjetivo.Tipo.Tip)
                                        {
                                            errores.AddLast(new Error("Semántico", "El valor a asignar no es del mismo tipo.", Linea, Columna));
                                            return null;
                                        }
                                    }
                                    else if (obj is AccesoLista)
                                    {
                                        if (expI.GetTipo().Tip != listObjetivo.Tipo.Tip)
                                        {
                                            errores.AddLast(new Error("Semántico", "El valor a asignar no es del mismo tipo.", Linea, Columna));
                                            return null;
                                        }
                                    }
                                    rsList.AddLast(rsTemp);
                                }
                                else
                                {
                                    errores.AddLast(new Error("Semántico", "El valor contiene errores.", Linea, Columna));
                                    return null;
                                }
                            }
                        }
                        else
                        {
                            if (expI is Identificador)
                            {
                                if (!Tipo.IsIndefinido())
                                    ((Identificador)expI).GetLocal = true;
                                ((Identificador)expI).Acceso = false;
                                Result rsTemp = expI.GetC3D(e, funcion, ciclo, isObjeto, errores);

                                if (rsTemp == null) //si no existe, creo la variable
                                {
                                    if (!Tipo.IsIndefinido())
                                    {
                                        rsTemp = new Result();

                                        Sim s = new Sim(((Identificador)expI).Id, Tipo, Rol.LOCAL, 1, e.GetPos(), e.Ambito, -1, -1);
                                        rsTemp.Simbolo = s;

                                        if (isObjeto)
                                            s.IsAtributo = true;

                                        e.Add(s);

                                        if (!isDeclaracion)
                                        {
                                            string ptrStack = NuevoTemporal();
                                            if (!isObjeto)
                                            {
                                                rsTemp.Codigo = ptrStack + " = P + " + s.Pos + ";\n";
                                                rsTemp.Valor = "stack[" + ptrStack + "]";
                                            }
                                            else
                                            {
                                                string ptrHeap = NuevoTemporal();
                                                string valorHeap = NuevoTemporal();

                                                rsTemp.Codigo = ptrStack + " = P + " + 1 + ";\n";
                                                rsTemp.Codigo += valorHeap + " = stack[" + ptrStack + "];\n";
                                                rsTemp.Codigo += ptrHeap + " = " + valorHeap + " + " + s.Pos + ";\n";
                                                rsTemp.Valor = "heap[" + ptrHeap + "]";
                                            }

                                            rsTemp.PtrStack = s.Pos;
                                            
                                        }
                                    }
                                    else
                                    {
                                        if(!isDeclaracion)
                                            errores.AddLast(new Error("Semántico", "El valor contiene errores.", Linea, Columna));
                                        return null;
                                    }
                                }
                                else
                                {
                                    if (!Tipo.IsIndefinido())
                                    {
                                        if(!isDeclaracion)
                                            errores.AddLast(new Error("Semántico", "Ya se declaró una variable con el id: " + idObjetivo.Id + ".", Linea, Columna));
                                        return null;
                                    }
                                }
                                rsList.AddLast(rsTemp);
                            }
                            else
                                return null; /*No implementado otro tipo de valores(listas)*/
                        }
                    }

                    if (!isDeclaracion)
                    {
                        if (rsList.Count() == 1)
                        {
                            if (rsObj.Simbolo != null)
                            {
                                if (rsObj.Simbolo.Tipo.IsList())
                                {
                                    if (rsList.ElementAt(0).Tipo != null)
                                    {
                                        rsObj.Simbolo.Tipo = rsList.ElementAt(0).Tipo;
                                    }
                                }
                            }
                            //rsObj.Tipo = rsList.ElementAt(0).Tipo;
                            rsObj.Codigo += rsList.ElementAt(0).Codigo;
                            rsObj.Codigo += rsObj.Valor + " = " + rsList.ElementAt(0).Valor + ";\n";
                        }
                        else
                        {
                            Result rsAnt = rsList.ElementAt(rsList.Count() - 1);

                            for (int k = rsList.Count() - 2; k >= 0; k--)
                            {
                                Result rsAct = rsList.ElementAt(k);
                                rsAct.Codigo += rsAnt.Codigo;
                                rsAct.Codigo += rsAct.Valor + " = " + rsAnt.Valor + ";\n";

                                /*Esto solo funciona con ids*/
                                string ptrStack = NuevoTemporal();
                                rsAct.Codigo += ptrStack + " = P + " + rsAct.PtrStack + ";\n";
                                rsAct.Valor = NuevoTemporal();
                                rsAct.Codigo += rsAct.Valor + " = stack[" + ptrStack + "];\n";

                                rsAnt = rsAct;
                            }

                            if (rsObj.Simbolo != null)
                            {
                                if (rsObj.Simbolo.Tipo.IsList())
                                {
                                    if (rsAnt.Tipo != null)
                                    {
                                        rsObj.Simbolo.Tipo = rsAnt.Tipo;
                                    }
                                }
                            }

                            rsObj.Codigo += rsAnt.Codigo;
                            rsObj.Codigo += rsObj.Valor + " = " + rsAnt.Valor + ";\n";
                        }
                    }
                    if(rsObj != null)
                        result.Codigo += rsObj.Codigo;

                }
            }

            return result;
        }
    }
}
