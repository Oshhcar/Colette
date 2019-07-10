using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser.Colette.ast.entorno;
using Compilador.parser.Colette.ast.expresion;

namespace Compilador.parser.Colette.ast.instruccion
{
    class Clase : Instruccion
    {
        public Clase(string id, Bloque bloque, int linea, int columna) : base(linea, columna)
        {
            Id = id;
            Bloque = bloque;
        }

        public string Id { get; set; }
        public Bloque Bloque { get; set; }

        public override Result GetC3D(Ent e, bool funcion, bool ciclo, bool isDeclaracion, bool isObjeto, LinkedList<Error> errores)
        {
            Result result = new Result();
            Sim clase = e.GetClase(Id);

            if (!isDeclaracion)
            {
                if (clase != null)
                {
                    Ent local = new Ent(Id, e); /*Verificar*/
                    local.Size = clase.Tam;

                    /*Guardo Clases*/


                    /*Guardar Funciones*/
                    foreach (Nodo sentencia in Bloque.Sentencias)
                    {
                        if (sentencia is Funcion)
                        {
                            Funcion fun = ((Funcion)sentencia);
                            fun.GetC3D(local, false, false, true, isObjeto, errores);
                        }
                    }

                    /*Buscar si hay constructor*/

                    /*Genero Constructor*/
                    result.Codigo += "proc " + Id + "_" + Id + "() begin\n";
                    /*Verificar si hay returns, error*/

                    string tmp = NuevoTemporal();
                    result.Codigo += tmp + " = P + 0;\n";
                    result.Codigo += "stack[" + tmp + "] = H;\n";
                    

                    /*Guardo Posición self en 1*/
                    string tmp2 = NuevoTemporal();
                    result.Codigo += tmp2 + " = P + 1;\n";
                    result.Codigo += "stack[" + tmp2 + "] = H;\n";

                    result.Codigo += "H = H + " + local.Size + ";\n"; //reservo memoria

                    /*Ejecuto sentencias que estan en la clase*/
                    foreach (Nodo sentencia in Bloque.Sentencias)
                    {
                        if (!(sentencia is Funcion) && !(sentencia is Clase))
                        {
                            Result rsNodo = null;
                            if (sentencia is Instruccion)
                            {
                                if(sentencia is Asignacion) //Solo sentencias de asignación
                                    rsNodo = ((Instruccion)sentencia).GetC3D(local, false, false, false, true, errores);

                                if (sentencia is Global)
                                    ((Global)sentencia).GetC3D(local, false, false, false, true, errores);
                            }

                            if (rsNodo != null)
                                if (rsNodo.Codigo != null)
                                    result.Codigo += rsNodo.Codigo;
                        }
                    }
                    //reservo memoria
                    //Posicion 1 siempre vendra self

                    result.Codigo += "end\n\n";

                    /*Obtener C3D Funciones*/
                    foreach (Nodo sentencia in Bloque.Sentencias)
                    {
                        if (sentencia is Funcion)
                        {
                            Result rsNodo = ((Funcion)sentencia).GetC3D(local, false, false, false, isObjeto, errores);
                            if (rsNodo != null)
                                if (rsNodo.Codigo != null)
                                    result.Codigo += rsNodo.Codigo;
                        }
                    }

                    //PrintTabla t = new PrintTabla(0,0);
                    //t.GetC3D(clase.Entorno, false, false, false, errores);
                    foreach (Sim s in local.Simbolos)
                    {
                        clase.Entorno.Add(s);
                    }
                }
            }
            else
            {
                if (clase == null)
                {
                    Ent local = new Ent(Id, e);

                    foreach (Nodo sentencia in Bloque.Sentencias)
                    {
                        if (sentencia is Instruccion)
                        {
                            if (!(sentencia is Funcion) && !(sentencia is Clase))/*ojo*/
                            {
                                ((Instruccion)sentencia).GetC3D(local, false, false, true, isObjeto, errores);
                            }
                        }
                    }

                    clase = new Sim(Id, new Tipo(Id), Rol.CLASE, local.Pos, -1, e.Ambito, -1, -1);
                    clase.Entorno = local;
                    e.Add(clase);
                }
                else
                {
                    errores.AddLast(new Error("Semántico", "Ya se declaró una clase con el mismo id: " + Id + ".", Linea, Columna));
                }
            }

            return result;
            //return Bloque.GetC3D(e, funcion, ciclo, isDeclaracion, errores);
        }
    }
}
