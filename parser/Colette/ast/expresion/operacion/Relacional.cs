﻿using Compilador.parser.Colette.ast.entorno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser.Colette.ast.expresion.operacion
{
    class Relacional : Operacion
    {
        public Relacional(Expresion op1, Expresion op2, Operacion.Operador op, int linea, int columna)
            : base(op1, op2, op, linea, columna)
        { Cortocircuito = false; }

        public bool Cortocircuito { get; set; }

        public override Result GetC3D(Ent e)
        {
            Result result = new Result
            {
                Codigo = ""
            };

            Result rsOp1 = Op1.GetC3D(e);
            Result rsOp2 = Op2.GetC3D(e);

            result.Codigo += rsOp1.Codigo;
            result.Codigo += rsOp2.Codigo;
            

            result.Valor = NuevoTemporal();

            result.EtiquetaV = NuevaEtiqueta();
            result.EtiquetaF = NuevaEtiqueta();
            
            string op = "";

            switch (Op)
            {
                case Operador.MAYORQUE:
                    op = ">";
                    break;
                case Operador.MENORQUE:
                    op = "<";
                    break;
                case Operador.MAYORIGUALQUE:
                    op = ">=";
                    break;
                case Operador.MENORIGUALQUE:
                    op = "<=";
                    break;
                case Operador.IGUAL:
                    op = "==";
                    break;
                case Operador.DIFERENTE:
                    op = "!=";
                    break;
            }




            if (!Cortocircuito)
            {
                result.Codigo += "if (" + rsOp1.Valor + " " + op + " " + rsOp2.Valor + ") goto " + result.EtiquetaV + ";\n";
                result.Codigo += "goto " + result.EtiquetaF + ";\n";

                string etiquetaS = NuevaEtiqueta();
                result.Codigo += result.EtiquetaV + ":\n";
                result.Codigo += result.Valor + " = 1;\n";
                result.Codigo += "goto " + etiquetaS + ";\n";
                result.Codigo += result.EtiquetaF + ":\n";
                result.Codigo += result.Valor + " = 0;\n";
                result.Codigo += etiquetaS + ":\n";
            }
            else
            {
                result.Codigo += "ifFalse (" + rsOp1.Valor + " " + op + " " + rsOp2.Valor + ") goto " + result.EtiquetaV + ";\n";
                result.Codigo += "goto " + result.EtiquetaF + ";\n";
            }

            return result;
        }

    }
}
