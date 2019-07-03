using Compilador.parser.Colette.ast.entorno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser.Colette.ast.expresion.operacion
{
    class Aritmetica : Operacion
    {
        public Aritmetica(Expresion op1, Expresion op2, Operador op, int linea, int columna)
            : base(op1, op2, op, linea, columna)
        { }

        public override Result GetC3D(Ent e)
        {
            Result result = new Result
            {
                Codigo = ""
            };

            Result rsOp1 = Op1.GetC3D(e);

            if (rsOp1.Codigo != null)
            {
                result.Codigo += rsOp1.Codigo;
            }

            if (Op2 != null)
            {
                Result rsOp2 = Op2.GetC3D(e);

                if (rsOp2.Codigo != null)
                {
                    result.Codigo += rsOp2.Codigo;
                }

                result.Valor = NuevoTemporal();

                switch (Op)
                {
                    case Operador.SUMA:
                        result.Codigo += result.Valor + " = " + rsOp1.Valor + " + " + rsOp2.Valor + ";\n";
                        break;
                    case Operador.RESTA:
                        result.Codigo += result.Valor + " = " + rsOp1.Valor + " - " + rsOp2.Valor + ";\n";
                        break;
                    case Operador.MULTIPLICACION:
                        result.Codigo += result.Valor + " = " + rsOp1.Valor + " * " + rsOp2.Valor + ";\n";
                        break;
                    case Operador.DIVISION:
                        result.Codigo += result.Valor + " = " + rsOp1.Valor + " / " + rsOp2.Valor + ";\n";
                        break;
                    case Operador.MODULO:
                        result.Codigo += result.Valor + " = " + rsOp1.Valor + " % " + rsOp2.Valor + ";\n";
                        break;
                }
            }
            else
            {
                result.Valor = NuevoTemporal();

                switch (Op)
                {
                    case Operador.SUMA:
                        result.Codigo += result.Valor + " = " + rsOp1.Valor + " * 1;\n";
                        break;
                    case Operador.RESTA:
                        result.Codigo += result.Valor + " = " + rsOp1.Valor + " * -1;\n";
                        break;
                }
            }

            return result;
        }

        public override Tipo GetTipo(Ent e)
        {
            throw new NotImplementedException();
        }
    }
}
