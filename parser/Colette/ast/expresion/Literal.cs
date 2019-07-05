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

            if (Tipo.IsInt())
            {
                result.Valor = Valor.ToString();
            }
            else if (Tipo.IsDouble())
            {
                result.Valor = Valor.ToString();
            }
            else if (Tipo.IsString())
            {
                string tmp;
                int cont = 0;
                foreach (char c in Valor.ToString().Substring(1, Valor.ToString().Length - 2))
                {
                    tmp = NuevoTemporal();
                    result.Codigo += tmp + " = H + " + cont++ + ";\n";
                    result.Codigo += "heap[" + tmp + "] = " + (int)c + ";\n";

                }
                tmp = NuevoTemporal();
                result.Codigo += tmp + " = H + " + cont++ + ";\n";
                result.Codigo += "heap[" + tmp + "] = 0;\n";

                result.Valor = NuevoTemporal();
                result.Codigo += result.Valor + " = H;\n";
                result.Codigo += "H = H + " + cont + ";\n";
            }
                    
            return result;
        }

        public override Tipo GetTipo()
        {
            return Tipo;
        }
    }
}
