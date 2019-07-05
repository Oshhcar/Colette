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
                            if (tipoPosicion == Tipo.ENTERO || tipoPosicion == Tipo.DECIMAL)
                            {
                                Object valorPosicion = Posicion.GetValor();
                                if (sim.Tipo == Tipo.ARREGLO)
                                {
                                    try
                                    {
                                        int pos = Convert.ToInt32(valorPosicion);

                                        double[] sArray = sim.Valor as double[];

                                        try
                                        {
                                            int ent = Convert.ToInt32(sArray[pos]);
                                            valor = ent;
                                            return Tipo.ENTERO;
                                        }
                                        catch (Exception)
                                        {
                                            double dec = Convert.ToDouble(sArray[pos]);
                                            valor = dec;
                                            return Tipo.DECIMAL;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Error, la posición para acceder al arreglo debe se entero. Línea:" + Linea);
                                    }
                                    
                                }
                                else
                                {
                                    Console.WriteLine("Error, variable " + Id + " no es arreglo. Línea: " + Linea);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error, la posicion debe ser valor entero. Línea: " + Linea);
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
