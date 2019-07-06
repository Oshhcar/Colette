using Compilador.parser.Colette.ast.entorno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser.Colette.ast.expresion.operacion
{
    class Logica : Operacion
    {
        public Logica(Expresion op1, Expresion op2, Operacion.Operador op, int linea, int columna)
            : base(op1, op2, op, linea, columna)
        { Evaluar = false;  Tipo = new Tipo(Tipo.Type.INDEFINIDO); }

        private Tipo Tipo { get; set; }
        public bool Evaluar { get; set; }

        public override Result GetC3D(Ent e)
        {
            Result result = new Result();

            if (Op1 is Relacional)
                ((Relacional)Op1).Cortocircuito = true;
            else if (Op1 is Logica)
                ((Logica)Op1).Evaluar = true;

            Result rsOp1 = Op1.GetC3D(e);

            if (Op2 != null)
            {
                if (Op2 is Relacional)
                    ((Relacional)Op2).Cortocircuito = true;
                else if (Op2 is Logica)
                    ((Logica)Op2).Evaluar = true;


                Result rsOp2 = Op2.GetC3D(e);

                TipoResultante(Op1.GetTipo(), Op2.GetTipo());

                if (!Tipo.IsIndefinido())
                {
                    if (Tipo.IsBoolean()) /*S*/
                    {
                        if (Op1 is Literal)
                        {
                            rsOp1.EtiquetaV = NuevaEtiqueta();
                            rsOp1.EtiquetaF = NuevaEtiqueta();

                            rsOp1.Codigo += "ifFalse (" + rsOp1.Valor + " == 1) goto " + rsOp1.EtiquetaV + ";\n";
                            rsOp1.Codigo += "goto " + rsOp1.EtiquetaF + ";\n";
                            rsOp1.EtiquetaV += ":\n";
                            rsOp1.EtiquetaF += ":\n";
                        }

                        if (Op2 is Literal)
                        {
                            rsOp2.EtiquetaV = NuevaEtiqueta();
                            rsOp2.EtiquetaF = NuevaEtiqueta();

                            rsOp2.Codigo += "ifFalse (" + rsOp2.Valor + " == 1) goto " + rsOp2.EtiquetaV + ";\n";
                            rsOp2.Codigo += "goto " + rsOp2.EtiquetaF + ";\n";
                            rsOp2.EtiquetaV += ":\n";
                            rsOp2.EtiquetaF += ":\n";
                        }

                        if (Op == Operador.AND)
                        {
                            result.Codigo += rsOp1.Codigo;

                            if (Op1 is Relacional || Op1 is Literal)
                            {
                                result.Codigo += rsOp1.EtiquetaF;
                                rsOp1.EtiquetaF = rsOp1.EtiquetaV;
                                rsOp1.EtiquetaV = null;
                            }

                            result.Codigo += rsOp2.Codigo;

                            if (Op2 is Relacional || Op2 is Literal)
                            {
                                string copy = rsOp2.EtiquetaV;
                                rsOp2.EtiquetaV = rsOp2.EtiquetaF;
                                rsOp2.EtiquetaF = copy;
                            }

                            /*
                            if(Op2 is Relacional)
                                result.Codigo += rsOp2.EtiquetaF;*/

                            if (!Evaluar)
                            {
                                result.Valor = NuevoTemporal();
                                string etiquetaS = NuevaEtiqueta();

                                if (rsOp1.EtiquetaV != null)
                                    result.Codigo += rsOp1.EtiquetaV;

                                if (rsOp2.EtiquetaV != null)
                                    result.Codigo += rsOp2.EtiquetaV;

                                result.Codigo += result.Valor + " = 1;\n";
                                result.Codigo += "goto " + etiquetaS + ";\n";

                                if (rsOp1.EtiquetaF != null)
                                    result.Codigo += rsOp1.EtiquetaF;

                                if (rsOp2.EtiquetaF != null)
                                    result.Codigo += rsOp2.EtiquetaF;

                                result.Codigo += result.Valor + " = 0;\n";
                                result.Codigo += etiquetaS + ":\n";
                            }
                            else
                            {
                                if (rsOp1.EtiquetaV != null)
                                    result.Codigo += rsOp1.EtiquetaV;
                                if (rsOp1.EtiquetaF != null)
                                    result.EtiquetaF += rsOp1.EtiquetaF;
                                if (rsOp2.EtiquetaV != null)
                                    result.Codigo += rsOp2.EtiquetaV;
                                if (rsOp2.EtiquetaF != null)
                                    result.EtiquetaF += rsOp2.EtiquetaF;
                            }

                        }
                        else
                        {
                            result.Codigo += rsOp1.Codigo;

                            if (Op1 is Relacional || Op1 is Literal)
                            {
                                result.Codigo += rsOp1.EtiquetaV;
                                rsOp1.EtiquetaV = rsOp1.EtiquetaF;
                                rsOp1.EtiquetaF = null;
                            }

                            result.Codigo += rsOp2.Codigo;

                            if (Op2 is Relacional || Op2 is Literal)
                            {
                                string copy = rsOp2.EtiquetaV;
                                rsOp2.EtiquetaV = rsOp2.EtiquetaF;
                                rsOp2.EtiquetaF = copy;
                            }

                            if (!Evaluar)
                            {
                                result.Valor = NuevoTemporal();
                                string etiquetaS = NuevaEtiqueta();

                                if (rsOp1.EtiquetaV != null)
                                    result.Codigo += rsOp1.EtiquetaV;

                                if (rsOp2.EtiquetaV != null)
                                    result.Codigo += rsOp2.EtiquetaV;

                                result.Codigo += result.Valor + " = 1;\n";
                                result.Codigo += "goto " + etiquetaS + ";\n";

                                if (rsOp1.EtiquetaF != null)
                                    result.Codigo += rsOp1.EtiquetaF;

                                if (rsOp2.EtiquetaF != null)
                                    result.Codigo += rsOp2.EtiquetaF;

                                result.Codigo += result.Valor + " = 0;\n";
                                result.Codigo += etiquetaS + ":\n";
                            }
                            else
                            {
                                if (rsOp1.EtiquetaV != null)
                                    result.EtiquetaV += rsOp1.EtiquetaV;
                                if (rsOp1.EtiquetaF != null)
                                    result.Codigo += rsOp1.EtiquetaF;
                                if (rsOp2.EtiquetaV != null)
                                    result.EtiquetaV += rsOp2.EtiquetaV;
                                if (rsOp2.EtiquetaF != null)
                                    result.Codigo += rsOp2.EtiquetaF;
                            }
                        }
                    }
                    else
                    {
                        if (Op1.GetTipo().IsBoolean() || Op2.GetTipo().IsBoolean())
                        {

                        }
                        else
                        {
                            if (Op == Operador.AND)
                            {
                                if (Op2 is Literal && Op2.GetTipo().Tip != Tipo.Type.NONE)
                                {
                                    result.Valor = NuevoTemporal();
                                    result.Codigo += result.Valor + " = " + rsOp2.Valor + ";\n";
                                }
                                else
                                {
                                    result.Codigo += rsOp2.Codigo;
                                    result.Valor = rsOp2.Valor;
                                }
                                Tipo = Op2.GetTipo();
                            }
                            else
                            {
                                if (Op1 is Literal && Op2.GetTipo().Tip != Tipo.Type.NONE)
                                {
                                    result.Valor = NuevoTemporal();
                                    result.Codigo += result.Valor + " = " + rsOp1.Valor + ";\n";
                                }
                                else
                                {
                                    result.Codigo += rsOp1.Codigo;
                                    result.Valor = rsOp1.Valor;
                                }
                                Tipo = Op1.GetTipo();
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Error, no se pudo realizar la operacion logica. Linea" + Linea);
                }

            }
            else
            {
                
            }

            return result;
        }

        public override Tipo GetTipo()
        {
            return Tipo;
        }

        private void TipoResultante(Tipo op1, Tipo op2)
        {
            if (!op1.IsIndefinido() && !op2.IsIndefinido())
            {
                if (op1.IsBoolean() && op2.IsBoolean())
                {
                    Tipo.Tip = Tipo.Type.BOOLEAN;
                    return;
                }
                else
                {
                    if (Op == Operador.AND)
                        Tipo = op2;
                    else
                        Tipo = op1;
                    return;
                }
            }
            Tipo.Tip = Tipo.Type.INDEFINIDO;
        }

        private void TipoResultante(Tipo op1)
        {
            Tipo = op1;
        }
    }
}
