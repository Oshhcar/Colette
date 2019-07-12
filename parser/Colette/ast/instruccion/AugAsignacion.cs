using Compilador.parser.Colette.ast.entorno;
using Compilador.parser.Colette.ast.expresion;
using Compilador.parser.Colette.ast.expresion.operacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Compilador.parser.Colette.ast.expresion.operacion.Operacion;

namespace Compilador.parser.Colette.ast.instruccion
{
    class AugAsignacion : Instruccion
    {
        public AugAsignacion(Expresion objetivo, Expresion valor, Operador op, int linea, int columna) : base(linea, columna)
        {
            Objetivo = objetivo;
            Valor = valor;
            Op = op;
        }

        public Expresion Objetivo { get; set; }
        public Expresion Valor { get; set; }
        public Operador Op { get; set; }

        public override Result GetC3D(Ent e, bool funcion, bool ciclo, bool isDeclaracion, bool isObjeto, LinkedList<Error> errores)
        {
            if (!isDeclaracion)
                Debugger(e, "Asignacion");

            Result result = new Result();

            if (!isDeclaracion)
            {
                Aritmetica operacion = new Aritmetica(Objetivo, Valor, Op, Linea, Columna);

                Result rsOperacion = operacion.GetC3D(e, funcion, ciclo, isObjeto, errores);

                if (rsOperacion != null)
                {
                    if (!operacion.GetTipo().IsIndefinido())
                    {
                        if (Objetivo is Identificador)
                            ((Identificador)Objetivo).Acceso = false;
                        else if (Objetivo is Referencia)
                            ((Referencia)Objetivo).Acceso = false;

                        Result rsObjetivo = Objetivo.GetC3D(e, funcion,ciclo,isObjeto,errores);

                        if (operacion.GetTipo().Tip == Objetivo.GetTipo().Tip)
                        {
                            result.Codigo += rsOperacion.Codigo;
                            result.Codigo += rsObjetivo.Codigo;
                            result.Codigo += rsObjetivo.Valor + " = " + rsOperacion.Valor + ";\n";
                        }
                        else
                        {
                            errores.AddLast(new Error("Semántico", "El valor a asignar no es del mismo tipo.", Linea, Columna));
                            return null;
                        }
                    }
                }
            }

            return result;
        }
    }
}
