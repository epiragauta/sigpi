namespace SIGPI_10
{
  partial class FrmSIGPIPrincipal
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSIGPIPrincipal));
      this.label1 = new System.Windows.Forms.Label();
      this.txtFechaUltimoModelo = new System.Windows.Forms.TextBox();
      this.btnModelo2 = new System.Windows.Forms.Button();
      this.btnAbout = new System.Windows.Forms.Button();
      this.btnCerrar = new System.Windows.Forms.Button();
      this.btnExportar = new System.Windows.Forms.Button();
      this.btnProbabilidadCalculada = new System.Windows.Forms.Button();
      this.btnIncorporarLecturas = new System.Windows.Forms.Button();
      this.btnGenerarGrids = new System.Windows.Forms.Button();
      this.button3 = new System.Windows.Forms.Button();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.lblTitulo = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(27, 63);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(160, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Ultima Fecha de Procesamiento:";
      // 
      // txtFechaUltimoModelo
      // 
      this.txtFechaUltimoModelo.Location = new System.Drawing.Point(207, 58);
      this.txtFechaUltimoModelo.Name = "txtFechaUltimoModelo";
      this.txtFechaUltimoModelo.Size = new System.Drawing.Size(359, 20);
      this.txtFechaUltimoModelo.TabIndex = 6;
      // 
      // btnModelo2
      // 
      this.btnModelo2.Location = new System.Drawing.Point(402, 113);
      this.btnModelo2.Name = "btnModelo2";
      this.btnModelo2.Size = new System.Drawing.Size(164, 23);
      this.btnModelo2.TabIndex = 19;
      this.btnModelo2.Text = "Algoritmo Amenazas";
      this.btnModelo2.UseVisualStyleBackColor = true;
      this.btnModelo2.Click += new System.EventHandler(this.btnModelo2_Click);
      // 
      // btnAbout
      // 
      this.btnAbout.Location = new System.Drawing.Point(402, 236);
      this.btnAbout.Name = "btnAbout";
      this.btnAbout.Size = new System.Drawing.Size(91, 23);
      this.btnAbout.TabIndex = 18;
      this.btnAbout.Text = "&Acerca de...";
      this.btnAbout.UseVisualStyleBackColor = true;
      this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
      // 
      // btnCerrar
      // 
      this.btnCerrar.Location = new System.Drawing.Point(499, 236);
      this.btnCerrar.Name = "btnCerrar";
      this.btnCerrar.Size = new System.Drawing.Size(67, 23);
      this.btnCerrar.TabIndex = 17;
      this.btnCerrar.Text = "&Cerrar";
      this.btnCerrar.UseVisualStyleBackColor = true;
      this.btnCerrar.Click += new System.EventHandler(this.button6_Click);
      // 
      // btnExportar
      // 
      this.btnExportar.Location = new System.Drawing.Point(402, 205);
      this.btnExportar.Name = "btnExportar";
      this.btnExportar.Size = new System.Drawing.Size(164, 23);
      this.btnExportar.TabIndex = 16;
      this.btnExportar.Text = "Exportar Resultado";
      this.btnExportar.UseVisualStyleBackColor = true;
      this.btnExportar.Click += new System.EventHandler(this.button5_Click);
      // 
      // btnProbabilidadCalculada
      // 
      this.btnProbabilidadCalculada.Enabled = false;
      this.btnProbabilidadCalculada.Location = new System.Drawing.Point(403, 171);
      this.btnProbabilidadCalculada.Name = "btnProbabilidadCalculada";
      this.btnProbabilidadCalculada.Size = new System.Drawing.Size(164, 23);
      this.btnProbabilidadCalculada.TabIndex = 15;
      this.btnProbabilidadCalculada.Text = "Probabilidad Calculada";
      this.btnProbabilidadCalculada.UseVisualStyleBackColor = true;
      this.btnProbabilidadCalculada.Visible = false;
      this.btnProbabilidadCalculada.Click += new System.EventHandler(this.button4_Click);
      // 
      // btnIncorporarLecturas
      // 
      this.btnIncorporarLecturas.Location = new System.Drawing.Point(402, 84);
      this.btnIncorporarLecturas.Name = "btnIncorporarLecturas";
      this.btnIncorporarLecturas.Size = new System.Drawing.Size(164, 23);
      this.btnIncorporarLecturas.TabIndex = 14;
      this.btnIncorporarLecturas.Text = "Incorporar y Procesar Lecturas";
      this.btnIncorporarLecturas.UseVisualStyleBackColor = true;
      this.btnIncorporarLecturas.Click += new System.EventHandler(this.button2_Click);
      // 
      // btnGenerarGrids
      // 
      this.btnGenerarGrids.Enabled = false;
      this.btnGenerarGrids.Location = new System.Drawing.Point(403, 142);
      this.btnGenerarGrids.Name = "btnGenerarGrids";
      this.btnGenerarGrids.Size = new System.Drawing.Size(164, 23);
      this.btnGenerarGrids.TabIndex = 13;
      this.btnGenerarGrids.Text = "Generar Grids Meteorologicos";
      this.btnGenerarGrids.UseVisualStyleBackColor = true;
      this.btnGenerarGrids.Visible = false;
      this.btnGenerarGrids.Click += new System.EventHandler(this.button1_Click);
      // 
      // button3
      // 
      this.button3.Location = new System.Drawing.Point(12, 236);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(88, 23);
      this.button3.TabIndex = 21;
      this.button3.Text = "&Propiedades";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new System.EventHandler(this.button3_Click);
      // 
      // pictureBox1
      // 
      this.pictureBox1.Location = new System.Drawing.Point(12, 86);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(88, 110);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pictureBox1.TabIndex = 20;
      this.pictureBox1.TabStop = false;
      // 
      // richTextBox1
      // 
      this.richTextBox1.Location = new System.Drawing.Point(139, 86);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.Size = new System.Drawing.Size(257, 178);
      this.richTextBox1.TabIndex = 22;
      this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
      // 
      // lblTitulo
      // 
      this.lblTitulo.BackColor = System.Drawing.Color.White;
      this.lblTitulo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblTitulo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTitulo.ForeColor = System.Drawing.Color.Black;
      this.lblTitulo.Location = new System.Drawing.Point(103, 4);
      this.lblTitulo.Name = "lblTitulo";
      this.lblTitulo.Size = new System.Drawing.Size(419, 50);
      this.lblTitulo.TabIndex = 23;
      this.lblTitulo.Text = "SISTEMA DE INFORMACION GEOGRAFICA \r\nPARA LA PREVENCION DE INCENDIOS SIGPI";
      this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // FrmSIGPIPrincipal
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(578, 272);
      this.Controls.Add(this.lblTitulo);
      this.Controls.Add(this.richTextBox1);
      this.Controls.Add(this.button3);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.btnModelo2);
      this.Controls.Add(this.btnAbout);
      this.Controls.Add(this.btnCerrar);
      this.Controls.Add(this.btnExportar);
      this.Controls.Add(this.btnProbabilidadCalculada);
      this.Controls.Add(this.btnIncorporarLecturas);
      this.Controls.Add(this.btnGenerarGrids);
      this.Controls.Add(this.txtFechaUltimoModelo);
      this.Controls.Add(this.label1);
      this.Name = "FrmSIGPIPrincipal";
      this.Text = "FrmSIGPIPrincipal";
      this.Load += new System.EventHandler(this.FrmSIGPIPrincipal_Load);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtFechaUltimoModelo;
    private System.Windows.Forms.Button btnModelo2;
    private System.Windows.Forms.Button btnAbout;
    private System.Windows.Forms.Button btnCerrar;
    private System.Windows.Forms.Button btnExportar;
    private System.Windows.Forms.Button btnProbabilidadCalculada;
    private System.Windows.Forms.Button btnIncorporarLecturas;
    private System.Windows.Forms.Button btnGenerarGrids;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.Label lblTitulo;
  }
}