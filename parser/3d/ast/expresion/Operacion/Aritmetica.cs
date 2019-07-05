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
                    if ((tip1 == Tipo.ENTERO || tip1 == Tipo.DECIMAL) && (tip2 == Tipo.ENTERO || tip2 == Tipo.DECIMAL))
                    {
                        Object val1 = Op1.GetValor();
                        Object val2 = Op2.GetValor();
                        try
                        {
                            switch (Op)
                            {
                                case Operador.MAS:
                                    if (tip1 == Tipo.DECIMAL || tip2 == Tipo.DECIMAL)
                                    {
                                        valor = Convert.ToDouble(val1) + Convert.ToDouble(val2);
                                        return Tipo.DECIMAL;
                                    }
                                    else
                                    {
                                        valor = Convert.ToInt32(val1) + Convert.ToInt32(val2);
                                        return Tipo.ENTERO;
                                    }
                                case Operador.MENOS:
                                    if (tip1 == Tipo.DECIMAL || tip2 == Tipo.DECIMAL)
                                    {
                                        valor = Convert.ToDouble(val1) - Convert.ToDouble(val2);
                                        return Tipo.DECIMAL;
                                    }
                                    else
                                    {
                                        valor = Convert.ToInt32(val1) - Convert.ToInt32(val2);
                                        return Tipo.ENTERO;
                                    }
                                case Operador.POR:
                                    if (tip1 == Tipo.DECIMAL || tip2 == Tipo.DECIMAL)
                                    {
                                        valor = Convert.ToDouble(val1) * Convert.ToDouble(val2);
                                        return Tipo.DECIMAL;
                                    }
                                    else
                                    {
                                        valor = Convert.ToInt32(val1) * Convert.ToInt32(val2);
                                        return Tipo.ENTERO;
                                    }
                                case Operador.DIVIDIO:
                                    if (Convert.ToInt32(val2) != 0)
                                    {
                                        if (tip1 == Tipo.DECIMAL || tip2 == Tipo.DECIMAL)
                                        {
                                            valor = Convert.ToDouble(val1) / Convert.ToDouble(val2);
                                            return Tipo.DECIMAL;
                                        }
                                        else
                                        {
                                            valor = Convert.ToInt32(val1) / Convert.ToInt32(val2);
                                            return Tipo.ENTERO;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Division entre 0-> linea: " + Op2.Linea + " columna: " + Op2.Columna);
                                        return Tipo.NULL;
                                    }
                                case Operador.MODULO:
                                    if (tip1 == Tipo.DECIMAL || tip2 == Tipo.DECIMAL)
                                    {
                                        valor = Convert.ToDouble(val1) % Convert.ToDouble(val2);
                                        return Tipo.DECIMAL;
                                    }
                                    else
                                    {
                                        valor = Convert.ToInt32(val1) % Convert.ToInt32(val2);
                                        return Tipo.ENTERO;
                                    }
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Error, no se puede realizar la operación aritmética. Línea: " + Linea);
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
