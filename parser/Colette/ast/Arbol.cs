using Compilador.parser.Colette.ast.entorno;
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

        public String GenerarC3D(LinkedList<Error> errores)
        {
            Nodo.Etiquetas = 0;
            Nodo.Temporales = 0;
            Nodo.H = 0;

            Ent global = new Ent("Global");

            Result result = new Result();
            result.Codigo = "";

            /*Reservo memoria*/
            Ent local = new Ent("Global");
            foreach (Nodo sentencia in Sentencias)
            {
                if (sentencia is Instruccion)
                {
                    if (!(sentencia is Funcion) && !(sentencia is Clase))
                    {
                        ((Instruccion)sentencia).GetC3D(local, false, false, true, false, errores);
                    }
                }
            }
            global.Size = local.Pos;

            /*Guardo Clases*/
            foreach (Nodo sentencia in Sentencias)
            {
                if (sentencia is Clase)
                {
                    ((Clase)sentencia).GetC3D(global, false, false, true, false, errores);
                }
            }

            /*Obtener C3D Clases*/
            foreach (Nodo sentencia in Sentencias)
            {
                if (sentencia is Clase)
                {
                    Result rsNodo = ((Clase)sentencia).GetC3D(global, false, false, false, false, errores);
                    if (rsNodo != null)
                        if (rsNodo.Codigo != null)
                            result.Codigo += rsNodo.Codigo;
                }
            }

            /*Guardar Funciones*/
            foreach (Nodo sentencia in Sentencias)
            {
                if (sentencia is Funcion)
                {
                    Funcion fun = ((Funcion)sentencia);
                    fun.GetC3D(global, false, false, true, false, errores);
                }
            }

            /*Obtener C3D Funciones*/
            foreach (Nodo sentencia in Sentencias)
            {
                if (sentencia is Funcion)
                {
                    Result rsNodo = ((Funcion)sentencia).GetC3D(global, false, false, false, false, errores);
                    if (rsNodo != null)
                        if (rsNodo.Codigo != null)
                            result.Codigo += rsNodo.Codigo;
                }
            }

            /*Obtener C3D de Todo lo demás*/
            foreach (Nodo sentencia in Sentencias)
            {
                if (!(sentencia is Funcion) && !(sentencia is Clase))
                {
                    Result rsNodo;
                    if (sentencia is Instruccion)
                    {
                        rsNodo = ((Instruccion)sentencia).GetC3D(global, false, false, false, false, errores);
                    }
                    else
                    {
                        if (sentencia is Llamada)
                            ((Llamada)sentencia).ObtenerReturn = false;

                        rsNodo = ((Expresion)sentencia).GetC3D(global, false, false, false, errores);
                    }

                    if (rsNodo != null)
                        if (rsNodo.Codigo != null)
                            result.Codigo += rsNodo.Codigo;
                }
            }

            //global.Recorrer();

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
                codigo += "var H = " + Nodo.H + "; \n\n";

                codigo += result.Codigo;
            }

            return codigo;
        }
    }
}
