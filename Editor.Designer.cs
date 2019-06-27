namespace Compilador
{
    partial class Editor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle71 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle75 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle72 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle73 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle74 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuPrincipal = new System.Windows.Forms.MenuStrip();
            this.subMenuArchivo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNuevo = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarComoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarTodoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitBottom = new System.Windows.Forms.Splitter();
            this.tabArchivo = new System.Windows.Forms.TabControl();
            this.pageErrores = new System.Windows.Forms.TabPage();
            this.gridGrammarErrors = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pageSalida = new System.Windows.Forms.TabPage();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.tabSalida = new System.Windows.Forms.TabControl();
            this.lbLinea = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.deshacerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copiarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pegarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cortarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seleccionarTodoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPrincipal.SuspendLayout();
            this.pageErrores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGrammarErrors)).BeginInit();
            this.pageSalida.SuspendLayout();
            this.tabSalida.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuPrincipal
            // 
            this.menuPrincipal.BackColor = System.Drawing.SystemColors.Control;
            this.menuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuArchivo,
            this.editarToolStripMenuItem});
            this.menuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.menuPrincipal.Name = "menuPrincipal";
            this.menuPrincipal.Size = new System.Drawing.Size(1022, 24);
            this.menuPrincipal.TabIndex = 10;
            this.menuPrincipal.Text = "Principal";
            // 
            // subMenuArchivo
            // 
            this.subMenuArchivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemNuevo,
            this.abrirToolStripMenuItem,
            this.guardarToolStripMenuItem,
            this.guardarComoToolStripMenuItem,
            this.cerrarToolStripMenuItem,
            this.cerrarTodoToolStripMenuItem});
            this.subMenuArchivo.Name = "subMenuArchivo";
            this.subMenuArchivo.Size = new System.Drawing.Size(60, 20);
            this.subMenuArchivo.Text = "Archivo";
            // 
            // menuItemNuevo
            // 
            this.menuItemNuevo.Name = "menuItemNuevo";
            this.menuItemNuevo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.menuItemNuevo.Size = new System.Drawing.Size(225, 22);
            this.menuItemNuevo.Text = "Nuevo";
            this.menuItemNuevo.Click += new System.EventHandler(this.NuevoToolStripMenuItem_Click);
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.AbrirToolStripMenuItem_Click);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.guardarToolStripMenuItem.Text = "Guardar";
            this.guardarToolStripMenuItem.Click += new System.EventHandler(this.GuardarToolStripMenuItem_Click);
            // 
            // guardarComoToolStripMenuItem
            // 
            this.guardarComoToolStripMenuItem.Name = "guardarComoToolStripMenuItem";
            this.guardarComoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.guardarComoToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.guardarComoToolStripMenuItem.Text = "Guardar como";
            this.guardarComoToolStripMenuItem.Click += new System.EventHandler(this.GuardarComoToolStripMenuItem_Click);
            // 
            // cerrarToolStripMenuItem
            // 
            this.cerrarToolStripMenuItem.Name = "cerrarToolStripMenuItem";
            this.cerrarToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.cerrarToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.cerrarToolStripMenuItem.Text = "Cerrar";
            this.cerrarToolStripMenuItem.Click += new System.EventHandler(this.CerrarToolStripMenuItem_Click);
            // 
            // cerrarTodoToolStripMenuItem
            // 
            this.cerrarTodoToolStripMenuItem.Name = "cerrarTodoToolStripMenuItem";
            this.cerrarTodoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.W)));
            this.cerrarTodoToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.cerrarTodoToolStripMenuItem.Text = "Cerrar todo";
            this.cerrarTodoToolStripMenuItem.Click += new System.EventHandler(this.CerrarTodoToolStripMenuItem_Click);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deshacerToolStripMenuItem,
            this.cortarToolStripMenuItem,
            this.copiarToolStripMenuItem,
            this.pegarToolStripMenuItem,
            this.seleccionarTodoToolStripMenuItem});
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.editarToolStripMenuItem.Text = "Editar";
            // 
            // splitBottom
            // 
            this.splitBottom.BackColor = System.Drawing.SystemColors.Control;
            this.splitBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitBottom.Location = new System.Drawing.Point(0, 410);
            this.splitBottom.Name = "splitBottom";
            this.splitBottom.Size = new System.Drawing.Size(1022, 6);
            this.splitBottom.TabIndex = 24;
            this.splitBottom.TabStop = false;
            // 
            // tabArchivo
            // 
            this.tabArchivo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabArchivo.Location = new System.Drawing.Point(0, 24);
            this.tabArchivo.Name = "tabArchivo";
            this.tabArchivo.SelectedIndex = 0;
            this.tabArchivo.Size = new System.Drawing.Size(1022, 386);
            this.tabArchivo.TabIndex = 25;
            // 
            // pageErrores
            // 
            this.pageErrores.Controls.Add(this.gridGrammarErrors);
            this.pageErrores.Location = new System.Drawing.Point(4, 22);
            this.pageErrores.Name = "pageErrores";
            this.pageErrores.Padding = new System.Windows.Forms.Padding(3);
            this.pageErrores.Size = new System.Drawing.Size(1014, 161);
            this.pageErrores.TabIndex = 4;
            this.pageErrores.Text = "Errores";
            this.pageErrores.UseVisualStyleBackColor = true;
            // 
            // gridGrammarErrors
            // 
            this.gridGrammarErrors.AllowUserToAddRows = false;
            this.gridGrammarErrors.AllowUserToDeleteRows = false;
            dataGridViewCellStyle71.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle71.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle71.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle71.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle71.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle71.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle71.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridGrammarErrors.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle71;
            this.gridGrammarErrors.ColumnHeadersHeight = 24;
            this.gridGrammarErrors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridGrammarErrors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            dataGridViewCellStyle75.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle75.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle75.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle75.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle75.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle75.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle75.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridGrammarErrors.DefaultCellStyle = dataGridViewCellStyle75;
            this.gridGrammarErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridGrammarErrors.Location = new System.Drawing.Point(3, 3);
            this.gridGrammarErrors.MultiSelect = false;
            this.gridGrammarErrors.Name = "gridGrammarErrors";
            this.gridGrammarErrors.ReadOnly = true;
            this.gridGrammarErrors.RowHeadersVisible = false;
            this.gridGrammarErrors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridGrammarErrors.Size = new System.Drawing.Size(1008, 155);
            this.gridGrammarErrors.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle72.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle72;
            this.dataGridViewTextBoxColumn2.HeaderText = "Error Level";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.ToolTipText = "Double-click grid cell to locate in source code";
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle73.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle73;
            this.dataGridViewTextBoxColumn5.HeaderText = "Description";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Width = 800;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "State";
            dataGridViewCellStyle74.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle74;
            this.dataGridViewTextBoxColumn6.HeaderText = "Parser State";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn6.ToolTipText = "Double-click grid cell to navigate to state details";
            this.dataGridViewTextBoxColumn6.Width = 71;
            // 
            // pageSalida
            // 
            this.pageSalida.Controls.Add(this.txtOutput);
            this.pageSalida.Location = new System.Drawing.Point(4, 22);
            this.pageSalida.Name = "pageSalida";
            this.pageSalida.Padding = new System.Windows.Forms.Padding(3);
            this.pageSalida.Size = new System.Drawing.Size(1014, 161);
            this.pageSalida.TabIndex = 0;
            this.pageSalida.Text = "Salida";
            this.pageSalida.UseVisualStyleBackColor = true;
            // 
            // txtOutput
            // 
            this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutput.Location = new System.Drawing.Point(3, 3);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(1008, 155);
            this.txtOutput.TabIndex = 1;
            // 
            // tabSalida
            // 
            this.tabSalida.Controls.Add(this.pageSalida);
            this.tabSalida.Controls.Add(this.pageErrores);
            this.tabSalida.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabSalida.Location = new System.Drawing.Point(0, 416);
            this.tabSalida.Name = "tabSalida";
            this.tabSalida.SelectedIndex = 0;
            this.tabSalida.Size = new System.Drawing.Size(1022, 187);
            this.tabSalida.TabIndex = 23;
            // 
            // lbLinea
            // 
            this.lbLinea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLinea.BackColor = System.Drawing.SystemColors.Control;
            this.lbLinea.Location = new System.Drawing.Point(125, 0);
            this.lbLinea.Name = "lbLinea";
            this.lbLinea.Size = new System.Drawing.Size(890, 22);
            this.lbLinea.TabIndex = 26;
            this.lbLinea.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "collete";
            this.openFileDialog1.FileName = "nuevo";
            this.openFileDialog1.Tag = "collete, txt";
            this.openFileDialog1.Title = "Abrir Archivo";
            // 
            // deshacerToolStripMenuItem
            // 
            this.deshacerToolStripMenuItem.Name = "deshacerToolStripMenuItem";
            this.deshacerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.deshacerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deshacerToolStripMenuItem.Text = "Deshacer";
            this.deshacerToolStripMenuItem.Click += new System.EventHandler(this.DeshacerToolStripMenuItem_Click);
            // 
            // copiarToolStripMenuItem
            // 
            this.copiarToolStripMenuItem.Name = "copiarToolStripMenuItem";
            this.copiarToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copiarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.copiarToolStripMenuItem.Text = "Copiar";
            this.copiarToolStripMenuItem.Click += new System.EventHandler(this.CopiarToolStripMenuItem_Click);
            // 
            // pegarToolStripMenuItem
            // 
            this.pegarToolStripMenuItem.Name = "pegarToolStripMenuItem";
            this.pegarToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.pegarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.pegarToolStripMenuItem.Text = "Pegar";
            this.pegarToolStripMenuItem.Click += new System.EventHandler(this.PegarToolStripMenuItem_Click);
            // 
            // cortarToolStripMenuItem
            // 
            this.cortarToolStripMenuItem.Name = "cortarToolStripMenuItem";
            this.cortarToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cortarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cortarToolStripMenuItem.Text = "Cortar";
            this.cortarToolStripMenuItem.Click += new System.EventHandler(this.CortarToolStripMenuItem_Click);
            // 
            // seleccionarTodoToolStripMenuItem
            // 
            this.seleccionarTodoToolStripMenuItem.Name = "seleccionarTodoToolStripMenuItem";
            this.seleccionarTodoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.seleccionarTodoToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.seleccionarTodoToolStripMenuItem.Text = "Seleccionar Todo";
            this.seleccionarTodoToolStripMenuItem.Click += new System.EventHandler(this.SeleccionarTodoToolStripMenuItem_Click);
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 603);
            this.Controls.Add(this.lbLinea);
            this.Controls.Add(this.tabArchivo);
            this.Controls.Add(this.splitBottom);
            this.Controls.Add(this.tabSalida);
            this.Controls.Add(this.menuPrincipal);
            this.MainMenuStrip = this.menuPrincipal;
            this.Name = "Editor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editor";
            this.menuPrincipal.ResumeLayout(false);
            this.menuPrincipal.PerformLayout();
            this.pageErrores.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridGrammarErrors)).EndInit();
            this.pageSalida.ResumeLayout(false);
            this.pageSalida.PerformLayout();
            this.tabSalida.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuPrincipal;
        private System.Windows.Forms.ToolStripMenuItem subMenuArchivo;
        private System.Windows.Forms.ToolStripMenuItem menuItemNuevo;
        private System.Windows.Forms.Splitter splitBottom;
        private System.Windows.Forms.TabControl tabArchivo;
        private System.Windows.Forms.TabPage pageErrores;
        private System.Windows.Forms.DataGridView gridGrammarErrors;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.TabPage pageSalida;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.TabControl tabSalida;
        private System.Windows.Forms.Label lbLinea;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem guardarComoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarTodoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deshacerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copiarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pegarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cortarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem seleccionarTodoToolStripMenuItem;
    }
}