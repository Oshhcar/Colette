using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser._3d.ast.entorno;

namespace Compilador.parser._3d.ast.expresion
{
    class Identificador : Expresion
    {
        public Identificador(string id, int linea, int columna) : base(linea, columna)
        {
            Id = id;
            valor = null;
        }

        public string Id { get; set; }

        private Object valor;


        public override Tipo GetTipo(Entorno e)
        {
            Simbolo sim = e.GetSimbolo(Id);
            if (sim != null)
            {
                valor = sim.Valor;
                return sim.Tipo;
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
