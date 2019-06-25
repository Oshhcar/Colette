using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser._3d.ast.entorno;

namespace Compilador.parser._3d.ast.expresion.Operacion
{
    class Operacion : Expresion
    {
        public Operacion(Expresion op1, Expresion op2, Operador op, int linea, int columna) : base(linea, columna)
        {
            Op1 = op1;
            Op2 = op2;
            Op = op;
        }

        public Expresion Op1 { get; set; }
        public Expresion Op2 { get; set; }
        public Operador Op { get; set; }

        public override Tipo GetTipo(Entorno e)
        {
            return Tipo.NULL;
        }

        public override object GetValor()
        {
            return null;
        }

    }

    public enum Operador
    {
        MAS,
        MENOS,
        POR,
        DIVIDIO,
        MODULO,
        MENORQUE,
        MAYORQUE,
        MENORIGUAL,
        MAYORIGUAL,
        IGUAL,
        DIFERENTE,
        NULL,
    }
}
