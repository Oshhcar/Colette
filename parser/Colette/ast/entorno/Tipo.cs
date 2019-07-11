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
            SubTip = Type.INDEFINIDO;
        }

        public Tipo(string objeto)
        {
            Tip = Type.OBJECT;
            Objeto = objeto;
        }

        public Type Tip { get; set; }
        public string Objeto { get; set; }
        public Type SubTip { get; set;}

        public bool IsInt() { return Tip == Type.INT; }
        public bool IsDouble() { return Tip == Type.DOUBLE; }
        public bool IsString() { return Tip == Type.STRING; }
        public bool IsBoolean() { return Tip == Type.BOOLEAN; }
        public bool IsVoid() { return Tip == Type.VOID; }
        public bool IsObject() { return Tip == Type.OBJECT; }
        public bool IsList() { return Tip == Type.LIST; }
        public bool IsNone() { return Tip == Type.NONE; }
        public bool IsIndefinido() { return Tip == Type.INDEFINIDO; }
        public bool IsNumeric() { return Tip == Type.INT || Tip == Type.DOUBLE; }

        public override string ToString()
        {
            switch (Tip)
            {
                case Type.INT:
                    return "int";
                case Type.DOUBLE:
                    return "double";
                case Type.STRING:
                    return "string";
                case Type.BOOLEAN:
                    return "boolean";
                case Type.OBJECT:
                    return "object";
                case Type.VOID:
                    return "void";
                case Type.LIST:
                    return "list";
                default:
                    return "indefinido";
            }
        }
        
        public enum Type
        {
            INT,
            DOUBLE,
            STRING,
            BOOLEAN,
            VOID,
            LIST,
            OBJECT,
            NONE,
            INDEFINIDO /*error*/
        }
    }

    enum Rol
    {
        //METHOD,
        CLASE,
        FUNCION,
        RETURN,
        PARAMETER,
        LOCAL,
        GLOBAL
    }
}
