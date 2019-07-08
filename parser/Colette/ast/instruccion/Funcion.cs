using Compilador.parser.Colette.ast.entorno;
using Compilador.parser.Colette.ast.expresion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser.Colette.ast.instruccion
{
    class Funcion : Instruccion
    {
        public Funcion(Tipo tipo, string id, LinkedList<Expresion> parametros, Bloque bloque, int linea, int columna) : base(linea, columna)
        {
            Tipo = tipo;
            Id = id;
            Parametros = parametros;
            Bloque = bloque;
        }

        public Tipo Tipo { get; set; }
        public string Id { get; set; }
        public LinkedList<Expresion> Parametros { get; set; }
        public Bloque Bloque { get; set; }

        public override Result GetC3D(Ent e)
        {
            string firma = Id;
            Ent local = new Ent(firma, e);

            Result result = new Result();
            result.Codigo += "proc " + firma + "() begin\n";
            /*agregar salto final para return (recorrer aqui el bloque)*/
            result.Codigo += Bloque.GetC3D(local).Codigo;
            result.Codigo += "end\n\n";

            Sim funcion = new Sim(Id,Tipo,Rol.FUNCION, local.Pos+1,-1,e.Ambito,0,-1);
            funcion.Firma = firma;

            e.Add(funcion);
            return result;
        }
    }
}
