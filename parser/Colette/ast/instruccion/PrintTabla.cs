using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Compilador.parser.Colette.ast.entorno;

namespace Compilador.parser.Colette.ast.instruccion
{
    class PrintTabla : Instruccion
    {
        public PrintTabla(int linea, int columna) : base(linea, columna)
        {

        }

        public override Result GetC3D(Ent e, bool funcion, bool ciclo, bool isDeclaracion, LinkedList<Error> errores)
        {
            string cadena = "digraph G \n{\n";
            cadena += "node[ shape = none, fontname = \"Arial\" ];\n";
            cadena += "rankdir=LR;\n";
            cadena += "edge [color=red, dir=back, style=bold];\n\n\n";

            Ent actual = e;
            int i = 0;
            while (actual != null)
            {
                cadena += "entorno" + i + "[ label =<\n";
                cadena += "<TABLE BORDER=\"0\" CELLBORDER=\"1\" CELLSPACING=\"0\" CELLPADDING=\"4\">\n";
                cadena += "<TR>\n";
                cadena += "<TD COLSPAN = \"5\"> Entorno " + i + " </TD>\n";
                cadena += "</TR>\n";
                cadena += "<TR>\n";
                cadena += "<TD> Id </TD>\n";
                cadena += "<TD> Tipo </TD>\n";
                cadena += "<TD> Rol </TD>\n";
                cadena += "<TD> Tam </TD>\n";
                cadena += "<TD> Pos </TD>\n";
                cadena += "<TD> Ambito </TD>\n";
                cadena += "<TD> No_parametros </TD>\n";
                cadena += "<TD> Tipo_parametro </TD>\n";
                cadena += "</TR>\n";

                foreach (Sim s in actual.Simbolos)
                {
                    cadena += "<TR>\n";
                    cadena += "<TD> " + s.Id + " </TD>\n";
                    cadena += "<TD> " + s.Tipo.ToString() + " </TD>\n";
                    cadena += "<TD> " + s.Rol.ToString() + " </TD>\n";
                    cadena += "<TD> " + s.Tam + " </TD>\n";
                    cadena += "<TD> " + (s.Pos == -1? "-":s.Pos+"")+ " </TD>\n";
                    cadena += "<TD> " + s.Ambito + " </TD>\n";
                    cadena += "<TD> " + (s.NumParam == -1 ? "-" : s.NumParam + "")+ " </TD>\n";
                    cadena += "<TD> " + (s.TipoParam == -1 ? "-" : s.TipoParam + "") + " </TD>\n";
                    cadena += "</TR>\n";
                }

                cadena += "</TABLE>>];\n\n\n";

                i++;
                actual = actual.Padre;
                if (actual != null)
                {
                    cadena += "entorno" + i + " -> " + "entorno" + (i - 1) + ";\n\n";
                }
            }
            cadena += "\n\n}";

            string archivo = "tabla.dot";

            StreamWriter writer = null;

            try
            {
                writer = new StreamWriter(archivo);
                writer.Write(cadena);
                writer.Close();

                var command = string.Format("dot -Tpng tabla.dot  -o tabla.png");
                var procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/C " + command);
                var proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                proc.WaitForExit();

                if (proc.ExitCode == 1)
                {
                    MessageBox.Show("Error al graficar", "Graphviz");
                }
                else
                {
                    Process.Start("tabla.png");
                }

            }
            catch (Exception x)
            {
                MessageBox.Show("Error en graficar! \n" + x);
            }

            return null;
        }
    }
}
