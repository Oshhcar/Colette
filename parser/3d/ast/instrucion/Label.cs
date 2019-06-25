using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser._3d.ast.entorno;

namespace Compilador.parser._3d.ast.instrucion
{
    class Label : Instruccion
    {
        public Label(string label, int linea, int columna) : base(linea, columna)
        {
            Label_ = label;
            I = 0;
        }

        public string Label_ { get; set; }
        public int I { get; set; }

        public override object Ejecutar(Entorno e)
        {
            if (e.GetSimbolo(Label_) == null)
            {
                e.AddSimbolo(new Simbolo(Label_, I, Tipo.LABEL));
            }
            else
            {
                Console.WriteLine("Error, etiquta " + Label_ + " ya definida. Línea: " + Linea);
            }

            return null;
        }
    }
}
