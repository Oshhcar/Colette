using Compilador.parser._3d.ast.entorno;
using Compilador.parser._3d.ast.expresion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser._3d.ast.instrucion
{
    class Asignacion : Instruccion
    {
        public Asignacion(string id, Expresion valor, int linea, int columna) : base(linea, columna)
        {
            Id = id;
            Valor = valor;
        }

        public Asignacion(string id, Expresion posicion, Expresion valor, int linea, int columna) : base(linea, columna)
        {
            Id = id;
            Posicion = posicion;
            Valor = valor;
        }

        public string Id { get; set; }
        public Expresion Valor { get; set; }
        public Expresion Posicion { get; set; }

        public override object Ejecutar(Entorno e)
        {
            Simbolo sim = e.GetSimbolo(Id);
            if (sim != null)
            {
                Tipo tipoValor = Valor.GetTipo(e);
                if (tipoValor != Tipo.NULL)
                {
                    if (tipoValor == Tipo.NUMERO)
                    {
                        if (Posicion == null)
                        {
                            if (sim.Tipo == Tipo.NUMERO || sim.Tipo == Tipo.VAR)
                            {
                                sim.Tipo = Tipo.NUMERO;
                                sim.Valor = Valor.GetValor();
                            }
                            else
                            {
                                Console.WriteLine("Error, no se puede asignar el valor. Línea: " + Linea);
                            }
                        }
                        else
                        {
                            if (Posicion is Identificador || Posicion is Literal)
                            {
                                Tipo tipoPosicion = Posicion.GetTipo(e);
                                if (tipoPosicion != Tipo.NULL)
                                {
                                    if (tipoPosicion == Tipo.NUMERO)
                                    {
                                        Object valorPosicion = Posicion.GetValor();
                                        if (sim.Tipo == Tipo.ARREGLO)
                                        {
                                            double[] sArray = sim.Valor as double[];
                                            sArray[Convert.ToInt32(valorPosicion)] = Convert.ToDouble(Valor.GetValor());
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error, variable " + Id + " no es arreglo. Línea: " + Linea);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error, la posicion debe ser valor numerico. Línea: " + Linea);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error, la posicion debe ser id o numero. Línea: " + Linea);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error, valor indefinido. Línea: " + Linea);
                    }
                }
            }
            else
            {
                Console.WriteLine("Error, No existe una variable con el id " + Id + " Línea: " + Linea);
            }
            return null;
        }
    }
}
