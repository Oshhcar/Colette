﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Compilador.parser._3d.ast.entorno;

namespace Compilador.parser._3d.ast.instrucion
{
    class Print : Instruccion
    {
        public Print(string char_, string id, int linea, int columna) : base(linea, columna)
        {
            Char = char_;
            Id = id;
            Output = null;
        }

        public string Char { get; set; }
        public string Id { get; set; }

        public TextBox Output { get; set; }

        public override object Ejecutar(Entorno e)
        {
            Simbolo sim = e.GetSimbolo(Id);

            if (sim != null)
            {
                if (sim.Tipo == Tipo.ENTERO || sim.Tipo == Tipo.DECIMAL)
                {
                    if (Output != null)
                    {
                        if (Char.Equals("c"))
                        {
                            try
                            {
                                int valor = Convert.ToInt32(sim.Valor);
                                if (valor != 10) /*Salto de línea*/
                                    Output.Text += (char)valor;
                                else
                                    Output.Text += "\r\n";
                                //Console.Write((char)valor);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Error, no puede convertirse el valor. Línea: " + Linea);
                            }

                        }
                        else if (Char.Equals("i"))
                        {
                            try
                            {
                                int valor = Convert.ToInt32(sim.Valor);
                                Output.Text += valor;
                                //Console.Write(valor);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Error, no puede convertirse el valor. Línea: " + Linea);
                            }

                        }
                        else
                        {
                            double valor = Convert.ToDouble(sim.Valor);
                            Output.Text += Math.Round(valor, 6);
                            //Console.Write(valor);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Error, variable " + Id + " no definida. Línea: " + Linea);
                }
            }
            else
            {
                Console.WriteLine("Error, variable " + Id + " no encontrada. Línea: " + Linea);
            }
            return null;
        }
    }
}
