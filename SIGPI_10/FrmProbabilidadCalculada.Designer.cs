namespace SIGPI_10
{
  partial class FrmProbabilidadCalculada
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
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.radioBtoSobreescribirProbabilidad = new System.Windows.Forms.RadioButton();
      this.radioBtnCargarArchivoExistente = new System.Windows.Forms.RadioButton();
      this.radioBtnDias5 = new System.Windows.Forms.RadioButton();
      this.radioBtnDias4 = new System.Windows.Forms.RadioButton();
      this.btnCerrar = new System.Windows.Forms.Button();
      this.listModelos = new System.Windows.Forms.ListBox();
      this.btnProbabilidad = new System.Windows.Forms.Button();
      this.radioBtnDias3 = new System.Windows.Forms.RadioButton();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.label1 = new System.Windows.Forms.Label();
      this.groupBox2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.radioBtoSobreescribirProbabilidad);
      this.groupBox2.Controls.Add(this.radioBtnCargarArchivoExistente);
      this.groupBox2.Location = new System.Drawing.Point(213, 143);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(218, 95);
      this.groupBox2.TabIndex = 11;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Opciones";
      // 
      // radioBtoSobreescribirProbabilidad
      // 
      this.radioBtoSobreescribirProbabilidad.AutoSize = true;
      this.radioBtoSobreescribirProbabilidad.Checked = true;
      this.radioBtoSobreescribirProbabilidad.Location = new System.Drawing.Point(3, 42);
      this.radioBtoSobreescribirProbabilidad.Name = "radioBtoSobreescribirProbabilidad";
      this.radioBtoSobreescribirProbabilidad.Size = new System.Drawing.Size(186, 17);
      this.radioBtoSobreescribirProbabilidad.TabIndex = 2;
      this.radioBtoSobreescribirProbabilidad.TabStop = true;
      this.radioBtoSobreescribirProbabilidad.Text = "Sobreescribir probabilidad si existe";
      this.radioBtoSobreescribirProbabilidad.UseVisualStyleBackColor = true;
      this.radioBtoSobreescribirProbabilidad.CheckedChanged += new System.EventHandler(this.radioBtoSobreescribirProbabilidad_CheckedChanged_1);
      // 
      // radioBtnCargarArchivoExistente
      // 
      this.radioBtnCargarArchivoExistente.AutoSize = true;
      this.radioBtnCargarArchivoExistente.Location = new System.Drawing.Point(3, 19);
      this.radioBtnCargarArchivoExistente.Name = "radioBtnCargarArchivoExistente";
      this.radioBtnCargarArchivoExistente.Size = new System.Drawing.Size(156, 17);
      this.radioBtnCargarArchivoExistente.TabIndex = 1;
      this.radioBtnCargarArchivoExistente.TabStop = true;
      this.radioBtnCargarArchivoExistente.Text = "Cargar probabilidad si existe";
      this.radioBtnCargarArchivoExistente.UseVisualStyleBackColor = true;
      this.radioBtnCargarArchivoExistente.CheckedChanged += new System.EventHandler(this.radioBtnCargarArchivoExistente_CheckedChanged_1);
      // 
      // radioBtnDias5
      // 
      this.radioBtnDias5.AutoSize = true;
      this.radioBtnDias5.Checked = true;
      this.radioBtnDias5.Location = new System.Drawing.Point(3, 62);
      this.radioBtnDias5.Name = "radioBtnDias5";
      this.radioBtnDias5.Size = new System.Drawing.Size(31, 17);
      this.radioBtnDias5.TabIndex = 2;
      this.radioBtnDias5.TabStop = true;
      this.radioBtnDias5.Text = "5";
      this.radioBtnDias5.UseVisualStyleBackColor = true;
      this.radioBtnDias5.CheckedChanged += new System.EventHandler(this.radioBtnDias5_CheckedChanged_1);
      // 
      // radioBtnDias4
      // 
      this.radioBtnDias4.AutoSize = true;
      this.radioBtnDias4.Location = new System.Drawing.Point(3, 39);
      this.radioBtnDias4.Name = "radioBtnDias4";
      this.radioBtnDias4.Size = new System.Drawing.Size(31, 17);
      this.radioBtnDias4.TabIndex = 1;
      this.radioBtnDias4.Text = "4";
      this.radioBtnDias4.UseVisualStyleBackColor = true;
      this.radioBtnDias4.CheckedChanged += new System.EventHandler(this.radioBtnDias4_CheckedChanged_1);
      // 
      // btnCerrar
      // 
      this.btnCerrar.Location = new System.Drawing.Point(356, 244);
      this.btnCerrar.Name = "btnCerrar";
      this.btnCerrar.Size = new System.Drawing.Size(75, 23);
      this.btnCerrar.TabIndex = 10;
      this.btnCerrar.Text = "&Cerrar";
      this.btnCerrar.UseVisualStyleBackColor = true;
      this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click_1);
      // 
      // listModelos
      // 
      this.listModelos.FormattingEnabled = true;
      this.listModelos.Location = new System.Drawing.Point(1, 3);
      this.listModelos.Name = "listModelos";
      this.listModelos.Size = new System.Drawing.Size(202, 264);
      this.listModelos.TabIndex = 6;
      // 
      // btnProbabilidad
      // 
      this.btnProbabilidad.Location = new System.Drawing.Point(216, 244);
      this.btnProbabilidad.Name = "btnProbabilidad";
      this.btnProbabilidad.Size = new System.Drawing.Size(75, 23);
      this.btnProbabilidad.TabIndex = 9;
      this.btnProbabilidad.Text = "&Probabilidad";
      this.btnProbabilidad.UseVisualStyleBackColor = true;
      this.btnProbabilidad.Click += new System.EventHandler(this.btnProbabilidad_Click_1);
      // 
      // radioBtnDias3
      // 
      this.radioBtnDias3.AutoSize = true;
      this.radioBtnDias3.Location = new System.Drawing.Point(3, 16);
      this.radioBtnDias3.Name = "radioBtnDias3";
      this.radioBtnDias3.Size = new System.Drawing.Size(31, 17);
      this.radioBtnDias3.TabIndex = 0;
      this.radioBtnDias3.Text = "3";
      this.radioBtnDias3.UseVisualStyleBackColor = true;
      this.radioBtnDias3.CheckedChanged += new System.EventHandler(this.radioBtnDias3_CheckedChanged_1);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.radioBtnDias5);
      this.groupBox1.Controls.Add(this.radioBtnDias4);
      this.groupBox1.Controls.Add(this.radioBtnDias3);
      this.groupBox1.Location = new System.Drawing.Point(213, 51);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(218, 86);
      this.groupBox1.TabIndex = 8;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Numero de Dias:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(209, 8);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(222, 40);
      this.label1.TabIndex = 7;
      this.label1.Text = "Lista de imagenes existentes \r\npara el calculo de probabilidad";
      // 
      // FrmProbabilidadCalculada
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(439, 276);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.btnCerrar);
      this.Controls.Add(this.listModelos);
      this.Controls.Add(this.btnProbabilidad);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.label1);
      this.Name = "FrmProbabilidadCalculada";
      this.Text = "FrmProbabilidadCalculada";
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.RadioButton radioBtoSobreescribirProbabilidad;
    private System.Windows.Forms.RadioButton radioBtnCargarArchivoExistente;
    private System.Windows.Forms.RadioButton radioBtnDias5;
    private System.Windows.Forms.RadioButton radioBtnDias4;
    private System.Windows.Forms.Button btnCerrar;
    private System.Windows.Forms.ListBox listModelos;
    private System.Windows.Forms.Button btnProbabilidad;
    private System.Windows.Forms.RadioButton radioBtnDias3;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label1;
  }
}