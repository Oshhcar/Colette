using Compilador.parser._3d.ast.entorno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser._3d.ast.expresion
{
    abstract class Expresion : NodoAST
    {
        public Expresion(int linea, int columna) : base(linea, columna) { }

        public abstract Tipo GetTipo(Entorno e) ;
        public abstract Object GetValor();
    }
}
