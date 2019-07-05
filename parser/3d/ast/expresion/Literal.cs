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
        public Literal(Object valor, Tipo tipo, int linea, int columna) : base(linea, columna)
        {
            Valor = valor;
            Tipo = tipo;
        }

        public Object Valor { get; set; }
        private Tipo Tipo { get; set; }

        public override Tipo GetTipo(Entorno e)
        {
            return Tipo;
        }

        public override object GetValor()
        {
            return Valor;
        }
    }
}
