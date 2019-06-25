using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser._3d.ast.entorno;

namespace Compilador.parser._3d.ast.instrucion
{
    class Metodo : Instruccion
    {
        public Metodo(string id, LinkedList<Instruccion> bloque, int linea, int columna)
            : base(linea, columna)
        {
            Id = id;
            Bloque = bloque;
        }

        public string Id { get; set; }
        public LinkedList<Instruccion> Bloque { get; set; }

        public override object Ejecutar(Entorno e)
        {
            if (e.GetSimbolo(Id) == null)
            {
                e.AddSimbolo(new Simbolo(Id, Bloque, Tipo.METODO));
                return Bloque;
            }
            else
            {
                Console.WriteLine("Error, método " + Id + " ya definido. Línea: " + Linea);
            }
            return null;
        }
    }
}
