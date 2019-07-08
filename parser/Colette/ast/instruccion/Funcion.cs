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
        public Funcion(Tipo tipo, string id, LinkedList<Identificador> parametros, Bloque bloque, int linea, int columna) : base(linea, columna)
        {
            Tipo = tipo;
            Id = id;
            Parametros = parametros;
            Bloque = bloque;
        }

        public Tipo Tipo { get; set; }
        public string Id { get; set; }
        public LinkedList<Identificador> Parametros { get; set; }
        public Bloque Bloque { get; set; }

        public override Result GetC3D(Ent e)
        {
            string firma = Id;

            if(Parametros != null)
            {
                for (int i = 1; i < Parametros.Count(); i++)
                {
                    firma += "_" + Parametros.ElementAt(i).GetTipo().ToString();
                }
            }

            Ent local = new Ent(firma, e);

            /*Agrego return*/
            local.Add(new Sim("return", Tipo, Rol.RETURN, 1, local.GetPos(),local.Ambito,-1,-1));

            /*Agrego parametros*/
            int parametros = 0;
            if (Parametros != null)
            {
                foreach (Identificador id in Parametros)
                {
                    local.Add(new Sim(id.Id, id.Tipo, Rol.PARAMETER, 1, local.GetPos(), local.Ambito, -1, 0));
                    parametros++;
                }
            }

            Result result = new Result();
            result.Codigo += "proc " + firma + "() begin\n";
            /*agregar salto final para return (recorrer aqui el bloque)*/
            result.Codigo += Bloque.GetC3D(local).Codigo;
            result.Codigo += "end\n\n";

            Sim funcion = new Sim(Id,Tipo,Rol.FUNCION, local.Pos+1,-1,e.Ambito, parametros, -1);
            funcion.Firma = firma;

            //local.Recorrer();

            e.Add(funcion);
            return result;
        }
    }
}
