using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        private void NuevoArchivo()
        {
            TabPage nPage = MakeTextPane(null);
            nPage.Text = "nuevo" + Archivo + ".collete";

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
            
            TabPage nPage = new TabPage();
            nPage.Controls.Add(ctbArchivo);

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
            openFileDialog1.Filter = "Archivos de collete(*.collete) | *.collete| Todos los archivos(*.*)| *.*";
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            /*Limpiar salida*/

            if (Path.GetExtension(openFileDialog1.FileName).ToLower().Equals(".collete"))
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
                MessageBox.Show("La extensión del archivo debe ser .collete.", "Error");
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

                saveFileDialog1.Filter = "Archivos de collete(*.collete) | *.collete| Todos los archivos(*.*)| *.*";
                saveFileDialog1.FileName = sPage.Text;
                if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

                if (Path.GetExtension(saveFileDialog1.FileName).ToLower().Equals(".collete"))
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
                    MessageBox.Show("La extensión del archivo debe ser .collete.", "Error");
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

        
    }
}
