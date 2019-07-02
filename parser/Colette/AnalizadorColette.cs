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
                case "CLASE":
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
                case "ATOM":
                    return GenerarArbol(hijos[0]);
                case "LITERAL":
                    linea = hijos[0].Token.Location.Line + 1;
                    columna = hijos[0].Token.Location.Column + 1;
                    if (hijos[0].Term.Name.Equals("number"))
                    { 
                        try
                        {
                            int valor2 = Convert.ToInt32(hijos[0].Token.Text);
                            return new Literal(Tipo.ENTERO, valor2, linea, columna);
                        }
                        catch (Exception)
                        {
                            double valor = Convert.ToDouble(hijos[0].Token.Text);
                            return new Literal(Tipo.DECIMAL, valor, linea, columna);
                        }   
                    }
                    return null;
                case "E_ARITMETICA":
                    linea = hijos[1].Token.Location.Line + 1;
                    columna = hijos[1].Token.Location.Column + 1;
                    return new Aritmetica((Expresion)GenerarArbol(hijos[0]), (Expresion)GenerarArbol(hijos[2]), GetOperador(hijos[1]), linea, columna);

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
            }
            return Operador.INDEFINIDO;
        }

    }
}
