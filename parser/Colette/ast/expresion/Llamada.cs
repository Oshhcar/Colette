using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser.Colette.ast.entorno;

namespace Compilador.parser.Colette.ast.expresion
{
    class Llamada : Expresion
    {
        public Llamada(string id, LinkedList<Expresion> parametros, int linea, int columna) : base(linea, columna)
        {
            Id = id;
            Parametros = parametros;
            Tipo = new Tipo(Tipo.Type.INDEFINIDO);
            ObtenerReturn = true;
        }

        public string Id { get; set; }
        public LinkedList<Expresion> Parametros { get; set; }
        public Tipo Tipo { get; set; }
        public bool ObtenerReturn { get; set; }

        public override Result GetC3D(Ent e, bool funcion, bool ciclo, LinkedList<Error> errores)
        {
            Result result = new Result();

            string firma = Id;

            if (Parametros != null)
            {
                LinkedList<Result> rsParametros = new LinkedList<Result>();
                foreach (Expresion parametro in Parametros)
                {
                    Result rsParametro = parametro.GetC3D(e, funcion, ciclo, errores);
                    if (rsParametro != null)
                    {
                        if (!parametro.GetTipo().IsIndefinido())
                        {
                            rsParametros.AddLast(rsParametro);
                            firma += "_" + parametro.GetTipo().ToString();
                            continue;
                        }
                    }

                    errores.AddLast(new Error("Semántico", "El parámetro contiene error.", Linea, Columna));

                    return null;
                }

                Sim metodo = e.GetMetodo(firma);
                if (metodo != null)
                {
                    Tipo = metodo.Tipo;

                    int pos = 2; /*inicia en dos por return y self*/

                    foreach (Result rsParametro in rsParametros)
                    {
                        string ptrCambio = NuevoTemporal();
                        string tmp = NuevoTemporal();
                        result.Codigo += ptrCambio + " = P + " + e.Size + ";\n"; //Cambio simulado
                        result.Codigo += tmp + " = " + ptrCambio + " + " + pos++ + ";\n";

                        result.Codigo += rsParametro.Codigo;
                        
                        result.Codigo += "stack[" + tmp + "] = " + rsParametro.Valor + ";\n";
                    }

                    result.Codigo += "P = P + " + e.Size + ";\n";
                    result.Codigo += "call " + firma + ";\n";

                    if (ObtenerReturn)/*Si se espera valor*/
                    {
                        string ptrReturn = NuevoTemporal();
                        result.Valor = NuevoTemporal();

                        result.Codigo += ptrReturn + " = P + 0;\n";
                        result.Codigo += result.Valor + " = stack[" + ptrReturn + "];\n";

                    }
                    result.Codigo += "P = P - " + e.Size + ";\n";

                }
                else
                {
                    errores.AddLast(new Error("Semántico", "La función: " + Id + " no está declarada.", Linea, Columna));
                    return null;
                }
            }
            else
            {
                Sim metodo = e.GetMetodo(firma);

                if (metodo != null)
                {
                    Tipo = metodo.Tipo;
                    result.Codigo += "P = P + " + e.Size + ";\n";
                    result.Codigo += "call " + firma + ";\n";

                    if (ObtenerReturn)/*Si se espera valor*/
                    {
                        string ptrReturn = NuevoTemporal();
                        result.Valor = NuevoTemporal();

                        result.Codigo += ptrReturn + " = P + 0;\n";
                        result.Codigo += result.Valor + " = stack[" + ptrReturn + "];\n";

                    }

                    result.Codigo += "P = P - " + e.Size + ";\n";
                }
                else
                {
                    errores.AddLast(new Error("Semántico", "La función: " + Id + " no está declarada.", Linea, Columna));
                    return null;
                }
            }

            return result;
        }

        public override Tipo GetTipo()
        {
            return Tipo;
        }
    }
}
