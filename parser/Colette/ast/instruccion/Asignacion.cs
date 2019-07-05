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

            foreach (LinkedList<Expresion> valList in Valor)
            {
                if (Objetivo.Count() != valList.Count())
                {
                    Console.WriteLine("Error! Listas no simetricas. Linea: " + Linea);
                    return null;
                }
            }

            for (int i = 0; i < Objetivo.Count(); i++)
            {
                Expresion obj = Objetivo.ElementAt(i);
                if (obj is Identificador) //verifico que sea id
                {
                    ((Identificador)obj).Acceso = false;
                    Result rsObj = obj.GetC3D(e);

                    if (rsObj == null) //si no existe, creo la variable
                    {
                        rsObj = new Result();

                        Sim s = new Sim(((Identificador)obj).Id, new Tipo(Tipo.Type.INT), Rol.LOCAL, 1, e.GetPos(), e.Ambito, -1, -1);
                        e.Add(s);
                        rsObj.Simbolo = s;

                        string ptrStack = NuevoTemporal();
                        rsObj.Codigo = ptrStack + " = P + " + s.Pos + ";\n";
                        rsObj.Valor = "stack[" + ptrStack + "]";
                    }

                    LinkedList<Result> rsList = new LinkedList<Result>();
                    for (int j = 0; j < Valor.Count(); j++)
                    {
                        LinkedList<Expresion> valList = Valor.ElementAt(j);
                        Expresion expI = valList.ElementAt(i);

                        if (j + 1 == Valor.Count())
                        {
                            Result rsTemp = expI.GetC3D(e);
                            if (rsTemp != null)
                                rsList.AddLast(rsTemp);
                            else
                            {
                                Console.WriteLine("Error! No se pudo encontrar el valor. Linea: " + Linea);
                                return null;
                            }
                        }
                        else
                        {
                            if (expI is Identificador)
                            {
                                ((Identificador)expI).Acceso = false;
                                Result rsTemp = expI.GetC3D(e);

                                if (rsTemp == null) //si no existe, creo la variable
                                {
                                    rsTemp = new Result();

                                    Sim s = new Sim(((Identificador)expI).Id, new Tipo(Tipo.Type.INT), Rol.LOCAL, 1, e.GetPos(), e.Ambito, -1, -1);
                                    e.Add(s);

                                    string ptrStack = NuevoTemporal();
                                    rsTemp.Codigo = ptrStack + " = P + " + s.Pos + ";\n";
                                    rsTemp.Valor = "stack[" + ptrStack + "]";
                                    rsTemp.PtrStack = s.Pos;
                                }
                                rsList.AddLast(rsTemp);
                            }
                            else
                                return null; /*No implementado*/
                        }
                    }

                    if (rsList.Count() == 1)
                    {
                        rsObj.Tipo = rsList.ElementAt(0).Tipo;
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

                        rsObj.Codigo += rsAnt.Codigo;
                        rsObj.Codigo += rsObj.Valor + " = " + rsAnt.Valor + ";\n";
                    }

                    result.Codigo += rsObj.Codigo;

                }
            }

            return result;
        }
    }
}
