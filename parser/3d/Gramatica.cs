using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;

namespace Compilador.parser._3d
{
    class Gramatica : Grammar
    {
        public Gramatica() : base(caseSensitive: true) {

            CommentTerminal blockComment = new CommentTerminal("block-comment", "/*", "*/");
            CommentTerminal lineComment = new CommentTerminal("line-comment", "//",
                "\r", "\n", "\u2085", "\u2028", "\u2029");

            NonGrammarTerminals.Add(blockComment);
            NonGrammarTerminals.Add(lineComment);

            /* Reserved Words */
            KeyTerm
                var_ = ToTerm("var"),
                goto_ = ToTerm("goto"),
                if_ = ToTerm("if"),
                ifFalse_ = ToTerm("ifFalse"),
                void_ = ToTerm("void"),
                begin_ = ToTerm("begin"),
                end_ = ToTerm("end"),
                call_ = ToTerm("call"),
                print_ = ToTerm("print"),
                c_ = ToTerm("c"),
                e_ = ToTerm("e"),
                d_ = ToTerm("d");

            MarkReservedWords("var", "goto", "if", "ifFalse", "void", "begin", "end", "call", "print",
                "c", "e", "d");

            /* Arithmetic Operators*/
            KeyTerm
                mas = ToTerm("+"),
                menos = ToTerm("-"),
                por = ToTerm("*"),
                division = ToTerm("/"),
                modulo = ToTerm("%");

            /* Relational operators */
            KeyTerm
                menorigual = ToTerm("<="),
                mayorigual = ToTerm(">="),
                menorque = ToTerm("<"),
                mayorque = ToTerm(">"),
                igual = ToTerm("=="),
                diferente = ToTerm("!=");

            /* Symbols*/
            KeyTerm
                equal = ToTerm("="),
                semicolon = ToTerm(";"),
                colon = ToTerm(":"),
                comma = ToTerm(","),
                leftPar = ToTerm("("),
                rightPar = ToTerm(")"),
                leftCor = ToTerm("["),
                rightCor = ToTerm("]"),
                quotes = ToTerm("\"");

            MarkPunctuation(equal, semicolon, colon, comma, leftPar, rightPar, quotes);

            IdentifierTerminal id = TerminalFactory.CreateCSharpIdentifier("id");
            RegexBasedTerminal label = new RegexBasedTerminal("label", "L[0-9]+");
            NumberLiteral num = new NumberLiteral("num");

            NonTerminal
                START = new NonTerminal("START"),
                INSTRUCTIONS = new NonTerminal("INSTRUCTIONS"),
                INSTRUCTION = new NonTerminal("INSTRUCTION"),
                DECLARATION = new NonTerminal("DECLARATION"),
                ASSIGNMENT = new NonTerminal("ASSIGNMENT"),
                LABEL = new NonTerminal("LABEL"),
                GOTO = new NonTerminal("GOTO"),
                IF = new NonTerminal("IF"),
                METHOD = new NonTerminal("METHOD"),
                CALL = new NonTerminal("CALL"),
                PRINT = new NonTerminal("PRINT"),
                CHART = new NonTerminal("CHART"),
                IDENTIFIER_LIST = new NonTerminal("IDENTIFIER_LIST"),
                BLOCKS = new NonTerminal("BLOCKS"),
                BLOCK = new NonTerminal("BLOCK"),
                E = new NonTerminal("E"),
                E_ARITHMETIC = new NonTerminal("E_ARITHMETIC"),
                E_RELATIONAL = new NonTerminal("E_RELATIONAL"),
                E_PRIMITIVE = new NonTerminal("E_PRIMITIVE");



            this.RegisterOperators(1, Associativity.Left, igual, diferente);
            this.RegisterOperators(2, Associativity.Left, mayorigual, menorigual, mayorque, menorque);
            this.RegisterOperators(3, Associativity.Left, mas, menos);
            this.RegisterOperators(4, Associativity.Left, por, division);
            this.RegisterOperators(5, Associativity.Right, modulo);

            this.Root = START;

            START.Rule = INSTRUCTIONS;

            INSTRUCTIONS.Rule = MakePlusRule(INSTRUCTIONS, INSTRUCTION);

            INSTRUCTION.Rule = DECLARATION + semicolon
                            | ASSIGNMENT + semicolon
                            | LABEL
                            | GOTO + semicolon
                            | IF + semicolon
                            | METHOD
                            | CALL + semicolon
                            | PRINT + semicolon;

            LABEL.Rule = label + colon;

            GOTO.Rule = goto_ + label;

            IF.Rule = if_ + leftPar + E + rightPar + goto_ + label
                    | ifFalse_ + leftPar + E + rightPar + goto_ + label;

            METHOD.Rule = void_ + id + leftPar + rightPar + begin_ + BLOCKS + end_;

            CALL.Rule = call_ + id;

            PRINT.Rule = print_ + leftPar + quotes + modulo + CHART + quotes + comma + id + rightPar;

            CHART.Rule = c_
                        | e_
                        | d_;

            DECLARATION.Rule = var_ + IDENTIFIER_LIST
                            | var_ + id + equal + E
                            | var_ + id + leftCor + rightCor;

            IDENTIFIER_LIST.Rule = MakeStarRule(IDENTIFIER_LIST, comma, id);

            ASSIGNMENT.Rule = id + equal + E
                            | id + leftCor + E + rightCor + equal + E;

            BLOCKS.Rule = MakeStarRule(BLOCKS, BLOCK);

            BLOCK.Rule = DECLARATION + semicolon
                        | ASSIGNMENT + semicolon
                        | LABEL
                        | GOTO + semicolon
                        | IF + semicolon
                        | CALL + semicolon
                        | PRINT + semicolon;

            E.Rule = E_ARITHMETIC
                    | E_RELATIONAL
                    | E_PRIMITIVE
                    | leftPar + E + rightPar;

            E_ARITHMETIC.Rule = E + mas + E
                                | E + menos + E
                                | E + por + E
                                | E + division + E
                                | E + modulo + E;

            E_RELATIONAL.Rule = E + menorigual + E
                                | E + mayorigual + E
                                | E + menorque + E
                                | E + mayorque + E
                                | E + igual + E
                                | E + diferente + E;

            E_PRIMITIVE.Rule = num
                             | id
                             | id + leftCor + E + rightCor;
        }
    }
}
