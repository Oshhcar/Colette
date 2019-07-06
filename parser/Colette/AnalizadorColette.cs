using Compilador.parser.Colette.ast;
using Compilador.parser.Colette.ast.entorno;
using Compilador.parser.Colette.ast.expresion;
using Compilador.parser.Colette.ast.expresion.operacion;
using Compilador.parser.Colette.ast.instruccion;
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
                    return new Clase(hijos[1].Token.Text, (Bloque)GenerarArbol(hijos[3]), linea, columna);
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
                case "PRINT":
                    linea = hijos[0].Token.Location.Line+1;
                    columna = hijos[0].Token.Location.Column+1;
                    return new Print((Expresion)GenerarArbol(hijos[2]), linea, columna);
                case "ASSIGNMENT_STMT":
                    linea = hijos[1].Token.Location.Line + 1;
                    columna = hijos[1].Token.Location.Column + 1;
                    //LinkedList<LinkedList<Expresion>> assignmet_list = ;
                    return new Asignacion((LinkedList<Expresion>)GenerarArbol(hijos[0]), (LinkedList<LinkedList<Expresion>>)GenerarArbol(hijos[2]), linea, columna);
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
                    return GenerarArbol(hijos[0]);
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
                        linea = hijos[0].Token.Location.Line + 1;
                        columna = hijos[0].Token.Location.Column + 1;
                        return new Aritmetica((Expresion)GenerarArbol(hijos[0]), (Expresion)GenerarArbol(hijos[2]), Operador.POTENCIA, linea, columna);
                    }
                case "PRIMARY":
                    return GenerarArbol(hijos[0]);
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
                case "PARENTH_FORM":
                    if (hijos.Count() == 3)
                    {
                        return GenerarArbol(hijos[1]);
                    }
                    return null;//parenth vacíos
            }

            return null;
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
