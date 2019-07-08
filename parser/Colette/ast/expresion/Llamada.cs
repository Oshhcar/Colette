﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser.Colette.ast.entorno;

namespace Compilador.parser.Colette.ast.expresion
{
    class Llamada : Expresion
    {
        public Llamada(string id, LinkedList<Expresion> parametros, int linea, int columna) : base(linea, columna)
        {
            Id = id;
            Parametros = parametros;
            Tipo = new Tipo(Tipo.Type.INDEFINIDO);
        }

        public string Id { get; set; }
        public LinkedList<Expresion> Parametros { get; set; }
        public Tipo Tipo { get; set; }


        public override Result GetC3D(Ent e)
        {
            Result result = new Result();

            string firma = Id;

            if (Parametros != null)
            {
                LinkedList<Result> rsParametros = new LinkedList<Result>();
                foreach (Expresion parametro in Parametros)
                {
                    Result rsParametro = parametro.GetC3D(e);
                    if (rsParametro != null)
                    {
                        if (!parametro.GetTipo().IsIndefinido())
                        {
                            rsParametros.AddLast(rsParametro);
                            firma += "_" + parametro.GetTipo().ToString();
                            continue;
                        }
                    }

                    Console.WriteLine("Error! En parametro. Linea: " + Linea);
                    return null;
                }

                Sim metodo = e.GetMetodo(firma);
                if (metodo != null)
                {
                    result.Valor = NuevoTemporal();
                    result.Codigo += result.Valor + " = P + " + e.Pos + ";\n"; //Cambio simulado

                    int pos = 2; /*inicia en dos por return y self*/

                    foreach (Result rsParametro in rsParametros)
                    {
                        result.Codigo += rsParametro.Codigo;
                        string tmp = NuevoTemporal();
                        result.Codigo += tmp + " = " + result.Valor + " + " + pos++ + ";\n";
                        result.Codigo += "stack[" + tmp + "] = " + rsParametro.Valor + ";\n";
                    }

                    result.Codigo += "P = P + " + e.Pos + ";\n";
                    result.Codigo += "call " + firma + ";\n";
                    result.Codigo += "P = P - " + e.Pos + ";\n";

                }
                else
                {
                    Console.WriteLine("Error! No se pudo encontrar el Metodo " + Id + ". Linea: " + Linea);
                    return null;
                }
            }
            else
            {
                Sim metodo = e.GetMetodo(firma);

                if (metodo != null)
                {
                    result.Codigo += "P = P + " + metodo.Tam + ";\n";
                    result.Codigo += "call " + firma + ";\n";
                    result.Codigo += "P = P - " + metodo.Tam + ";\n";
                }
                else
                {
                    Console.WriteLine("Error! No se pudo encontrar el Metodo " + Id + ". Linea: " + Linea);
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
