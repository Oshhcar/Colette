using Compilador.parser._3d.ast;
using Compilador.parser._3d.ast.entorno;
using Compilador.parser._3d.ast.expresion;
using Compilador.parser._3d.ast.expresion.Operacion;
using Compilador.parser._3d.ast.instrucion;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser._3d
{
    class Analizador
    {
        private ParseTree raiz;

        public ParseTree Raiz { get => raiz; set => raiz = value; }

        public bool AnalizarEntrada(String entrada)
        {
            Gramatica gramatica = new Gramatica();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(entrada);
            this.Raiz = arbol;

            if (arbol.Root != null)
                return true;

            return false;
        }

        public Object GenerarAST(ParseTreeNode raiz)
        {
            string r = raiz.ToString();
            ParseTreeNode[] hijos = null;

            int linea = 0;
            int columna = 0;

            if(raiz.ChildNodes.Count > 0)
            {
                hijos = raiz.ChildNodes.ToArray();
            }

            switch (r)
            {
                case "START":
                    return GenerarAST(hijos[0]);

                case "INSTRUCTIONS":
                    LinkedList<Instruccion> instrucciones = new LinkedList<Instruccion>();
                    foreach (ParseTreeNode hijo in hijos)
                    {
                        instrucciones.AddLast((Instruccion)GenerarAST(hijo));
                    }
                    return new AST(instrucciones);

                case "INSTRUCTION":
                    return GenerarAST(hijos[0]);
                case "DECLARATION":
                    linea = hijos[0].Token.Location.Line + 1;
                    columna = hijos[0].Token.Location.Column + 1;

                    if (hijos.Count() == 2)
                    {
                        return new Declaracion((LinkedList<string>)GenerarAST(hijos[1]), linea, columna);
                    }
                    else if (hijos.Count() == 3)
                    {
                        return new Declaracion(hijos[1].Token.Text, (Expresion)GenerarAST(hijos[2]), linea, columna);
                    }
                    else
                    {
                        return new Declaracion(hijos[1].Token.Text, linea, columna);
                    }
                case "IDENTIFIER_LIST":
                    LinkedList<string> ids = new LinkedList<string>();
                    foreach (ParseTreeNode hijo in hijos)
                    {
                        ids.AddLast(hijo.Token.Text);
                    }
                    return ids;
                case "ASSIGNMENT":
                    linea = hijos[0].Token.Location.Line + 1;
                    columna = hijos[0].Token.Location.Column + 1;

                    if (hijos.Count() == 2)
                    {
                        return new Asignacion(hijos[0].Token.Text, (Expresion)GenerarAST(hijos[1]), linea, columna);
                    }
                    else
                    {
                        return new Asignacion(hijos[0].Token.Text, (Expresion)GenerarAST(hijos[2]), (Expresion)GenerarAST(hijos[4]), linea, columna);
                    }
                case "LABEL":
                    linea = hijos[0].Token.Location.Line + 1;
                    columna = hijos[0].Token.Location.Column + 1;
                    return new Etiqueta(hijos[0].Token.Text, linea, columna);
                case "GOTO":
                    linea = hijos[0].Token.Location.Line + 1;
                    columna = hijos[0].Token.Location.Column + 1;
                    return new Salto(hijos[1].Token.Text, linea, columna);
                case "IF":
                    linea = hijos[0].Token.Location.Line + 1;
                    columna = hijos[0].Token.Location.Column + 1;
                    if (hijos[0].Token.Text.Equals("if"))
                        return new SaltoCond((Expresion)GenerarAST(hijos[1]), hijos[3].Token.Text, 1, linea, columna);
                    else
                        return new SaltoCond((Expresion)GenerarAST(hijos[1]), hijos[3].Token.Text, 2, linea, columna);
                case "METHOD":
                    linea = hijos[0].Token.Location.Line + 1;
                    columna = hijos[0].Token.Location.Column + 1;
                    LinkedList<Instruccion> bloque = GenerarAST(hijos[3]) as LinkedList<Instruccion>;
                    return new Metodo(hijos[1].Token.Text, bloque, linea, columna);
                case "CALL":
                    linea = hijos[0].Token.Location.Line + 1;
                    columna = hijos[0].Token.Location.Column + 1;
                    return new Call(hijos[1].Token.Text, linea, columna);
                case "PRINT":
                    linea = hijos[0].Token.Location.Line + 1;
                    columna = hijos[0].Token.Location.Column + 1;
                    string char_ = GenerarAST(hijos[2]).ToString();
                    return new Print(char_, hijos[3].Token.Text, linea, columna);
                case "CHART":
                    return hijos[0].Term.Name;
                case "BLOCKS":
                    LinkedList<Instruccion> bloques = new LinkedList<Instruccion>();
                    foreach (ParseTreeNode hijo in hijos)
                    {
                        bloques.AddLast((Instruccion)GenerarAST(hijo));
                    }
                    return bloques;
                case "BLOCK":
                    return GenerarAST(hijos[0]);
                case "E":
                    return GenerarAST(hijos[0]);
                case "E_ARITHMETIC":
                    linea = hijos[1].Token.Location.Line + 1;
                    columna = hijos[1].Token.Location.Column + 1;
                    return new Aritmetica((Expresion)GenerarAST(hijos[0]), (Expresion)GenerarAST(hijos[2]), GetOperador(hijos[1]), linea, columna);
                case "E_RELATIONAL":
                    linea = hijos[1].Token.Location.Line + 1;
                    columna = hijos[1].Token.Location.Column + 1;
                    return new Relacional((Expresion)GenerarAST(hijos[0]), (Expresion)GenerarAST(hijos[2]), GetOperador(hijos[1]), linea, columna);
                case "E_PRIMITIVE":
                    linea = hijos[0].Token.Location.Line + 1;
                    columna = hijos[0].Token.Location.Column + 1;
                    if (hijos[0].Term.Name.Equals("num"))
                    {
                        try
                        {
                            int valor = Convert.ToInt32(hijos[0].Token.Text);
                            return new Literal(valor, Tipo.ENTERO, linea, columna);
                        }
                        catch (Exception)
                        {
                            double valor = Convert.ToDouble(hijos[0].Token.Text);
                            return new Literal(valor, Tipo.DECIMAL, linea, columna);
                        }
                    }
                    else
                    {
                        if (hijos.Count() == 1)
                        {
                            return new Identificador(hijos[0].Token.Text, linea, columna);
                        }
                        else
                        {
                            return new AccesoArreglo(hijos[0].Token.Text,(Expresion)GenerarAST(hijos[2]), linea, columna);
                        }
                    }
            }
            return null;
        }

        private Operador GetOperador(ParseTreeNode raiz)
        {
            switch (raiz.Token.Text)
            {
                case "+":
                    return Operador.MAS;
                case "-":
                    return Operador.MENOS;
                case "*":
                    return Operador.POR;
                case "/":
                    return Operador.DIVIDIO;
                case "%":
                    return Operador.MODULO;
                case "<":
                    return Operador.MENORQUE;
                case ">":
                    return Operador.MAYORQUE;
                case "<=":
                    return Operador.MENORIGUAL;
                case ">=":
                    return Operador.MAYORIGUAL;
                case "==":
                    return Operador.IGUAL;
                case "!=":
                    return Operador.DIFERENTE;
            }
            return Operador.NULL;
        }

    }
}
