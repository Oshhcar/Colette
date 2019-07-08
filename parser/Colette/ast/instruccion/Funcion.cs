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

        public override Result GetC3D(Ent e, bool funcion, bool ciclo, LinkedList<Error> errores)
        {
            Result result = new Result();
            string firma = Id;

            if(Parametros != null)
            {
                for (int i = 1; i < Parametros.Count(); i++)/*en 1 porque el self no lo cuento*/
                {
                    firma += "_" + Parametros.ElementAt(i).GetTipo().ToString();
                }
            }

            if (e.GetMetodo(firma) == null)
            {
                Ent local = new Ent(firma, e);

                /*Agrego return*/
                local.Add(new Sim("return", Tipo, Rol.RETURN, 1, local.GetPos(), local.Ambito, -1, -1));

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


                result.Codigo += "proc " + firma + "() begin\n";
                /*agregar salto final para return (recorrer aqui el bloque)*/
                result.Codigo += Bloque.GetC3D(local, true, false, errores).Codigo;
                result.Codigo += "end\n\n";

                Sim fun = new Sim(firma, Tipo, Rol.FUNCION, local.Pos, -1, e.Ambito, parametros, -1);
                fun.Firma = firma;
                fun.Entorno = local;
                //local.Recorrer();

                e.Add(fun);

            }
            else
            {
                errores.AddLast(new Error("Semántico","Ya se definió una funcion con la misma firma: "+ Id,Linea, Columna));
            }
            return result;
        }
    }
}
