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
        { Tipo = new Tipo(Tipo.Type.INDEFINIDO); }

        public Tipo Tipo { get; set; }

        public override Result GetC3D(Ent e)
        {
            Result result = new Result();

            Result rsOp1 = Op1.GetC3D(e);

            if (Op2 != null)
            {
                Result rsOp2 = Op2.GetC3D(e);

                TipoDominante(Op1.GetTipo(), Op2.GetTipo());

                if (!Tipo.IsIndefinido())
                {
                    result.Codigo += rsOp1.Codigo;
                    result.Codigo += rsOp2.Codigo;

                    result.Valor = NuevoTemporal();

                    if (Tipo.IsString())
                    {
                        if (!Op1.GetTipo().IsString())
                            ConvertirString(Op1, rsOp1, result);
                        if (!Op2.GetTipo().IsString())
                            ConvertirString(Op2, rsOp2, result);

                        result.EtiquetaV = NuevaEtiqueta();
                        result.EtiquetaF = NuevaEtiqueta();
                        string etqCiclo = NuevaEtiqueta();
                        string tmpCiclo = NuevoTemporal();

                        result.Codigo += result.Valor + " = H;\n";

                        result.Codigo += tmpCiclo + " = heap[" + rsOp1.Valor + "];\n";
                        result.Codigo += etqCiclo + ":\n";
                        result.Codigo += "if (" + tmpCiclo + " == 0) goto " + result.EtiquetaV + ";\n";
                        result.Codigo += "goto " + result.EtiquetaF + ";\n";
                        result.Codigo += result.EtiquetaF + ":\n";
                        result.Codigo += "heap[H] = " + tmpCiclo + ";\n";
                        result.Codigo += "H = H + 1;\n";
                        result.Codigo += rsOp1.Valor + " = " + rsOp1.Valor + " + 1;\n";
                        result.Codigo += tmpCiclo + " = heap[" + rsOp1.Valor + "];\n";
                        result.Codigo += "goto " + etqCiclo + ";\n";
                        result.Codigo += result.EtiquetaV + ":\n";

                        result.EtiquetaV = NuevaEtiqueta();
                        result.EtiquetaF = NuevaEtiqueta();
                        etqCiclo = NuevaEtiqueta();
                        tmpCiclo = NuevoTemporal();

                        result.Codigo += tmpCiclo + " = heap[" + rsOp2.Valor + "];\n";
                        result.Codigo += etqCiclo + ":\n";
                        result.Codigo += "if (" + tmpCiclo + " == 0) goto " + result.EtiquetaV + ";\n";
                        result.Codigo += "goto " + result.EtiquetaF + ";\n";
                        result.Codigo += result.EtiquetaF + ":\n";
                        result.Codigo += "heap[H] = " + tmpCiclo + ";\n";
                        result.Codigo += "H = H + 1;\n";
                        result.Codigo += rsOp2.Valor + " = " + rsOp2.Valor + " + 1;\n";
                        result.Codigo += tmpCiclo + " = heap[" + rsOp2.Valor + "];\n";
                        result.Codigo += "goto " + etqCiclo + ";\n";
                        result.Codigo += result.EtiquetaV + ":\n";

                        result.Codigo += "heap[H] = 0;\n";
                        result.Codigo += "H = H + 1;\n";

                        
                    }
                    else 
                    {
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
                            case Operador.FLOOR:
                                result.EtiquetaV = NuevaEtiqueta();
                                string etqSalida = NuevaEtiqueta();
                                
                                if (Op1 is Literal)
                                {
                                    string tmpOp1 = NuevoTemporal();
                                    result.Codigo += tmpOp1 + " = " + rsOp1.Valor + ";\n";
                                    rsOp1.Valor = tmpOp1;
                                }
                                if (Op2 is Literal)
                                {
                                    string tmpOp2 = NuevoTemporal();
                                    result.Codigo += tmpOp2 + " = " + rsOp2.Valor + ";\n";
                                    rsOp2.Valor = tmpOp2;
                                }

                                result.Codigo += "if (" + rsOp2.Valor + " != 0) goto " + result.EtiquetaV + ";\n";
                                result.Codigo += "goto " + etqSalida + ";\n";
                                result.Codigo += result.EtiquetaV + ":\n";

                                string tmpCond = NuevoTemporal();
                                result.Codigo += tmpCond + " = 0;\n";

                                result.EtiquetaV = NuevaEtiqueta();
                                result.EtiquetaF = NuevaEtiqueta();
                                result.Codigo += "if (" + rsOp2.Valor + " > 0) goto " + result.EtiquetaV + ";\n";
                                result.Codigo += "goto " + result.EtiquetaF + ";\n";
                                result.Codigo += result.EtiquetaF + ":\n";
                                result.Codigo += tmpCond + " = 1;\n";
                                string menos = NuevoTemporal();
                                result.Codigo += menos + " = 0 - 1;\n";
                                result.Codigo += rsOp2.Valor + " = " + rsOp2.Valor + " * " + menos + ";\n";
                                result.Codigo += result.EtiquetaV + ":\n";

                                string tmpCond2 = NuevoTemporal();
                                result.Codigo += tmpCond2 + " = 0;\n";

                                result.EtiquetaV = NuevaEtiqueta();
                                result.EtiquetaF = NuevaEtiqueta();
                                result.Codigo += "if (" + rsOp1.Valor + " >= 0) goto " + result.EtiquetaV + ";\n";
                                result.Codigo += "goto " + result.EtiquetaF + ";\n";
                                result.Codigo += result.EtiquetaF + ":\n";
                                result.Codigo += tmpCond2 + " = 1;\n";
                                menos = NuevoTemporal();
                                result.Codigo += menos + " = 0 - 1;\n";
                                result.Codigo += rsOp1.Valor + " = " + rsOp1.Valor + " * " + menos + ";\n";
                                result.Codigo += result.EtiquetaV + ":\n";

                                result.EtiquetaV = NuevaEtiqueta();
                                result.EtiquetaF = NuevaEtiqueta();
                                result.Codigo += result.Valor + " = 1;\n";
                                result.Codigo += "if (" + rsOp2.Valor + " > " + rsOp1.Valor + ") goto " + result.EtiquetaV + ";\n";
                                result.Codigo += "goto " + result.EtiquetaF + ";\n";
                                result.Codigo += result.EtiquetaV + ":\n";
                                result.Codigo += result.Valor + " = 0;\n";
                                result.Codigo += result.EtiquetaF + ":\n";

                                result.EtiquetaV = NuevaEtiqueta();
                                result.EtiquetaF = NuevaEtiqueta();
                                string etqCiclo = NuevaEtiqueta();
                                string tmp = NuevoTemporal();

                                result.Codigo += tmp + " = " + rsOp1.Valor + " - " + rsOp2.Valor + ";\n";
                                result.Codigo += etqCiclo + ":\n";
                                result.Codigo += tmp + " = " + tmp + " - " + rsOp2.Valor + ";\n";
                                result.Codigo += "if (" + tmp + " >= 0) goto " + result.EtiquetaV + ";\n";
                                result.Codigo += "goto " + result.EtiquetaF + ";\n";
                                result.Codigo += result.EtiquetaV + ":\n";
                                result.Codigo += result.Valor + " = " + result.Valor + " + 1;\n";
                                result.Codigo += "goto " + etqCiclo + ";\n";
                                result.Codigo += result.EtiquetaF + ":\n";

                                result.EtiquetaV = NuevaEtiqueta();
                                result.EtiquetaF = NuevaEtiqueta();
                                result.Codigo += "if (" + tmpCond + " == 0) goto " + result.EtiquetaV + ";\n";
                                result.Codigo += "goto " + result.EtiquetaF + ";\n";
                                result.Codigo += result.EtiquetaF + ":\n";
                                string etqVerd = result.EtiquetaV;

                                result.EtiquetaV = NuevaEtiqueta();
                                result.EtiquetaF = NuevaEtiqueta();
                                result.Codigo += "if (" + tmpCond2 + " == 1) goto " + result.EtiquetaV + ";\n";
                                result.Codigo += "goto " + result.EtiquetaF + ";\n";
                                result.Codigo += result.EtiquetaF + ":\n";
                                result.Codigo += result.Valor + " = " + result.Valor + " + 1;\n";
                                menos = NuevoTemporal();
                                result.Codigo += menos + " = 0 - 1;\n";
                                result.Codigo += result.Valor + " = " + result.Valor + " * " + menos +";\n";
                                result.Codigo += etqVerd + ":\n";
                                result.Codigo += result.EtiquetaV + ":\n";

                                result.EtiquetaV = NuevaEtiqueta();
                                result.EtiquetaF = NuevaEtiqueta();
                                result.Codigo += "if (" + tmpCond2 + " == 0) goto " + result.EtiquetaV + ";\n";
                                result.Codigo += "goto " + result.EtiquetaF + ";\n";
                                result.Codigo += result.EtiquetaF + ":\n";
                                etqVerd = result.EtiquetaV;

                                result.EtiquetaV = NuevaEtiqueta();
                                result.EtiquetaF = NuevaEtiqueta();
                                result.Codigo += "if (" + tmpCond + " == 1) goto " + result.EtiquetaV + ";\n";
                                result.Codigo += "goto " + result.EtiquetaF + ";\n";
                                result.Codigo += result.EtiquetaF + ":\n";
                                result.Codigo += result.Valor + " = " + result.Valor + " + 1;\n";
                                menos = NuevoTemporal();
                                result.Codigo += menos + " = 0 - 1;\n";
                                result.Codigo += result.Valor + " = " + result.Valor + " * " + menos + ";\n";
                                result.Codigo += etqVerd + ":\n";
                                result.Codigo += result.EtiquetaV + ":\n";

                                result.Codigo += etqSalida + ":\n";
                                break;
                            case Operador.POTENCIA:

                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Error de tipos Aritmetica" + Linea);
                }
            }
            else
            {
                Tipo tipOp1 = Op1.GetTipo();
                if (tipOp1.IsNumeric() || tipOp1.IsBoolean())
                {
                    if (tipOp1.IsNumeric())
                        Tipo = Op1.GetTipo();
                    else
                        Tipo = new Tipo(Tipo.Type.INT);

                    result.Valor = NuevoTemporal();

                    switch (Op)
                    {
                        case Operador.SUMA:
                            result.Codigo += result.Valor + " = " + rsOp1.Valor + " * 1;\n";
                            break;
                        case Operador.RESTA:
                            result.Codigo += result.Valor + " = 0 - 1;\n";
                            string tmp = result.Valor;
                            result.Valor = NuevoTemporal();
                            result.Codigo += result.Valor + " = " + rsOp1.Valor + " * " + tmp + ";\n";
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Error de tipos Aritmetica. Linea" + Linea);
                }
            }
            return result;
        }

        public override Tipo GetTipo()
        {
            return Tipo;
        }

        public void TipoDominante(Tipo op1, Tipo op2)
        {
            if (!op1.IsIndefinido() && !op2.IsIndefinido())
            {
                if (!op1.IsNone() && !op2.IsNone())
                {
                    if (op1.IsString() || op2.IsString())
                    {
                        if (Op == Operador.SUMA)
                        {
                            Tipo.Tip = Tipo.Type.STRING;
                            return;
                        }
                    }
                    else //if (!op1.IsBoolean() && !op2.IsBoolean())
                    {
                        if (op1.IsDouble() || op2.IsDouble())
                        {
                            Tipo.Tip = Tipo.Type.DOUBLE;
                            return;
                        }
                        else if (op1.IsInt() || op2.IsInt())
                        {
                            Tipo.Tip = Tipo.Type.INT;
                            return;
                        }
                        else if (op1.IsBoolean() || op2.IsBoolean())
                        {
                            Tipo.Tip = Tipo.Type.INT;
                            return;
                        }
                    }
                }
            }
            Tipo.Tip = Tipo.Type.INDEFINIDO;  
        }

        private void ConvertirString(Expresion op, Result rsOp, Result result)
        {
            if (op.GetTipo().IsInt())
            {
                rsOp.EtiquetaV = NuevaEtiqueta();
                rsOp.EtiquetaF = NuevaEtiqueta();
                string etqCiclo = NuevaEtiqueta();
                string tmpCiclo;

                if (op is Literal)
                {
                    string tmpOp2 = NuevoTemporal();
                    result.Codigo += tmpOp2 + " = " + rsOp.Valor + ";\n";
                    rsOp.Valor = tmpOp2;
                }

                /*Verificar si el número es negativo*/
                string negativo = NuevoTemporal();
                string factor = NuevoTemporal();
                result.Codigo += negativo + " = 0;\n";
                result.Codigo += "if (" + rsOp.Valor + " >= 0) goto " + rsOp.EtiquetaV + ";\n";
                result.Codigo += "goto " + rsOp.EtiquetaF + ";\n";
                result.Codigo += rsOp.EtiquetaF + ":\n";
                result.Codigo += negativo + " = 1;\n";
                result.Codigo += factor + " = 0-1;\n";
                result.Codigo += rsOp.Valor + " = " + rsOp.Valor + " * " + factor + ";\n";
                result.Codigo += rsOp.EtiquetaV + ":\n";

                rsOp.EtiquetaV = NuevaEtiqueta();
                rsOp.EtiquetaF = NuevaEtiqueta();

                result.Codigo += "heap[H] = 0;\n";
                result.Codigo += "H = H + 1;\n";

                tmpCiclo = NuevoTemporal();
                result.Codigo += tmpCiclo + " = " + rsOp.Valor + " % 10;\n";

                result.Codigo += etqCiclo + ":\n";
                result.Codigo += "if (" + rsOp.Valor + " < 1) goto " + rsOp.EtiquetaV + ";\n";
                result.Codigo += "goto " + rsOp.EtiquetaF + ";\n";
                result.Codigo += rsOp.EtiquetaF + ":\n";
                result.Codigo += "heap[H] = " + tmpCiclo + " + 48;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += rsOp.Valor + " = " + rsOp.Valor + " / 10;\n";
                result.Codigo += tmpCiclo + " = " + rsOp.Valor + " % 10;\n";
                result.Codigo += "goto " + etqCiclo + ";\n";
                result.Codigo += rsOp.EtiquetaV + ":\n";
                
                /*Coloco el (-) de negativo*/
                rsOp.EtiquetaV = NuevaEtiqueta();
                rsOp.EtiquetaF = NuevaEtiqueta();
                result.Codigo += "if (" + negativo + " == 0) goto " + rsOp.EtiquetaV + ";\n";
                result.Codigo += "goto " + rsOp.EtiquetaF + ";\n";
                result.Codigo += rsOp.EtiquetaF + ":\n";
                result.Codigo += "heap[H] = 45;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += rsOp.EtiquetaV + ":\n";

                string tmp = NuevoTemporal();
                result.Codigo += tmp + " = H - 1;\n";

                rsOp.EtiquetaV = NuevaEtiqueta();
                rsOp.EtiquetaF = NuevaEtiqueta();
                etqCiclo = NuevaEtiqueta();

                rsOp.Valor = NuevoTemporal();
                result.Codigo += rsOp.Valor + " = H;\n";

                tmpCiclo = NuevoTemporal();
                result.Codigo += tmpCiclo + " = heap[" + tmp + "];\n";
                result.Codigo += etqCiclo + ":\n";
                result.Codigo += "if (" + tmpCiclo + " == 0) goto " + rsOp.EtiquetaV + ";\n";
                result.Codigo += "goto " + rsOp.EtiquetaF + ";\n";
                result.Codigo += rsOp.EtiquetaF + ":\n";
                result.Codigo += "heap[H] = " + tmpCiclo + ";\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += tmp + " = " + tmp + " - 1;\n";
                result.Codigo += tmpCiclo + " = heap[" + tmp + "];\n";
                result.Codigo += "goto " + etqCiclo + ";\n";
                result.Codigo += rsOp.EtiquetaV + ":\n";
                result.Codigo += "heap[H] = 0;\n";
                result.Codigo += "H = H + 1;\n";

            }
            else if (op.GetTipo().IsDouble())
            {

                rsOp.EtiquetaV = NuevaEtiqueta();
                rsOp.EtiquetaF = NuevaEtiqueta();
                string etqCiclo = NuevaEtiqueta();
                string tmpCiclo;

                if (op is Literal)
                {
                    string tmpOp2 = NuevoTemporal();
                    result.Codigo += tmpOp2 + " = " + rsOp.Valor + ";\n";
                    rsOp.Valor = tmpOp2;
                }

                /*Verificar si el número es negativo*/
                string negativo = NuevoTemporal();
                string factor = NuevoTemporal();
                result.Codigo += negativo + " = 0;\n";
                result.Codigo += "if (" + rsOp.Valor + " >= 0) goto " + rsOp.EtiquetaV + ";\n";
                result.Codigo += "goto " + rsOp.EtiquetaF + ";\n";
                result.Codigo += rsOp.EtiquetaF + ":\n";
                result.Codigo += negativo + " = 1;\n";
                result.Codigo += factor + " = 0-1;\n";
                result.Codigo += rsOp.Valor + " = " + rsOp.Valor + " * " + factor + ";\n";
                result.Codigo += rsOp.EtiquetaV + ":\n";

                rsOp.EtiquetaV = NuevaEtiqueta();
                rsOp.EtiquetaF = NuevaEtiqueta();


                string contador = NuevoTemporal();
                result.Codigo += contador + " = 0;\n";
                result.Codigo += rsOp.Valor + " = " + rsOp.Valor + " * 10;\n";

                tmpCiclo = NuevoTemporal();
                result.Codigo += etqCiclo + ":\n";
                result.Codigo += tmpCiclo + " = " + rsOp.Valor + " % 10;\n";
                result.Codigo += "if (" + tmpCiclo + " > 0) goto " + rsOp.EtiquetaV + ";\n";
                result.Codigo += "goto " + rsOp.EtiquetaF + ";\n";
                result.Codigo += rsOp.EtiquetaV + ":\n";
                result.Codigo += contador + " = " + contador + " + 1;\n";
                result.Codigo += rsOp.Valor + " = " + rsOp.Valor + " * 10;\n";
                result.Codigo += "goto " + etqCiclo + ";\n";
                result.Codigo += rsOp.EtiquetaF + ":\n";

                result.Codigo += rsOp.Valor + " = " + rsOp.Valor + " / 10;\n";

                rsOp.EtiquetaV = NuevaEtiqueta();
                rsOp.EtiquetaF = NuevaEtiqueta();
                etqCiclo = NuevaEtiqueta();

                result.Codigo += "heap[H] = 0;\n";
                result.Codigo += "H = H + 1;\n";

                string contador2 = NuevoTemporal();
                string etiqContV = NuevaEtiqueta();
                string etiqContF = NuevaEtiqueta();
                result.Codigo += contador2 + " = 0;\n";
                tmpCiclo = NuevoTemporal();
                result.Codigo += tmpCiclo + " = " + rsOp.Valor + " % 10;\n";

                result.Codigo += etqCiclo + ":\n";
                result.Codigo += "if (" + rsOp.Valor + " < 1) goto " + rsOp.EtiquetaV + ";\n";
                result.Codigo += "goto " + rsOp.EtiquetaF + ";\n";
                result.Codigo += rsOp.EtiquetaF + ":\n";
                result.Codigo += "heap[H] = " + tmpCiclo + " + 48;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += rsOp.Valor + " = " + rsOp.Valor + " / 10;\n";
                result.Codigo += tmpCiclo + " = " + rsOp.Valor + " % 10;\n";

                result.Codigo += contador2 + " = " + contador2 + " + 1;\n";
                result.Codigo += "if (" + contador2 + " == " + contador + ") goto " + etiqContV + ";\n";
                result.Codigo += "goto " + etiqContF + ";\n";
                result.Codigo += etiqContV + ":\n";
                result.Codigo += "heap[H] = 46;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += etiqContF + ":\n";

                result.Codigo += "goto " + etqCiclo + ";\n";
                result.Codigo += rsOp.EtiquetaV + ":\n";

                /*Coloco el (-) de negativo*/
                rsOp.EtiquetaV = NuevaEtiqueta();
                rsOp.EtiquetaF = NuevaEtiqueta();
                result.Codigo += "if (" + negativo + " == 0) goto " + rsOp.EtiquetaV + ";\n";
                result.Codigo += "goto " + rsOp.EtiquetaF + ";\n";
                result.Codigo += rsOp.EtiquetaF + ":\n";
                result.Codigo += "heap[H] = 45;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += rsOp.EtiquetaV + ":\n";

                string tmp = NuevoTemporal();
                result.Codigo += tmp + " = H - 1;\n";

                rsOp.EtiquetaV = NuevaEtiqueta();
                rsOp.EtiquetaF = NuevaEtiqueta();
                etqCiclo = NuevaEtiqueta();

                rsOp.Valor = NuevoTemporal();
                result.Codigo += rsOp.Valor + " = H;\n";

                tmpCiclo = NuevoTemporal();
                result.Codigo += tmpCiclo + " = heap[" + tmp + "];\n";
                result.Codigo += etqCiclo + ":\n";
                result.Codigo += "if (" + tmpCiclo + " == 0) goto " + rsOp.EtiquetaV + ";\n";
                result.Codigo += "goto " + rsOp.EtiquetaF + ";\n";
                result.Codigo += rsOp.EtiquetaF + ":\n";
                result.Codigo += "heap[H] = " + tmpCiclo + ";\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += tmp + " = " + tmp + " - 1;\n";
                result.Codigo += tmpCiclo + " = heap[" + tmp + "];\n";
                result.Codigo += "goto " + etqCiclo + ";\n";
                result.Codigo += rsOp.EtiquetaV + ":\n";
                result.Codigo += "heap[H] = 0;\n";
                result.Codigo += "H = H + 1;\n";

            }
            else if (op.GetTipo().IsBoolean())
            {
                rsOp.EtiquetaV = NuevaEtiqueta();
                rsOp.EtiquetaF = NuevaEtiqueta();
                string etqSalida = NuevaEtiqueta();


                string tmp = NuevoTemporal();
                result.Codigo += tmp + " = H;\n";

                result.Codigo += "if (" + rsOp.Valor + " == 0) goto " + rsOp.EtiquetaV + ";\n";
                result.Codigo += "goto " + rsOp.EtiquetaF + ";\n";
                result.Codigo += rsOp.EtiquetaV + ":\n";
                result.Codigo += "heap[H] = 70;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += "heap[H] = 97;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += "heap[H] = 108;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += "heap[H] = 115;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += "heap[H] = 101;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += "goto " + etqSalida + ";\n";
                result.Codigo += rsOp.EtiquetaF + ":\n";
                result.Codigo += "heap[H] = 84;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += "heap[H] = 114;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += "heap[H] = 117;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += "heap[H] = 101;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += etqSalida + ":\n";

                result.Codigo += "heap[H] = 0;\n";
                result.Codigo += "H = H + 1;\n";

                rsOp.Valor = tmp;
            }
        }
    }
}
