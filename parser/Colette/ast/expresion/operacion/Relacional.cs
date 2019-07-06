using Compilador.parser.Colette.ast.entorno;
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
        { Cortocircuito = false; Tipo = new Tipo(Tipo.Type.INDEFINIDO); }

        public bool Cortocircuito { get; set; }
        private Tipo Tipo { get; set; }

        public override Result GetC3D(Ent e)
        {
            Result result = new Result();

            Result rsOp1 = Op1.GetC3D(e);
            Result rsOp2 = Op2.GetC3D(e);

            TipoResultante(Op1.GetTipo(), Op2.GetTipo());

            if (!Tipo.IsIndefinido())
            {
                if (!Op1.GetTipo().IsObject() && !Op2.GetTipo().IsObject()) /*si los dos no son objetos*/
                {
                    /*
                    if (Op == Operador.IGUAL)
                    {
                        if (Op1.GetTipo() != Op2.GetTipo())
                        {
                            result.Codigo += rsOp1.Codigo;
                            result.Codigo += rsOp2.Codigo;
                            result.Valor = NuevoTemporal();
                            result.Codigo += result.Valor + " = 0;\n";
                            return result;
                        }
                    }

                    if (Op == Operador.DIFERENTE)
                    {
                        if (Op1.GetTipo() != Op2.GetTipo())
                        {
                            result.Codigo += rsOp1.Codigo;
                            result.Codigo += rsOp2.Codigo;
                            result.Valor = NuevoTemporal();
                            result.Codigo += result.Valor + " = 1;\n";
                            return result;
                        }
                    }
                    */

                    if (Op1.GetTipo().IsNumeric() || Op1.GetTipo().IsString() || Op1.GetTipo().IsBoolean())
                    {
                        if (Op2.GetTipo().IsNumeric() || Op2.GetTipo().IsString() || Op2.GetTipo().IsBoolean())
                        {
                            result.Codigo += rsOp1.Codigo;
                            result.Codigo += rsOp2.Codigo;

                            if (Op1.GetTipo().IsString())
                                ObtenerValor(rsOp1, result);
                            if (Op2.GetTipo().IsString())
                                ObtenerValor(rsOp2, result);

                            result.Valor = NuevoTemporal();

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

                            result.EtiquetaV = NuevaEtiqueta();
                            result.EtiquetaF = NuevaEtiqueta();

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
                                result.EtiquetaV += ":\n";
                                result.EtiquetaF += ":\n";
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error, no se pudo realizar la operacion relacional. Linea" + Linea);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error, no se pudo realizar la operacion relacional. Linea" + Linea);
                    }
                }
                else
                {
                    if (Op == Operador.IGUAL || Op == Operador.DIFERENTE) /*con objetos solo se puede == !=*/
                    {
                        result.Codigo += rsOp1.Codigo;
                        result.Codigo += rsOp2.Codigo;
                        result.Valor = NuevoTemporal();

                        if (Op == Operador.IGUAL)
                        {
                            if (Op1.GetTipo().Tip == Op2.GetTipo().Tip)
                            {
                                /*comparar objetos*/
                            }
                            else
                            {
                                result.Codigo += result.Valor + " = 0;\n";
                            }
                        }
                        else
                        {
                            if (Op1.GetTipo().Tip == Op2.GetTipo().Tip)
                            {
                                /*comparar objetos*/
                            }
                            else
                            {
                                result.Codigo += result.Valor + " = 1;\n";
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error, no se pudo realizar la operacion relacional. Linea" + Linea);
                    }
                }
            }
            else
            {
                Console.WriteLine("Error, no se pudo realizar la operacion relacional. Linea" + Linea);
            }
            return result;
        }

        public override Tipo GetTipo()
        {
            return Tipo;
        }

        public void TipoResultante(Tipo op1, Tipo op2)
        {
            if (!op1.IsIndefinido() && !op2.IsIndefinido())
            {
                if (Op == Operador.IGUAL || Op == Operador.DIFERENTE)
                {
                    Tipo.Tip = Tipo.Type.BOOLEAN;
                    return;
                }
                else
                {
                    if ((op1.IsNumeric() || op1.IsBoolean()) && (op2.IsNumeric() || op2.IsBoolean()))
                    {
                        Tipo.Tip = Tipo.Type.BOOLEAN;
                        return;
                    }
                    else if (op1.IsString() || op2.IsString())
                    {
                        Tipo.Tip = Tipo.Type.BOOLEAN;
                        return;
                    }
                }
            }
            Tipo.Tip = Tipo.Type.INDEFINIDO;
        }

        public void ObtenerValor(Result rsOp, Result result)
        {
            rsOp.EtiquetaV = NuevaEtiqueta();
            rsOp.EtiquetaF = NuevaEtiqueta();
            string etqCiclo = NuevaEtiqueta();
            string tmpCiclo = NuevoTemporal();
            string contador = NuevoTemporal();

            result.Codigo += contador + " = 0;\n";
            result.Codigo += etqCiclo + ":\n";
            result.Codigo += tmpCiclo + " = heap[" + rsOp.Valor + "];\n";
            result.Codigo += "if (" + tmpCiclo + " == 0) goto " + rsOp.EtiquetaV + ";\n";
            result.Codigo += "goto " + rsOp.EtiquetaF + ";\n";
            result.Codigo += rsOp.EtiquetaF + ":\n";
            result.Codigo += contador + " = " + contador + " + " + tmpCiclo + ";\n";
            result.Codigo += rsOp.Valor + " = " + rsOp.Valor + " + 1;\n";
            result.Codigo += "goto " + etqCiclo + ";\n";
            result.Codigo += rsOp.EtiquetaV + ":\n";
            rsOp.Valor = contador;
        }
    }
}
