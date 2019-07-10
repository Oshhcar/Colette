using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser.Colette.ast.entorno
{
    class Sim
    {
        public Sim(string id, Tipo tipo, Rol rol, int tam, int pos, string ambito, int numParam, int tipoParam)
        {
            Id = id;
            Tipo = tipo;
            Rol = rol;
            Tam = tam;
            Pos = pos;
            Ambito = ambito;
            NumParam = numParam;
            TipoParam = tipoParam;
            Entorno = null;
            IsAtributo = false;
        }

        public string Id { get; set; }
        public Tipo Tipo { get; set; }
        public Rol Rol { get; set; }
        public int Tam { get; set; }
        public int Pos { get; set; }
        public string Ambito { get; set; }
        public int NumParam { get; set; }
        public int TipoParam { get; set; }
        public Ent Entorno { get; set; } //para funciones y clases
        public string Firma { get; set; }
        public bool IsAtributo { get; set; }
    }
}
