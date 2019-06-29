using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser.Collete
{
    class GramaticaCollete : Grammar
    {
        public GramaticaCollete() : base(true)
        {
            CommentTerminal blockComment = new CommentTerminal("block-comment", "\"\"\"\"", "\"\"\"\"");
            CommentTerminal lineComment = new CommentTerminal("line-comment", "#",
                "\r", "\n", "\u2085", "\u2028", "\u2029");

            NonGrammarTerminals.Add(blockComment);
            NonGrammarTerminals.Add(lineComment);


            /* Reserved Words */
            KeyTerm
                class_ = ToTerm("class"),
                def_ = ToTerm("def"),
                //init_ = ToTerm("__init__"),
                self_ = ToTerm("self"),
                print_ = ToTerm("print");

            /* Symbols*/
            KeyTerm
                equal = ToTerm("="),
                colon = ToTerm(":"),
                comma = ToTerm(","),
                semicolon = ToTerm(";"),
                leftPar = ToTerm("("),
                rightPar = ToTerm(")"),
                leftCor = ToTerm("["),
                rightCor = ToTerm("]");

            var numero = TerminalFactory.CreatePythonNumber("number");
            IdentifierTerminal id = TerminalFactory.CreateCSharpIdentifier("id");

            NonTerminal
                INICIO = new NonTerminal("INICIO"),
                INSTRUCCIONES = new NonTerminal("INSTRUCCIONES"),
                INSTRUCCION = new NonTerminal("INSTRUCCION"),
                CLASE = new NonTerminal("CLASE"),
                PRINT = new NonTerminal("PRINT"),
                BLOQUE = new NonTerminal("BLOQUE"),
                SENTENCIAS = new NonTerminal("SENTENCIAS"),
                SENTENCIA = new NonTerminal("SENTENCIA");

            this.Root = INICIO; 

            INICIO.Rule = INSTRUCCIONES;

            INSTRUCCIONES.Rule = MakePlusRule(INSTRUCCIONES, INSTRUCCION);

            INSTRUCCION.Rule = (CLASE);

            CLASE.Rule = class_ + id + colon + Eos + BLOQUE ;

            BLOQUE.Rule = Indent + SENTENCIAS + Dedent;

            SENTENCIAS.Rule = MakePlusRule(SENTENCIAS, SENTENCIA);

            SENTENCIA.Rule = PRINT + Eos;

            PRINT.Rule = print_ + leftPar + rightPar;

            //LanguageFlags = LanguageFlags.NewLineBeforeEOF;

            // 4. Token filters - created in a separate method CreateTokenFilters
            //    we need to add continuation symbol to NonGrammarTerminals because it is not used anywhere in grammar
            NonGrammarTerminals.Add(ToTerm(@"\"));

            // 7. Error recovery rule
            SENTENCIA.ErrorRule = SyntaxError + Eos;
            CLASE.ErrorRule = SyntaxError + Dedent;
            BLOQUE.ErrorRule = SyntaxError + Dedent;
        }

        public override void CreateTokenFilters(LanguageData language, TokenFilterList filters)
        {
            //base.CreateTokenFilters(language, filters);
            var outlineFilter = new CodeOutlineFilter(language.GrammarData,
                OutlineOptions.ProduceIndents | OutlineOptions.CheckBraces, ToTerm(@"\"));
            filters.Add(outlineFilter);
        }
    }
}
