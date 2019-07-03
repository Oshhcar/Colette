using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser.Colette.ast.entorno;

namespace Compilador.parser.Colette.ast.expresion
{
    class Identificador : Expresion
    {
        public Identificador(string id, int linea, int columna) : base(linea, columna)
        {
            Id = id;
            Acceso = true;
        }

        public string Id { get; set; }
        public bool Acceso { get; set; }

        public override Result GetC3D(Ent e)
        {
            Sim s = e.Get(Id);

            if (s != null)
            {
                /*verificar tipo para acceder a heap*/
                Result result = new Result();
                string ptrStack = NuevoTemporal();
                result.Codigo = ptrStack + " = P + " + s.Pos + ";\n";

                if (Acceso)
                {
                    result.Valor = NuevoTemporal();
                    result.Codigo += result.Valor + " = stack[" + ptrStack + "];\n";
                }
                else
                {
                    result.PtrStack = s.Pos;
                    result.Valor = "stack[" + ptrStack + "]";
                }
                return result;
            }
            return null;
        }

        public override Tipo GetTipo(Ent e)
        {
            throw new NotImplementedException();
        }
    }
}
