﻿using Compilador.parser.Colette.ast.entorno;
using Compilador.parser.Colette.ast.expresion;
using Compilador.parser.Colette.ast.instruccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser.Colette.ast
{
    class Arbol
    {
        public Arbol(LinkedList<Nodo> sentencias)
        {
            Sentencias = sentencias;
        }

        public LinkedList<Nodo> Sentencias { get; set; }

        public String GenerarC3D()
        {
            Nodo.Etiquetas = 0;
            Nodo.Temporales = 0;

            Ent global = new Ent("Global");

            Result result = new Result();
            result.Codigo = "";

            foreach (Nodo sentencia in Sentencias)
            {
                if (sentencia is Funcion)
                {
                    Result rsNodo = ((Funcion)sentencia).GetC3D(global);
                    if(rsNodo != null)
                        if(rsNodo.Codigo != null)
                            result.Codigo += rsNodo.Codigo;
                }
            }

            foreach(Nodo sentencia in Sentencias)
            {
                if (!(sentencia is Funcion))
                {
                    Result rsNodo;
                    if (sentencia is Instruccion)
                    {
                        rsNodo = ((Instruccion)sentencia).GetC3D(global);
                    }
                    else
                    {
                        rsNodo = ((Expresion)sentencia).GetC3D(global);
                    }

                    if (rsNodo != null)
                        if (rsNodo.Codigo != null)
                            result.Codigo += rsNodo.Codigo;
                }
            }

           // global.Recorrer();

            string codigo = "";

            if (Nodo.Temporales > 0)
            {
                codigo += "var ";
                int i = 1;
                while (i <= Nodo.Temporales)
                {
                    codigo += "t" + i;

                    if (++i <= Nodo.Temporales)
                        codigo += ",";
                }
                codigo += ";\n";

            }

            if (!result.Codigo.Equals(String.Empty))
            {
                codigo += "var stack[];\n";
                codigo += "var heap[];\n";
                codigo += "var P = 0;\n";
                codigo += "var H = 0; \n\n";

                codigo += result.Codigo;
            }

            return codigo;
        }
    }
}
