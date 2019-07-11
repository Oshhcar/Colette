using Compilador.parser.Colette.ast;
using Compilador.parser.Colette.ast.entorno;
using Compilador.parser.Colette.ast.expresion;
using Compilador.parser.Colette.ast.expresion.operacion;
using Compilador.parser.Colette.ast.instruccion;
using Compilador.parser.Colette.ast.instruccion.ciclos;
using Compilador.parser.Colette.ast.instruccion.condicionales;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Compilador.parser.Colette.ast.expresion.operacion.Operacion;

namespace Compilador.parser.Collete
{
    class AnalizadorColette
    {
        public ParseTree Raiz { get; set; }

        public bool AnalizarEntrada(String entrada)
        {
            GramaticaColette gramatica = new GramaticaColette();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(entrada);
            this.Raiz = arbol;

            if (arbol.Root != null)
                return true;

            return false;
        }

        public Object GenerarArbol(ParseTreeNode raiz)
        {
            string r = raiz.ToString();
            ParseTreeNode[] hijos = null;

            int linea = 0;
            int columna = 0;

            if (raiz.ChildNodes.Count > 0)
            {
                hijos = raiz.ChildNodes.ToArray();
            }

            switch (r)
            {
                case "INICIO":
                    return GenerarArbol(hijos[0]);
                case "INSTRUCCIONES":
                    LinkedList<Nodo> sentencias = new LinkedList<Nodo>();
                    foreach (ParseTreeNode hijo in hijos)
                    {
                        sentencias.AddLast((Nodo)GenerarArbol(hijo));
                    }
                    return new Arbol(sentencias);
                case "INSTRUCCION":
                    return GenerarArbol(hijos[0]);
                case "CLASEDEF":
                    linea = hijos[1].Token.Location.Line + 1;
                    columna = hijos[1].Token.Location.Column + 1;
                    return new Clase(hijos[1].Token.Text,(Bloque)GenerarArbol(hijos[3]), linea, columna);
                case "FUNCDEF":
                    linea = hijos[0].Token.Location.Line + 1;
                    columna = hijos[0].Token.Location.Column + 1;
                    if (hijos.Count() == 8)
                        return new Funcion((Tipo)GenerarArbol(hijos[1]), hijos[2].Token.Text, (LinkedList<Identificador>)GenerarArbol(hijos[4]), (Bloque)GenerarArbol(hijos[7]), linea, columna);
                    else
                        return new Funcion((Tipo)GenerarArbol(hijos[1]), hijos[2].Token.Text, null, (Bloque)GenerarArbol(hijos[6]), linea, columna);
                case "PARAMETER_LIST":
                    if (hijos.Count() == 4)
                    {
                        linea = hijos[3].Token.Location.Line + 1;
                        columna = hijos[3].Token.Location.Column + 1;
                        LinkedList<Identificador> parameter_list = (LinkedList<Identificador>)GenerarArbol(hijos[0]);
                        parameter_list.AddLast(new Identificador(hijos[3].Token.Text, (Tipo)GenerarArbol(hijos[2]), linea, columna));
                        return parameter_list;
                    }
                    else
                    {
                        linea = hijos[1].Token.Location.Line + 1;
                        columna = hijos[1].Token.Location.Column + 1;
                        LinkedList<Identificador> parameter_list = new LinkedList<Identificador>();
                        parameter_list.AddLast(new Identificador(hijos[1].Token.Text, (Tipo)GenerarArbol(hijos[0]), linea, columna));
                        return parameter_list;
                    }
                case "BLOQUE_SUP":
                    return GenerarArbol(hijos[0]);
                case "INSTRUCCIONES_SUP":
                    LinkedList<Nodo> inst_sup = new LinkedList<Nodo>();
                    foreach (ParseTreeNode hijo in hijos)
                    {
                        inst_sup.AddLast((Nodo)GenerarArbol(hijo));
                    }
                    return new Bloque(inst_sup, 0, 0);
                case "INSTRUCCION_SUP":
                    return GenerarArbol(hijos[0]);
                case "BLOQUE":
                    return GenerarArbol(hijos[0]);
                case "SENTENCIAS":
                    LinkedList<Nodo> sent = new LinkedList<Nodo>();
                    foreach (ParseTreeNode hijo in hijos)
                    {
                        sent.AddLast((Nodo)GenerarArbol(hijo));
                    }
                    return new Bloque(sent, 0, 0);
                case "SENTENCIA":
                    return GenerarArbol(hijos[0]);
                case "IMPORT_STMT":
                    linea = hijos[0].Token.Location.Line + 1;
                    columna = hijos[0].Token.Location.Column + 1;
                    return new Import(hijos[1].Token.Text, linea, columna);
                case "TYPE":
                    switch (hijos[0].Term.Name)
                    {
                        case "int":
                            return new Tipo(Tipo.Type.INT);
                        case "double":
                            return new Tipo(Tipo.Type.DOUBLE);
                        case "String":
                        case "string":
                            return new Tipo(Tipo.Type.STRING);
                        case "boolean":
                            return new Tipo(Tipo.Type.BOOLEAN);
                        case "identifier":
                            return new Tipo(hijos[0].Token.Text);
                        case "void":
                            return new Tipo(Tipo.Type.VOID);
                        case "list":
                            return new Tipo(Tipo.Type.LIST);
                        case "dictionary":
                        case "tup":
                        default:
                            return new Tipo(Tipo.Type.INDEFINIDO);
                    }
                case "PRINT":
                    linea = hijos[0].Token.Location.Line + 1;
                    columna = hijos[0].Token.Location.Column + 1;
                    return new Print((Expresion)GenerarArbol(hijos[2]), linea, columna);
                case "PRINTTABLA":
                    linea = hijos[0].Token.Location.Line + 1;
                    columna = hijos[0].Token.Location.Column + 1;
                    return new PrintTabla(linea, columna);
                case "IF_STMT":
                    if (hijos.Count() == 1)
                        return new If((LinkedList<SubIf>)GenerarArbol(hijos[0]), 0, 0);
                    else
                    {
                        linea = hijos[1].Token.Location.Line + 1;
                        columna = hijos[1].Token.Location.Column + 1;
                        LinkedList<SubIf> subifs = (LinkedList<SubIf>)GenerarArbol(hijos[0]);
                        subifs.AddLast(new SubIf(null, (Bloque)GenerarArbol(hijos[3]), linea, columna));
                        return new If(subifs, 0, 0);
                    }
                case "IF_LIST":
                    if (hijos.Count() == 4)
                    {
                        linea = hijos[0].Token.Location.Line + 1;
                        columna = hijos[0].Token.Location.Column + 1;
                        LinkedList<SubIf> subifs = new LinkedList<SubIf>();
                        subifs.AddLast(new SubIf((Expresion)GenerarArbol(hijos[1]), (Bloque)GenerarArbol(hijos[3]), linea, columna));
                        return subifs;
                    }
                    else
                    {
                        linea = hijos[1].Token.Location.Line + 1;
                        columna = hijos[1].Token.Location.Column + 1;
                        LinkedList<SubIf> subifs = (LinkedList<SubIf>)GenerarArbol(hijos[0]);
                        subifs.AddLast(new SubIf((Expresion)GenerarArbol(hijos[2]), (Bloque)GenerarArbol(hijos[4]), linea, columna));
                        return subifs;
                    }
                case "WHILE_STMT":
                    if (hijos.Count() == 4)
                    {
                        linea = hijos[0].Token.Location.Line + 1;
                        columna = hijos[0].Token.Location.Column + 1;
                        return new While((Expresion)GenerarArbol(hijos[1]), (Bloque)GenerarArbol(hijos[3]), null, linea, columna);
                    }
                    else
                    {
                        linea = hijos[0].Token.Location.Line + 1;
                        columna = hijos[0].Token.Location.Column + 1;
                        return new While((Expresion)GenerarArbol(hijos[1]), (Bloque)GenerarArbol(hijos[3]), (Bloque)GenerarArbol(hijos[6]), linea, columna);

                    }
                case "RETURN_STMT":
                    linea = hijos[0].Token.Location.Line + 1;
                    columna = hijos[0].Token.Location.Column + 1;
                    if (hijos.Count() == 1)
                    {
                        return new Return(null, linea, columna);
                    }
                    return new Return((Expresion)GenerarArbol(hijos[1]), linea, columna);
                case "CONTINUE_STMT":
                    linea = hijos[0].Token.Location.Line + 1;
                    columna = hijos[0].Token.Location.Column + 1;
                    return new Continue(linea, columna);
                case "BREAK_STMT":
                    linea = hijos[0].Token.Location.Line + 1;
                    columna = hijos[0].Token.Location.Column + 1;
                    return new Break(linea, columna);
                case "PASS_STMT":
                    linea = hijos[0].Token.Location.Line + 1;
                    columna = hijos[0].Token.Location.Column + 1;
                    return new Pass(linea, columna);
                case "GLOBAL_STMT":
                    linea = hijos[0].Token.Location.Line + 1;
                    columna = hijos[0].Token.Location.Column + 1;
                    return new Global((LinkedList<string>)GenerarArbol(hijos[2]), (Tipo)GenerarArbol(hijos[1]), linea, columna);
                case "NONLOCAL_STMT":
                    linea = hijos[0].Token.Location.Line + 1;
                    columna = hijos[0].Token.Location.Column + 1;
                    return new NonLocal((LinkedList<string>)GenerarArbol(hijos[1]), (Tipo)GenerarArbol(hijos[1]), linea, columna);
                case "DEL_STMT":
                    linea = hijos[0].Token.Location.Line + 1;
                    columna = hijos[0].Token.Location.Column + 1;
                    return new Del((LinkedList<Expresion>)GenerarArbol(hijos[1]), linea, columna);
                case "ID_LIST":
                    if (hijos.Count() == 3)
                    {
                        LinkedList<string> id_list = (LinkedList<string>)GenerarArbol(hijos[0]);
                        id_list.AddLast(hijos[2].Token.Text);
                        return id_list;
                    }
                    else
                    {
                        LinkedList<string> id_list = new LinkedList<string>();
                        id_list.AddLast(hijos[0].Token.Text);
                        return id_list;
                    }
                case "ASSIGNMENT_STMT":
                    //LinkedList<LinkedList<Expresion>> assignmet_list = ;
                    if (hijos.Count() == 3)
                    {
                        linea = hijos[1].Token.Location.Line + 1;
                        columna = hijos[1].Token.Location.Column + 1;
                        return new Asignacion(new Tipo(Tipo.Type.INDEFINIDO), (LinkedList<Expresion>)GenerarArbol(hijos[0]), (LinkedList<LinkedList<Expresion>>)GenerarArbol(hijos[2]), linea, columna);
                    }
                    else
                    {
                        linea = hijos[2].Token.Location.Line + 1;
                        columna = hijos[2].Token.Location.Column + 1;
                        return new Asignacion((Tipo)GenerarArbol(hijos[0]), (LinkedList<Expresion>)GenerarArbol(hijos[1]), (LinkedList<LinkedList<Expresion>>)GenerarArbol(hijos[3]), linea, columna);
                    }
                case "TARGET_LIST":
                    LinkedList<Expresion> target_list = new LinkedList<Expresion>();
                    foreach (ParseTreeNode hijo in hijos)
                    {
                        target_list.AddLast((Expresion)GenerarArbol(hijo));
                    }
                    return target_list;
                case "TARGET":
                    if (hijos[0].Term.Name.Equals("identifier"))
                    {
                        linea = hijos[0].Token.Location.Line + 1;
                        columna = hijos[0].Token.Location.Column + 1;
                        return new Identificador(hijos[0].Token.Text, linea, columna);

                    }
                    return GenerarArbol(hijos[0]);
                case "ASSIGNMENT_LIST":
                    LinkedList<LinkedList<Expresion>> assignment_list = new LinkedList<LinkedList<Expresion>>();
                    foreach (ParseTreeNode hijo in hijos)
                    {
                        Object starred_expression = GenerarArbol(hijo);
                        if (starred_expression is Expresion)
                        {
                            LinkedList<Expresion> assignment_list2 = new LinkedList<Expresion>();
                            assignment_list2.AddLast((Expresion)starred_expression);
                            assignment_list.AddLast(assignment_list2);
                        }
                        else
                        {
                            assignment_list.AddLast((LinkedList<Expresion>)starred_expression);
                        }
                    }
                    return assignment_list;
                case "AUGMENTED_ASSIGNMENT_STMT":
                    Expresion aug_objetivo = (Expresion)GenerarArbol(hijos[0]);
                    return new AugAsignacion(aug_objetivo, (Expresion)GenerarArbol(hijos[2]), (Operador)GenerarArbol(hijos[1]),aug_objetivo.Linea, aug_objetivo.Columna);
                case "AUGTARGET":
                    if (hijos[0].Term.Name.Equals("identifier"))
                    {
                        linea = hijos[0].Token.Location.Line + 1;
                        columna = hijos[0].Token.Location.Column + 1;
                        return new Identificador(hijos[0].Token.Text, linea, columna);

                    }
                    return GenerarArbol(hijos[0]);
                case "AUG_OPERATOR":
                    return GetAugOperador(hijos[0]);
                case "EXPRESSION_STMT":
                    return GenerarArbol(hijos[0]);
                case "EXPRESSION_LIST":
                    return GenerarArbol(hijos[0]);//falta
                case "EXPRESSION_LIST_COMMA":
                    return GenerarArbol(hijos[0]); //falta lista /*return solo 1*/
                case "STARRED_LIST":
                    return GenerarArbol(hijos[0]);
                case "STARRED_LIST_COMMA":
                    LinkedList<Expresion> starred_list = new LinkedList<Expresion>();
                    foreach (ParseTreeNode hijo in hijos)
                    {
                        starred_list.AddLast((Expresion)GenerarArbol(hijo));
                    }
                    return starred_list;
                case "STARRED_EXPRESSION":
                    return GenerarArbol(hijos[0]);
                case "STARRED_ITEM":
                    return GenerarArbol(hijos[0]);
                case "EXPRESSION":
                    return GenerarArbol(hijos[0]);
                case "CONDITIONAL_EXPRESSION":
                    if (hijos.Count() == 1)
                        return GenerarArbol(hijos[0]);
                    else
                    {
                        linea = hijos[1].Token.Location.Line + 1;
                        columna = hijos[1].Token.Location.Column + 1;
                        return new Ternario((Expresion)GenerarArbol(hijos[2]), (Expresion)GenerarArbol(hijos[0]), (Expresion)GenerarArbol(hijos[4]), linea, columna);
                    }
                case "OR_TEST":
                    if (hijos.Count() == 1)
                        return GenerarArbol(hijos[0]);
                    else
                    {
                        linea = hijos[1].Token.Location.Line + 1;
                        columna = hijos[1].Token.Location.Column + 1;
                        return new Logica((Expresion)GenerarArbol(hijos[0]), (Expresion)GenerarArbol(hijos[2]), Operador.OR, linea, columna);
                    }
                case "AND_TEST":
                    if (hijos.Count() == 1)
                        return GenerarArbol(hijos[0]);
                    else
                    {
                        linea = hijos[1].Token.Location.Line + 1;
                        columna = hijos[1].Token.Location.Column + 1;
                        return new Logica((Expresion)GenerarArbol(hijos[0]), (Expresion)GenerarArbol(hijos[2]), Operador.AND, linea, columna);
                    }
                case "NOT_TEST":
                    if (hijos.Count() == 1)
                        return GenerarArbol(hijos[0]);
                    else
                    {
                        linea = hijos[0].Token.Location.Line + 1;
                        columna = hijos[0].Token.Location.Column + 1;
                        return new Logica((Expresion)GenerarArbol(hijos[1]), null, Operador.NOT, linea, columna);
                    }
                case "COMPARISON":
                    if (hijos.Count() == 1)
                        return GenerarArbol(hijos[0]);
                    else
                    {
                        ParseTreeNode comp_operator = (ParseTreeNode)GenerarArbol(hijos[1]);
                        linea = comp_operator.Token.Location.Line + 1;
                        columna = comp_operator.Token.Location.Column + 1;
                        return new Relacional((Expresion)GenerarArbol(hijos[0]), (Expresion)GenerarArbol(hijos[2]), GetOperador(comp_operator), linea, columna);
                    }
                case "COMP_OPERATOR":
                    return hijos[0];
                case "OR_EXPR":
                    return GenerarArbol(hijos[0]);
                case "XOR_EXPR":
                    return GenerarArbol(hijos[0]);
                case "AND_EXPR":
                    return GenerarArbol(hijos[0]);
                case "SHIFT_EXPR":
                    return GenerarArbol(hijos[0]);
                case "A_EXPR":
                    if (hijos.Count() == 1)
                        return GenerarArbol(hijos[0]);
                    else
                    {
                        linea = hijos[1].Token.Location.Line + 1;
                        columna = hijos[1].Token.Location.Column + 1;
                        return new Aritmetica((Expresion)GenerarArbol(hijos[0]), (Expresion)GenerarArbol(hijos[2]), GetOperador(hijos[1]), linea, columna);
                    }
                case "M_EXPR":
                    if (hijos.Count() == 1)
                        return GenerarArbol(hijos[0]);
                    else
                    {
                        linea = hijos[1].Token.Location.Line + 1;
                        columna = hijos[1].Token.Location.Column + 1;
                        return new Aritmetica((Expresion)GenerarArbol(hijos[0]), (Expresion)GenerarArbol(hijos[2]), GetOperador(hijos[1]), linea, columna);
                    }
                case "U_EXPR":
                    if (hijos.Count() == 1)
                        return GenerarArbol(hijos[0]);
                    else
                    {
                        linea = hijos[0].Token.Location.Line + 1;
                        columna = hijos[0].Token.Location.Column + 1;
                        return new Aritmetica((Expresion)GenerarArbol(hijos[1]), null, GetOperador(hijos[0]), linea, columna);
                    }
                case "POWER":
                    if (hijos.Count() == 1)
                        return GenerarArbol(hijos[0]);
                    else
                    {
                        linea = hijos[1].Token.Location.Line + 1;
                        columna = hijos[1].Token.Location.Column + 1;
                        return new Aritmetica((Expresion)GenerarArbol(hijos[0]), (Expresion)GenerarArbol(hijos[2]), Operador.POTENCIA, linea, columna);
                    }
                case "PRIMARY":
                    return GenerarArbol(hijos[0]);
                case "SUBSCRIPTION":
                    linea = hijos[1].Token.Location.Line + 1;
                    columna = hijos[1].Token.Location.Column + 1;
                    return new AccesoLista((Expresion)GenerarArbol(hijos[0]), (Expresion)GenerarArbol(hijos[2]), linea, columna);
                case "ATTRIBUTEREF":
                    linea = hijos[1].Token.Location.Line + 1;
                    columna = hijos[1].Token.Location.Column + 1;
                    return new Referencia((Expresion)GenerarArbol(hijos[0]), hijos[2].Token.Text, linea, columna);
                case "ATOM":
                    if (hijos[0].Term.Name.Equals("identifier"))
                    {
                        linea = hijos[0].Token.Location.Line + 1;
                        columna = hijos[0].Token.Location.Column + 1;
                        return new Identificador(hijos[0].Token.Text, linea, columna);

                    }
                    return GenerarArbol(hijos[0]);
                case "LITERAL":
                    linea = hijos[0].Token.Location.Line + 1;
                    columna = hijos[0].Token.Location.Column + 1;
                    if (hijos[0].Term.Name.Equals("number"))
                    {
                        try
                        {
                            int valor2 = Convert.ToInt32(hijos[0].Token.Text);
                            return new Literal(new Tipo(Tipo.Type.INT), valor2, linea, columna);
                        }
                        catch (Exception)
                        {
                            double valor = Convert.ToDouble(hijos[0].Token.Text);
                            return new Literal(new Tipo(Tipo.Type.DOUBLE), valor, linea, columna);
                        }
                    }
                    else if (hijos[0].Term.Name.Equals("stringliteral") || hijos[0].Term.Name.Equals("stringliteral2"))
                    {
                        return new Literal(new Tipo(Tipo.Type.STRING), hijos[0].Token.Text, linea, columna);
                    }
                    else if (hijos[0].Term.Name.Equals("true") || hijos[0].Term.Name.Equals("True"))
                    {
                        return new Literal(new Tipo(Tipo.Type.BOOLEAN), 1, linea, columna);
                    }
                    else if (hijos[0].Term.Name.Equals("false") || hijos[0].Term.Name.Equals("False"))
                    {
                        return new Literal(new Tipo(Tipo.Type.BOOLEAN), 0, linea, columna);
                    }
                    else
                    {
                        return new Literal(new Tipo(Tipo.Type.NONE), null, linea, columna); /*agregar clase new None()*/
                    }
                case "ENCLOSURE":
                    return GenerarArbol(hijos[0]);
                case "LIST_DISPLAY":
                    linea = hijos[0].Token.Location.Line + 1;
                    columna = hijos[0].Token.Location.Column + 1;
                    if (hijos.Count() == 3)
                        return new Lista((LinkedList<Expresion>)GenerarArbol(hijos[1]), linea, columna);
                    return new Lista(null, linea, columna);
                case "PARENTH_FORM":
                    if (hijos.Count() == 3)
                    {
                        return GenerarArbol(hijos[1]);
                    }
                    return null;//parenth vacíos
                case "CALL":
                    linea = hijos[1].Token.Location.Line + 1;
                    columna = hijos[1].Token.Location.Column + 1;
                    Expresion callPrimary = (Expresion)GenerarArbol(hijos[0]);
                    if (callPrimary is Identificador)
                    {
                        if (hijos.Count() == 3)/*Sin parametros*/
                        {
                            return new Llamada(((Identificador)callPrimary).Id, null, linea, columna);
                        }
                        else
                        {
                            return new Llamada(((Identificador)callPrimary).Id, (LinkedList<Expresion>)GenerarArbol(hijos[2]), linea, columna);
                        }
                    }
                    else
                    {
                        if (hijos.Count() == 3)/*Sin parametros*/
                        {
                            return new Llamada(callPrimary, null, linea, columna);
                        }
                        else
                        {
                            return new Llamada(callPrimary, (LinkedList<Expresion>)GenerarArbol(hijos[2]), linea, columna);
                        }
                    }
                case "ARGUMENT_LIST": //para parametros
                    return GenerarArbol(hijos[0]);
                case "POSITIONAL_ARGUMENTS":
                    if (hijos.Count() == 3)
                    {
                        LinkedList<Expresion> positional_arguments = (LinkedList<Expresion>)GenerarArbol(hijos[0]);
                        positional_arguments.AddLast((Expresion)GenerarArbol(hijos[2]));
                        return positional_arguments;
                    }
                    else
                    {
                        LinkedList<Expresion> positional_arguments = new LinkedList<Expresion>();
                        positional_arguments.AddLast((Expresion)GenerarArbol(hijos[0]));
                        return positional_arguments;
                    }
            }

            return null;
        }

        public Operador GetAugOperador(ParseTreeNode raiz)
        {
            switch (raiz.Token.Text)
            {
                case "+=":
                    return Operador.SUMA;
                case "-=":
                    return Operador.RESTA;
                case "*=":
                    return Operador.MULTIPLICACION;
                case "/=":
                    return Operador.DIVISION;
                case "%=":
                    return Operador.MODULO;
                case "//=":
                    return Operador.FLOOR;
                case "**=":
                    return Operador.POTENCIA;
            }
            return Operador.INDEFINIDO;
        }

        private Operador GetOperador(ParseTreeNode raiz)
        {
            switch (raiz.Token.Text)
            {
                case "+":
                    return Operador.SUMA;
                case "-":
                    return Operador.RESTA;
                case "*":
                    return Operador.MULTIPLICACION;
                case "/":
                    return Operador.DIVISION;
                case "%":
                    return Operador.MODULO;
                case "//":
                    return Operador.FLOOR;
                case ">":
                    return Operador.MAYORQUE;
                case "<":
                    return Operador.MENORQUE;
                case ">=":
                    return Operador.MAYORIGUALQUE;
                case "<=":
                    return Operador.MENORIGUALQUE;
                case "==":
                    return Operador.IGUAL;
                case "!=":
                    return Operador.DIFERENTE;
            }
            return Operador.INDEFINIDO;
        }

    }
}
