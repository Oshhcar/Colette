using Compilador.parser._3d;
using Compilador.parser._3d.ast;
using Compilador.parser.Collete;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compilador
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Btn_graficar_Click(object sender, EventArgs e)
        {
            if (!rtb_entrada.Text.Equals(string.Empty)) {

                Analizador analizador = new Analizador();
                string entrada = this.rtb_entrada.Text.Replace("\\", "\\\\");

                if (analizador.AnalizarEntrada(entrada))
                {
                    if (analizador.Raiz.ParserMessages.Count > 0)
                    {
                        MessageBox.Show("El archivo tiene errores.");
                        this.ReporteErrores(analizador.Raiz);
                    }

                    GraficarArbol(analizador.Raiz.Root);

                }
                else
                {
                    MessageBox.Show("El archivo tiene errores.");
                    this.ReporteErrores(analizador.Raiz);
                }
            }
        }

        public void GraficarArbol(ParseTreeNode raiz)
        {
            string archivo = "arbol.dot";
            FileStream stream = new FileStream(archivo, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine("digraph arbol{");
            writer.WriteLine("rankdir=UD;");
            writer.WriteLine("node [shape = box, style=filled, color=blanchedalmond];");
            writer.WriteLine("edge[color=chocolate3];");
            writer.WriteLine(GraficarNodo(raiz));
            writer.WriteLine("}");
            writer.Close();

            try
            {
                var command = string.Format("dot -Tpng arbol.dot  -o arbol.png");
                var procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/C " + command);
                var proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                proc.WaitForExit();

                //var appname = "Microsoft.Photos.exe";
                // Process.Start(appname, "arbol.jpg");

                Process.Start("arbol.png");
            }
            catch (Exception x)
            {
                MessageBox.Show("Error en graficar! \n" + x);
            }
        }

        public string GraficarNodo(ParseTreeNode raiz) 
        {
            string nodoString = "";
            //MessageBox.Show("label = "+raiz.ToString());
            string label = raiz.ToString();

            nodoString = "nodo" + raiz.GetHashCode() + "[label=\"" + label + " \", fillcolor=\"blanchedalmond\", style =\"filled\", shape=\"box\"]; \n";
            if (raiz.ChildNodes.Count > 0)
            {
                ParseTreeNode[] hijos = raiz.ChildNodes.ToArray();
                for (int i = 0; i < raiz.ChildNodes.Count; i++)
                {
                    nodoString += GraficarNodo(hijos[i]);
                    nodoString += "\"nodo" + raiz.GetHashCode() + "\"-> \"nodo" + hijos[i].GetHashCode() + "\" \n";
                }
            }

            return nodoString;
        }

        public void ReporteErrores(ParseTree raiz)
        {
            string archivo = "reporteError.html";
            FileStream stream = new FileStream(archivo, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine("<!DOCTYPE html PUBLIC \" -//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
            writer.WriteLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            writer.WriteLine("<head>");
            writer.WriteLine("<meta http-equiv=\"Content - Type\" content=\"text / html; charset = utf - 8\" />");
            writer.WriteLine("<title>Errores</title>");
            writer.WriteLine("<style type=\"text/css\">");
            writer.WriteLine("  p { color: white; font-family: Arial; text-align:center; font-size:28px; }");
            writer.WriteLine("  h1 {color: white; font-family: Arial; color:#C00; font-size:36px; text-align:center;}");
            writer.WriteLine("  h2 {color:#FF0; font-family:Arial; font-size:36px; text-align:center;}");
            writer.WriteLine("  table{color:#FFF; font-family:Arial; border-color:#9F3;}");
            writer.WriteLine("</style>");
            writer.WriteLine("</head>");
            writer.WriteLine("<body bgcolor=\"#000000\">");
            writer.WriteLine("<h1>Errores</h1>");
            writer.WriteLine("</b>");
            writer.WriteLine("</b>");
            writer.WriteLine("</b>");

            writer.WriteLine("<table align=\"center\" border=\"5\">");
            writer.WriteLine("<tr>\n<th>Error</th>\n<th>Linea</th>\n<th>Columna</th>\n<th>Mensaje</th>\n</tr>");

            for (int i = 0; i < raiz.ParserMessages.Count(); i++)
            {
                writer.WriteLine("<tr>");
                writer.WriteLine("<td>" + raiz.ParserMessages.ElementAt(i).Level.ToString() + "</td>");
                writer.WriteLine("<td>" + (raiz.ParserMessages.ElementAt(i).Location.Line+1) + "</td>");
                writer.WriteLine("<td>" + (raiz.ParserMessages.ElementAt(i).Location.Column+1) + "</td>");
                writer.WriteLine("<td>" + raiz.ParserMessages.ElementAt(i).Message + "</td>");
                writer.WriteLine("</tr>");
            }
            writer.WriteLine("</table>");

            writer.WriteLine("</body>");
            writer.WriteLine("</html>");

            writer.Close();

            Process.Start("reporteError.html");
        }

        private void Btn_compilar_Click(object sender, EventArgs e)
        {
            if (!rtb_entrada.Text.Equals(string.Empty))
            {

                Analizador analizador = new Analizador();
                string entrada = this.rtb_entrada.Text.Replace("\\", "\\\\");

                if (analizador.AnalizarEntrada(entrada))
                {
                    MessageBox.Show("Archivo sin errores.");
                    AST ast = (AST)analizador.GenerarAST(analizador.Raiz.Root);
                    ast.ejecutar();

                }
                else
                {
                    MessageBox.Show("El archivo tiene errores.");
                    this.ReporteErrores(analizador.Raiz);
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!rtb_entrada.Text.Equals(string.Empty))
            {

                AnalizadorCollete analizador = new AnalizadorCollete();
                string entrada = this.rtb_entrada.Text;//.Replace("\\", "\\\\");
                
                if (analizador.AnalizarEntrada(entrada))
                {
                    MessageBox.Show("Archivo sin errores.");

                }
                else
                {
                    MessageBox.Show("El archivo tiene errores.");
                    this.ReporteErrores(analizador.Raiz);
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (!rtb_entrada.Text.Equals(string.Empty))
            {

                AnalizadorCollete analizador = new AnalizadorCollete();
                string entrada = this.rtb_entrada.Text.Replace("\\", "\\\\");

                if (analizador.AnalizarEntrada(entrada))
                {
                    if (analizador.Raiz.ParserMessages.Count > 0)
                    {
                        MessageBox.Show("El archivo tiene errores.");
                        this.ReporteErrores(analizador.Raiz);
                    }

                    GraficarArbol(analizador.Raiz.Root);

                }
                else
                {
                    MessageBox.Show("El archivo tiene errores.");
                    this.ReporteErrores(analizador.Raiz);
                }
            }
        }
    }
}
