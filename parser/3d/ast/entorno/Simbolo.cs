using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser._3d.ast.entorno
{
    class Simbolo
    {
        public Simbolo(string id, object valor, Tipo tipo)
        {
            Id = id;
            Valor = valor;
            Tipo = tipo;
        }

        public string Id { get; set; }
        public Object Valor { get; set; }
        public Tipo Tipo { get; set; }
    }
}
