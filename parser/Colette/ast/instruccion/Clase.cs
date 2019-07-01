using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser.Colette.ast.entorno;

namespace Compilador.parser.Colette.ast.instruccion
{
    class Clase : Instruccion
    {
        public Clase(string id, Bloque bloque, int linea, int columna) : base(linea, columna)
        {
            Id = id;
            Bloque = bloque;
        }

        public string Id { get; set; }
        public Bloque Bloque { get; set; }

        public override Result GetC3D(Ent e)
        {
            return Bloque.GetC3D(e);
        }
    }
}
