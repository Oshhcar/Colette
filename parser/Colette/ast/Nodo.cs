using Compilador.parser.Colette.ast.entorno;
using Compilador.parser.Colette.ast.instruccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compilador.parser.Colette.ast
{
    class Nodo
    {
        public Nodo(int linea, int columna)
        {
            Linea = linea;
            Columna = columna;
        }

        public int Linea { get; set; }
        public int Columna { get; set; }

        public static int Etiquetas { get; set; }
        public static int Temporales { get; set; }
        public static int H { get; set; }
        public static bool Debug { get; set; }
        public string NuevaEtiqueta() { return "L"+(++Etiquetas); }
        public string NuevoTemporal() { return "t"+(++Temporales); }

        public void Debugger(Ent e, string inst)
        {
            if (Debug)
            {
                DialogResult result = MessageBox.Show("Línea: " + Linea + " Columna: " + Columna + "\n" +
                    "Instrucción: " + inst + "\n\n¿Desea ver el entorno actual?\n", "Debugger", MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Yes)
                {
                    PrintTabla tabla = new PrintTabla(Linea, Columna);
                    tabla.GetC3D(e, false, false, false, false, null);

                }
                else if (result == DialogResult.No)
                {

                }
                else
                {
                    Debug = false;
                }
            }
        }
    }
}
