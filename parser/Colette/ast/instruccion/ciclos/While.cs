﻿using System;
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
            if (!isDeclaracion)
                Debugger(e, "While");

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

                Ent local = new Ent(e.Ambito + "_while", e);
                local.EtiquetaCiclo = etqCiclo;
                local.EtiquetaSalida = e.EtiquetaSalida;
                local.EtiquetaSalidaCiclo = NuevaEtiqueta();
                local.Size = e.Size;
                local.Pos = e.Pos;

                if (BloqueElse == null)
                {
                    result.Codigo += etqCiclo + ":\n";
                    result.Codigo += rsCondicion.Codigo;
                    result.Codigo += rsCondicion.EtiquetaV;
                    result.Codigo += Bloque.GetC3D(local, funcion, true, isDeclaracion, isObjeto, errores).Codigo;
                    result.Codigo += "goto " + etqCiclo + ";\n";
                    result.Codigo += rsCondicion.EtiquetaF;
                    result.Codigo += local.EtiquetaSalidaCiclo +":\n";
                }
                else
                {
                    string bandera = NuevoTemporal();
                    result.Codigo += bandera + " = 0;\n";
                    result.Codigo += etqCiclo + ":\n";
                    result.Codigo += rsCondicion.Codigo;
                    result.Codigo += rsCondicion.EtiquetaV;
                    result.Codigo += bandera + " = 1;\n";
                    result.Codigo += Bloque.GetC3D(local, funcion, true, isDeclaracion, isObjeto, errores).Codigo;
                    result.Codigo += "goto " + etqCiclo + ";\n";
                    result.Codigo += rsCondicion.EtiquetaF;

                    e.Pos = local.Pos;

                    Ent local2 = new Ent(e.Ambito + "_else", e);
                    local2.EtiquetaCiclo = e.EtiquetaCiclo;
                    local2.EtiquetaSalida = e.EtiquetaSalida;
                    local2.Size = e.Size;
                    local2.Pos = e.Pos;

                    result.EtiquetaV = NuevaEtiqueta();
                    result.EtiquetaF = NuevaEtiqueta();
                    result.Codigo += "ifFalse (" + bandera + " == 0) goto " + result.EtiquetaV + ";\n";
                    result.Codigo += "goto " + result.EtiquetaF + ";\n";
                    result.Codigo += result.EtiquetaF + ":\n";
                    result.Codigo += BloqueElse.GetC3D(local2, funcion, ciclo, isDeclaracion, isObjeto, errores).Codigo;
                    result.Codigo += result.EtiquetaV + ":\n";

                    result.Codigo += local.EtiquetaSalidaCiclo + ":\n";

                    e.Pos = local2.Pos;
                }
            }
            else
            {
                if (BloqueElse == null)
                {
                    Ent local = new Ent(e.Ambito + "_while", e);
                    local.Pos = e.Pos;
                    result.Codigo += Bloque.GetC3D(local, funcion, ciclo, isDeclaracion, isObjeto, errores).Codigo;
                    e.Pos = local.Pos;
                }
                else
                {
                    Ent local = new Ent(e.Ambito + "_while", e);
                    local.Pos = e.Pos;
                    result.Codigo += Bloque.GetC3D(local, funcion, ciclo, isDeclaracion, isObjeto, errores).Codigo;
                    e.Pos = local.Pos;
                    Ent local2 = new Ent(e.Ambito + "_else", e);
                    local2.Pos = e.Pos;
                    result.Codigo += BloqueElse.GetC3D(local2, funcion, ciclo, isDeclaracion, isObjeto, errores).Codigo;
                    e.Pos = local.Pos;
                }
            }
            return result;
        }
    }
}
