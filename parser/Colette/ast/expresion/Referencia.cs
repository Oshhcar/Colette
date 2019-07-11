using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser.Colette.ast.entorno;

namespace Compilador.parser.Colette.ast.expresion
{
    class Referencia : Expresion
    {
        public Referencia(Expresion expresion, string id, int linea, int columna) : base(linea, columna)
        {
            Expresion = expresion;
            Id = id;
            Tipo = new Tipo(Tipo.Type.INDEFINIDO);
            Acceso = true;
            ObtenerValor = false;
        }

        public Expresion Expresion { get; set; }
        public string Id { get; set; }
        public Tipo Tipo { get; set; }
        public bool Acceso { get; set; }
        public Sim Simbolo { get; set; }
        public string PtrVariable { get; set; }
        public bool ObtenerValor { get; set; }

        public override Result GetC3D(Ent e, bool funcion, bool ciclo, bool isObjeto, LinkedList<Error> errores)
        {
            Result result = new Result();

            Result rsExpresion;

            if (Acceso)
            {
                /*Puede que sea global*/
                if (Expresion is Identificador)
                {
                    Sim clase = e.GetClase(((Identificador)Expresion).Id);
                    if (clase != null)
                    {
                        Sim atributo = clase.Entorno.Get(Id);
                        if (atributo != null)
                        {
                            result.Simbolo =  atributo;
                            if (atributo.Rol == Rol.GLOBAL)
                            {
                                result.Valor = NuevoTemporal();
                                result.Codigo += result.Valor + " = " + "heap[" + atributo.Pos + "];\n";
                                Tipo = atributo.Tipo;
                                return result;
                            }
                        }
                    }
                }

                rsExpresion = Expresion.GetC3D(e, funcion, ciclo, isObjeto, errores);

                if (rsExpresion != null)
                {
                    Sim var = null;

                    if (Expresion is Identificador)
                        var = ((Identificador)Expresion).Simbolo;
                    else if (Expresion is Referencia)
                        var = ((Referencia)Expresion).Simbolo;

                    if (var != null)
                    {
                        Sim clase = e.GetClase(var.Tipo.Objeto);

                        if (clase != null)
                        {
                            Sim atributo = clase.Entorno.Get(Id);

                            if (atributo != null)
                            {
                                result.Simbolo = atributo;
                                result.Codigo += rsExpresion.Codigo;

                                string ptrHeap = NuevoTemporal();
                                result.Codigo += ptrHeap + " = " + rsExpresion.Valor + " + " + atributo.Pos + ";\n";

                                result.Valor = NuevoTemporal();
                                result.Codigo += result.Valor + " = " + "heap[" + ptrHeap + "];\n";

                                Tipo = atributo.Tipo;
                            }
                            else
                            {
                                errores.AddLast(new Error("Semántico", "El atributo: " + Id + " no está declarado.", Linea, Columna));
                                return null;
                            }
                        }
                    }
                }
                else
                {
                    errores.AddLast(new Error("Semántico", "Error obteniendo la referencia", Linea, Columna));
                    return null;
                }
            }
            else
            {
                /*Puede que sea global*/
                if (Expresion is Identificador)
                {
                    Sim clase = e.GetClase(((Identificador)Expresion).Id);
                    if (clase != null)
                    {
                        Sim atributo = clase.Entorno.Get(Id);
                        if (atributo != null)
                        {
                            if (atributo.Rol == Rol.GLOBAL)
                            {
                                result.Simbolo = atributo;
                                result.Valor = NuevoTemporal();
                                result.Valor = "heap[" + atributo.Pos + "]";
                                Tipo = atributo.Tipo;
                                return result;
                            }
                        }
                    }
                }

                if (Expresion is Identificador)
                    ((Identificador)Expresion).Acceso = false;
                else if (Expresion is Referencia)
                    ((Referencia)Expresion).Acceso = false;

                rsExpresion = Expresion.GetC3D(e, funcion, ciclo, isObjeto, errores);

                if (rsExpresion != null)
                {
                    Tipo = Expresion.GetTipo();
                    result.Codigo += rsExpresion.Codigo;
                    result.Valor = rsExpresion.Valor;
                    
                    if (Expresion is Identificador)
                        Simbolo = ((Identificador)Expresion).Simbolo;
                    else if (Expresion is Referencia)
                        Simbolo = ((Referencia)Expresion).Simbolo;

                    if (ObtenerValor)
                    {
                        if (Simbolo != null)
                        {
                            Sim clase = e.GetClase(Simbolo.Tipo.Objeto);
                            if (clase != null)
                            {
                                Sim atributo = clase.Entorno.Get(Id);

                                if (atributo != null)
                                {
                                    result.Simbolo = atributo;
                                    string valor = NuevoTemporal();
                                    result.Codigo += valor + " = " + rsExpresion.Valor + ";\n";
                                    string ptrHeap = NuevoTemporal();
                                    result.Codigo += ptrHeap + " = " + valor + " + " + atributo.Pos + ";\n";

                                    result.Valor = NuevoTemporal();
                                    result.Valor = "heap[" + ptrHeap + "]";

                                    PtrVariable = atributo.Pos + "";

                                    Tipo = atributo.Tipo;
                                }
                            }
                        }
                    }
                }
                else
                {
                    errores.AddLast(new Error("Semántico", "Error obteniendo la referencia", Linea, Columna));
                    return null;
                }

            }

            return result;
        }

        public override Tipo GetTipo()
        {
            return Tipo;
        }
    }
}
