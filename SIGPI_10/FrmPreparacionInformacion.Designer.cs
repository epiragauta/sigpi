namespace SIGPI_10
{
  partial class FrmPreparacionInformacion
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
      this.btnIncorporar = new System.Windows.Forms.Button();
      this.label4 = new System.Windows.Forms.Label();
      this.dtPickerFechaAIncorporar = new System.Windows.Forms.DateTimePicker();
      this.textBox3 = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.txtUltimaFechaProceso = new System.Windows.Forms.TextBox();
      this.dtPickerFechaAProcesar = new System.Windows.Forms.DateTimePicker();
      this.btnProcesar = new System.Windows.Forms.Button();
      this.label5 = new System.Windows.Forms.Label();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.statusLblProcesando = new System.Windows.Forms.ToolStripStatusLabel();
      this.statusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.txtUltimoDiaIncorporacion = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.btnCerrar = new System.Windows.Forms.Button();
      this.picLogoIdeam = new System.Windows.Forms.PictureBox();
      this.label1 = new System.Windows.Forms.Label();
      this.groupBox2.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.picLogoIdeam)).BeginInit();
      this.SuspendLayout();
      // 
      // btnIncorporar
      // 
      this.btnIncorporar.Location = new System.Drawing.Point(101, 104);
      this.btnIncorporar.Name = "btnIncorporar";
      this.btnIncorporar.Size = new System.Drawing.Size(75, 23);
      this.btnIncorporar.TabIndex = 4;
      this.btnIncorporar.Text = "&Incorporar";
      this.btnIncorporar.UseVisualStyleBackColor = true;
      this.btnIncorporar.Click += new System.EventHandler(this.btnIncorporar_Click_1);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(21, 62);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(188, 13);
      this.label4.TabIndex = 5;
      this.label4.Text = "Ultima fecha de ejecucion del proceso";
      // 
      // dtPickerFechaAIncorporar
      // 
      this.dtPickerFechaAIncorporar.Location = new System.Drawing.Point(18, 78);
      this.dtPickerFechaAIncorporar.Name = "dtPickerFechaAIncorporar";
      this.dtPickerFechaAIncorporar.Size = new System.Drawing.Size(247, 20);
      this.dtPickerFechaAIncorporar.TabIndex = 3;
      // 
      // textBox3
      // 
      this.textBox3.BackColor = System.Drawing.Color.White;
      this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.textBox3.Location = new System.Drawing.Point(130, 33);
      this.textBox3.Multiline = true;
      this.textBox3.Name = "textBox3";
      this.textBox3.Size = new System.Drawing.Size(360, 33);
      this.textBox3.TabIndex = 6;
      this.textBox3.Text = "Procedimiento para la incorporacion de informacion de temperatura y precipitacion" +
    " desde archivos de Excel\r\n";
      this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(15, 62);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(99, 13);
      this.label3.TabIndex = 2;
      this.label3.Text = "Fecha a incorporar:";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.txtUltimaFechaProceso);
      this.groupBox2.Controls.Add(this.dtPickerFechaAProcesar);
      this.groupBox2.Controls.Add(this.label4);
      this.groupBox2.Controls.Add(this.btnProcesar);
      this.groupBox2.Controls.Add(this.label5);
      this.groupBox2.Location = new System.Drawing.Point(271, 89);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(264, 129);
      this.groupBox2.TabIndex = 12;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Procesamiento";
      // 
      // txtUltimaFechaProceso
      // 
      this.txtUltimaFechaProceso.Location = new System.Drawing.Point(23, 78);
      this.txtUltimaFechaProceso.Name = "txtUltimaFechaProceso";
      this.txtUltimaFechaProceso.Size = new System.Drawing.Size(247, 20);
      this.txtUltimaFechaProceso.TabIndex = 6;
      // 
      // dtPickerFechaAProcesar
      // 
      this.dtPickerFechaAProcesar.Location = new System.Drawing.Point(23, 35);
      this.dtPickerFechaAProcesar.Name = "dtPickerFechaAProcesar";
      this.dtPickerFechaAProcesar.Size = new System.Drawing.Size(247, 20);
      this.dtPickerFechaAProcesar.TabIndex = 4;
      // 
      // btnProcesar
      // 
      this.btnProcesar.Location = new System.Drawing.Point(102, 104);
      this.btnProcesar.Name = "btnProcesar";
      this.btnProcesar.Size = new System.Drawing.Size(70, 22);
      this.btnProcesar.TabIndex = 3;
      this.btnProcesar.Text = "Procesar";
      this.btnProcesar.UseVisualStyleBackColor = true;
      this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click_1);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(21, 20);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(151, 13);
      this.label5.TabIndex = 1;
      this.label5.Text = "Dia para procesar informacion:";
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLblProcesando,
            this.statusProgressBar});
      this.statusStrip1.Location = new System.Drawing.Point(0, 259);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(544, 22);
      this.statusStrip1.TabIndex = 7;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // statusLblProcesando
      // 
      this.statusLblProcesando.Name = "statusLblProcesando";
      this.statusLblProcesando.Size = new System.Drawing.Size(403, 17);
      this.statusLblProcesando.Text = "Incorporacion y Procesamiento de Lecturas de Precipitacion y Temperatura";
      // 
      // statusProgressBar
      // 
      this.statusProgressBar.Name = "statusProgressBar";
      this.statusProgressBar.Size = new System.Drawing.Size(100, 16);
      this.statusProgressBar.Visible = false;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.btnIncorporar);
      this.groupBox1.Controls.Add(this.dtPickerFechaAIncorporar);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.txtUltimoDiaIncorporacion);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Location = new System.Drawing.Point(6, 89);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(259, 129);
      this.groupBox1.TabIndex = 11;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Incorporacion";
      // 
      // txtUltimoDiaIncorporacion
      // 
      this.txtUltimoDiaIncorporacion.Location = new System.Drawing.Point(18, 36);
      this.txtUltimoDiaIncorporacion.Name = "txtUltimoDiaIncorporacion";
      this.txtUltimoDiaIncorporacion.Size = new System.Drawing.Size(247, 20);
      this.txtUltimoDiaIncorporacion.TabIndex = 1;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(13, 20);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(138, 13);
      this.label2.TabIndex = 0;
      this.label2.Text = "Ultimo dia de incorporacion:";
      // 
      // btnCerrar
      // 
      this.btnCerrar.Location = new System.Drawing.Point(460, 224);
      this.btnCerrar.Name = "btnCerrar";
      this.btnCerrar.Size = new System.Drawing.Size(75, 23);
      this.btnCerrar.TabIndex = 10;
      this.btnCerrar.Text = "&Cerrar";
      this.btnCerrar.UseVisualStyleBackColor = true;
      this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click_1);
      // 
      // picLogoIdeam
      // 
      this.picLogoIdeam.Location = new System.Drawing.Point(6, 4);
      this.picLogoIdeam.Name = "picLogoIdeam";
      this.picLogoIdeam.Size = new System.Drawing.Size(80, 79);
      this.picLogoIdeam.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.picLogoIdeam.TabIndex = 9;
      this.picLogoIdeam.TabStop = false;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.ForeColor = System.Drawing.Color.Blue;
      this.label1.Location = new System.Drawing.Point(92, 4);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(443, 16);
      this.label1.TabIndex = 8;
      this.label1.Text = "Sistema de Informacion Geografica para la Prevencion de Incendios";
      // 
      // FrmPreparacionInformacion
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(544, 281);
      this.Controls.Add(this.textBox3);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.btnCerrar);
      this.Controls.Add(this.picLogoIdeam);
      this.Controls.Add(this.label1);
      this.Name = "FrmPreparacionInformacion";
      this.Text = "FrmPreparacionInformacion";
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.picLogoIdeam)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnIncorporar;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.DateTimePicker dtPickerFechaAIncorporar;
    private System.Windows.Forms.TextBox textBox3;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.TextBox txtUltimaFechaProceso;
    private System.Windows.Forms.DateTimePicker dtPickerFechaAProcesar;
    private System.Windows.Forms.Button btnProcesar;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel statusLblProcesando;
    private System.Windows.Forms.ToolStripProgressBar statusProgressBar;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox txtUltimoDiaIncorporacion;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button btnCerrar;
    private System.Windows.Forms.PictureBox picLogoIdeam;
    private System.Windows.Forms.Label label1;
  }
}