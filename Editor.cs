using Compilador.parser._3d;
using Compilador.parser._3d.ast;
using Compilador.parser.Colette.ast;
using Compilador.parser.Collete;
using FastColoredTextBoxNS;
using Irony;
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
    public partial class Editor : Form
    {
        public int Archivo { get; set; }

        public Editor()
        {
            InitializeComponent();
            Archivo = 0;
        }

        private void NuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Archivo++;
            NuevoArchivo();
        }

        private void AbrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirArchivo();
        }


        private void GuardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GuardarArchivo();
        }

        private void GuardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GuardarComo();
        }

        private void CerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarArchivo(tabArchivo.SelectedIndex);
        }

        private void CerrarTodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarTodo();
        }

        private void DeshacerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UndoArchivo();
        }

        private void CopiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopiarArchivo();
        }

        private void PegarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PegarArchivo();
        }

        private void CortarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CortarArchivo();
        }

        private void SeleccionarTodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeleccionarTodo();
        }

        private void AcercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Colette 2019\nOscar Morales\noskralberto14@gmail.com", "Acerca de");
        }

        private void EjecutarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ejecutar3D();
        }

        private void TraducirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TraducirColette();
        }

        private void NuevoArchivo()
        {
            TabPage nPage = MakeTextPane(null);
            nPage.Text = "nuevo" + Archivo + ".colette";

            tabArchivo.Controls.Add(nPage);
            tabArchivo.SelectedTab = nPage;
            lbLinea.Text = "Ln: 1 Col: 1";

            //coloredTextBox.Selection.Start = new Place(0, 0);//select first char of line iLine
            //coloredTextBox.DoSelectionVisible();//scroll to selection
        }

        private void ColoredTextBox_SelectionChanged(object sender, EventArgs e)
        {
            Range selection = ((FastColoredTextBox)sender).Selection;
            lbLinea.Text = "Ln: " + (selection.Start.iLine + 1) + " Col: " + (selection.Start.iChar + 1);
        }

        private TabPage MakeTextPane(string path)
        {
            
            FastColoredTextBox ctbArchivo = new FastColoredTextBox();
            ctbArchivo.BorderStyle = BorderStyle.Fixed3D;
            ctbArchivo.AutoScrollMinSize = new Size(25, 15);
            ctbArchivo.Dock = DockStyle.Fill;
            ctbArchivo.Language = Language.CSharp;
            ctbArchivo.IndentBackColor = Color.LightGray;
            ctbArchivo.LineNumberColor = Color.Black;
            ctbArchivo.SelectionChanged += ColoredTextBox_SelectionChanged;

            TabControl tab = new TabControl();

            TabPage c3d = new TabPage();
            c3d.Text = "Código 3d";
            FastColoredTextBox ctb3D = new FastColoredTextBox();
            ctb3D.BorderStyle = BorderStyle.Fixed3D;
            ctb3D.AutoScrollMinSize = new Size(25,15);
            ctb3D.Dock = DockStyle.Fill;
            ctb3D.Language = Language.Custom;
            ctb3D.SelectionChanged += ColoredTextBox_SelectionChanged;
            c3d.Controls.Add(ctb3D);
            tab.Controls.Add(c3d);

            TabPage AST = new TabPage();
            AST.Text = "AST";
            tab.Controls.Add(AST);
           

            TabPage ASM = new TabPage();
            ASM.Text = "ASM";
            FastColoredTextBox ctbAsm = new FastColoredTextBox();
            ctbAsm.BorderStyle = BorderStyle.Fixed3D;
            ctbAsm.AutoScrollMinSize = new Size(25, 15);
            ctbAsm.Dock = DockStyle.Fill;
            ctbAsm.Language = Language.Custom;
            ctbAsm.SelectionChanged += ColoredTextBox_SelectionChanged;
            ASM.Controls.Add(ctbAsm);
            tab.Controls.Add(ASM);

            tab.Dock = DockStyle.Right;

            Splitter sp = new Splitter();
            sp.Dock = DockStyle.Right;
            sp.Cursor = Cursors.VSplit;

            TabPage nPage = new TabPage();
            nPage.Controls.Add(ctbArchivo);
            nPage.Controls.Add(sp);
            nPage.Controls.Add(tab);

            if (path == null)
            {
                ctbArchivo.Text = "";
                ctbArchivo.Name = "";
            }
            else
            {
                StreamReader reader = null;

                try
                {
                    reader = new StreamReader(path);
                    ctbArchivo.Text = null;
                    ctbArchivo.Text = reader.ReadToEnd();
                    ctbArchivo.Selection = ctbArchivo.GetRange(0, 0);

                }
                catch(Exception e)
                {
                    MessageBox.Show("Error al abrir el archivo. \n" + e.Message,"Error");
                }
                finally
                {
                    if (reader != null)
                        reader.Close();
                }
            }
            return nPage;
        }

        private void AbrirArchivo()
        {
            openFileDialog1.Filter = "Archivos de colette(*.colette) | *.colette| Todos los archivos(*.*)| *.*";
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            /*Limpiar salida*/

            if (Path.GetExtension(openFileDialog1.FileName).ToLower().Equals(".colette"))
            {
                int i = tabArchivo.SelectedIndex;
                if (i != -1)
                {
                    LeerArchivo(openFileDialog1.FileName);
                }
                else
                {
                    NuevoArchivo();
                    LeerArchivo(openFileDialog1.FileName);
                }
            }
            else
            {
                MessageBox.Show("La extensión del archivo debe ser .colette.", "Error");
                AbrirArchivo();
            }
        }

        private void LeerArchivo(string path)
        {
            TabPage sPage = tabArchivo.SelectedTab;
            FastColoredTextBox ctbArchivo = (FastColoredTextBox)sPage.Controls[0];

            if (ctbArchivo.Text.Equals("") && ctbArchivo.Name.Equals(""))
            {
                StreamReader reader = null;

                try
                {
                    reader = new StreamReader(path);
                    ctbArchivo.Text = null;
                    ctbArchivo.ClearUndo();
                    ctbArchivo.Text = reader.ReadToEnd();
                    ctbArchivo.SetVisibleState(0, FastColoredTextBoxNS.VisibleState.Visible);
                    ctbArchivo.Selection = ctbArchivo.GetRange(0, 0);
                    ctbArchivo.Name = path;
                    ctbArchivo.IsChanged = false;
                    sPage.Text = Path.GetFileName(path);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error al abrir el archivo. \n" + e.Message + "Error");
                }
                finally
                {
                    if (reader != null)
                        reader.Close();
                }
            }
            else
            {
                NuevoArchivo();
                LeerArchivo(path);
            }

        }

        private void GuardarArchivo()
        {
            int i = tabArchivo.SelectedIndex;

            if (i != -1)
            {
                TabPage sPage = tabArchivo.SelectedTab;
                FastColoredTextBox ctbArchivo = (FastColoredTextBox)sPage.Controls[0];

                if (!ctbArchivo.Name.Equals(""))
                {
                    StreamWriter writer = null;
                    try
                    {
                        writer = new StreamWriter(ctbArchivo.Name);
                        writer.Write(ctbArchivo.Text);
                        MessageBox.Show("Se guardo el archivo correctamente en " + ctbArchivo.Name,"Mensaje");

                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Error al guardar el archivo \n" + e.Message, "Error");
                    }
                    finally
                    {
                        if (writer != null)
                            writer.Close();
                    }

                }
                else
                {
                    GuardarComo();
                }
            }
            else
            {
                MessageBox.Show("No ha abierto un archivo.", "Error");
            }

        }

        private void GuardarComo()
        {
            int i = tabArchivo.SelectedIndex;

            if (i != -1)
            {
                TabPage sPage = tabArchivo.SelectedTab;
                FastColoredTextBox ctbArchivo = (FastColoredTextBox)sPage.Controls[0];

                saveFileDialog1.Filter = "Archivos de collete(*.colette) | *.colette| Todos los archivos(*.*)| *.*";
                saveFileDialog1.FileName = sPage.Text;
                if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

                if (Path.GetExtension(saveFileDialog1.FileName).ToLower().Equals(".colette"))
                {
                    StreamWriter writer = null;

                    try
                    {
                        writer = new StreamWriter(saveFileDialog1.FileName);
                        writer.Write(ctbArchivo.Text);

                        if (ctbArchivo.Name.Equals(""))
                            Archivo--;

                        ctbArchivo.Name = saveFileDialog1.FileName;
                        sPage.Text = Path.GetFileName(saveFileDialog1.FileName);

                        MessageBox.Show("Se guardo el archivo correctamente en " + ctbArchivo.Name, "Mensaje");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Ocurrió un error al guardar el archivo " + e.Message, "Error");
                    }
                    finally
                    {
                        if (writer != null)
                            writer.Close();
                    }
                }
                else
                {
                    MessageBox.Show("La extensión del archivo debe ser .colette.", "Error");
                }

            }
            else
            {
                MessageBox.Show("No ha abierto un archivo.", "Error");
            }
        }

        private void CerrarArchivo(int i)
        {
            if (i != -1)
            {
                TabPage sPage = tabArchivo.SelectedTab;
                FastColoredTextBox ctbArchivo = (FastColoredTextBox)sPage.Controls[0];

                
                if (ctbArchivo.IsChanged)
                {
                    DialogResult result = MessageBox.Show("¿Desea guardar el archivo?", "Guardar", MessageBoxButtons.YesNoCancel);

                    if (result == DialogResult.Yes)
                    {
                        GuardarArchivo();
                        tabArchivo.TabPages.Remove(sPage);
                    }
                    else if (result == DialogResult.No)
                    {
                        tabArchivo.TabPages.Remove(sPage);
                    }
                }
                else
                {
                    tabArchivo.TabPages.Remove(sPage);
                }
            }
            else
            {
                MessageBox.Show("No ha abierto un archivo.", "Error");
            }
        }

        private void CerrarTodo()
        {
            int j = tabArchivo.TabCount;

            if(j == 0)
            {
                MessageBox.Show("No ha abierto un archivo.", "Error");
                return;
            }

            for (int i = 0; i < j; i++)
            {
                CerrarArchivo(tabArchivo.SelectedIndex);
            }

        }

        private void UndoArchivo()
        {
            if (tabArchivo.SelectedIndex != -1)
            {
                TabPage sPage = tabArchivo.SelectedTab;
                FastColoredTextBox ctbArchivo = (FastColoredTextBox)sPage.Controls[0];

                ctbArchivo.Undo();
                //ctbArchivo.ClearUndo();
            }
        }

        private void CopiarArchivo()
        {
            if (tabArchivo.SelectedIndex != -1)
            {
                TabPage sPage = tabArchivo.SelectedTab;
                FastColoredTextBox ctbArchivo = (FastColoredTextBox)sPage.Controls[0];

                if (!ctbArchivo.Selection.Text.Equals(""))
                {
                    ctbArchivo.Copy();
                }
            }
        }

        private void PegarArchivo()
        {
            if (tabArchivo.SelectedIndex != -1)
            {
                TabPage sPage = tabArchivo.SelectedTab;
                FastColoredTextBox ctbArchivo = (FastColoredTextBox)sPage.Controls[0];

                if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
                {
                    ctbArchivo.Paste();
                }
            }
        }

        private void CortarArchivo()
        {
            if (tabArchivo.SelectedIndex != -1)
            {
                TabPage sPage = tabArchivo.SelectedTab;
                FastColoredTextBox ctbArchivo = (FastColoredTextBox)sPage.Controls[0];

                if (!ctbArchivo.Selection.Text.Equals(""))
                {
                    ctbArchivo.Cut();
                }
            }
        }

        private void SeleccionarTodo()
        {
            if (tabArchivo.SelectedIndex != -1)
            {
                TabPage sPage = tabArchivo.SelectedTab;
                FastColoredTextBox ctbArchivo = (FastColoredTextBox)sPage.Controls[0];

                if (!ctbArchivo.Text.Equals(""))
                {
                    ctbArchivo.SelectAll();
                }
            }
        }

        private void Ejecutar3D()
        {
            if (tabArchivo.SelectedIndex != -1)
            {
                TabPage sPage = tabArchivo.SelectedTab;
                TabControl tControl = (TabControl) sPage.Controls[2];

                TabPage sPage3d = tControl.TabPages[0];
                FastColoredTextBox ctb3D = (FastColoredTextBox)sPage3d.Controls[0];

                if (!ctb3D.Text.Equals(string.Empty)) {
                    Analizador analizador = new Analizador();
                    string entrada = ctb3D.Text;//.Replace("\\", "\\\\");

                    txtOutput.Clear();

                    if (analizador.AnalizarEntrada(entrada))
                    {
                        //MessageBox.Show("Archivo sin errores.");
                        ReporteErrores(analizador.Raiz);
                        tabSalida.SelectedTab = pageSalida;
                        AST ast = (AST)analizador.GenerarAST(analizador.Raiz.Root);
                        ast.ejecutar(this.txtOutput);

                    }
                    else
                    {
                        MessageBox.Show("El archivo tiene errores.");
                        tabSalida.SelectedTab = pageErrores;
                        ReporteErrores(analizador.Raiz);
                    }
                }
            }
            else
            {
                MessageBox.Show("No ha traducido un archivo colette.", "Error");
            }
            
        }

        private void TraducirColette()
        {
            if (tabArchivo.SelectedIndex != -1)
            {
                TabPage sPage = tabArchivo.SelectedTab;
                FastColoredTextBox ctbArchivo = (FastColoredTextBox)sPage.Controls[0];

                TabControl tControl = (TabControl)sPage.Controls[2];

                TabPage sPage3d = tControl.TabPages[0];
                FastColoredTextBox ctb3D = (FastColoredTextBox)sPage3d.Controls[0];

                if (!ctbArchivo.Text.Equals(string.Empty))
                {
                    AnalizadorColette analizador = new AnalizadorColette();
                    string entrada = ctbArchivo.Text;//.Replace("\\", "\\\\");
                    ctb3D.Clear();
                    txtOutput.Clear();

                    if (analizador.AnalizarEntrada(entrada))
                    {
                        //MessageBox.Show("Archivo sin errores.");
                        ReporteErrores(analizador.Raiz);

                        Arbol arbol = (Arbol)analizador.GenerarArbol(analizador.Raiz.Root);
                        
                        if (arbol != null)
                        {
                            //MessageBox.Show("todo bien");
                            LinkedList<Error> errores = new LinkedList<Error>();
                            string c3d = arbol.GenerarC3D(errores);
                            ctb3D.Text = c3d;

                            if (errores.Count() > 0)
                            {
                                MessageBox.Show("El archivo tiene errores.");
                                tabSalida.SelectedTab = pageErrores;
                                ReporteErroresAnalisis(errores);
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("El archivo tiene errores.");
                        tabSalida.SelectedTab = pageErrores;
                        ReporteErrores(analizador.Raiz);
                    }
                }
            }
            else
            {
                MessageBox.Show("No ha abierto un archivo colette.", "Error");
            }
        }

        private void Traducir3D()
        {
            if (tabArchivo.SelectedIndex != -1)
            {
                TabPage sPage = tabArchivo.SelectedTab;
                TabControl tControl = (TabControl)sPage.Controls[2];

                TabPage sPage3d = tControl.TabPages[0];
                FastColoredTextBox ctb3D = (FastColoredTextBox)sPage3d.Controls[0];

                if (!ctb3D.Text.Equals(string.Empty))
                {
                    Analizador analizador = new Analizador();
                    string entrada = ctb3D.Text;//.Replace("\\", "\\\\");

                    txtOutput.Clear();

                    if (analizador.AnalizarEntrada(entrada))
                    {
                        MessageBox.Show("Archivo sin errores.");
                        ReporteErrores(analizador.Raiz);
                        tabSalida.SelectedTab = pageSalida;
                        AST ast = (AST)analizador.GenerarAST(analizador.Raiz.Root);
                        ast.ejecutar(this.txtOutput);

                    }
                    else
                    {
                        MessageBox.Show("El archivo tiene errores.");
                        tabSalida.SelectedTab = pageErrores;
                        ReporteErrores(analizador.Raiz);
                    }
                }
            }
            else
            {
                MessageBox.Show("No ha traducido un archivo colette.", "Error");
            }
        }

        private void ReporteErrores(ParseTree raiz)
        {
            gridErrors.Rows.Clear();
            for (int i = 0; i < raiz.ParserMessages.Count(); i++)
            {
                LogMessage m = raiz.ParserMessages.ElementAt(i);
                if(m.Message.ToString().Contains("character"))
                    gridErrors.Rows.Add("Léxico", m.Message.Replace("Invalid character","Carácter inválido"), (m.Location.Line + 1), (m.Location.Column + 1));
                else
                    gridErrors.Rows.Add("Sintáctico", m.Message.Replace("Syntax error, expected:","Error de sintáxis, se esperaba:"), (m.Location.Line + 1), (m.Location.Column + 1));

            }
        }

        private void ReporteErroresAnalisis(LinkedList<Error> errores)
        {
            gridErrors.Rows.Clear();
            foreach (Error error in errores)
            {
                gridErrors.Rows.Add(error.Valor, error.Descripcion, error.Linea, error.Columna);
            }
        }

        private void GraficarASTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabArchivo.SelectedIndex != -1)
            {
                TabPage sPage = tabArchivo.SelectedTab;
                FastColoredTextBox ctbArchivo = (FastColoredTextBox)sPage.Controls[0];

                TabControl tControl = (TabControl)sPage.Controls[2];

                TabPage sPage3d = tControl.TabPages[0];
                FastColoredTextBox ctb3D = (FastColoredTextBox)sPage3d.Controls[0];

                if (!ctbArchivo.Text.Equals(string.Empty))
                {
                    AnalizadorColette analizador = new AnalizadorColette();
                    string entrada = ctbArchivo.Text;
                    ctb3D.Clear();
                    txtOutput.Clear();

                    if (analizador.AnalizarEntrada(entrada))
                    {
                        MessageBox.Show("Archivo sin errores.");
                        ReporteErrores(analizador.Raiz);
                        GraficarArbol(analizador.Raiz.Root);

                    }
                    else
                    {
                        MessageBox.Show("El archivo tiene errores.");
                        tabSalida.SelectedTab = pageErrores;
                        ReporteErrores(analizador.Raiz);
                    }
                }
            }
            else
            {
                MessageBox.Show("No ha abierto un archivo colette.", "Error");
            }
        }

        public void GraficarArbol(ParseTreeNode raiz)
        {
            string archivo = "arbol.dot";

            StreamWriter writer = null;

            try
            {
                //FileStream stream = new FileStream(archivo, FileMode.OpenOrCreate, FileAccess.Write);
                //StreamWriter writer = new StreamWriter(stream);
                writer = new StreamWriter(archivo);

                string grafica = "digraph arbol{\n";
                grafica += "rankdir=UD;\n";
                grafica += "node [shape = box, style=filled, color=blanchedalmond];\n";
                grafica += "edge[color=chocolate3];\n";
                grafica += GraficarNodo(raiz) + "\n";
                grafica += "}\n";

                writer.Write(grafica);
                writer.Close();

                var command = string.Format("dot -Tpng arbol.dot  -o arbol.png");
                var procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/C " + command);
                var proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                proc.WaitForExit();

                //var appname = "Microsoft.Photos.exe";
                // Process.Start(appname, "arbol.jpg");
                if (proc.ExitCode == 1)
                {
                    MessageBox.Show("Error al graficar","Graphviz");
                } else
                {
                    Process.Start("arbol.png");
                }
               
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
            string label = raiz.ToString().Replace("\"","\\\""); //caracteres especiales

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

    }
}
