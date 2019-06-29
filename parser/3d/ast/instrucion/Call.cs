using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser._3d.ast.entorno;

namespace Compilador.parser._3d.ast.instrucion
{
    class Call : Instruccion
    {
        public Call(string id, int linea, int columna) : base(linea, columna)
        {
            Id = id;
        }

        public string Id { get; set; }

        public override object Ejecutar(Entorno e)
        {
            Simbolo sim = e.GetSimbolo(Id);
            if (sim != null)
            {
                LinkedList<Instruccion> bloques = sim.Valor as LinkedList<Instruccion>;
                e.EntrarAmbito();
                for(int i = 0; i < bloques.Count(); i++)
                {
                    Instruccion bloque = bloques.ElementAt(i);

                    if (!(bloque is Etiqueta))
                    {
                        if (!(bloque is Salto) && !(bloque is SaltoCond))
                        {
                            bloque.Ejecutar(e);
                        }
                        else
                        {
                            Object o = bloque.Ejecutar(e);
                            if (o != null)
                            {
                                i = Convert.ToInt32(o);
                            }
                        }
                    }
                }
                e.SalirAmbito();
            }
            else
            {
                Console.WriteLine("Error, método " + Id + " no encontrado. Línea: " + Linea);
            }
            return null;
        }
    }
}
