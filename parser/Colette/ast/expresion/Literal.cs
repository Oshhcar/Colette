using Compilador.parser.Colette.ast.entorno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser.Colette.ast.expresion
{
    class Literal : Expresion
    {
        public Literal(Tipo tipo, Object valor, int linea, int columna) : base(linea, columna)
        {
            Tipo = tipo;
            Valor = valor;
        }

        public Tipo Tipo { get; set; }
        
        public Object Valor { get; set; }

        public override Result GetC3D(Ent e)
        {
            Result result = new Result();
            result.Tipo = Tipo;

            switch (Tipo)
            {
                case Tipo.INT:
                    result.Tipo = Tipo.INT;
                    result.Valor = Valor.ToString();
                    break;
                case Tipo.DOUBLE:
                    result.Tipo = Tipo.DOUBLE;
                    result.Valor = Valor.ToString();
                    break;
                case Tipo.STRING:
                    result.Tipo = Tipo.STRING;
                    result.Valor = NuevoTemporal();
                    result.Codigo = result.Valor + " = H;\n";
                    result.Codigo += "H = H + " + (Valor.ToString().Length - 1) + ";\n";

                    int cont = 0;
                    foreach(char c in Valor.ToString().Substring(1, Valor.ToString().Length-2))
                    {
                        string tmp = NuevoTemporal();
                        result.Codigo += tmp + " = " + result.Valor + " + " + cont++ + ";\n";
                        result.Codigo += "heap[" +tmp+ "] = "+(int)c+";\n";

                    }
                    string tmp2 = NuevoTemporal();
                    result.Codigo += tmp2 + " = " + result.Valor + " + " + cont++ + ";\n";
                    result.Codigo += "heap[" + tmp2 + "] = 0;\n";
                    break;
            }
            
            return result;
        }

        public override Tipo GetTipo(Ent e)
        {
            return Tipo;
        }
    }
}
