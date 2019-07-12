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

        public override Result GetC3D(Ent e, bool funcion, bool ciclo, bool isDeclaracion, bool isObjeto, LinkedList<Error> errores)
        {
            if(!isDeclaracion)
                Debugger(e, "Funcion");

            Result result = new Result();
            /*Si esto da problemas dejar firma solo con Id*/
            string firma = Id;

            if(Parametros != null)
            {
                for (int i = 1; i < Parametros.Count(); i++)/*en 1 porque el self no lo cuento*/
                {
                    firma += "_" + Parametros.ElementAt(i).GetTipo().ToString();
                }
            }

            Sim fun = e.GetMetodo(firma);

            if (!isDeclaracion)
            {
                if (fun != null)
                {
                    Ent local = new Ent(firma, e);
                    local.Size = fun.Tam;

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

                    /*Agrego etiqueta salida*/
                    local.EtiquetaSalida = NuevaEtiqueta();

                    result.Codigo += "proc " + firma + "() begin\n";
                    result.Codigo += Bloque.GetC3D(local, true, false, false, isObjeto, errores).Codigo;
                    result.Codigo += local.EtiquetaSalida + ":\n";
                    result.Codigo += "end\n\n";

                    fun.Entorno = local;
                }
            }
            else
            {
                if (fun == null)
                {
                    Ent local = new Ent(firma);

                    //local.Pos++; /*Para self*/
                    local.Add(new Sim("return", Tipo, Rol.RETURN, 1, local.GetPos(), local.Ambito, -1, -1));

                    local.GetPos(); /*Para return*/

                    int parametros = 0;
                    if (Parametros != null)
                    {
                        foreach (Identificador id in Parametros)
                        {
                            parametros++;
                            local.GetPos();
                        }
                    }

                    /*Recorro declaraciones*/
                    foreach (Nodo sentencia in Bloque.Sentencias)
                    {
                        if (sentencia is Instruccion)
                        {
                            ((Instruccion)sentencia).GetC3D(local, true, false, true, isObjeto, errores);
                        }
                        else
                        {
                           //((Expresion)sentencia).GetC3D(local, true, false, errores); no genera espacios
                        }
                    }
                         

                    fun = new Sim(firma, Tipo, Rol.FUNCION, local.Pos-1, -1, e.Ambito, parametros, -1);
                    fun.Firma = firma;
                    fun.Entorno = local;
                    e.Add(fun);
                }
                else
                {
                    errores.AddLast(new Error("Semántico", "Ya se declaró una función con la misma firma: " + firma + ".", Linea, Columna));
                }
            }

            //local.Recorrer();

            return result;
        }
    }
}
