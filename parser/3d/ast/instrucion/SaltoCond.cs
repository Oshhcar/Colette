using Compilador.parser._3d.ast.entorno;
using Compilador.parser._3d.ast.expresion;
using Compilador.parser._3d.ast.expresion.Operacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser._3d.ast.instrucion
{
    class SaltoCond : Instruccion
    {
        public SaltoCond(Expresion cond, string label, int tipo, int linea, int columna)
            : base(linea, columna)
        {
            Cond = cond;
            Label = label;
            Type = tipo;
        }

        public Expresion Cond { get; set; }
        public string Label { get; set; }
        private int Type;

        public override object Ejecutar(Entorno e)
        {
            if (Cond is Relacional)
            {
                Tipo tipCond = Cond.GetTipo(e);
                if(tipCond != Tipo.NULL)
                {
                    if (tipCond == Tipo.ENTERO || tipCond == Tipo.DECIMAL)
                    {
                        Simbolo label = e.GetSimbolo(Label);
                        if (label != null)
                        {
                            int valCond = Convert.ToInt32(Cond.GetValor());

                            if (valCond == 1)
                            {
                                if (Type == 1)
                                    return label.Valor;
                            }
                            else
                            {
                                if (Type == 2)
                                    return label.Valor;
                            }

                        }
                        else
                        {
                            Console.WriteLine("Error, etiqueta " + Label + " no encontrada. Línea: " + Linea);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Error, no se puede realizar el salto condicional. Línea: " + Linea);
            }
            return null;
        }
    }
}
