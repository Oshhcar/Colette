using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser.Colette.ast.entorno;

namespace Compilador.parser.Colette.ast.expresion
{
    class Referencia : Expresion
    {
        public Referencia(Expresion expresion, string id, int linea, int columna) : base(linea, columna)
        {
            Expresion = expresion;
            Id = id;
            Tipo = new Tipo(Tipo.Type.INDEFINIDO);
            Acceso = true;
        }

        public Expresion Expresion { get; set; }
        public string Id { get; set; }
        public Tipo Tipo { get; set; }
        public bool Acceso { get; set; }
        public Sim Simbolo { get; set; }

        public override Result GetC3D(Ent e, bool funcion, bool ciclo, LinkedList<Error> errores)
        {
            Result result = new Result();

            Result rsExpresion;

            if (Acceso)
            {
                rsExpresion = Expresion.GetC3D(e, funcion, ciclo, errores);

                if (rsExpresion != null)
                {

                }
                else
                {
                    errores.AddLast(new Error("Semántico", "Error obteniendo la referencia", Linea, Columna));
                    return null;
                }
            }
            else
            {
                if (Expresion is Identificador)
                    ((Identificador)Expresion).Acceso = false;
                else if (Expresion is Referencia)
                    ((Referencia)Expresion).Acceso = false;

                rsExpresion = Expresion.GetC3D(e, funcion, ciclo, errores);

                if (rsExpresion != null)
                {
                    result.Codigo += rsExpresion.Codigo;

                    Tipo = Expresion.GetTipo();

                    if (Expresion is Identificador)
                        Simbolo = ((Identificador)Expresion).Simbolo;
                    else if (Expresion is Referencia)
                        Simbolo = ((Referencia)Expresion).Simbolo;

                }
                else
                {
                    errores.AddLast(new Error("Semántico", "Error obteniendo la referencia", Linea, Columna));
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
