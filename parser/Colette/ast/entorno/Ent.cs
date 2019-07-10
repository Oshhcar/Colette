using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser.Colette.ast.entorno
{
    class Ent
    {
        public Ent(string ambito)
        {
            Simbolos = new LinkedList<Sim>();
            Pos = 0;
            Ambito = ambito;
            Padre = null;
            Size = 0;
        }

        public Ent(string ambito, Ent padre)
        {
            Simbolos = new LinkedList<Sim>();
            Pos = 0;
            Ambito = ambito;
            Padre = padre;
            Size = 0;
        }

        public LinkedList<Sim> Simbolos { get; set; }
        public int Pos { get; set; }
        public string Ambito { get; set; }
        public Ent Padre { get; set; }
        public string EtiquetaSalida { get; set; }
        public int Size { get; set; }
        public string EtiquetaCiclo { get; set; }
        public string EtiquetaSalidaCiclo { get; set; }

        public int GetPos() { return Pos++; }

        public void Add(Sim s)
        {
            Simbolos.AddLast(s);
        }

        public Sim Get(string id)
        {
            foreach (Sim s in Simbolos)
            {
                if (s.Rol != Rol.FUNCION && s.Rol != Rol.CLASE)
                {
                    if (s.Id.Equals(id))
                        return s;
                }
            }
            return null;
        }

        public Sim GetGlobal(string id)
        {
            Ent actual = this;

            while (actual != null)
            {
                foreach (Sim s in actual.Simbolos)
                {
                    if (s.Rol != Rol.FUNCION && s.Rol != Rol.CLASE)
                    {
                        if (s.Id.Equals(id))
                            return s;
                    }
                }
                actual = actual.Padre;
            }
            return null;
        }

        public Sim GetMetodo(string firma)
        {
            Ent actual = this;

            while (actual != null)
            {
                foreach (Sim s in actual.Simbolos)
                {
                    if (s.Rol == Rol.FUNCION)
                    {
                        if (s.Firma.Equals(firma))
                            return s;
                    }
                }
                actual = actual.Padre;
            }
            return null;
        }

        public Sim GetClase(string id)
        {
            Ent actual = this;

            while (actual != null)
            {
                foreach (Sim s in actual.Simbolos)
                {
                    if (s.Rol == Rol.CLASE)
                    {
                        if (s.Id.Equals(id))
                            return s;
                    }
                }
                actual = actual.Padre;
            }
            return null;
        }

        public void Recorrer()
        {
            foreach (Sim s in Simbolos)
            {
                Console.WriteLine(s.Id + ", " + s.Tipo.Tip + ", " + s.Rol + ", " + s.Tam + ", " 
                    + s.Pos+", "+s.Ambito +", "+s.NumParam+", "+s.TipoParam);

                if (s.Entorno != null)
                {
                    s.Entorno.Recorrer();
                }
            }
        }
    }
}
