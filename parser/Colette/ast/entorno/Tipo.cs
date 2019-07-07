using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser.Colette.ast.entorno
{
    class Tipo
    {
        public Tipo(Type tip)
        {
            Tip = tip;
        }

        public Tipo(string objeto)
        {
            Tip = Type.OBJECT;
            Objeto = objeto;
        }

        public Type Tip { get; set; }
        public string Objeto { get; set; }

        public bool IsInt() { return Tip == Type.INT; }
        public bool IsDouble() { return Tip == Type.DOUBLE; }
        public bool IsString() { return Tip == Type.STRING; }
        public bool IsBoolean() { return Tip == Type.BOOLEAN; }
        public bool IsVoid() { return Tip == Type.VOID; }
        public bool IsObject() { return Tip == Type.OBJECT; }
        public bool IsNone() { return Tip == Type.NONE; }
        public bool IsIndefinido() { return Tip == Type.INDEFINIDO; }
        public bool IsNumeric() { return Tip == Type.INT || Tip == Type.DOUBLE; }

        public enum Type
        {
            INT,
            DOUBLE,
            STRING,
            BOOLEAN,
            VOID,
            OBJECT,
            NONE,
            INDEFINIDO /*error*/
        }
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
