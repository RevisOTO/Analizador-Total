namespace Analizador_Sintactico
{
    partial class Form1
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
            this.btnAnalizar = new System.Windows.Forms.Button();
            this.rtxtCodigo = new System.Windows.Forms.RichTextBox();
            this.dtgVariables = new System.Windows.Forms.DataGridView();
            this.Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CONT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rtxtLexico = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLinea = new System.Windows.Forms.TextBox();
            this.btnValidate = new System.Windows.Forms.Button();
            this.btnAbrir = new System.Windows.Forms.Button();
            this.dtgvCuadruplos = new System.Windows.Forms.DataGridView();
            this.Res = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DF1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DF2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Operador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rtxtPostfijo = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAsm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgVariables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvCuadruplos)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAnalizar
            // 
            this.btnAnalizar.Location = new System.Drawing.Point(651, 771);
            this.btnAnalizar.Margin = new System.Windows.Forms.Padding(4);
            this.btnAnalizar.Name = "btnAnalizar";
            this.btnAnalizar.Size = new System.Drawing.Size(130, 49);
            this.btnAnalizar.TabIndex = 0;
            this.btnAnalizar.Text = "Analizar";
            this.btnAnalizar.UseVisualStyleBackColor = true;
            this.btnAnalizar.Click += new System.EventHandler(this.button1_Click);
            // 
            // rtxtCodigo
            // 
            this.rtxtCodigo.Location = new System.Drawing.Point(44, 49);
            this.rtxtCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.rtxtCodigo.Name = "rtxtCodigo";
            this.rtxtCodigo.Size = new System.Drawing.Size(345, 413);
            this.rtxtCodigo.TabIndex = 1;
            this.rtxtCodigo.Text = "";
            this.rtxtCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.onEnter);
            // 
            // dtgVariables
            // 
            this.dtgVariables.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgVariables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgVariables.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Tipo,
            this.Nombre,
            this.CONT,
            this.ID});
            this.dtgVariables.Location = new System.Drawing.Point(1105, 187);
            this.dtgVariables.Name = "dtgVariables";
            this.dtgVariables.RowHeadersWidth = 51;
            this.dtgVariables.Size = new System.Drawing.Size(429, 275);
            this.dtgVariables.TabIndex = 3;
            // 
            // Tipo
            // 
            this.Tipo.HeaderText = "Tipo de dato";
            this.Tipo.MinimumWidth = 6;
            this.Tipo.Name = "Tipo";
            this.Tipo.ReadOnly = true;
            // 
            // Nombre
            // 
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.MinimumWidth = 6;
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            // 
            // CONT
            // 
            this.CONT.HeaderText = "Cointenido";
            this.CONT.MinimumWidth = 6;
            this.CONT.Name = "CONT";
            this.CONT.ReadOnly = true;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Enabled = false;
            this.richTextBox1.Location = new System.Drawing.Point(13, 49);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(24, 413);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Cadena de entrada";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(708, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Lexico";
            // 
            // rtxtLexico
            // 
            this.rtxtLexico.Enabled = false;
            this.rtxtLexico.Location = new System.Drawing.Point(396, 49);
            this.rtxtLexico.Name = "rtxtLexico";
            this.rtxtLexico.Size = new System.Drawing.Size(703, 413);
            this.rtxtLexico.TabIndex = 7;
            this.rtxtLexico.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1299, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Sintactico";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1299, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Semantica";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1105, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "Linea:";
            // 
            // txtLinea
            // 
            this.txtLinea.Enabled = false;
            this.txtLinea.Location = new System.Drawing.Point(1160, 62);
            this.txtLinea.Name = "txtLinea";
            this.txtLinea.Size = new System.Drawing.Size(374, 22);
            this.txtLinea.TabIndex = 11;
            // 
            // btnValidate
            // 
            this.btnValidate.BackColor = System.Drawing.Color.Red;
            this.btnValidate.Enabled = false;
            this.btnValidate.ForeColor = System.Drawing.Color.SeaShell;
            this.btnValidate.Location = new System.Drawing.Point(1259, 90);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(158, 23);
            this.btnValidate.TabIndex = 12;
            this.btnValidate.UseVisualStyleBackColor = false;
            // 
            // btnAbrir
            // 
            this.btnAbrir.Location = new System.Drawing.Point(157, 470);
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(75, 23);
            this.btnAbrir.TabIndex = 13;
            this.btnAbrir.Text = "Abrir";
            this.btnAbrir.UseVisualStyleBackColor = true;
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            // 
            // dtgvCuadruplos
            // 
            this.dtgvCuadruplos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvCuadruplos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvCuadruplos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Res,
            this.DF1,
            this.DF2,
            this.Operador,
            this.Num});
            this.dtgvCuadruplos.Location = new System.Drawing.Point(787, 499);
            this.dtgvCuadruplos.Name = "dtgvCuadruplos";
            this.dtgvCuadruplos.Size = new System.Drawing.Size(733, 265);
            this.dtgvCuadruplos.TabIndex = 14;
            this.dtgvCuadruplos.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dtgvCuadruplos_RowsAdded);
            // 
            // Res
            // 
            this.Res.HeaderText = "Resultado";
            this.Res.Name = "Res";
            this.Res.ReadOnly = true;
            // 
            // DF1
            // 
            this.DF1.HeaderText = "Dato Fuente 1";
            this.DF1.Name = "DF1";
            this.DF1.ReadOnly = true;
            // 
            // DF2
            // 
            this.DF2.HeaderText = "Dato Fuente 2";
            this.DF2.Name = "DF2";
            this.DF2.ReadOnly = true;
            // 
            // Operador
            // 
            this.Operador.HeaderText = "Operador";
            this.Operador.Name = "Operador";
            this.Operador.ReadOnly = true;
            // 
            // Num
            // 
            this.Num.HeaderText = "Num";
            this.Num.Name = "Num";
            this.Num.ReadOnly = true;
            // 
            // rtxtPostfijo
            // 
            this.rtxtPostfijo.Location = new System.Drawing.Point(12, 499);
            this.rtxtPostfijo.Name = "rtxtPostfijo";
            this.rtxtPostfijo.Size = new System.Drawing.Size(769, 265);
            this.rtxtPostfijo.TabIndex = 15;
            this.rtxtPostfijo.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(288, 477);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 16);
            this.label1.TabIndex = 16;
            this.label1.Text = "Postfijo Tokens";
            // 
            // btnAsm
            // 
            this.btnAsm.Location = new System.Drawing.Point(787, 772);
            this.btnAsm.Margin = new System.Windows.Forms.Padding(4);
            this.btnAsm.Name = "btnAsm";
            this.btnAsm.Size = new System.Drawing.Size(130, 49);
            this.btnAsm.TabIndex = 18;
            this.btnAsm.Text = "Ensamblador";
            this.btnAsm.UseVisualStyleBackColor = true;
            this.btnAsm.Click += new System.EventHandler(this.btnAsm_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1552, 833);
            this.Controls.Add(this.dtgvCuadruplos);
            this.Controls.Add(this.btnAsm);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtxtPostfijo);
            this.Controls.Add(this.btnAbrir);
            this.Controls.Add(this.btnValidate);
            this.Controls.Add(this.txtLinea);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rtxtLexico);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.dtgVariables);
            this.Controls.Add(this.rtxtCodigo);
            this.Controls.Add(this.btnAnalizar);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Analizador semantico";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgVariables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvCuadruplos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAnalizar;
        private System.Windows.Forms.RichTextBox rtxtCodigo;
        private System.Windows.Forms.DataGridView dtgVariables;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rtxtLexico;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLinea;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.Button btnAbrir;
        private System.Windows.Forms.DataGridView dtgvCuadruplos;
        private System.Windows.Forms.RichTextBox rtxtPostfijo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn CONT;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Res;
        private System.Windows.Forms.DataGridViewTextBoxColumn DF1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DF2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Operador;
        private System.Windows.Forms.DataGridViewTextBoxColumn Num;
        private System.Windows.Forms.Button btnAsm;
    }
}

