using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser._3d.ast.entorno;

namespace Compilador.parser._3d.ast.expresion
{
    class AccesoArreglo : Expresion
    {
        public AccesoArreglo(string id, Expresion posicion, int linea, int columna) : base(linea, columna)
        {
            Id = id;
            Posicion = posicion;
            valor = null;
        }

        public string Id { get; set; }
        public Expresion Posicion { get; set; }
        private Object valor;

        public override Tipo GetTipo(Entorno e)
        {
            Simbolo sim = e.GetSimbolo(Id);
            if (sim != null)
            {
                if (sim.Tipo == Tipo.ARREGLO)
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
                                    int[] sArray = sim.Valor as int[];
                                    valor = sArray[Convert.ToInt32(valorPosicion.ToString())];
                                    return Tipo.NUMERO;
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
                else
                {
                    Console.WriteLine("Error, variable " + Id + " no es arreglo. Línea: " + Linea);
                }

            }
            else
            {
                Console.WriteLine("Error, No existe una variable con el id " + Id + " Línea: " + Linea);
            }
            return Tipo.NULL;
        }

        public override object GetValor()
        {
            return valor;
        }
    }
}
