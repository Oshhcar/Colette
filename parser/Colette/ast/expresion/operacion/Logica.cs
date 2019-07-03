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
        { Evaluar = false; }

        public bool Evaluar { get; set; }

        public override Result GetC3D(Ent e)
        {
            Result result = new Result
            {
                Codigo = ""
            };

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

                if (Op == Operador.AND)
                {
                    if (!Evaluar)
                    {
                        /*EJEMPLO IDEAL=> DOS SON COMPARATIVA*/
                        result.Valor = NuevoTemporal();
                        string etiquetaS = NuevaEtiqueta();

                        result.Codigo += rsOp1.Codigo;
                        result.Codigo += rsOp1.EtiquetaF + ":\n";
                        result.Codigo += rsOp2.Codigo;
                        result.Codigo += rsOp2.EtiquetaF + ":\n";
                        result.Codigo += result.Valor + " = 1;\n";
                        result.Codigo += "goto " + etiquetaS + ";\n";
                        result.Codigo += rsOp1.EtiquetaV + ":\n";
                        result.Codigo += rsOp2.EtiquetaV + ":\n";
                        result.Codigo += result.Valor + " = 0;\n";
                        result.Codigo += etiquetaS + ":\n";
                    }
                    else
                    {
                        result.Codigo += rsOp1.Codigo;
                        result.Codigo += rsOp1.EtiquetaF + ":\n";
                        result.Codigo += rsOp2.Codigo;
                        result.EtiquetaV = rsOp2.EtiquetaF;
                        result.EtiquetaF = rsOp1.EtiquetaV + ":\n";
                        result.EtiquetaF += rsOp2.EtiquetaV;
                    }


                }
                else
                {
                    result.Valor = NuevoTemporal();
                    string etiquetaS = NuevaEtiqueta();

                    result.Codigo += rsOp1.Codigo;
                    result.Codigo += rsOp1.EtiquetaV + ":\n";
                    result.Codigo += rsOp2.Codigo;
                    result.Codigo += rsOp1.EtiquetaF + ":\n";
                    result.Codigo += rsOp2.EtiquetaF + ":\n";
                    result.Codigo += result.Valor + " = 1;\n";
                    result.Codigo += "goto " + etiquetaS + ";\n";
                    result.Codigo += rsOp2.EtiquetaV + ":\n";
                    result.Codigo += result.Valor + " = 0;\n";
                    result.Codigo += etiquetaS + ":\n";
                }

            }
            else
            {
                /*NOT*/
            }

            return result;
        }

        public override Tipo GetTipo(Ent e)
        {
            throw new NotImplementedException();
        }
    }
}
