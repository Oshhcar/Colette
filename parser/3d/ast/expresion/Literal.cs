using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser._3d.ast.entorno;

namespace Compilador.parser._3d.ast.expresion
{
    class Literal : Expresion
    {
        public Literal(Object valor, int linea, int columna) : base(linea, columna)
        {
            Valor = valor;
        }

        public Object Valor { get; set; }

        public override Tipo GetTipo(Entorno e)
        {
            return Tipo.NUMERO;
        }

        public override object GetValor()
        {
            return Valor;
        }
    }
}
