using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Compilador.parser._3d.ast.entorno
{
    class Entorno
    {
        public Entorno() {
            Padre = null;
            Tabla = new LinkedList<Simbolo>();
        }

        public Entorno Padre { get; set; }
        public LinkedList<Simbolo> Tabla { get; set; }

        public void AddSimbolo(Simbolo s)
        {
            Tabla.AddLast(s);
        }

        public Simbolo GetSimbolo(string id)
        {
            foreach (Simbolo s in Tabla)
            {
                if (s.Id.Equals(id))
                {
                    return s;
                }
            }
            if (Padre != null)
                return Padre.GetSimbolo(id);
            return null;
        }

        public void EntrarAmbito()
        {
            Entorno ant = Padre;
            Padre = new Entorno();
            Padre.Padre = ant;
            Padre.Tabla = Tabla;

            LinkedList<Simbolo> nuevo = new LinkedList<Simbolo>();

            Regex rgx = new Regex("^t[0-9]+$");

            foreach(Simbolo simbolo in Tabla)
            {
                if (rgx.IsMatch(simbolo.Id))
                {
                    nuevo.AddLast(new Simbolo(simbolo.Id, simbolo.Valor, simbolo.Tipo));
                }
            }

            Tabla = nuevo;
        }

        public void SalirAmbito()
        {
            Tabla = Padre.Tabla;
            Padre = Padre.Padre;
        }

        public void Recorrer()
        {
            int i = 0;
            Entorno actual = this;

            while (actual != null)
            {
                Console.WriteLine("Entorno " + i++);
                foreach (Simbolo s in actual.Tabla)
                {
                    Console.Write(s.Id + " : " + s.Tipo);
                    if (s.Valor != null)
                        Console.WriteLine(" => " + s.Valor);
                    else
                        Console.WriteLine("");
                }
                actual = actual.Padre;
            }
        }
        
    }
}
