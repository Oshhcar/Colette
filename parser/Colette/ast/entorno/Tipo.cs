using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser.Colette.ast.entorno
{
    enum Tipo
    {
        INT,
        DOUBLE,
        VOID,
        INDEF
    }

    enum Rol
    {
        METHOD,
        FUNCION,
        RETURN,
        PARAMETER,
        LOCAL
    }
}
