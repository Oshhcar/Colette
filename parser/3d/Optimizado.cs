using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser._3d
{
    class Optimizado
    {
        public Optimizado(int ocurrencia, int codigo, int regla)
        {
            Ocurrencia = ocurrencia;
            Codigo = codigo;
            Regla = regla;
        }

        public int Ocurrencia { get; set; }
        public int Codigo { get; set; }
        public int Regla { get; set; }
    }
}
