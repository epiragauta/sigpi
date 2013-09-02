namespace SIGPI_10
{
  partial class FrmSIGPIAlgoritmos
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
      this.button1 = new System.Windows.Forms.Button();
      this.btnCerrar = new System.Windows.Forms.Button();
      this.button3 = new System.Windows.Forms.Button();
      this.txtFechaUltimoModelo = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.btnIncorporarLecturas = new System.Windows.Forms.Button();
      this.btnGenerarGrids = new System.Windows.Forms.Button();
      this.lblTitulo = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(267, 213);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(91, 23);
      this.button1.TabIndex = 20;
      this.button1.Text = "&Acerca de...";
      this.button1.UseVisualStyleBackColor = true;
      // 
      // btnCerrar
      // 
      this.btnCerrar.Location = new System.Drawing.Point(376, 213);
      this.btnCerrar.Name = "btnCerrar";
      this.btnCerrar.Size = new System.Drawing.Size(67, 23);
      this.btnCerrar.TabIndex = 19;
      this.btnCerrar.Text = "&Cerrar";
      this.btnCerrar.UseVisualStyleBackColor = true;
      // 
      // button3
      // 
      this.button3.Location = new System.Drawing.Point(10, 213);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(88, 23);
      this.button3.TabIndex = 18;
      this.button3.Text = "&Propiedades";
      this.button3.UseVisualStyleBackColor = true;
      // 
      // txtFechaUltimoModelo
      // 
      this.txtFechaUltimoModelo.Location = new System.Drawing.Point(228, 76);
      this.txtFechaUltimoModelo.Name = "txtFechaUltimoModelo";
      this.txtFechaUltimoModelo.Size = new System.Drawing.Size(215, 20);
      this.txtFechaUltimoModelo.TabIndex = 17;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(7, 78);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(227, 16);
      this.label1.TabIndex = 16;
      this.label1.Text = "Ultima fecha de procesamiento:";
      // 
      // pictureBox1
      // 
      this.pictureBox1.Location = new System.Drawing.Point(7, 97);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(88, 110);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pictureBox1.TabIndex = 15;
      this.pictureBox1.TabStop = false;
      // 
      // btnIncorporarLecturas
      // 
      this.btnIncorporarLecturas.Location = new System.Drawing.Point(101, 101);
      this.btnIncorporarLecturas.Name = "btnIncorporarLecturas";
      this.btnIncorporarLecturas.Size = new System.Drawing.Size(164, 23);
      this.btnIncorporarLecturas.TabIndex = 14;
      this.btnIncorporarLecturas.Text = "Algoritmo Convencional";
      this.btnIncorporarLecturas.UseVisualStyleBackColor = true;
      // 
      // btnGenerarGrids
      // 
      this.btnGenerarGrids.Location = new System.Drawing.Point(101, 130);
      this.btnGenerarGrids.Name = "btnGenerarGrids";
      this.btnGenerarGrids.Size = new System.Drawing.Size(164, 23);
      this.btnGenerarGrids.TabIndex = 13;
      this.btnGenerarGrids.Text = "Algoritmo de Amenazas";
      this.btnGenerarGrids.UseVisualStyleBackColor = true;
      // 
      // lblTitulo
      // 
      this.lblTitulo.BackColor = System.Drawing.Color.White;
      this.lblTitulo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblTitulo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTitulo.ForeColor = System.Drawing.Color.Black;
      this.lblTitulo.Location = new System.Drawing.Point(24, 9);
      this.lblTitulo.Name = "lblTitulo";
      this.lblTitulo.Size = new System.Drawing.Size(419, 50);
      this.lblTitulo.TabIndex = 12;
      this.lblTitulo.Text = "SISTEMA DE INFORMACION GEOGRAFICA \r\nPARA LA PREVENCION DE INCENDIOS SIGPI";
      this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // FrmSIGPIAlgoritmos
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(458, 243);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.btnCerrar);
      this.Controls.Add(this.button3);
      this.Controls.Add(this.txtFechaUltimoModelo);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.btnIncorporarLecturas);
      this.Controls.Add(this.btnGenerarGrids);
      this.Controls.Add(this.lblTitulo);
      this.Name = "FrmSIGPIAlgoritmos";
      this.Text = "FrmSIGPIAlgoritmos";
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button btnCerrar;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.TextBox txtFechaUltimoModelo;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Button btnIncorporarLecturas;
    private System.Windows.Forms.Button btnGenerarGrids;
    private System.Windows.Forms.Label lblTitulo;
  }
}