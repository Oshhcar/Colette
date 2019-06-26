namespace Compilador
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_compilar = new System.Windows.Forms.Button();
            this.rtb_entrada = new System.Windows.Forms.RichTextBox();
            this.btn_graficar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_compilar
            // 
            this.btn_compilar.Location = new System.Drawing.Point(673, 422);
            this.btn_compilar.Name = "btn_compilar";
            this.btn_compilar.Size = new System.Drawing.Size(75, 23);
            this.btn_compilar.TabIndex = 0;
            this.btn_compilar.Text = "Compilar 3D";
            this.btn_compilar.UseVisualStyleBackColor = true;
            this.btn_compilar.Click += new System.EventHandler(this.Btn_compilar_Click);
            // 
            // rtb_entrada
            // 
            this.rtb_entrada.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_entrada.Location = new System.Drawing.Point(10, 12);
            this.rtb_entrada.Name = "rtb_entrada";
            this.rtb_entrada.Size = new System.Drawing.Size(738, 404);
            this.rtb_entrada.TabIndex = 1;
            this.rtb_entrada.Text = "";
            // 
            // btn_graficar
            // 
            this.btn_graficar.Location = new System.Drawing.Point(592, 422);
            this.btn_graficar.Name = "btn_graficar";
            this.btn_graficar.Size = new System.Drawing.Size(75, 23);
            this.btn_graficar.TabIndex = 2;
            this.btn_graficar.Text = "Graficar 3D";
            this.btn_graficar.UseVisualStyleBackColor = true;
            this.btn_graficar.Click += new System.EventHandler(this.Btn_graficar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 475);
            this.Controls.Add(this.btn_graficar);
            this.Controls.Add(this.rtb_entrada);
            this.Controls.Add(this.btn_compilar);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_compilar;
        private System.Windows.Forms.RichTextBox rtb_entrada;
        private System.Windows.Forms.Button btn_graficar;
    }
}

