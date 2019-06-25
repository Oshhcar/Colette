using Compilador.parser._3d.ast.entorno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser._3d.ast.expresion.Operacion
{
    class Relacional : Operacion
    {
        public Relacional(Expresion op1, Expresion op2, Operador op, int linea, int columna)
            : base(op1, op2, op, linea, columna) { valor = null; }

        private Object valor;

        public override Tipo GetTipo(Entorno e)
        {
            if (!(Op1 is Operacion) && !(Op2 is Operacion))
            {
                Tipo tip1 = Op1.GetTipo(e);
                Tipo tip2 = Op2.GetTipo(e);

                if (tip1 != Tipo.NULL && tip2 != Tipo.NULL)
                {
                    if (tip1 == Tipo.NUMERO && tip2 == Tipo.NUMERO)
                    {
                        int val1 = Convert.ToInt32(Op1.GetValor());
                        int val2 = Convert.ToInt32(Op2.GetValor());

                        switch (Op)
                        {
                            case Operador.MENORQUE:
                                valor = val1 < val2 ? 1 : 0;
                                return Tipo.NUMERO;
                            case Operador.MAYORQUE:
                                valor = val1 > val2 ? 1 : 0;
                                return Tipo.NUMERO;
                            case Operador.MENORIGUAL:
                                valor = val1 <= val2 ? 1 : 0;
                                return Tipo.NUMERO;
                            case Operador.MAYORIGUAL:
                                valor = val1 >= val2 ? 1 : 0;
                                return Tipo.NUMERO;
                            case Operador.IGUAL:
                                valor = val1 == val2 ? 1 : 0;
                                return Tipo.NUMERO;
                            case Operador.DIFERENTE:
                                valor = val1 != val2 ? 1 : 0;
                                return Tipo.NUMERO;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error, no se puede realizar la operación aritmética. Línea: " + Linea);
                    }
                }

            }
            else
            {
                Console.WriteLine("Error, se esta usando más de 3 direcciones. Línea: " + Linea);
            }
            return Tipo.NULL;
        }

        public override object GetValor()
        {
            return valor;
        }
    }
}
