using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser.Collete
{
    class AnalizadorCollete
    {
        public ParseTree Raiz { get; set; }

        public bool AnalizarEntrada(String entrada)
        {
            GramaticaCollete gramatica = new GramaticaCollete();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(entrada);
            this.Raiz = arbol;

            if (arbol.Root != null)
                return true;

            return false;
        }

    }
}
