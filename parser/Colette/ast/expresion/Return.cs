using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser.Colette.ast.entorno;

namespace Compilador.parser.Colette.ast.expresion
{
    class Return : Expresion
    {
        public Return(Expresion valor, int linea, int columna) : base(linea, columna)
        {
            Valor = valor;
            Tipo = new Tipo(Tipo.Type.INDEFINIDO);
        }

        public Expresion Valor { get; set; }
        public Tipo Tipo { get; set; }

        public override Result GetC3D(Ent e, bool funcion, bool ciclo, LinkedList<Error> errores)
        {
            Result result= new Result();


            return result;
        }

        public override Tipo GetTipo()
        {
            return Tipo;
        }
    }
}
