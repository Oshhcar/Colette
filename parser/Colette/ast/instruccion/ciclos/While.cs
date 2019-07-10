using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser.Colette.ast.entorno;
using Compilador.parser.Colette.ast.expresion;
using Compilador.parser.Colette.ast.expresion.operacion;

namespace Compilador.parser.Colette.ast.instruccion.ciclos
{
    class While : Instruccion
    {
        public While(Expresion condicion, Bloque bloque, Bloque bloqueElse, int linea, int columna) : base(linea, columna)
        {
            Condicion = condicion;
            Bloque = bloque;
            BloqueElse = bloqueElse;
        }

        public Expresion Condicion { get; set; }
        public Bloque Bloque { get; set; }
        public Bloque BloqueElse { get; set; }

        public override Result GetC3D(Ent e, bool funcion, bool ciclo, bool isDeclaracion, bool isObjeto, LinkedList<Error> errores)
        {
            Result result = new Result();

            if (!isDeclaracion)
            {
                if (Condicion is Relacional)
                    ((Relacional)Condicion).Cortocircuito = true;
                else if (Condicion is Logica)
                    ((Logica)Condicion).Evaluar = true;

                Result rsCondicion = Condicion.GetC3D(e, funcion, ciclo, isObjeto, errores);

                if (Condicion is Literal)
                {
                    rsCondicion.EtiquetaV = NuevaEtiqueta();
                    rsCondicion.EtiquetaF = NuevaEtiqueta();

                    rsCondicion.Codigo += "ifFalse (" + rsCondicion.Valor + " == 1) goto " + rsCondicion.EtiquetaV + ";\n";
                    rsCondicion.Codigo += "goto " + rsCondicion.EtiquetaF + ";\n";
                    rsCondicion.EtiquetaV += ":\n";
                    rsCondicion.EtiquetaF += ":\n";
                }

                if (Condicion is Relacional || Condicion is Literal)
                {
                    string copy = rsCondicion.EtiquetaF;
                    rsCondicion.EtiquetaF = rsCondicion.EtiquetaV;
                    rsCondicion.EtiquetaV = copy;
                }

                string etqCiclo = NuevaEtiqueta();
                e.EtiquetaCiclo = etqCiclo;

                if (BloqueElse == null)
                {
                    result.Codigo += etqCiclo + ":\n";
                    result.Codigo += rsCondicion.Codigo;
                    result.Codigo += rsCondicion.EtiquetaV;
                    result.Codigo += Bloque.GetC3D(e, funcion, true, isDeclaracion, isObjeto, errores).Codigo;
                    result.Codigo += "goto " + etqCiclo + ";\n";
                    result.Codigo += rsCondicion.EtiquetaF;
                }
                else
                {
                    string bandera = NuevoTemporal();
                    result.Codigo += bandera + " = 0;\n";
                    result.Codigo += etqCiclo + ":\n";
                    result.Codigo += rsCondicion.Codigo;
                    result.Codigo += rsCondicion.EtiquetaV;
                    result.Codigo += bandera + " = 1;\n";
                    result.Codigo += Bloque.GetC3D(e, funcion, true, isDeclaracion, isObjeto, errores).Codigo;
                    result.Codigo += "goto " + etqCiclo + ";\n";
                    result.Codigo += rsCondicion.EtiquetaF;

                    result.EtiquetaV = NuevaEtiqueta();
                    result.EtiquetaF = NuevaEtiqueta();
                    result.Codigo += "ifFalse (" + bandera + " == 0) goto " + result.EtiquetaV + ";\n";
                    result.Codigo += "goto " + result.EtiquetaF + ";\n";
                    result.Codigo += result.EtiquetaF + ":\n";
                    result.Codigo += BloqueElse.GetC3D(e, funcion, ciclo, isDeclaracion, isObjeto, errores).Codigo;
                    result.Codigo += result.EtiquetaV + ":\n";
                }
            }
            else
            {
                if (BloqueElse == null)
                {
                    result.Codigo += Bloque.GetC3D(e, funcion, ciclo, isDeclaracion, isObjeto, errores).Codigo;
                }
                else
                {
                    result.Codigo += Bloque.GetC3D(e, funcion, ciclo, isDeclaracion, isObjeto, errores).Codigo;
                    result.Codigo += BloqueElse.GetC3D(e, funcion, ciclo, isDeclaracion, isObjeto, errores).Codigo;
                }
            }
            return result;
        }
    }
}
