﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser.Colette.ast.entorno;

namespace Compilador.parser.Colette.ast.expresion
{
    class Identificador : Expresion
    {
        public Identificador(string id, int linea, int columna) : base(linea, columna)
        {
            Id = id;
            Acceso = true;
            Tipo = new Tipo(Tipo.Type.INDEFINIDO);
            PtrVariable = null;
            Simbolo = null;
        }

        public Identificador(string id, Tipo tipo, int linea, int columna) : base(linea, columna)
        {
            Id = id;
            Acceso = true;
            Tipo = tipo;
            PtrVariable = null;
            Simbolo = null;
        }

        public string Id { get; set; }
        public bool Acceso { get; set; }
        public Tipo Tipo { get; set; }
        public string PtrVariable { get; set; }
        public Sim Simbolo { get; set; }

        public override Result GetC3D(Ent e, bool funcion, bool ciclo, bool isObjeto, LinkedList<Error> errores)
        {
            Sim s = e.GetGlobal(Id);

            if (s != null)
            {
                Tipo = s.Tipo;
                Result result = new Result();

                PtrVariable = s.Pos+"";
                Simbolo = s;

                if (Acceso)
                {
                    result.Valor = NuevoTemporal();
                    if (s.Rol != Rol.GLOBAL)
                    {
                        string ptrStack = NuevoTemporal();
                        result.Codigo = ptrStack + " = P + " + s.Pos + ";\n";
                        result.Codigo += result.Valor + " = stack[" + ptrStack + "];\n";
                    }
                    else
                        result.Codigo += result.Valor + " = heap[" + s.Pos + "];\n";
                }
                else
                {
                    result.PtrStack = s.Pos;
                    if (s.Rol != Rol.GLOBAL)
                    {
                        string ptrStack = NuevoTemporal();
                        result.Codigo = ptrStack + " = P + " + s.Pos + ";\n";
                        result.Valor = "stack[" + ptrStack + "]";
                    }
                    else
                        result.Valor = "heap[" + s.Pos + "]";
                }
                return result;
            }
            else
            {
                if(Acceso)
                    errores.AddLast(new Error("Semántico", "La variable: " + Id + " no está declarada.", Linea, Columna));

            }
            return null;
        }

        public override Tipo GetTipo()
        {
            return Tipo;
        }
    }
}
