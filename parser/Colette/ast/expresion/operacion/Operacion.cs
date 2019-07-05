using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser.Colette.ast.entorno;

namespace Compilador.parser.Colette.ast.expresion.operacion
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

        public override Result GetC3D(Ent e)
        {
            throw new NotImplementedException();
        }

        public override Tipo GetTipo()
        {
            throw new NotImplementedException();
        }

        public enum Operador
        {
            SUMA,
            RESTA,
            MULTIPLICACION,
            DIVISION,
            MODULO,
            FLOOR,
            MENORQUE,
            MAYORQUE,
            MENORIGUALQUE,
            MAYORIGUALQUE,
            IGUAL,
            DIFERENTE,
            OR,
            AND,
            NOT,
            INDEFINIDO
        }
    }
}
