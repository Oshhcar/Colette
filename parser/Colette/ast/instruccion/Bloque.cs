using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser.Colette.ast.entorno;
using Compilador.parser.Colette.ast.expresion;

namespace Compilador.parser.Colette.ast.instruccion
{
    class Bloque : Instruccion
    {
        public Bloque(LinkedList<Nodo> sentencias, int linea, int columna) : base(linea, columna)
        {
            Sentencias = sentencias;
        }

        public LinkedList<Nodo> Sentencias { get; set; }

        public override Result GetC3D(Ent e, bool funcion, bool ciclo, LinkedList<Error> errores)
        {
            Result result = new Result();

            foreach(Nodo sentencia in Sentencias)
            {
                Result rsNodo;

                if (sentencia is Instruccion)
                {
                    rsNodo = ((Instruccion)sentencia).GetC3D(e, funcion, ciclo, errores);
                }
                else
                {
                    if (sentencia is Llamada)
                        ((Llamada)sentencia).ObtenerReturn = false;
                    
                    rsNodo = ((Expresion)sentencia).GetC3D(e, funcion, ciclo, errores);
                }

                if(rsNodo != null)
                    if(rsNodo.Codigo != null)
                        result.Codigo += rsNodo.Codigo;
            }

            return result;
        }
    }
}
