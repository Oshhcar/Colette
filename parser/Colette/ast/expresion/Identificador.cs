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
            GetLocal = false;
            IsDeclaracion = false;
        }

        public Identificador(string id, Tipo tipo, int linea, int columna) : base(linea, columna)
        {
            Id = id;
            Acceso = true;
            Tipo = tipo;
            PtrVariable = null;
            Simbolo = null;
            GetLocal = false;
            IsDeclaracion = false;
        }

        public string Id { get; set; }
        public bool Acceso { get; set; }
        public Tipo Tipo { get; set; }
        public string PtrVariable { get; set; }
        public Sim Simbolo { get; set; }
        public bool GetLocal { get; set; }
        public bool IsDeclaracion { get; set; }

        public override Result GetC3D(Ent e, bool funcion, bool ciclo, bool isObjeto, LinkedList<Error> errores)
        {
            Sim s = null;

            if (GetLocal)
                s = e.Get(Id);
            else
                s = e.GetGlobal(Id);

            if (s != null)
            {
                Tipo = s.Tipo;
                Result result = new Result();

                PtrVariable = s.Pos+"";
                Simbolo = s;
                result.Simbolo = s;

                if (!IsDeclaracion)
                {
                    if (Acceso)
                    {
                        result.Valor = NuevoTemporal();

                        if (s.Rol == Rol.GLOBAL)
                        {
                            result.Codigo += result.Valor + " = heap[" + s.Pos + "];\n";
                        }
                        else if (s.IsAtributo)
                        {
                            string ptrStack = NuevoTemporal();
                            string ptrHeap = NuevoTemporal();
                            string valorHeap = NuevoTemporal();

                            result.Codigo = ptrStack + " = P + 1;\n";
                            result.Codigo += valorHeap + " = stack[" + ptrStack + "];\n";
                            result.Codigo += ptrHeap + " = " + valorHeap + " + " + s.Pos + ";\n";
                            result.Codigo += result.Valor + " = heap[" + ptrHeap + "]";
                        }
                        else
                        {
                            string ptrStack = NuevoTemporal();
                            result.Codigo = ptrStack + " = P + " + s.Pos + ";\n";
                            result.Codigo += result.Valor + " = stack[" + ptrStack + "];\n";
                        }

                    }
                    else
                    {
                        result.PtrStack = s.Pos;
                        if (s.Rol == Rol.GLOBAL)
                        {
                            result.Valor = "heap[" + s.Pos + "]";
                        }
                        else if (s.IsAtributo)
                        {
                            string ptrStack = NuevoTemporal();
                            string ptrHeap = NuevoTemporal();
                            string valorHeap = NuevoTemporal();

                            result.Codigo = ptrStack + " = P + 1;\n";
                            result.Codigo += valorHeap + " = stack[" + ptrStack + "];\n";
                            result.Codigo += ptrHeap + " = " + valorHeap + " + " + s.Pos + ";\n";
                            result.Valor = "heap[" + ptrHeap + "]";
                        }
                        else
                        {
                            string ptrStack = NuevoTemporal();
                            result.Codigo = ptrStack + " = P + " + s.Pos + ";\n";
                            result.Valor = "stack[" + ptrStack + "]";
                        }
                    }
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
