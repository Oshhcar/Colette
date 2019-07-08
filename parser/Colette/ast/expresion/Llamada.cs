using System;
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
            }

            return result;
        }

        public override Tipo GetTipo()
        {
            return Tipo;
        }
    }
}
