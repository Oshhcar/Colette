using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser._3d.ast.entorno;
using Compilador.parser._3d.ast.expresion;

namespace Compilador.parser._3d.ast.instrucion
{
    class Declaracion : Instruccion
    {
        public Declaracion(LinkedList<string> ids, int linea, int columna) : base(linea, columna)
        {
            Ids = ids;
            Id = null;
            Valor = null;
        }

        public Declaracion(string id, Expresion valor, int linea, int columna) : base(linea, columna)
        {
            Ids = null;
            Id = id;
            Valor = valor;
        }

        public Declaracion(string id, int linea, int columna) : base(linea, columna)
        {
            Ids = null;
            Id = id;
            Valor = null;
        }

        public LinkedList<string> Ids { get; set; }
        public string Id { get; set; }
        public Expresion Valor { get; set; }

        public override object Ejecutar(Entorno e)
        {
            if (Ids != null)
            {
                foreach (String id in Ids)
                {
                    if (e.GetSimbolo(id) == null)
                    {
                        e.AddSimbolo(new Simbolo(id, null, Tipo.VAR));
                    }
                    else
                    {
                        Console.WriteLine("Ya existe una variable con el id " + id + " Línea: " + Linea);
                    }
                }
            }
            else
            {
                if (e.GetSimbolo(Id) == null)
                {
                    if (Valor != null)
                    {
                        Tipo tipoValor = Valor.GetTipo(e);
                        if (tipoValor != Tipo.NULL)
                        {
                            if (tipoValor == Tipo.NUMERO)
                            {
                                e.AddSimbolo(new Simbolo(Id, Valor.GetValor(), tipoValor));
                            }
                            else
                            {
                                Console.WriteLine("Error, no se puede asignar el valor. Línea: " + Linea);
                            }
                        }
                    }
                    else
                    {
                        //Validar que solo al heap y al stack se le den valores altos.
                        e.AddSimbolo(new Simbolo(Id, new double[10000], Tipo.ARREGLO));
                    }
                }
                else
                {
                    Console.WriteLine("Ya existe una variable con el id " + Id + " Línea: " + Linea);
                }
            }
            return null;
        }
    }
}
