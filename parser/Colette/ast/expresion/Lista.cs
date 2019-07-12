using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser.Colette.ast.entorno;

namespace Compilador.parser.Colette.ast.expresion
{
    class Lista : Expresion
    {
        public Lista(LinkedList<Expresion> list, int linea, int columna) : base(linea, columna)
        {
            List = list;
            Tipo = new Tipo(Tipo.Type.LIST);
        }

        public LinkedList<Expresion> List { get; set; }
        public Tipo Tipo { get; set; }

        public override Result GetC3D(Ent e, bool funcion, bool ciclo, bool isObjeto, LinkedList<Error> errores)
        {
            Result result = new Result();

            result.Valor = NuevoTemporal();
            result.Codigo = result.Valor + " = H;\n";
            //result.Codigo += "H = H + 1;\n";

            if (List != null)
            {
                int i = 1;
                foreach (Expresion valor in List)
                {
                    string tmp = NuevoTemporal();
                    result.Codigo += tmp + " = H;\n";
                    result.Codigo += "H = H + 1;\n";

                    string ptr = NuevoTemporal();
                    result.Codigo += ptr + " = H;\n";
                    result.Codigo += "H = H + 1;\n";

                    Result rsValor = valor.GetC3D(e, funcion, ciclo, isObjeto, errores);

                    if (!valor.GetTipo().IsIndefinido())
                    {
                        if (Tipo.SubTip == Tipo.Type.INDEFINIDO)
                            Tipo.SubTip = valor.GetTipo().Tip;

                        if (rsValor != null)
                        {
                            if (!rsValor.Valor.Equals(""))
                            {
                                if (Tipo.SubTip == valor.GetTipo().Tip)
                                {
                                    result.Codigo += rsValor.Codigo;
                                    result.Codigo += "heap[" + tmp + "] = " + rsValor.Valor + ";\n";

                                    if (i != List.Count())
                                        result.Codigo += "heap[" + ptr + "] = H;\n";
                                    else
                                        result.Codigo += "heap[" + ptr + "] = 0 - 1;\n";
                                    i++;
                                    continue;
                                }
                                errores.AddLast(new Error("Semántico", "No es homogeneo.", Linea, Columna));
                            }

                        }
                    }
                    errores.AddLast(new Error("Semántico", "Error en lista.", Linea, Columna));
                    return null;
                }
            }
            else
            {
                result.Codigo += "H = H + 1;\n";
                result.Codigo += "heap[" + result.Valor + "] = 0 - 1;\n";
            }
            result.Tipo = Tipo;

            return result;
        }

        public override Tipo GetTipo()
        {
            return Tipo;
        }
    }
}
