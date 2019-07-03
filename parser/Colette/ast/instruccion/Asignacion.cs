using Compilador.parser.Colette.ast.entorno;
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
        public Asignacion(LinkedList<Expresion> objetivo, LinkedList<LinkedList<Expresion>> valor,
            int linea, int columna) : base(linea, columna)
        {
            Objetivo = objetivo;
            Valor = valor;
        }

        public LinkedList<Expresion> Objetivo { get; set; }
        public LinkedList<LinkedList<Expresion>> Valor { get; set; }

        public override Result GetC3D(Ent e)
        {
            Result result = new Result();

            foreach (LinkedList<Expresion> val in Valor)
            {
                if (Objetivo.Count() != val.Count())
                {
                    Console.WriteLine("Error! Listas no simetricas. Linea: " + Linea);
                    return null;
                }
            }

            LinkedList<Result> resultValor = new LinkedList<Result>();

            for (int i = Valor.Count()-1; i >= 0; i--)
            {
                if (resultValor.Count() == 0) /*Primera iteración*/
                {
                    foreach (Expresion expI in Valor.ElementAt(i)) /*Verificar que sean valores*/
                    {
                        Result rsTemp = expI.GetC3D(e);
                        if (rsTemp != null)
                            resultValor.AddLast(rsTemp);
                        else
                        {
                            Console.WriteLine("Error! No se pudo encontrar el valor. Linea: " + Linea);
                            return null;
                        }
                    }
                }
                else
                {
                    LinkedList<Result> resultAux = new LinkedList<Result>();

                    foreach (Expresion expI in Valor.ElementAt(i))
                    {
                        if (expI is Identificador) //verifico que sea id
                        {
                            ((Identificador)expI).Acceso = false;
                            Sim rsSim = e.Get(((Identificador)expI).Id);
                            Result rsExp = expI.GetC3D(e);

                            string ptrStack;

                            if (rsExp == null) //si no existe, creo la variable
                            {
                                rsExp = new Result();

                                Sim s = new Sim(((Identificador)expI).Id, Tipo.INT, Rol.LOCAL, 1, e.GetPos(), e.Ambito, -1, -1);
                                e.Add(s);
                                rsSim = s;

                                ptrStack = NuevoTemporal();
                                rsExp.Codigo = ptrStack + " = P + " + s.Pos + ";\n";
                                rsExp.Valor = "stack[" + ptrStack + "]";
                            }

                            /*ASIGNO*/
                            rsExp.Codigo += resultValor.ElementAt(i).Codigo;
                            rsExp.Codigo += rsExp.Valor + " = " + resultValor.ElementAt(i).Valor + ";\n";

                            /*Guardo el acceso para la siguiente iteración*/
                            ptrStack = NuevoTemporal();
                            rsExp.Codigo = ptrStack + " = P + " + rsSim.Pos + ";\n";
                            rsExp.Valor = NuevoTemporal();
                            rsExp.Codigo += rsExp.Valor + " = stack[" + ptrStack + "];\n";

                            resultAux.AddLast(rsExp);
                        }
                        else
                            return null; /*No implementado*/
                    }

                    resultValor.Clear();
                    resultValor = resultAux;
                    resultValor.Clear();
                }
            }

            for (int i = Objetivo.Count() - 1; i >= 0; i--)
            {
                Expresion obj = Objetivo.ElementAt(i);
                result.Codigo += resultValor.ElementAt(i).Codigo;
                if (obj is Identificador) //verifico que sea id
                {
                    Result rsObj = obj.GetC3D(e);

                    if (rsObj == null) //si no existe, creo la variable
                    {
                        rsObj = new Result();

                        Sim s = new Sim(((Identificador)obj).Id, Tipo.INT, Rol.LOCAL, 1, e.GetPos(), e.Ambito, -1, -1);
                        e.Add(s);

                        string ptrStack = NuevoTemporal();
                        rsObj.Codigo = ptrStack + " = P + " + s.Pos + ";\n";
                        rsObj.Valor = NuevoTemporal();
                        rsObj.Codigo += rsObj.Valor + " = stack[" + ptrStack + "];\n";
                    }

                    result.Codigo += rsObj.Codigo;

                }
            }

            return result;
        }
    }
}
