using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser._3d.ast.entorno;

namespace Compilador.parser._3d.ast.instrucion
{
    class Salto : Instruccion
    {
        public Salto(string label, int linea, int columna) : base(linea, columna)
        {
            Label = label;
        }

        public string Label { get; set; }

        public override object Ejecutar(Entorno e)
        {
            Simbolo sim = e.GetSimbolo(Label);
            if (sim != null)
            {
                return sim.Valor;
            }
            else
            {
                Console.WriteLine("Error, etiqueta " + Label + " no encontrada. Línea: " + Linea);
            }
            return null;
        }
    }
}
