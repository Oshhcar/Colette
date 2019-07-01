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

        public override Result GetC3D(Ent e)
        {
            Result result = new Result();
            result.Codigo = "";

            foreach(Nodo sentencia in Sentencias)
            {
                Result rsNodo;

                if (sentencia is Instruccion)
                {
                    rsNodo = ((Instruccion)sentencia).GetC3D(e);
                }
                else
                {
                    rsNodo = ((Expresion)sentencia).GetC3D(e);
                }

                result.Codigo += rsNodo.Codigo;
            }

            return result;
        }
    }
}
