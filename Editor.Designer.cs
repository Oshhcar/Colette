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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
            this.menuPrincipal = new System.Windows.Forms.MenuStrip();
            this.subMenuArchivo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNuevo = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarComoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarTodoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deshacerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cortarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copiarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pegarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seleccionarTodoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.coletteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.traducirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graficarASTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grafoDependenciasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c3DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ejecutarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optimizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ejecutarOptimizadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.traducirToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitBottom = new System.Windows.Forms.Splitter();
            this.tabArchivo = new System.Windows.Forms.TabControl();
            this.pageErrores = new System.Windows.Forms.TabPage();
            this.gridErrors = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pageSalida = new System.Windows.Forms.TabPage();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.tabSalida = new System.Windows.Forms.TabControl();
            this.pageOptimizacion = new System.Windows.Forms.TabPage();
            this.gridOptimizador = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbLinea = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.optimizarTodo = new System.Windows.Forms.CheckBox();
            this.debuggerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPrincipal.SuspendLayout();
            this.pageErrores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridErrors)).BeginInit();
            this.pageSalida.SuspendLayout();
            this.tabSalida.SuspendLayout();
            this.pageOptimizacion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridOptimizador)).BeginInit();
            this.SuspendLayout();
            // 
            // menuPrincipal
            // 
            this.menuPrincipal.BackColor = System.Drawing.SystemColors.Control;
            this.menuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuArchivo,
            this.editarToolStripMenuItem,
            this.coletteToolStripMenuItem,
            this.c3DToolStripMenuItem,
            this.ayudaToolStripMenuItem});
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
            // deshacerToolStripMenuItem
            // 
            this.deshacerToolStripMenuItem.Name = "deshacerToolStripMenuItem";
            this.deshacerToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.deshacerToolStripMenuItem.Text = "Deshacer";
            this.deshacerToolStripMenuItem.Click += new System.EventHandler(this.DeshacerToolStripMenuItem_Click);
            // 
            // cortarToolStripMenuItem
            // 
            this.cortarToolStripMenuItem.Name = "cortarToolStripMenuItem";
            this.cortarToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.cortarToolStripMenuItem.Text = "Cortar";
            this.cortarToolStripMenuItem.Click += new System.EventHandler(this.CortarToolStripMenuItem_Click);
            // 
            // copiarToolStripMenuItem
            // 
            this.copiarToolStripMenuItem.Name = "copiarToolStripMenuItem";
            this.copiarToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.copiarToolStripMenuItem.Text = "Copiar";
            this.copiarToolStripMenuItem.Click += new System.EventHandler(this.CopiarToolStripMenuItem_Click);
            // 
            // pegarToolStripMenuItem
            // 
            this.pegarToolStripMenuItem.Name = "pegarToolStripMenuItem";
            this.pegarToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.pegarToolStripMenuItem.Text = "Pegar";
            this.pegarToolStripMenuItem.Click += new System.EventHandler(this.PegarToolStripMenuItem_Click);
            // 
            // seleccionarTodoToolStripMenuItem
            // 
            this.seleccionarTodoToolStripMenuItem.Name = "seleccionarTodoToolStripMenuItem";
            this.seleccionarTodoToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.seleccionarTodoToolStripMenuItem.Text = "Seleccionar Todo";
            this.seleccionarTodoToolStripMenuItem.Click += new System.EventHandler(this.SeleccionarTodoToolStripMenuItem_Click);
            // 
            // coletteToolStripMenuItem
            // 
            this.coletteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.traducirToolStripMenuItem,
            this.graficarASTToolStripMenuItem,
            this.grafoDependenciasToolStripMenuItem,
            this.debuggerToolStripMenuItem});
            this.coletteToolStripMenuItem.Name = "coletteToolStripMenuItem";
            this.coletteToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.coletteToolStripMenuItem.Text = "Colette";
            // 
            // traducirToolStripMenuItem
            // 
            this.traducirToolStripMenuItem.Name = "traducirToolStripMenuItem";
            this.traducirToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.traducirToolStripMenuItem.Text = "Traducir";
            this.traducirToolStripMenuItem.Click += new System.EventHandler(this.TraducirToolStripMenuItem_Click);
            // 
            // graficarASTToolStripMenuItem
            // 
            this.graficarASTToolStripMenuItem.Name = "graficarASTToolStripMenuItem";
            this.graficarASTToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.graficarASTToolStripMenuItem.Text = "Graficar AST";
            this.graficarASTToolStripMenuItem.Click += new System.EventHandler(this.GraficarASTToolStripMenuItem_Click);
            // 
            // grafoDependenciasToolStripMenuItem
            // 
            this.grafoDependenciasToolStripMenuItem.Name = "grafoDependenciasToolStripMenuItem";
            this.grafoDependenciasToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.grafoDependenciasToolStripMenuItem.Text = "Grafo dependencias";
            this.grafoDependenciasToolStripMenuItem.Click += new System.EventHandler(this.GrafoDependenciasToolStripMenuItem_Click);
            // 
            // c3DToolStripMenuItem
            // 
            this.c3DToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ejecutarToolStripMenuItem,
            this.optimizarToolStripMenuItem,
            this.ejecutarOptimizadoToolStripMenuItem,
            this.traducirToolStripMenuItem1});
            this.c3DToolStripMenuItem.Name = "c3DToolStripMenuItem";
            this.c3DToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.c3DToolStripMenuItem.Text = "C3D";
            // 
            // ejecutarToolStripMenuItem
            // 
            this.ejecutarToolStripMenuItem.Name = "ejecutarToolStripMenuItem";
            this.ejecutarToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.ejecutarToolStripMenuItem.Text = "Ejecutar";
            this.ejecutarToolStripMenuItem.Click += new System.EventHandler(this.EjecutarToolStripMenuItem_Click);
            // 
            // optimizarToolStripMenuItem
            // 
            this.optimizarToolStripMenuItem.Name = "optimizarToolStripMenuItem";
            this.optimizarToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.optimizarToolStripMenuItem.Text = "Optimizar";
            this.optimizarToolStripMenuItem.Click += new System.EventHandler(this.OptimizarToolStripMenuItem_Click);
            // 
            // ejecutarOptimizadoToolStripMenuItem
            // 
            this.ejecutarOptimizadoToolStripMenuItem.Name = "ejecutarOptimizadoToolStripMenuItem";
            this.ejecutarOptimizadoToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.ejecutarOptimizadoToolStripMenuItem.Text = "Ejecutar Optimizado";
            this.ejecutarOptimizadoToolStripMenuItem.Click += new System.EventHandler(this.EjecutarOptimizadoToolStripMenuItem_Click);
            // 
            // traducirToolStripMenuItem1
            // 
            this.traducirToolStripMenuItem1.Name = "traducirToolStripMenuItem1";
            this.traducirToolStripMenuItem1.Size = new System.Drawing.Size(181, 22);
            this.traducirToolStripMenuItem1.Text = "Traducir";
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acercaDeToolStripMenuItem});
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.acercaDeToolStripMenuItem.Text = "Acerca de";
            this.acercaDeToolStripMenuItem.Click += new System.EventHandler(this.AcercaDeToolStripMenuItem_Click);
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
            this.pageErrores.Controls.Add(this.gridErrors);
            this.pageErrores.Location = new System.Drawing.Point(4, 22);
            this.pageErrores.Name = "pageErrores";
            this.pageErrores.Padding = new System.Windows.Forms.Padding(3);
            this.pageErrores.Size = new System.Drawing.Size(1014, 161);
            this.pageErrores.TabIndex = 4;
            this.pageErrores.Text = "Errores";
            this.pageErrores.UseVisualStyleBackColor = true;
            // 
            // gridErrors
            // 
            this.gridErrors.AllowUserToAddRows = false;
            this.gridErrors.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridErrors.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridErrors.ColumnHeadersHeight = 24;
            this.gridErrors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridErrors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridErrors.DefaultCellStyle = dataGridViewCellStyle5;
            this.gridErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridErrors.Location = new System.Drawing.Point(3, 3);
            this.gridErrors.MultiSelect = false;
            this.gridErrors.Name = "gridErrors";
            this.gridErrors.ReadOnly = true;
            this.gridErrors.RowHeadersVisible = false;
            this.gridErrors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridErrors.Size = new System.Drawing.Size(1008, 155);
            this.gridErrors.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn1.HeaderText = "Valor";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.ToolTipText = "Double-click grid cell to locate in source code";
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn2.HeaderText = "Descripción";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 800;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "State";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn3.HeaderText = "Línea";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.ToolTipText = "Double-click grid cell to navigate to state details";
            this.dataGridViewTextBoxColumn3.Width = 41;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Columna";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
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
            this.tabSalida.Controls.Add(this.pageOptimizacion);
            this.tabSalida.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabSalida.Location = new System.Drawing.Point(0, 416);
            this.tabSalida.Name = "tabSalida";
            this.tabSalida.SelectedIndex = 0;
            this.tabSalida.Size = new System.Drawing.Size(1022, 187);
            this.tabSalida.TabIndex = 23;
            // 
            // pageOptimizacion
            // 
            this.pageOptimizacion.Controls.Add(this.gridOptimizador);
            this.pageOptimizacion.Location = new System.Drawing.Point(4, 22);
            this.pageOptimizacion.Name = "pageOptimizacion";
            this.pageOptimizacion.Padding = new System.Windows.Forms.Padding(3);
            this.pageOptimizacion.Size = new System.Drawing.Size(1014, 161);
            this.pageOptimizacion.TabIndex = 5;
            this.pageOptimizacion.Text = "Optimización 3D";
            this.pageOptimizacion.UseVisualStyleBackColor = true;
            // 
            // gridOptimizador
            // 
            this.gridOptimizador.AllowUserToAddRows = false;
            this.gridOptimizador.AllowUserToDeleteRows = false;
            this.gridOptimizador.ColumnHeadersHeight = 24;
            this.gridOptimizador.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridOptimizador.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.gridOptimizador.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridOptimizador.Location = new System.Drawing.Point(3, 3);
            this.gridOptimizador.MultiSelect = false;
            this.gridOptimizador.Name = "gridOptimizador";
            this.gridOptimizador.ReadOnly = true;
            this.gridOptimizador.RowHeadersVisible = false;
            this.gridOptimizador.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridOptimizador.Size = new System.Drawing.Size(1008, 155);
            this.gridOptimizador.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Columna Ocurrencia";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 200;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Columna de Código Optimizado";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 200;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Número de regla aplicada";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 200;
            // 
            // lbLinea
            // 
            this.lbLinea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLinea.BackColor = System.Drawing.SystemColors.Control;
            this.lbLinea.Location = new System.Drawing.Point(286, 0);
            this.lbLinea.Name = "lbLinea";
            this.lbLinea.Size = new System.Drawing.Size(729, 22);
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
            // optimizarTodo
            // 
            this.optimizarTodo.AutoSize = true;
            this.optimizarTodo.Checked = true;
            this.optimizarTodo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.optimizarTodo.Location = new System.Drawing.Point(265, 1);
            this.optimizarTodo.Name = "optimizarTodo";
            this.optimizarTodo.Size = new System.Drawing.Size(15, 14);
            this.optimizarTodo.TabIndex = 27;
            this.optimizarTodo.UseVisualStyleBackColor = true;
            // 
            // debuggerToolStripMenuItem
            // 
            this.debuggerToolStripMenuItem.Name = "debuggerToolStripMenuItem";
            this.debuggerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.debuggerToolStripMenuItem.Text = "Debugger";
            this.debuggerToolStripMenuItem.Click += new System.EventHandler(this.DebuggerToolStripMenuItem_Click);
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 603);
            this.Controls.Add(this.optimizarTodo);
            this.Controls.Add(this.lbLinea);
            this.Controls.Add(this.tabArchivo);
            this.Controls.Add(this.splitBottom);
            this.Controls.Add(this.tabSalida);
            this.Controls.Add(this.menuPrincipal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuPrincipal;
            this.Name = "Editor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Colette IDE";
            this.menuPrincipal.ResumeLayout(false);
            this.menuPrincipal.PerformLayout();
            this.pageErrores.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridErrors)).EndInit();
            this.pageSalida.ResumeLayout(false);
            this.pageSalida.PerformLayout();
            this.tabSalida.ResumeLayout(false);
            this.pageOptimizacion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridOptimizador)).EndInit();
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
        private System.Windows.Forms.DataGridView gridErrors;
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
        private System.Windows.Forms.ToolStripMenuItem coletteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c3DToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ejecutarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem traducirToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.ToolStripMenuItem graficarASTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem traducirToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ejecutarOptimizadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grafoDependenciasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optimizarToolStripMenuItem;
        private System.Windows.Forms.TabPage pageOptimizacion;
        private System.Windows.Forms.DataGridView gridOptimizador;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.CheckBox optimizarTodo;
        private System.Windows.Forms.ToolStripMenuItem debuggerToolStripMenuItem;
    }
}