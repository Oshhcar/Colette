using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser.Colette.ast.entorno;
using Compilador.parser.Colette.ast.instruccion;

namespace Compilador.parser.Colette.ast.expresion
{
    class Llamada : Expresion
    {
        public Llamada(string id, LinkedList<Expresion> parametros, int linea, int columna) : base(linea, columna)
        {
            Expresion = null;
            Id = id;
            Parametros = parametros;
            Tipo = new Tipo(Tipo.Type.INDEFINIDO);
            ObtenerReturn = true;
            PtrVariable = null;
        }

        public Llamada(Expresion expresion, LinkedList<Expresion> parametros, int linea, int columna) : base(linea, columna)
        {
            Expresion = expresion;
            Id = null;
            Parametros = parametros;
            Tipo = new Tipo(Tipo.Type.INDEFINIDO);
            ObtenerReturn = true;
            PtrVariable = null;
        }

        public Expresion Expresion { get; set; }
        public string Id { get; set; }
        public LinkedList<Expresion> Parametros { get; set; }
        public Tipo Tipo { get; set; }
        public bool ObtenerReturn { get; set; }
        public String PtrVariable { get; set; }

        public override Result GetC3D(Ent e, bool funcion, bool ciclo, bool isObjeto, LinkedList<Error> errores)
        {
            Result result = new Result();

            if (Expresion == null)
            {
                switch (Id)
                {
                    case "chr":
                        if (Parametros != null)
                        {
                            if (Parametros.Count() > 0)
                            {
                                Expresion valor = Parametros.ElementAt(0);
                                Result rsValor = valor.GetC3D(e, funcion, ciclo, isObjeto, errores);

                                if (rsValor != null)
                                {
                                    if (valor.GetTipo().IsInt())
                                    {
                                        result.Codigo += rsValor.Codigo;

                                        result.Valor = NuevoTemporal();
                                        result.Codigo += result.Valor + " = H;\n";
                                        result.Codigo += "heap[H] = " + rsValor.Valor + ";\n";
                                        result.Codigo += "H = H + 1;\n";
                                        result.Codigo += "heap[H] = 0;\n";
                                        result.Codigo += "H = H + 1;\n";
                                        Tipo.Tip = Tipo.Type.STRING;
                                    }
                                    else
                                    {
                                        errores.AddLast(new Error("Semántico", "El argumento debe ser entero.", Linea, Columna));
                                    }
                                }
                                else
                                {
                                    errores.AddLast(new Error("Semántico", "Error en el argumento.", Linea, Columna));
                                }
                            }
                        }
                        else
                        {
                            errores.AddLast(new Error("Semántico", "La funcion chr necesita argumentos.", Linea, Columna));
                        }
                        break;
                    case "bool":
                        if (Parametros != null)
                        {
                            if (Parametros.Count() > 0)
                            {
                                Expresion valor = Parametros.ElementAt(0);
                                Result rsValor = valor.GetC3D(e, funcion, ciclo, isObjeto, errores);

                                if (rsValor != null)
                                {
                                    if (!valor.GetTipo().IsIndefinido())
                                    {
                                        result.Codigo += rsValor.Codigo;

                                        result.EtiquetaV = NuevaEtiqueta();
                                        result.EtiquetaF = NuevaEtiqueta();
                                        result.Valor = NuevoTemporal();

                                        result.Codigo += result.Valor + " = 0;\n";
                                        result.Codigo += "if (" + rsValor.Valor + " == 1) goto " + result.EtiquetaV + ";\n";
                                        result.Codigo += "goto " + result.EtiquetaF + ";\n";
                                        result.Codigo += result.EtiquetaV + ":\n";
                                        result.Codigo += result.Valor + " = 1;\n";
                                        result.Codigo += result.EtiquetaF + ":\n";

                                        Tipo.Tip = Tipo.Type.BOOLEAN;
                                    }
                                    else
                                    {
                                        errores.AddLast(new Error("Semántico", "El argumento contine errores.", Linea, Columna));
                                    }
                                }
                                else
                                {
                                    errores.AddLast(new Error("Semántico", "Error en el argumento.", Linea, Columna));
                                }
                            }
                        }
                        else
                        {
                            errores.AddLast(new Error("Semántico", "La funcion bool necesita argumentos.", Linea, Columna));
                        }
                        break;
                    case "float":
                        if (Parametros != null)
                        {
                            if (Parametros.Count() > 0)
                            {
                                Expresion valor = Parametros.ElementAt(0);
                                Result rsValor = valor.GetC3D(e, funcion, ciclo, isObjeto, errores);

                                if (rsValor != null)
                                {
                                    if (!valor.GetTipo().IsIndefinido())
                                    {
                                        result.Codigo += rsValor.Codigo;

                                        if (!valor.GetTipo().IsString())
                                        {
                                            result.Valor = NuevoTemporal();
                                            result.Codigo += result.Valor + " = " + rsValor.Valor + " + 0.0;\n";
                                        }
                                        else
                                        {
                                            result.EtiquetaV = NuevaEtiqueta();
                                            result.EtiquetaF = NuevaEtiqueta();
                                            string etqCiclo = NuevaEtiqueta();
                                            string tmpCiclo = NuevoTemporal();

                                            result.Codigo += "heap[H] = 0;\n";
                                            result.Codigo += "H = H + 1;\n";

                                            result.Codigo += tmpCiclo + " = heap[" + rsValor.Valor + "];\n";
                                            result.Codigo += etqCiclo + ":\n";
                                            result.Codigo += "if (" + tmpCiclo + " == 0) goto " + result.EtiquetaV + ";\n";
                                            result.Codigo += "goto " + result.EtiquetaF + ";\n";
                                            result.Codigo += result.EtiquetaF + ":\n";
                                            result.Codigo += "heap[H] = " + tmpCiclo + ";\n";
                                            result.Codigo += "H = H + 1;\n";
                                            result.Codigo += rsValor.Valor + " = " + rsValor.Valor + " + 1;\n";
                                            result.Codigo += tmpCiclo + " = heap[" + rsValor.Valor + "];\n";
                                            result.Codigo += "goto " + etqCiclo + ";\n";
                                            result.Codigo += result.EtiquetaV + ":\n";

                                            result.EtiquetaV = NuevaEtiqueta();
                                            result.EtiquetaF = NuevaEtiqueta();
                                            etqCiclo = NuevaEtiqueta();
                                            tmpCiclo = NuevoTemporal();
                                            string ptr = NuevoTemporal();
                                            string factor = NuevoTemporal();
                                            result.Valor = NuevoTemporal();

                                            result.Codigo += factor + " = 1;\n";
                                            result.Codigo += result.Valor + " = 0.0;\n";
                                            result.Codigo += ptr + " = H - 1;\n";
                                            result.Codigo += tmpCiclo + " = heap[" + ptr + "];\n";
                                            result.Codigo += etqCiclo + ":\n";
                                            result.Codigo += "if (" + tmpCiclo + " == 0) goto " + result.EtiquetaV + ";\n";
                                            result.Codigo += "goto " + result.EtiquetaF + ";\n";
                                            result.Codigo += result.EtiquetaF + ":\n";

                                            string tmp = NuevoTemporal();
                                            result.Codigo += tmp + " = " + tmpCiclo + " - 48;\n";

                                            string etqPunto = NuevaEtiqueta();
                                            result.Codigo += "ifFalse (" + tmpCiclo + " == 46) goto " + etqPunto + ";\n";

                                            result.Codigo += result.Valor + " = " + result.Valor + " / " + factor + ";\n";
                                            result.Codigo += factor + " = 1;\n";

                                            result.Codigo += ptr + " = " + ptr + " - 1;\n";
                                            result.Codigo += tmpCiclo + " = heap[" + ptr + "];\n";
                                            result.Codigo += "goto " + etqCiclo + ";\n";
                                            result.Codigo += etqPunto + ":\n";

                                            result.Codigo += "if (" + tmp + " < 0) goto " + result.EtiquetaV + ";\n";
                                            result.Codigo += "if (" + tmp + " > 9) goto " + result.EtiquetaV + ";\n";

                                            string tmp2 = NuevoTemporal();
                                            result.Codigo += tmp2 + " = " + tmp + " * " + factor + ";\n";
                                            result.Codigo += result.Valor + " = " + result.Valor + " + " + tmp2 + ";\n";
                                            result.Codigo += factor + " = " + factor + " * 10;\n";

                                            result.Codigo += ptr + " = " + ptr + " - 1;\n";
                                            result.Codigo += tmpCiclo + " = heap[" + ptr + "];\n";
                                            result.Codigo += "goto " + etqCiclo + ";\n";
                                            result.Codigo += result.EtiquetaV + ":\n";
                                        }

                                        Tipo.Tip = Tipo.Type.DOUBLE;
                                    }
                                    else
                                    {
                                        errores.AddLast(new Error("Semántico", "El argumento contine errores.", Linea, Columna));
                                    }
                                }
                                else
                                {
                                    errores.AddLast(new Error("Semántico", "Error en el argumento.", Linea, Columna));
                                }
                            }
                        }
                        else
                        {
                            errores.AddLast(new Error("Semántico", "La funcion float necesita argumentos.", Linea, Columna));
                        }
                        break;
                    case "Int":
                        if (Parametros != null)
                        {
                            if (Parametros.Count() > 0)
                            {
                                Expresion valor = Parametros.ElementAt(0);
                                Result rsValor = valor.GetC3D(e, funcion, ciclo, isObjeto, errores);

                                if (rsValor != null)
                                {
                                    if (!valor.GetTipo().IsIndefinido())
                                    {

                                        result.Codigo += rsValor.Codigo;

                                        if (!valor.GetTipo().IsString())
                                        {
                                            result.Valor = NuevoTemporal();
                                            result.Codigo += result.Valor + " = " + rsValor.Valor + ";\n";
                                        }
                                        else
                                        {
                                           
                                            result.EtiquetaV = NuevaEtiqueta();
                                            result.EtiquetaF = NuevaEtiqueta();
                                            string etqCiclo = NuevaEtiqueta();
                                            string tmpCiclo = NuevoTemporal();

                                            result.Codigo += "heap[H] = 0;\n";
                                            result.Codigo += "H = H + 1;\n";

                                            result.Codigo += tmpCiclo + " = heap[" + rsValor.Valor + "];\n";
                                            result.Codigo += etqCiclo + ":\n";
                                            result.Codigo += "if (" + tmpCiclo + " == 0) goto " + result.EtiquetaV + ";\n";
                                            result.Codigo += "goto " + result.EtiquetaF + ";\n";
                                            result.Codigo += result.EtiquetaF + ":\n";
                                            result.Codigo += "heap[H] = " + tmpCiclo + ";\n";
                                            result.Codigo += "H = H + 1;\n";
                                            result.Codigo += rsValor.Valor + " = " + rsValor.Valor + " + 1;\n";
                                            result.Codigo += tmpCiclo + " = heap[" + rsValor.Valor + "];\n";
                                            result.Codigo += "goto " + etqCiclo + ";\n";
                                            result.Codigo += result.EtiquetaV + ":\n";

                                            result.EtiquetaV = NuevaEtiqueta();
                                            result.EtiquetaF = NuevaEtiqueta();
                                            etqCiclo = NuevaEtiqueta();
                                            tmpCiclo = NuevoTemporal();
                                            string ptr = NuevoTemporal();
                                            string factor = NuevoTemporal();
                                            result.Valor = NuevoTemporal();

                                            result.Codigo += factor + " = 1;\n";
                                            result.Codigo += result.Valor + " = 0;\n";
                                            result.Codigo += ptr + " = H - 1;\n";
                                            result.Codigo += tmpCiclo + " = heap[" + ptr + "];\n";
                                            result.Codigo += etqCiclo + ":\n";
                                            result.Codigo += "if (" + tmpCiclo + " == 0) goto " + result.EtiquetaV + ";\n";
                                            result.Codigo += "goto " + result.EtiquetaF + ";\n";
                                            result.Codigo += result.EtiquetaF + ":\n";

                                            string tmp = NuevoTemporal();
                                            result.Codigo += tmp + " = " + tmpCiclo + " - 48;\n";

                                            result.Codigo += "if (" + tmp + " < 0) goto " + result.EtiquetaV + ";\n";
                                            result.Codigo += "if (" + tmp + " > 9) goto " + result.EtiquetaV + ";\n";

                                            string tmp2 = NuevoTemporal();
                                            result.Codigo += tmp2 + " = " + tmp + " * " + factor + ";\n";
                                            result.Codigo += result.Valor + " = " + result.Valor + " + " + tmp2 + ";\n";
                                            result.Codigo += factor + " = " + factor + " * 10;\n";

                                            result.Codigo += ptr + " = " + ptr + " - 1;\n";
                                            result.Codigo += tmpCiclo + " = heap[" + ptr + "];\n";
                                            result.Codigo += "goto " + etqCiclo + ";\n";
                                            result.Codigo += result.EtiquetaV + ":\n";

                                        }

                                        Tipo.Tip = Tipo.Type.INT;
                                    }
                                    else
                                    {
                                        errores.AddLast(new Error("Semántico", "El argumento contine errores.", Linea, Columna));
                                    }
                                }
                                else
                                {
                                    errores.AddLast(new Error("Semántico", "Error en el argumento.", Linea, Columna));
                                }
                            }
                        }
                        else
                        {
                            errores.AddLast(new Error("Semántico", "La funcion Int necesita argumentos.", Linea, Columna));
                        }
                        break;
                    case "str":
                        if (Parametros != null)
                        {
                            if (Parametros.Count() > 0)
                            {
                                Expresion valor = Parametros.ElementAt(0);
                                Result rsValor = valor.GetC3D(e, funcion, ciclo, isObjeto, errores);

                                if (rsValor != null)
                                {
                                    if (!valor.GetTipo().IsIndefinido())
                                    {

                                        result.Codigo += rsValor.Codigo;

                                        if (!valor.GetTipo().IsString())
                                            ConvertirString(valor, rsValor, result);


                                        result.Valor = rsValor.Valor;

                                        Tipo.Tip = Tipo.Type.STRING;
                                    }
                                    else
                                    {
                                        errores.AddLast(new Error("Semántico", "El argumento contine errores.", Linea, Columna));
                                    }
                                }
                                else
                                {
                                    errores.AddLast(new Error("Semántico", "Error en el argumento.", Linea, Columna));
                                }
                            }
                        }
                        else
                        {
                            errores.AddLast(new Error("Semántico", "La funcion str necesita argumentos.", Linea, Columna));
                        }
                        break;
                    case "type":
                        if (Parametros != null)
                        {
                            if (Parametros.Count() > 0)
                            {
                                Expresion valor = Parametros.ElementAt(0);
                                Result rsValor = valor.GetC3D(e, funcion, ciclo, isObjeto, errores);

                                if (rsValor != null)
                                {
                                    if (valor.GetTipo().IsObject())
                                    {
                                        string objeto = valor.GetTipo().Objeto;

                                        result.Valor = NuevoTemporal();
                                        result.Codigo += result.Valor + " = H;\n";

                                        foreach (char c in objeto)
                                        {
                                            result.Codigo += "heap[H] = " + (int)c + ";\n";
                                            result.Codigo += "H = H + 1;\n";

                                        }
                                        result.Codigo += "heap[H] = 0;\n";
                                        result.Codigo += "H = H + 1;\n";

                                        Tipo.Tip = Tipo.Type.STRING;
                                    }
                                    else
                                    {
                                        errores.AddLast(new Error("Semántico", "El argumento no es un objeto.", Linea, Columna));
                                    }
                                }
                                else
                                {
                                    errores.AddLast(new Error("Semántico", "Error en el argumento.", Linea, Columna));
                                }
                            }
                        }
                        else
                        {
                            errores.AddLast(new Error("Semántico", "La funcion type necesita argumentos.", Linea, Columna));
                        }
                        break;
                    case "len":
                        if (Parametros != null)
                        {
                            if (Parametros.Count() > 0)
                            {
                                Expresion valor = Parametros.ElementAt(0);
                                Result rsValor = valor.GetC3D(e, funcion, ciclo, isObjeto, errores);

                                if (rsValor != null)
                                {
                                    if (!valor.GetTipo().IsIndefinido())
                                    {
                                        if (valor.GetTipo().IsList())
                                        {
                                            result.Codigo += rsValor.Codigo;

                                            string ptr = NuevoTemporal();
                                            result.Codigo += ptr + " = " + rsValor.Valor + ";\n";

                                            result.EtiquetaF = NuevaEtiqueta();
                                            result.EtiquetaV = NuevaEtiqueta();
                                            string etqCiclo = NuevaEtiqueta();

                                            result.Valor = NuevoTemporal();
                                            result.Codigo += result.Valor + " = 0;\n";

                                            result.Codigo += etqCiclo + ":\n";
                                            result.Codigo += "ifFalse (" + ptr + " >= 0 ) goto " + result.EtiquetaF + ";\n";
                                            result.Codigo += "goto " + result.EtiquetaV + ";\n";
                                            result.Codigo += result.EtiquetaV + ":\n";
                                            result.Codigo += result.Valor + " = " + result.Valor + " + 1;\n";
                                            result.Codigo += ptr + " = " + ptr + " + 1;\n";
                                            result.Codigo += ptr + " = heap[" + ptr + "];\n";
                                            result.Codigo += "goto " + etqCiclo + ";\n";
                                            result.Codigo += result.EtiquetaF + ":\n";
                                        }
                                        else if (valor.GetTipo().IsString())
                                        {
                                            result.Codigo += rsValor.Codigo;

                                            result.EtiquetaV = NuevaEtiqueta();
                                            result.EtiquetaF = NuevaEtiqueta();
                                            string etqCiclo = NuevaEtiqueta();
                                            string tmpCiclo = NuevoTemporal();

                                            result.Valor = NuevoTemporal();
                                            result.Codigo += result.Valor + " = 0;\n";

                                            result.Codigo += etqCiclo + ":\n";
                                            result.Codigo += tmpCiclo + " = heap[" + rsValor.Valor + "];\n";
                                            result.Codigo += "if (" + tmpCiclo + " == 0) goto " + result.EtiquetaV + ";\n";
                                            result.Codigo += "goto " + result.EtiquetaF + ";\n";
                                            result.Codigo += result.EtiquetaF + ":\n";
                                            result.Codigo += rsValor.Valor + " = " + rsValor.Valor + " + 1;\n";
                                            result.Codigo += result.Valor + " = " + result.Valor + " + 1;\n";
                                            result.Codigo += "goto " + etqCiclo + ";\n";
                                            result.Codigo += result.EtiquetaV + ":\n";
                                        }
                                        else
                                        {
                                            result.Valor = NuevoTemporal();
                                            result.Codigo = result.Valor + " = 1;";
                                        }
                                        Tipo.Tip = Tipo.Type.INT;
                                    }
                                    else
                                    {
                                        errores.AddLast(new Error("Semántico", "El argumento contine errores.", Linea, Columna));
                                    }
                                }
                                else
                                {
                                    errores.AddLast(new Error("Semántico", "Error en el argumento.", Linea, Columna));
                                }
                            }
                        }
                        else
                        {
                            errores.AddLast(new Error("Semántico", "La funcion len necesita argumentos.", Linea, Columna));
                        }
                        break;
                    case "range":
                        if (Parametros != null)
                        {
                            if (Parametros.Count() > 0)
                            {
                                if (Parametros.Count() == 1)
                                {
                                    Expresion valor = Parametros.ElementAt(0);
                                    Result rsValor = valor.GetC3D(e, funcion, ciclo, isObjeto, errores);

                                    if (rsValor != null)
                                    {
                                        if (valor.GetTipo().IsInt())
                                        {
                                            result.Codigo += rsValor.Codigo;

                                            result.Valor = NuevoTemporal();
                                            result.Codigo += result.Valor + " = H;\n";

                                            string contador = NuevoTemporal();

                                            result.EtiquetaV = NuevaEtiqueta();
                                            result.EtiquetaF = NuevaEtiqueta();
                                            string etqCiclo = NuevaEtiqueta();

                                            string tmp = NuevoTemporal();
                                            string ptr = NuevoTemporal();

                                            result.Codigo += contador + " = 0;\n";

                                            result.Codigo += "ifFalse (" + rsValor.Valor + " <= 0) goto " + etqCiclo + ";\n";
                                            result.Codigo += "heap[" + result.Valor + "] = 0;\n";
                                            result.Codigo += "H = H + 1;\n";
                                            result.Codigo += ptr + " = H;\n";
                                            result.Codigo += "H = H + 1;\n";

                                            result.Codigo += "goto " + result.EtiquetaV + ";\n";

                                            result.Codigo += etqCiclo + ":\n";
                                            result.Codigo += "if (" + contador + " == " + rsValor.Valor + ") goto " + result.EtiquetaV + ";\n";
                                            result.Codigo += "goto " + result.EtiquetaF + ";\n";
                                            result.Codigo += result.EtiquetaF + ":\n";

                                            result.Codigo += tmp + " = H;\n";
                                            result.Codigo += "H = H + 1;\n";
                                            result.Codigo += "heap[" + tmp + "] = " + contador + ";\n";

                                            result.Codigo += ptr + " = H;\n";
                                            result.Codigo += "H = H + 1;\n";
                                            result.Codigo += "heap[" + ptr + "] = H;\n";

                                            result.Codigo += contador + " = " + contador + " + 1;\n";
                                            result.Codigo += "goto " + etqCiclo + ";\n";
                                            result.Codigo += result.EtiquetaV + ":\n";

                                            result.Codigo += ptr + " = H - 1;\n";
                                            result.Codigo += "heap[" + ptr + "] = 0 - 1;\n";


                                            Tipo.Tip = Tipo.Type.LIST;
                                            Tipo.SubTip = Tipo.Type.INT;
                                        }
                                        else
                                        {
                                            errores.AddLast(new Error("Semántico", "El argumento debe ser entero.", Linea, Columna));
                                        }
                                    }
                                }
                                else if (Parametros.Count() == 2)
                                {
                                    Expresion valor = Parametros.ElementAt(0);
                                    Result rsValor = valor.GetC3D(e, funcion, ciclo, isObjeto, errores);

                                    Expresion valor2 = Parametros.ElementAt(1);
                                    Result rsValor2 = valor2.GetC3D(e, funcion, ciclo, isObjeto, errores);

                                    if (rsValor != null && rsValor2 != null)
                                    {
                                        if (valor.GetTipo().IsInt() && valor2.GetTipo().IsInt())
                                        {
                                            result.Codigo = rsValor.Codigo;
                                            result.Codigo += rsValor2.Codigo;

                                            result.Valor = NuevoTemporal();
                                            result.Codigo += result.Valor + " = H;\n";

                                            string contador = NuevoTemporal();

                                            result.EtiquetaV = NuevaEtiqueta();
                                            result.EtiquetaF = NuevaEtiqueta();
                                            string etqCiclo = NuevaEtiqueta();

                                            string tmp = NuevoTemporal();
                                            string ptr = NuevoTemporal();

                                            result.Codigo += contador + " = " + rsValor.Valor + ";\n";

                                            result.Codigo += etqCiclo + ":\n";
                                            result.Codigo += "if (" + contador + " == " + rsValor2.Valor + ") goto " + result.EtiquetaV + ";\n";
                                            result.Codigo += "goto " + result.EtiquetaF + ";\n";
                                            result.Codigo += result.EtiquetaF + ":\n";

                                            result.Codigo += tmp + " = H;\n";
                                            result.Codigo += "H = H + 1;\n";
                                            result.Codigo += "heap[" + tmp + "] = " + contador + ";\n";

                                            result.Codigo += ptr + " = H;\n";
                                            result.Codigo += "H = H + 1;\n";
                                            result.Codigo += "heap[" + ptr + "] = H;\n";

                                            result.Codigo += contador + " = " + contador + " + 1;\n";
                                            result.Codigo += "goto " + etqCiclo + ";\n";
                                            result.Codigo += result.EtiquetaV + ":\n";

                                            result.Codigo += ptr + " = H - 1;\n";
                                            result.Codigo += "heap[" + ptr + "] = 0 - 1;\n";


                                            Tipo.Tip = Tipo.Type.LIST;
                                            Tipo.SubTip = Tipo.Type.INT;
                                        }
                                        else
                                        {
                                            errores.AddLast(new Error("Semántico", "El argumento debe ser entero.", Linea, Columna));
                                        }
                                    }
                                }
                                else if (Parametros.Count() == 3)
                                {
                                    Expresion valor = Parametros.ElementAt(0);
                                    Result rsValor = valor.GetC3D(e, funcion, ciclo, isObjeto, errores);

                                    Expresion valor2 = Parametros.ElementAt(1);
                                    Result rsValor2 = valor2.GetC3D(e, funcion, ciclo, isObjeto, errores);

                                    Expresion valor3 = Parametros.ElementAt(2);
                                    Result rsValor3 = valor3.GetC3D(e, funcion, ciclo, isObjeto, errores);

                                    if (rsValor != null && rsValor2 != null && rsValor3 != null)
                                    {
                                        if (valor.GetTipo().IsInt() && valor2.GetTipo().IsInt() && valor3.GetTipo().IsInt())
                                        {
                                            result.Codigo = rsValor.Codigo;
                                            result.Codigo += rsValor2.Codigo;
                                            result.Codigo += rsValor3.Codigo;

                                            result.Valor = NuevoTemporal();
                                            result.Codigo += result.Valor + " = H;\n";

                                            string contador = NuevoTemporal();

                                            result.EtiquetaV = NuevaEtiqueta();
                                            result.EtiquetaF = NuevaEtiqueta();
                                            string etqCiclo = NuevaEtiqueta();

                                            string tmp = NuevoTemporal();
                                            string ptr = NuevoTemporal();

                                            result.Codigo += contador + " = " + rsValor.Valor + ";\n";

                                            result.Codigo += etqCiclo + ":\n";
                                            result.Codigo += "if (" + contador + " == " + rsValor2.Valor + ") goto " + result.EtiquetaV + ";\n";
                                            result.Codigo += "goto " + result.EtiquetaF + ";\n";
                                            result.Codigo += result.EtiquetaF + ":\n";

                                            result.Codigo += tmp + " = H;\n";
                                            result.Codigo += "H = H + 1;\n";
                                            result.Codigo += "heap[" + tmp + "] = " + contador + ";\n";

                                            result.Codigo += ptr + " = H;\n";
                                            result.Codigo += "H = H + 1;\n";
                                            result.Codigo += "heap[" + ptr + "] = H;\n";

                                            result.Codigo += contador + " = " + contador + " + " + rsValor3.Valor + ";\n";
                                            result.Codigo += "goto " + etqCiclo + ";\n";
                                            result.Codigo += result.EtiquetaV + ":\n";

                                            result.Codigo += ptr + " = H - 1;\n";
                                            result.Codigo += "heap[" + ptr + "] = 0 - 1;\n";


                                            Tipo.Tip = Tipo.Type.LIST;
                                            Tipo.SubTip = Tipo.Type.INT;
                                        }
                                        else
                                        {
                                            errores.AddLast(new Error("Semántico", "El argumento debe ser entero.", Linea, Columna));
                                        }
                                    }
                                }
                                else
                                {
                                    errores.AddLast(new Error("Semántico", "Error en el argumento.", Linea, Columna));
                                }
                            }
                        }
                        else
                        {
                            errores.AddLast(new Error("Semántico", "La funcion range necesita argumentos.", Linea, Columna));
                        }
                        break;
                    default:
                        string firma;
                        firma = Id;

                        if (Parametros != null)
                        {
                            LinkedList<Result> rsParametros = new LinkedList<Result>();
                            foreach (Expresion parametro in Parametros)
                            {
                                Result rsParametro = parametro.GetC3D(e, funcion, ciclo, isObjeto, errores);
                                if (rsParametro != null)
                                {
                                    if (!parametro.GetTipo().IsIndefinido())
                                    {
                                        rsParametros.AddLast(rsParametro);
                                        firma += "_" + parametro.GetTipo().ToString();
                                        continue;
                                    }
                                }

                                errores.AddLast(new Error("Semántico", "El parámetro contiene error.", Linea, Columna));

                                return null;
                            }

                            Sim metodo = e.GetMetodo(firma);

                            if (metodo != null)
                            {
                                Tipo = metodo.Tipo;

                                int pos = 2; /*inicia en dos por return y self*/

                                foreach (Result rsParametro in rsParametros)
                                {
                                    string ptrCambio = NuevoTemporal();
                                    string tmp = NuevoTemporal();
                                    result.Codigo += ptrCambio + " = P + " + e.Size + ";\n"; //Cambio simulado
                                    result.Codigo += tmp + " = " + ptrCambio + " + " + pos++ + ";\n";

                                    result.Codigo += rsParametro.Codigo;

                                    result.Codigo += "stack[" + tmp + "] = " + rsParametro.Valor + ";\n";
                                }

                                result.Codigo += "P = P + " + e.Size + ";\n";
                                result.Codigo += "call " + metodo.Ambito + "_" + firma + ";\n";

                                if (ObtenerReturn)/*Si se espera valor*/
                                {
                                    string ptrReturn = NuevoTemporal();
                                    result.Valor = NuevoTemporal();

                                    result.Codigo += ptrReturn + " = P + 0;\n";
                                    result.Codigo += result.Valor + " = stack[" + ptrReturn + "];\n";

                                }
                                result.Codigo += "P = P - " + e.Size + ";\n";

                            }
                            else
                            {
                                /*Buscar si es posible clase*/
                                errores.AddLast(new Error("Semántico", "La función: " + Id + " no está declarada.", Linea, Columna));
                                return null;
                            }
                        }
                        else
                        {
                            Sim metodo = e.GetMetodo(firma);

                            if (metodo != null)
                            {
                                Tipo = metodo.Tipo;
                                result.Codigo += "P = P + " + e.Size + ";\n";
                                result.Codigo += "call " + metodo.Ambito + "_" + firma + ";\n";

                                if (ObtenerReturn)/*Si se espera valor*/
                                {
                                    string ptrReturn = NuevoTemporal();
                                    result.Valor = NuevoTemporal();

                                    result.Codigo += ptrReturn + " = P + 0;\n";
                                    result.Codigo += result.Valor + " = stack[" + ptrReturn + "];\n";

                                }

                                result.Codigo += "P = P - " + e.Size + ";\n";
                            }
                            else
                            {
                                /*Buscar si es posible clase*/
                                Sim clase = e.GetClase(Id);

                                if (clase != null)
                                {
                                    if (ObtenerReturn)
                                    {
                                        if (PtrVariable != null)
                                        {
                                            string ptrCambio = NuevoTemporal();
                                            string tmp = NuevoTemporal();
                                            string ptrVariable = NuevoTemporal();
                                            result.Valor = NuevoTemporal();

                                            result.Codigo += ptrCambio + " = P + " + e.Size + ";\n"; //Cambio simulado
                                            result.Codigo += tmp + " = " + ptrCambio + " + 1;\n"; //Posicion slef
                                            result.Codigo += ptrVariable + " = P + " + PtrVariable + " ;\n";
                                            result.Codigo += "stack[" + tmp + "] = " + ptrVariable + ";\n";
                                            result.Codigo += result.Valor + " = H;\n";

                                            result.Codigo += "P = P + " + e.Size + ";\n";
                                            result.Codigo += "call " + Id + "_" + Id + ";\n";
                                            result.Codigo += "P = P - " + e.Size + ";\n";

                                            Tipo = new Tipo(clase.Id);
                                        }
                                    }
                                }
                                else
                                {
                                    errores.AddLast(new Error("Semántico", "La función: " + Id + " no está declarada.", Linea, Columna));
                                    return null;
                                }
                            }
                        }
                        break;
                }

            }
            else
            {
                /*acceso a objetos o atributos*/
                if (Expresion is Referencia refExpresion)
                {
                    refExpresion.Acceso = false;

                    Result rsExpresion = refExpresion.GetC3D(e, funcion, ciclo, isObjeto, errores);

                    if (rsExpresion != null)
                    {
                        if (refExpresion.Simbolo != null)
                        {
                            switch (refExpresion.Id)
                            {
                                case "append":
                                    if (Parametros != null)
                                    {
                                        if (Parametros.Count() > 0)
                                        {
                                            if (refExpresion.Simbolo.Tipo.IsList())
                                            {
                                                Expresion valor = Parametros.ElementAt(0);
                                                Result rsValor = valor.GetC3D(e, funcion, ciclo, isObjeto, errores);

                                                if (rsValor != null)
                                                {
                                                    if (!valor.GetTipo().IsIndefinido())
                                                    {
                                                        if (valor.GetTipo().Tip == refExpresion.Simbolo.Tipo.SubTip)
                                                        {
                                                            result.Codigo = rsValor.Codigo;

                                                            string ptrStack = NuevoTemporal();
                                                            result.Codigo += ptrStack + " = P + " + refExpresion.Simbolo.Pos + ";\n";

                                                            string ptrHeap = NuevoTemporal();
                                                            result.Codigo += ptrHeap + " = stack[" + ptrStack + "];\n";

                                                            string ptr = NuevoTemporal();
                                                            result.Codigo += ptr + " = stack[" + ptrStack + "];\n";

                                                            result.EtiquetaF = NuevaEtiqueta();
                                                            result.EtiquetaV = NuevaEtiqueta();
                                                            string etqCiclo = NuevaEtiqueta();

                                                            result.Valor = NuevoTemporal();
                                                            result.Codigo += result.Valor + " = heap[" + ptr + "];\n";

                                                            result.Codigo += etqCiclo + ":\n";
                                                            result.Codigo += "ifFalse (" + ptr + " >= 0 ) goto " + result.EtiquetaF + ";\n";
                                                            result.Codigo += "goto " + result.EtiquetaV + ";\n";
                                                            result.Codigo += result.EtiquetaV + ":\n";

                                                            result.Codigo += result.Valor + " = heap[" + ptr + "];\n";
                                                            result.Codigo += ptrHeap + " = " + ptr + " + 1;\n";
                                                            result.Codigo += ptr + " = heap[" + ptrHeap + "];\n";

                                                            result.Codigo += "goto " + etqCiclo + ";\n";
                                                            result.Codigo += result.EtiquetaF + ":\n";

                                                            result.Codigo += "heap[" + ptrHeap + "] = H;\n";
                                                            result.Codigo += "heap[H] = " + rsValor.Valor + ";\n";
                                                            result.Codigo += "H = H + 1;\n";
                                                            result.Codigo += "heap[H] = 0 - 1;\n";
                                                            result.Codigo += "H = H + 1;\n";

                                                        }
                                                        else
                                                        {
                                                            errores.AddLast(new Error("Semántico", "El valor a agregar no es del mismo tipo.", Linea, Columna));
                                                        }
                                                    }
                                                    else
                                                    {
                                                        errores.AddLast(new Error("Semántico", "Error en valor.", Linea, Columna));
                                                    }
                                                }
                                                else
                                                {
                                                    errores.AddLast(new Error("Semántico", "Error en valor.", Linea, Columna));
                                                }
                                            }
                                            else
                                            {
                                                errores.AddLast(new Error("Semántico", "La variable no es una lista.", Linea, Columna));
                                            }
                                        }
                                    }
                                    break;
                                default:
                                    Sim clase = e.GetClase(refExpresion.Simbolo.Tipo.Objeto);
                                    if (clase != null)
                                    {
                                        //PrintTabla t = new PrintTabla(0, 0);
                                        //t.GetC3D(clase.Entorno, false, false, false, errores);

                                        string firma;
                                        firma = refExpresion.Id;

                                        if (Parametros != null)
                                        {
                                            /*con parametros*/
                                            LinkedList<Result> rsParametros = new LinkedList<Result>();
                                            foreach (Expresion parametro in Parametros)
                                            {
                                                Result rsParametro = parametro.GetC3D(e, funcion, ciclo, isObjeto, errores);
                                                if (rsParametro != null)
                                                {
                                                    if (!parametro.GetTipo().IsIndefinido())
                                                    {
                                                        rsParametros.AddLast(rsParametro);
                                                        firma += "_" + parametro.GetTipo().ToString();
                                                        continue;
                                                    }
                                                }
                                                errores.AddLast(new Error("Semántico", "El parámetro contiene error.", Linea, Columna));
                                                return null;
                                            }

                                            Sim metodo = clase.Entorno.GetMetodo(firma);

                                            if (metodo != null)
                                            {
                                                Tipo = metodo.Tipo;

                                                /*Paso de parametro self(h)*/
                                                string ptrCambioSelf = NuevoTemporal();
                                                string tmpSelf = NuevoTemporal();
                                                result.Codigo += ptrCambioSelf + " = P + " + e.Size + ";\n"; //Cambio simulado
                                                result.Codigo += tmpSelf + " = " + ptrCambioSelf + " + " + 1 + ";\n";

                                                string ptrStackSelf = NuevoTemporal();
                                                string valorSelf = NuevoTemporal();
                                                result.Codigo += ptrStackSelf + " = P + " + refExpresion.Simbolo.Pos + ";\n";
                                                result.Codigo += valorSelf + " = stack[" + ptrStackSelf + "];\n";
                                                result.Codigo += "stack[" + tmpSelf + "] = " + valorSelf + ";\n";

                                                int pos = 2; /*inicia en dos por return y self*/
                                                foreach (Result rsParametro in rsParametros)
                                                {
                                                    string ptrCambio = NuevoTemporal();
                                                    string tmp = NuevoTemporal();
                                                    result.Codigo += ptrCambio + " = P + " + e.Size + ";\n"; //Cambio simulado
                                                    result.Codigo += tmp + " = " + ptrCambio + " + " + pos++ + ";\n";

                                                    result.Codigo += rsParametro.Codigo;

                                                    result.Codigo += "stack[" + tmp + "] = " + rsParametro.Valor + ";\n";
                                                }

                                                result.Codigo += "P = P + " + e.Size + ";\n";
                                                result.Codigo += "call " + metodo.Ambito + "_" + firma + ";\n";

                                                if (ObtenerReturn)/*Si se espera valor*/
                                                {
                                                    string ptrReturn = NuevoTemporal();
                                                    result.Valor = NuevoTemporal();

                                                    result.Codigo += ptrReturn + " = P + 0;\n";
                                                    result.Codigo += result.Valor + " = stack[" + ptrReturn + "];\n";

                                                }
                                                result.Codigo += "P = P - " + e.Size + ";\n";

                                            }
                                            else
                                            {
                                                errores.AddLast(new Error("Semántico", "La función: " + refExpresion.Id + " no está declarada.", Linea, Columna));
                                                return null;
                                            }

                                        }
                                        else
                                        {
                                            Sim metodo = clase.Entorno.GetMetodo(firma);

                                            if (metodo != null)
                                            {
                                                Tipo = metodo.Tipo;

                                                /*Paso de parametro self(h)*/
                                                string ptrCambioSelf = NuevoTemporal();
                                                string tmpSelf = NuevoTemporal();
                                                result.Codigo += ptrCambioSelf + " = P + " + e.Size + ";\n"; //Cambio simulado
                                                result.Codigo += tmpSelf + " = " + ptrCambioSelf + " + " + 1 + ";\n";

                                                string ptrStackSelf = NuevoTemporal();
                                                string valorSelf = NuevoTemporal();
                                                result.Codigo += ptrStackSelf + " = P + " + refExpresion.Simbolo.Pos + ";\n";
                                                result.Codigo += valorSelf + " = stack[" + ptrStackSelf + "];\n";
                                                result.Codigo += "stack[" + tmpSelf + "] = " + valorSelf + ";\n";

                                                result.Codigo += "P = P + " + e.Size + ";\n";
                                                result.Codigo += "call " + metodo.Ambito + "_" + firma + ";\n";

                                                if (ObtenerReturn)/*Si se espera valor*/
                                                {
                                                    string ptrReturn = NuevoTemporal();
                                                    result.Valor = NuevoTemporal();

                                                    result.Codigo += ptrReturn + " = P + 0;\n";
                                                    result.Codigo += result.Valor + " = stack[" + ptrReturn + "];\n";

                                                }

                                                result.Codigo += "P = P - " + e.Size + ";\n";
                                            }
                                            else
                                            {
                                                errores.AddLast(new Error("Semántico", "La función: " + refExpresion.Id + " no está declarada.", Linea, Columna));
                                                return null;
                                            }
                                        }
                                    }
                                    break;
                            }      
                        }
                    }

                }
                else
                {
                    /*Revisar*/
                }
                
            }

            return result;
        }

        public override Tipo GetTipo()
        {
            return Tipo;
        }

        private void ConvertirString(Expresion op, Result rsOp, Result result)
        {
            if (op.GetTipo().IsInt())
            {
                rsOp.EtiquetaV = NuevaEtiqueta();
                rsOp.EtiquetaF = NuevaEtiqueta();
                string etqCiclo = NuevaEtiqueta();
                string tmpCiclo;

                if (op is Literal)
                {
                    string tmpOp2 = NuevoTemporal();
                    result.Codigo += tmpOp2 + " = " + rsOp.Valor + ";\n";
                    rsOp.Valor = tmpOp2;
                }

                /*verificar si el número es 0*/
                string cero = NuevoTemporal();
                result.Codigo += cero + " = 0;\n";
                result.Codigo += "if (" + rsOp.Valor + " != 0) goto " + rsOp.EtiquetaV + ";\n";
                result.Codigo += "goto " + rsOp.EtiquetaF + ";\n";
                result.Codigo += rsOp.EtiquetaF + ":\n";
                result.Codigo += cero + " = 1;\n";
                result.Codigo += rsOp.EtiquetaV + ":\n";

                rsOp.EtiquetaV = NuevaEtiqueta();
                rsOp.EtiquetaF = NuevaEtiqueta();


                /*Verificar si el número es negativo*/
                string negativo = NuevoTemporal();
                string factor = NuevoTemporal();
                result.Codigo += negativo + " = 0;\n";
                result.Codigo += "if (" + rsOp.Valor + " >= 0) goto " + rsOp.EtiquetaV + ";\n";
                result.Codigo += "goto " + rsOp.EtiquetaF + ";\n";
                result.Codigo += rsOp.EtiquetaF + ":\n";
                result.Codigo += negativo + " = 1;\n";
                result.Codigo += factor + " = 0-1;\n";
                result.Codigo += rsOp.Valor + " = " + rsOp.Valor + " * " + factor + ";\n";
                result.Codigo += rsOp.EtiquetaV + ":\n";

                rsOp.EtiquetaV = NuevaEtiqueta();
                rsOp.EtiquetaF = NuevaEtiqueta();

                result.Codigo += "heap[H] = 0;\n";
                result.Codigo += "H = H + 1;\n";

                tmpCiclo = NuevoTemporal();
                result.Codigo += tmpCiclo + " = " + rsOp.Valor + " % 10;\n";

                result.Codigo += etqCiclo + ":\n";
                result.Codigo += "if (" + rsOp.Valor + " < 1) goto " + rsOp.EtiquetaV + ";\n";
                result.Codigo += "goto " + rsOp.EtiquetaF + ";\n";
                result.Codigo += rsOp.EtiquetaF + ":\n";
                result.Codigo += "heap[H] = " + tmpCiclo + " + 48;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += rsOp.Valor + " = " + rsOp.Valor + " / 10;\n";
                result.Codigo += tmpCiclo + " = " + rsOp.Valor + " % 10;\n";
                result.Codigo += "goto " + etqCiclo + ";\n";
                result.Codigo += rsOp.EtiquetaV + ":\n";

                /*Coloco el (-) de negativo*/
                rsOp.EtiquetaV = NuevaEtiqueta();
                rsOp.EtiquetaF = NuevaEtiqueta();
                result.Codigo += "if (" + negativo + " == 0) goto " + rsOp.EtiquetaV + ";\n";
                result.Codigo += "goto " + rsOp.EtiquetaF + ";\n";
                result.Codigo += rsOp.EtiquetaF + ":\n";
                result.Codigo += "heap[H] = 45;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += rsOp.EtiquetaV + ":\n";

                /*Coloco el (0) si es cero*/
                rsOp.EtiquetaV = NuevaEtiqueta();
                rsOp.EtiquetaF = NuevaEtiqueta();
                result.Codigo += "if (" + cero + " == 0) goto " + rsOp.EtiquetaV + ";\n";
                result.Codigo += "goto " + rsOp.EtiquetaF + ";\n";
                result.Codigo += rsOp.EtiquetaF + ":\n";
                result.Codigo += "heap[H] = 48;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += rsOp.EtiquetaV + ":\n";

                string tmp = NuevoTemporal();
                result.Codigo += tmp + " = H - 1;\n";

                rsOp.EtiquetaV = NuevaEtiqueta();
                rsOp.EtiquetaF = NuevaEtiqueta();
                etqCiclo = NuevaEtiqueta();

                rsOp.Valor = NuevoTemporal();
                result.Codigo += rsOp.Valor + " = H;\n";

                tmpCiclo = NuevoTemporal();
                result.Codigo += tmpCiclo + " = heap[" + tmp + "];\n";
                result.Codigo += etqCiclo + ":\n";
                result.Codigo += "if (" + tmpCiclo + " == 0) goto " + rsOp.EtiquetaV + ";\n";
                result.Codigo += "goto " + rsOp.EtiquetaF + ";\n";
                result.Codigo += rsOp.EtiquetaF + ":\n";
                result.Codigo += "heap[H] = " + tmpCiclo + ";\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += tmp + " = " + tmp + " - 1;\n";
                result.Codigo += tmpCiclo + " = heap[" + tmp + "];\n";
                result.Codigo += "goto " + etqCiclo + ";\n";
                result.Codigo += rsOp.EtiquetaV + ":\n";
                result.Codigo += "heap[H] = 0;\n";
                result.Codigo += "H = H + 1;\n";

            }
            else if (op.GetTipo().IsDouble())
            {

                rsOp.EtiquetaV = NuevaEtiqueta();
                rsOp.EtiquetaF = NuevaEtiqueta();
                string etqCiclo = NuevaEtiqueta();
                string tmpCiclo;

                if (op is Literal)
                {
                    string tmpOp2 = NuevoTemporal();
                    result.Codigo += tmpOp2 + " = " + rsOp.Valor + ";\n";
                    rsOp.Valor = tmpOp2;
                }

                /*Verificar si el número es negativo*/
                string negativo = NuevoTemporal();
                string factor = NuevoTemporal();
                result.Codigo += negativo + " = 0;\n";
                result.Codigo += "if (" + rsOp.Valor + " >= 0) goto " + rsOp.EtiquetaV + ";\n";
                result.Codigo += "goto " + rsOp.EtiquetaF + ";\n";
                result.Codigo += rsOp.EtiquetaF + ":\n";
                result.Codigo += negativo + " = 1;\n";
                result.Codigo += factor + " = 0-1;\n";
                result.Codigo += rsOp.Valor + " = " + rsOp.Valor + " * " + factor + ";\n";
                result.Codigo += rsOp.EtiquetaV + ":\n";
                /***************************************************/

                /*Verificar si es menor que 1*/
                rsOp.EtiquetaV = NuevaEtiqueta();
                rsOp.EtiquetaF = NuevaEtiqueta();
                string menor1 = NuevoTemporal();

                result.Codigo += menor1 + " = 0;\n";
                result.Codigo += "ifFalse (" + rsOp.Valor + " < 1) goto " + rsOp.EtiquetaV + ";\n";
                result.Codigo += "goto " + rsOp.EtiquetaF + ";\n";
                result.Codigo += rsOp.EtiquetaF + ":\n";
                result.Codigo += menor1 + " = 1;\n";
                result.Codigo += rsOp.EtiquetaV + ":\n";
                /*****************************************/

                rsOp.EtiquetaV = NuevaEtiqueta();
                rsOp.EtiquetaF = NuevaEtiqueta();


                string contador = NuevoTemporal();
                result.Codigo += contador + " = 0;\n";
                result.Codigo += rsOp.Valor + " = " + rsOp.Valor + " * 10;\n";

                tmpCiclo = NuevoTemporal();
                result.Codigo += etqCiclo + ":\n";
                result.Codigo += tmpCiclo + " = " + rsOp.Valor + " % 10;\n";
                result.Codigo += "if (" + tmpCiclo + " > 0) goto " + rsOp.EtiquetaV + ";\n";
                result.Codigo += "goto " + rsOp.EtiquetaF + ";\n";
                result.Codigo += rsOp.EtiquetaV + ":\n";
                result.Codigo += contador + " = " + contador + " + 1;\n";
                result.Codigo += rsOp.Valor + " = " + rsOp.Valor + " * 10;\n";
                result.Codigo += "goto " + etqCiclo + ";\n";
                result.Codigo += rsOp.EtiquetaF + ":\n";

                result.Codigo += rsOp.Valor + " = " + rsOp.Valor + " / 10;\n";

                rsOp.EtiquetaV = NuevaEtiqueta();
                rsOp.EtiquetaF = NuevaEtiqueta();
                etqCiclo = NuevaEtiqueta();

                result.Codigo += "heap[H] = 0;\n";
                result.Codigo += "H = H + 1;\n";

                string contador2 = NuevoTemporal();
                string etiqContV = NuevaEtiqueta();
                string etiqContF = NuevaEtiqueta();
                result.Codigo += contador2 + " = 0;\n";
                tmpCiclo = NuevoTemporal();
                result.Codigo += tmpCiclo + " = " + rsOp.Valor + " % 10;\n";

                result.Codigo += etqCiclo + ":\n";
                result.Codigo += "if (" + rsOp.Valor + " < 1) goto " + rsOp.EtiquetaV + ";\n";
                result.Codigo += "goto " + rsOp.EtiquetaF + ";\n";
                result.Codigo += rsOp.EtiquetaF + ":\n";
                result.Codigo += "heap[H] = " + tmpCiclo + " + 48;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += rsOp.Valor + " = " + rsOp.Valor + " / 10;\n";
                result.Codigo += tmpCiclo + " = " + rsOp.Valor + " % 10;\n";

                result.Codigo += contador2 + " = " + contador2 + " + 1;\n";
                result.Codigo += "if (" + contador2 + " == " + contador + ") goto " + etiqContV + ";\n";
                result.Codigo += "goto " + etiqContF + ";\n";
                result.Codigo += etiqContV + ":\n";
                result.Codigo += "heap[H] = 46;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += etiqContF + ":\n";

                result.Codigo += "goto " + etqCiclo + ";\n";
                result.Codigo += rsOp.EtiquetaV + ":\n";


                /*Coloco el 0 antes del punto*/
                rsOp.EtiquetaV = NuevaEtiqueta();
                rsOp.EtiquetaF = NuevaEtiqueta();
                result.Codigo += "if (" + menor1 + " == 0) goto " + rsOp.EtiquetaV + ";\n";
                result.Codigo += "goto " + rsOp.EtiquetaF + ";\n";
                result.Codigo += rsOp.EtiquetaF + ":\n";
                result.Codigo += "heap[H] = 48;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += rsOp.EtiquetaV + ":\n";
                /*******************************************/

                /*Coloco el (-) de negativo*/
                rsOp.EtiquetaV = NuevaEtiqueta();
                rsOp.EtiquetaF = NuevaEtiqueta();
                result.Codigo += "if (" + negativo + " == 0) goto " + rsOp.EtiquetaV + ";\n";
                result.Codigo += "goto " + rsOp.EtiquetaF + ";\n";
                result.Codigo += rsOp.EtiquetaF + ":\n";
                result.Codigo += "heap[H] = 45;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += rsOp.EtiquetaV + ":\n";
                /*************************************************/

                string tmp = NuevoTemporal();
                result.Codigo += tmp + " = H - 1;\n";

                rsOp.EtiquetaV = NuevaEtiqueta();
                rsOp.EtiquetaF = NuevaEtiqueta();
                etqCiclo = NuevaEtiqueta();

                rsOp.Valor = NuevoTemporal();
                result.Codigo += rsOp.Valor + " = H;\n";

                tmpCiclo = NuevoTemporal();
                result.Codigo += tmpCiclo + " = heap[" + tmp + "];\n";
                result.Codigo += etqCiclo + ":\n";
                result.Codigo += "if (" + tmpCiclo + " == 0) goto " + rsOp.EtiquetaV + ";\n";
                result.Codigo += "goto " + rsOp.EtiquetaF + ";\n";
                result.Codigo += rsOp.EtiquetaF + ":\n";
                result.Codigo += "heap[H] = " + tmpCiclo + ";\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += tmp + " = " + tmp + " - 1;\n";
                result.Codigo += tmpCiclo + " = heap[" + tmp + "];\n";
                result.Codigo += "goto " + etqCiclo + ";\n";
                result.Codigo += rsOp.EtiquetaV + ":\n";
                result.Codigo += "heap[H] = 0;\n";
                result.Codigo += "H = H + 1;\n";

            }
            else if (op.GetTipo().IsBoolean())
            {
                rsOp.EtiquetaV = NuevaEtiqueta();
                rsOp.EtiquetaF = NuevaEtiqueta();
                string etqSalida = NuevaEtiqueta();


                string tmp = NuevoTemporal();
                result.Codigo += tmp + " = H;\n";

                result.Codigo += "if (" + rsOp.Valor + " == 0) goto " + rsOp.EtiquetaV + ";\n";
                result.Codigo += "goto " + rsOp.EtiquetaF + ";\n";
                result.Codigo += rsOp.EtiquetaV + ":\n";
                result.Codigo += "heap[H] = 70;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += "heap[H] = 97;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += "heap[H] = 108;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += "heap[H] = 115;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += "heap[H] = 101;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += "goto " + etqSalida + ";\n";
                result.Codigo += rsOp.EtiquetaF + ":\n";
                result.Codigo += "heap[H] = 84;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += "heap[H] = 114;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += "heap[H] = 117;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += "heap[H] = 101;\n";
                result.Codigo += "H = H + 1;\n";
                result.Codigo += etqSalida + ":\n";

                result.Codigo += "heap[H] = 0;\n";
                result.Codigo += "H = H + 1;\n";

                rsOp.Valor = tmp;
            }
        }
    }
}
