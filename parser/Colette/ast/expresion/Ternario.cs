using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser.Colette.ast.entorno;
using Compilador.parser.Colette.ast.expresion.operacion;

namespace Compilador.parser.Colette.ast.expresion
{
    class Ternario : Expresion
    {
        public Ternario(Expresion condicion, Expresion verdadera, Expresion falsa, int linea, int columna) : base(linea, columna)
        {
            Condicion = condicion;
            Verdadera = verdadera;
            Falsa = falsa;
            Tipo = new Tipo(Tipo.Type.INDEFINIDO);
        }

        public Expresion Condicion { get; set; }
        public Expresion Verdadera { get; set; }
        public Expresion Falsa { get; set; }
        public Tipo Tipo { get; set; }

        public override Result GetC3D(Ent e, bool funcion, bool ciclo, bool isObjeto, LinkedList<Error> errores)
        {
            Result result = new Result();

            Result rsVerdadera = Verdadera.GetC3D(e, funcion, ciclo, isObjeto, errores);
            Result rsFalsa = Falsa.GetC3D(e, funcion, ciclo, isObjeto, errores);

            if (Condicion is Relacional)
                ((Relacional)Condicion).Cortocircuito = true;
            else if (Condicion is Logica)
                ((Logica)Condicion).Evaluar = true;

            Result rsCondicion = Condicion.GetC3D(e, funcion, ciclo, isObjeto, errores);

            if (rsVerdadera != null && rsFalsa != null && rsCondicion != null)
            {
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

                string etqSalida = NuevaEtiqueta();
                result.Valor = NuevoTemporal();
                result.Codigo += rsCondicion.Codigo;
                result.Codigo += rsCondicion.EtiquetaV;
                result.Codigo += rsVerdadera.Codigo;
                result.Codigo += result.Valor + " = " + rsVerdadera.Valor + ";\n";
                result.Codigo += "goto " + etqSalida + ";\n";
                result.Codigo += rsCondicion.EtiquetaF;
                result.Codigo += rsFalsa.Codigo;
                result.Codigo += result.Valor + " = " + rsFalsa.Valor + ";\n";
                result.Codigo += etqSalida + ":\n";

                Tipo = Verdadera.GetTipo();
            }
            return result;
        }

        public override Tipo GetTipo()
        {
            return Tipo;
        }
    }
}
