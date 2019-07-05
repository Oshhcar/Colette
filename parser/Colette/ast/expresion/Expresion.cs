using Compilador.parser.Colette.ast.entorno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser.Colette.ast.expresion
{
    abstract class Expresion : Nodo
    {
        public Expresion(int linea, int columna) : base(linea, columna)
        { }

        public abstract Tipo GetTipo();

        public abstract Result GetC3D(Ent e);


    }
}
