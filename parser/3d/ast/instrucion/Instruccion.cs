using Compilador.parser._3d.ast.entorno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser._3d.ast.instrucion
{
    abstract class Instruccion : NodoAST
    {
        public Instruccion(int linea, int columna) : base(linea, columna) { }

        public abstract Object Ejecutar(Entorno e);
    }
}
