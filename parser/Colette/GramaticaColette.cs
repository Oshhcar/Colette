using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser.Collete
{
    class GramaticaColette : Grammar
    {
        public GramaticaColette() : base(true)
        {
            CommentTerminal blockComment = new CommentTerminal("block-comment", "\"\"\"", "\"\"\"");
            CommentTerminal lineComment = new CommentTerminal("line-comment", "#",
                "\r", "\n", "\u2085", "\u2028", "\u2029");

            NonGrammarTerminals.Add(blockComment);
            NonGrammarTerminals.Add(lineComment);


            /* Reserved Words */
            KeyTerm
                class_ = ToTerm("class"),
                def_ = ToTerm("def"),
                lambda_ = ToTerm("lambda"),
                print_ = ToTerm("print"),
                or_ = ToTerm("or"),
                and_ = ToTerm("and"),
                not_ = ToTerm("not"),
                for_ = ToTerm("for"),
                if_ = ToTerm("if"),
                elif_ = ToTerm("elif"),
                else_ = ToTerm("else"),
                None_ = ToTerm("None"),
                none_ = ToTerm("none"),
                break_ = ToTerm("break"),
                continue_ = ToTerm("continue"),
                return_ = ToTerm("return"),
                pass_ = ToTerm("pass"),
                global_ = ToTerm("global"),
                nonlocal_ = ToTerm("nonlocal"),
                del_ = ToTerm("del"),
                int_ = ToTerm("int"),
                double_ = ToTerm("double"),
                String_ = ToTerm("String"),
                string_ = ToTerm("string"),
                boolean_ = ToTerm("boolean"),
                dictionary_ = ToTerm("dictionary"),
                list_ = ToTerm("list"),
                tup_ = ToTerm("tup"),
                while_ = ToTerm("while"),
                true_ = ToTerm("true"),
                false_ = ToTerm("false"),
                True_ = ToTerm("True"),
                False_ = ToTerm("False"),
                void_ = ToTerm("void"),
                printTabla_ = ToTerm("printTabla"),
                import_ = ToTerm("import");

            MarkReservedWords("class", "def", "lambda", "print", "or", "and", "not", "for",
                "if", "elif", "else", "None", "break", "continue", "return", "pass", "global", "nonlocal",
                "del", "int", "double", "String", "string", "boolean", "dictionary", "list", "tup", "while", "true", 
                "false", "True", "False", "none", "void", "printTabla");

            /* Relational operators */
            KeyTerm
                menorigual = ToTerm("<="),
                mayorigual = ToTerm(">="),
                menorque = ToTerm("<"),
                mayorque = ToTerm(">"),
                igual = ToTerm("=="),
                diferente = ToTerm("!="),
                is_ = ToTerm("is"),
                in_ = ToTerm("in");

            /* Logic operators */
            KeyTerm
                o_ = ToTerm("|"),
                y_ = ToTerm("&"),
                xor_ = ToTerm("^");

            /* Shift operators */
            KeyTerm
                leftShift = ToTerm("<<"),
                rightShift = ToTerm(">>");

            /* Arithmetic Operators*/
            KeyTerm
                mas = ToTerm("+"),
                menos = ToTerm("-"),
                por = ToTerm("*"),
                division = ToTerm("/"),
                floor = ToTerm("//"),
                modulo = ToTerm("%"),
                potencia = ToTerm("**");

            /* Symbols*/
            KeyTerm
                equal = ToTerm("="),
                colon = ToTerm(":"),
                comma = ToTerm(","),
                dot = ToTerm("."),
                leftPar = ToTerm("("),
                rightPar = ToTerm(")"),
                leftCor = ToTerm("["),
                rightCor = ToTerm("]"),
                leftLla = ToTerm("{"),
                rightLla = ToTerm("}"),
                masEqual = ToTerm("+="),
                menosEqual = ToTerm("-="),
                porEqual = ToTerm("*="),
                divisionEqual = ToTerm("/="),
                floorEqual = ToTerm("//="),
                moduloEqual = ToTerm("%="),
                potenciaEqual = ToTerm("**=");

            //var number = TerminalFactory.CreatePythonNumber("number");
            var number = new NumberLiteral("number");
            var identifier = new IdentifierTerminal("identifier");
            var stringliteral = new StringLiteral("stringliteral", "\"", StringOptions.IsTemplate);
            var stringliteral2 = new StringLiteral("stringliteral2", "'", StringOptions.IsTemplate);
            //var identifier = TerminalFactory.CreatePythonIdentifier("identifier");
            //var stringliteral = TerminalFactory.CreatePythonString("stringliteral");

            NonTerminal
                INICIO = new NonTerminal("INICIO"),
                INSTRUCCIONES = new NonTerminal("INSTRUCCIONES"),
                INSTRUCCION = new NonTerminal("INSTRUCCION"),

                CLASEDEF = new NonTerminal("CLASEDEF"),
                PRINT = new NonTerminal("PRINT"),
                PRINTTABLA = new NonTerminal("PRINTTABLA"),

                BLOQUE_SUP = new NonTerminal("BLOQUE_SUP"),
                INSTRUCCIONES_SUP = new NonTerminal("INSTRUCCIONES_SUP"),
                INSTRUCCION_SUP = new NonTerminal("INSTRUCCION_SUP"),

                BLOQUE = new NonTerminal("BLOQUE"),
                SENTENCIAS = new NonTerminal("SENTENCIAS"),
                SENTENCIA = new NonTerminal("SENTENCIA"),

                IMPORT_STMT = new NonTerminal("IMPORT_STMT"),

                TYPE = new NonTerminal("TYPE"),
                EXPRESSION_STMT = new NonTerminal("EXPRESSION_STMT"),
                ASSIGNMENT_STMT = new NonTerminal("ASSIGNMENT_STMT"),
                ASSIGNMENT_LIST = new NonTerminal("ASSIGNMENT_LIST"),
                AUGMENTED_ASSIGNMENT_STMT = new NonTerminal("AUGMENTED_ASSIGNMENT_STMT"),
                AUGTARGET = new NonTerminal("AUGTARGET"),
                AUG_OPERATOR = new NonTerminal("AUG_OPERATOR"),
                TARGET_LIST = new NonTerminal("TARGET_LIST"),
                TARGET = new NonTerminal("TARGET"),
                IF_STMT = new NonTerminal("IF_STMT"),
                IF_LIST = new NonTerminal("IF_LIST"),
                WHILE_STMT = new NonTerminal("WHILE_STMT"),
                FOR_STMT = new NonTerminal("FOR_STMT"),
                RETURN_STMT = new NonTerminal("RETURN_STMT"),
                BREAK_STMT = new NonTerminal("BREAK_STMT"),
                CONTINUE_STMT = new NonTerminal("CONTINUE_STMT"),
                PASS_STMT = new NonTerminal("PASS_STMT"),
                FUNCDEF = new NonTerminal("FUNCDEF"),
                PARAMETER_LIST = new NonTerminal("PARAMETER_LIST"),
                GLOBAL_STMT = new NonTerminal("GLOBAL_STMT"),
                NONLOCAL_STMT = new NonTerminal("NONLOCAL_STMT"),
                ID_LIST = new NonTerminal("ID_LIST"),
                DEL_STMT = new NonTerminal("DEL_STMT"),

                CONDITIONAL_EXPRESSION = new NonTerminal("CONDITIONAL_EXPRESSION"),
                EXPRESSION = new NonTerminal("EXPRESSION"),
                EXPRESSION_NOCOND = new NonTerminal("EXPRESSION_NOCOND"),
                LAMBDA_EXP = new NonTerminal("LAMBDA_EXP"),
                LAMBDA_EXP_NOCOND = new NonTerminal("LAMBDA_EXP_NOCOND"),
                OR_TEST = new NonTerminal("OR_TEST"),
                AND_TEST = new NonTerminal("AND_TEST"),
                NOT_TEST = new NonTerminal("NOT_TEST"),
                COMPARISON = new NonTerminal("COMPARISON"),
                COMP_OPERATOR = new NonTerminal("COMP_OPERATOR"),
                OR_EXPR = new NonTerminal("OR_EXPR"),
                XOR_EXPR = new NonTerminal("XOR_EXPR"),
                AND_EXPR = new NonTerminal("AND_EXPR"),
                SHIFT_EXPR = new NonTerminal("SHIFT_EXPR"),
                A_EXPR = new NonTerminal("A_EXPR"),
                M_EXPR = new NonTerminal("M_EXPR"),
                U_EXPR = new NonTerminal("U_EXPR"),
                POWER = new NonTerminal("POWER"),
                PRIMARY = new NonTerminal("PRIMARY"),
                ATOM = new NonTerminal("ATOM"),
                ATTRIBUTEREF = new NonTerminal("ATTRIBUTEREF"),
                SUBSCRIPTION = new NonTerminal("SUBSCRIPTION"),
                SLICING = new NonTerminal("SLICING"),
                CALL = new NonTerminal("CALL"),
                LITERAL = new NonTerminal("LITERAL"),
                ENCLOSURE = new NonTerminal("ENCLOSURE"),
                PARENTH_FORM = new NonTerminal("PARENTH_FORM"),
                LIST_DISPLAY = new NonTerminal("LIST_DISPLAY"),
                DICT_DISPLAY = new NonTerminal("DICT_DISPLAY"),
                SET_DISPLAY = new NonTerminal("SET_DISPLAY"),
                GENERATOR_EXPRESSION = new NonTerminal("GENERATOR_EXPRESSION"),
                EXPRESSION_LIST = new NonTerminal("EXPRESSION_LIST"),
                EXPRESSION_LIST_COMMA = new NonTerminal("EXPRESSION_LIST_COMMA"),
                STARRED_LIST = new NonTerminal("STARRED_LIST"),
                STARRED_LIST_COMMA = new NonTerminal("STARRED_LIST_COMMA"),
                STARRED_EXPRESSION = new NonTerminal("STARRED_EXPRESSION"),
                STARRED_ITEM = new NonTerminal("STARRED_ITEM"),
                COMPREHENSION = new NonTerminal("COMPREHENSION"),
                COMP_FOR = new NonTerminal("COMP_FOR"),
                COMP_ITER = new NonTerminal("COMP_ITER"),
                COMP_IF = new NonTerminal("COMP_IF"),
                KEY_DATUM_LIST = new NonTerminal("KEY_DATUM_LIST"),
                KEY_DATUM = new NonTerminal("KEY_DATUN"),
                DICT_COMPREHENSION = new NonTerminal("DICT_COMPREHENSION"),
                SLICE_LIST = new NonTerminal("SLICE_LIST"),
                SLICE_ITEM = new NonTerminal("SLICE_ITEM"),
                PROPER_SLICE = new NonTerminal("PROPER_SLICE"),
                ARGUMENT_LIST = new NonTerminal("ARGUMENT_LIST"),
                POSITIONAL_ARGUMENTS = new NonTerminal("POSITIONAL_ARGUMENTS"),
                STARRED_AND_KEYWORDS = new NonTerminal("STARRED_AND_KEYWORDS"),
                KEYWORDS_ARGUMENTS = new NonTerminal("KEYWORDS_ARGUMENTS"),
                KEYWORD_ITEM = new NonTerminal("KEYWORD_ITEM");


            this.Root = INICIO;

            INICIO.Rule = INSTRUCCIONES;

            INSTRUCCIONES.Rule = MakePlusRule(INSTRUCCIONES, INSTRUCCION);

            INSTRUCCION.Rule = CLASEDEF 
                            | FUNCDEF
                            | IMPORT_STMT + Eos
                            | PRINT + Eos
                            | PRINTTABLA + Eos
                            | IF_STMT
                            | WHILE_STMT
                            | FOR_STMT
                            | GLOBAL_STMT + Eos
                            | NONLOCAL_STMT + Eos
                            | DEL_STMT + Eos
                            | ASSIGNMENT_STMT + Eos
                            | AUGMENTED_ASSIGNMENT_STMT + Eos
                            | EXPRESSION_STMT + Eos
                           ;

            CLASEDEF.Rule = class_ + identifier + colon + Eos + BLOQUE_SUP;

            BLOQUE_SUP.Rule = Indent + INSTRUCCIONES_SUP + Dedent;

            INSTRUCCIONES_SUP.Rule = MakePlusRule(INSTRUCCIONES_SUP, INSTRUCCION_SUP);

            INSTRUCCION_SUP.Rule = CLASEDEF
                            | FUNCDEF
                            | PRINT + Eos
                            | PRINTTABLA + Eos
                            | IF_STMT
                            | WHILE_STMT
                            | FOR_STMT
                            | PASS_STMT + Eos
                            | GLOBAL_STMT + Eos
                            | NONLOCAL_STMT + Eos
                            | DEL_STMT + Eos
                            | ASSIGNMENT_STMT + Eos
                            | AUGMENTED_ASSIGNMENT_STMT + Eos
                            | EXPRESSION_STMT + Eos
                           ;

            BLOQUE.Rule = Indent + SENTENCIAS + Dedent;

            SENTENCIAS.Rule = MakePlusRule(SENTENCIAS, SENTENCIA);

            SENTENCIA.Rule = PRINT + Eos
                            | PRINTTABLA + Eos
                            | IF_STMT
                            | WHILE_STMT
                            | FOR_STMT
                            | RETURN_STMT + Eos
                            | BREAK_STMT + Eos
                            | CONTINUE_STMT + Eos
                            | PASS_STMT + Eos
                            //| CLASEDEF
                            | GLOBAL_STMT + Eos
                            | NONLOCAL_STMT + Eos
                            | DEL_STMT + Eos
                            | ASSIGNMENT_STMT + Eos
                            | AUGMENTED_ASSIGNMENT_STMT + Eos
                            | EXPRESSION_STMT + Eos
                            ;

            IMPORT_STMT.Rule = import_ + stringliteral
                             | import_ + stringliteral2;

            TYPE.Rule = int_ | double_ | string_ | String_ | boolean_ | identifier | dictionary_ | list_ | tup_
                        |void_;

            PRINT.Rule = print_ + leftPar + STARRED_EXPRESSION + rightPar; //CORR

            PRINTTABLA.Rule = printTabla_ + leftPar + rightPar;

            IF_STMT.Rule = IF_LIST + else_ + colon + Eos + BLOQUE //CORR
                    | IF_LIST;

            IF_LIST.Rule = IF_LIST + elif_ + EXPRESSION + colon + Eos + BLOQUE //CORR
                          | if_ + EXPRESSION + colon + Eos + BLOQUE;

            WHILE_STMT.Rule = while_ + EXPRESSION + colon + Eos + BLOQUE //CORR
                            | while_ + EXPRESSION + colon + Eos + BLOQUE + else_ + colon + Eos + BLOQUE;

            FOR_STMT.Rule = for_ + TARGET_LIST + in_ + EXPRESSION_LIST + colon + Eos + BLOQUE //CORR
                          | for_ + TARGET_LIST + in_ + EXPRESSION_LIST + colon + Eos + BLOQUE + else_ + colon + Eos + BLOQUE;

            RETURN_STMT.Rule = return_ | return_ + EXPRESSION_LIST; //CORR

            BREAK_STMT.Rule = break_; //CORR

            CONTINUE_STMT.Rule = continue_; //CORR

            PASS_STMT.Rule = pass_; //CORR

            FUNCDEF.Rule = def_ + TYPE + identifier + leftPar + PARAMETER_LIST + rightPar + colon + Eos + BLOQUE //CORR bloque clase
                        | def_ + TYPE + identifier + leftPar + rightPar + colon + Eos + BLOQUE;

            PARAMETER_LIST.Rule = PARAMETER_LIST + comma + TYPE + identifier
                                | TYPE + identifier; //CORR

            GLOBAL_STMT.Rule = global_ + TYPE + ID_LIST; //CORR

            NONLOCAL_STMT.Rule = nonlocal_ + TYPE + ID_LIST; //CORR

            ID_LIST.Rule = ID_LIST + comma + identifier //CORR
                        | identifier;

            DEL_STMT.Rule = del_ + TARGET_LIST; //CORR

            ASSIGNMENT_STMT.Rule = TARGET_LIST + equal + ASSIGNMENT_LIST //CORR
                                | TYPE + TARGET_LIST + equal + ASSIGNMENT_LIST; 

            ASSIGNMENT_LIST.Rule = MakePlusRule(ASSIGNMENT_LIST, equal, STARRED_EXPRESSION); //CORR

            TARGET_LIST.Rule = MakePlusRule(TARGET_LIST, comma, TARGET); //probar con [","] //CORR

            TARGET.Rule = identifier //CORR
                        //| leftPar + TARGET_LIST + rightPar //PUENDE SER VACIO
                        //| leftCor + TARGET_LIST + leftPar //PUENDE SER VACIO
                        | ATTRIBUTEREF
                        | SUBSCRIPTION //ACCESO ARREGLO
                        | SLICING; 

            AUGMENTED_ASSIGNMENT_STMT.Rule = AUGTARGET + AUG_OPERATOR + EXPRESSION_LIST; //CORR

            AUGTARGET.Rule = identifier | ATTRIBUTEREF | SUBSCRIPTION | SLICING; //CORR

            AUG_OPERATOR.Rule = masEqual | menosEqual | porEqual | divisionEqual //CORR
                                | moduloEqual | potenciaEqual | floorEqual;

            EXPRESSION_STMT.Rule = STARRED_EXPRESSION; //CORR

            CONDITIONAL_EXPRESSION.Rule = OR_TEST | OR_TEST + if_ + OR_TEST + else_ + EXPRESSION; //CORR

            EXPRESSION.Rule = LAMBDA_EXP //CORR
                            | CONDITIONAL_EXPRESSION;

            EXPRESSION_NOCOND.Rule = OR_TEST | LAMBDA_EXP_NOCOND; //CORR

            LAMBDA_EXP.Rule = lambda_ + PARAMETER_LIST + colon + EXPRESSION; //CORR

            LAMBDA_EXP_NOCOND.Rule = lambda_ + PARAMETER_LIST + colon + EXPRESSION_NOCOND; //CORR

            OR_TEST.Rule = AND_TEST | OR_TEST + or_ + AND_TEST; //CORR

            AND_TEST.Rule = NOT_TEST | AND_TEST + and_ + NOT_TEST; //CORR

            NOT_TEST.Rule = COMPARISON | not_ + NOT_TEST; //CORR

            COMPARISON.Rule = OR_EXPR | COMPARISON + COMP_OPERATOR + OR_EXPR; //CORR

            COMP_OPERATOR.Rule = menorque | mayorque | igual | mayorigual | menorigual //CORR
                                | diferente | is_ | in_ | is_ + not_ | not_ + in_;

            OR_EXPR.Rule = XOR_EXPR | OR_EXPR + o_ + XOR_EXPR; //No implementar

            XOR_EXPR.Rule = AND_EXPR | XOR_EXPR + xor_ + AND_EXPR; //No implementar

            AND_EXPR.Rule = SHIFT_EXPR | AND_EXPR + y_ + SHIFT_EXPR; //No implementar

            SHIFT_EXPR.Rule = A_EXPR | SHIFT_EXPR + leftShift + A_EXPR | SHIFT_EXPR + rightShift + A_EXPR; //No implementar

            A_EXPR.Rule = M_EXPR | A_EXPR + mas + M_EXPR | A_EXPR + menos + M_EXPR; //CORR

            M_EXPR.Rule = U_EXPR | M_EXPR + por + U_EXPR | M_EXPR + floor + U_EXPR //CORR
                        | M_EXPR + division + U_EXPR | M_EXPR + modulo + U_EXPR;

            U_EXPR.Rule = POWER | menos + U_EXPR | mas + U_EXPR; //CORR

            POWER.Rule = PRIMARY | PRIMARY + potencia + U_EXPR; //CORR

            PRIMARY.Rule = ATOM | ATTRIBUTEREF | SUBSCRIPTION | SLICING | CALL; //CORR

            ATOM.Rule = identifier | LITERAL | ENCLOSURE; //CORR

            LITERAL.Rule = number | stringliteral | stringliteral2 | none_ | true_ | false_
                            |True_ | False_ | None_; //CORR

            ENCLOSURE.Rule = PARENTH_FORM | LIST_DISPLAY | DICT_DISPLAY | SET_DISPLAY //CORR
                            /*| GENERATOR_EXPRESSION*/;

            PARENTH_FORM.Rule = leftPar + STARRED_EXPRESSION + rightPar //TUPLAS CORR
                               | leftPar + rightPar;

            LIST_DISPLAY.Rule = leftCor + (STARRED_LIST | COMPREHENSION) + rightCor //CORR
                               |leftCor + rightCor;

            /*x for x in 'abracadabra' if x not in 'abc'*/
            COMPREHENSION.Rule = EXPRESSION + COMP_FOR; //CORR

            COMP_FOR.Rule = for_ + TARGET_LIST + in_ + OR_TEST //CORR
                            | for_ + TARGET_LIST + in_ + OR_TEST + COMP_ITER;

            COMP_ITER.Rule = COMP_FOR | COMP_IF; //CORR

            COMP_IF.Rule = if_ + EXPRESSION_NOCOND //CORR
                          | if_ + EXPRESSION_NOCOND + COMP_ITER;
            /*------------------------------------------*/

            SET_DISPLAY.Rule = leftLla + (STARRED_LIST | COMPREHENSION) + rightLla; //CONJUNTOS CORR

            DICT_DISPLAY.Rule = leftLla + (KEY_DATUM_LIST | DICT_COMPREHENSION) + rightLla  //DICCIONARIOS CORR
                                | leftLla + rightLla; 

            KEY_DATUM_LIST.Rule = MakePlusRule(KEY_DATUM_LIST, comma, KEY_DATUM);  //CORR [","]

            KEY_DATUM.Rule = EXPRESSION + colon + EXPRESSION; //CORR

            DICT_COMPREHENSION.Rule = EXPRESSION + colon + EXPRESSION + COMP_FOR; //CORR

            EXPRESSION_LIST.Rule = EXPRESSION_LIST_COMMA + comma //CORR
                                | EXPRESSION_LIST_COMMA;

            EXPRESSION_LIST_COMMA.Rule = EXPRESSION_LIST_COMMA + comma + EXPRESSION
                                        |EXPRESSION; //CORR

            STARRED_LIST.Rule = STARRED_LIST_COMMA + comma //CORR
                            | STARRED_LIST_COMMA;

            STARRED_LIST_COMMA.Rule = MakePlusRule(STARRED_LIST_COMMA, comma, STARRED_ITEM); //CORR

            STARRED_EXPRESSION.Rule = EXPRESSION | STARRED_LIST; //CORR

            STARRED_ITEM.Rule = EXPRESSION //CORR
                               | por + OR_EXPR;

            //GENERATOR_EXPRESSION.Rule = leftPar + EXPRESSION + COMP_FOR + rightPar;

            ATTRIBUTEREF.Rule = PRIMARY + dot + identifier; //CORR

            SUBSCRIPTION.Rule = PRIMARY + leftCor + EXPRESSION_LIST + rightCor; //CORR

            SLICING.Rule = PRIMARY + leftCor + SLICE_LIST + rightCor; //CORR

            SLICE_LIST.Rule = MakePlusRule(SLICE_LIST, comma, SLICE_ITEM); //CORR

            SLICE_ITEM.Rule = EXPRESSION | PROPER_SLICE; //CORR

            PROPER_SLICE.Rule = EXPRESSION + colon + EXPRESSION //CORR
                                | colon;

            CALL.Rule = PRIMARY + leftPar + (ARGUMENT_LIST /*| COMPREHENSION*/) + rightPar
                      | PRIMARY + leftPar + rightPar; //FALTA EL [","]

            ARGUMENT_LIST.Rule = POSITIONAL_ARGUMENTS //CORR
                                | POSITIONAL_ARGUMENTS + comma + STARRED_AND_KEYWORDS
                                | POSITIONAL_ARGUMENTS + comma + STARRED_AND_KEYWORDS + comma + KEYWORDS_ARGUMENTS
                                | STARRED_AND_KEYWORDS
                                | STARRED_AND_KEYWORDS + comma + KEYWORDS_ARGUMENTS
                                | KEYWORDS_ARGUMENTS;

            POSITIONAL_ARGUMENTS.Rule = POSITIONAL_ARGUMENTS + comma + EXPRESSION
                                       | EXPRESSION; //CORR

            STARRED_AND_KEYWORDS.Rule = STARRED_AND_KEYWORDS + comma + (EXPRESSION | KEYWORD_ITEM) //CORR
                                       | (EXPRESSION | KEYWORD_ITEM);

            KEYWORDS_ARGUMENTS.Rule = KEYWORDS_ARGUMENTS + comma + (KEYWORD_ITEM | potencia + EXPRESSION) //CORR
                                    | (KEYWORD_ITEM | potencia + EXPRESSION);

            KEYWORD_ITEM.Rule = identifier + equal + EXPRESSION; //CORR

            //LanguageFlags = LanguageFlags.NewLineBeforeEOF;

            // 4. Token filters - created in a separate method CreateTokenFilters
            //    we need to add continuation symbol to NonGrammarTerminals because it is not used anywhere in grammar
            NonGrammarTerminals.Add(ToTerm(@"\"));

            // 7. Error recovery rule
            //SENTENCIA.ErrorRule = SyntaxError + Eos;
            //CLASEDEF.ErrorRule = SyntaxError + Dedent;
            //BLOQUE.ErrorRule = SyntaxError + Dedent;
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
