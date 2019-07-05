using Compilador.parser._3d.ast.entorno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser._3d.ast.expresion.Operacion
{
    class Aritmetica : Operacion
    {
        public Aritmetica(Expresion op1, Expresion op2, Operador op, int linea, int columna)
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
                        Object val1 = Op1.GetValor();
                        Object val2 = Op2.GetValor();

                        switch (Op)
                        {
                            case Operador.MAS:
                                valor = Convert.ToDouble(val1) + Convert.ToDouble(val2);
                                return Tipo.NUMERO;
                            case Operador.MENOS:
                                valor = Convert.ToDouble(val1) - Convert.ToDouble(val2);
                                return Tipo.NUMERO;
                            case Operador.POR:
                                valor = Convert.ToDouble(val1) * Convert.ToDouble(val2);
                                return Tipo.NUMERO;
                            case Operador.DIVIDIO:
                                if (Convert.ToDouble(val2) != 0)
                                {
                                    valor = Convert.ToDouble(val1) / Convert.ToDouble(val2);
                                    return Tipo.NUMERO;
                                }
                                else
                                {
                                    Console.WriteLine("Division entre 0-> linea: " + Op2.Linea + " columna: " + Op2.Columna);
                                    return Tipo.NULL;
                                }
                            case Operador.MODULO:
                                valor = Convert.ToDouble(val1) % Convert.ToDouble(val2);
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
