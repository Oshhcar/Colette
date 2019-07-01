using Compilador.parser.Colette.ast.entorno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser.Colette.ast.expresion
{
    class Literal : Expresion
    {
        public Literal(Tipo tipo, Object valor, int linea, int columna) : base(linea, columna)
        {
            Tipo = tipo;
            Valor = valor;
        }

        public Tipo Tipo { get; set; }
        
        public Object Valor { get; set; }

        public override Result GetC3D(Ent e)
        {
            Result result = new Result();
            result.Valor = Valor.ToString();
            return result;
        }

        public override Tipo GetTipo(Ent e)
        {
            return Tipo;
        }
    }
}
