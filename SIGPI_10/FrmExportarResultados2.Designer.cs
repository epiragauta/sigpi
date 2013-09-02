namespace SIGPI_10
{
  partial class FrmExportarResultados2
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
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.lblCapaAsociada = new System.Windows.Forms.Label();
      this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
      this.btnCerrar = new System.Windows.Forms.Button();
      this.btnExp2Map = new System.Windows.Forms.Button();
      this.btnExportarAExcel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // progressBar1
      // 
      this.progressBar1.Location = new System.Drawing.Point(5, 192);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(328, 23);
      this.progressBar1.TabIndex = 14;
      this.progressBar1.Visible = false;
      // 
      // lblCapaAsociada
      // 
      this.lblCapaAsociada.AutoSize = true;
      this.lblCapaAsociada.Location = new System.Drawing.Point(2, 176);
      this.lblCapaAsociada.Name = "lblCapaAsociada";
      this.lblCapaAsociada.Size = new System.Drawing.Size(35, 13);
      this.lblCapaAsociada.TabIndex = 13;
      this.lblCapaAsociada.Text = "label1";
      // 
      // monthCalendar1
      // 
      this.monthCalendar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.monthCalendar1.Location = new System.Drawing.Point(63, 7);
      this.monthCalendar1.Name = "monthCalendar1";
      this.monthCalendar1.TabIndex = 12;
      // 
      // btnCerrar
      // 
      this.btnCerrar.Location = new System.Drawing.Point(215, 219);
      this.btnCerrar.Name = "btnCerrar";
      this.btnCerrar.Size = new System.Drawing.Size(75, 23);
      this.btnCerrar.TabIndex = 11;
      this.btnCerrar.Text = "&Cerrar";
      this.btnCerrar.UseVisualStyleBackColor = true;
      this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click_1);
      // 
      // btnExp2Map
      // 
      this.btnExp2Map.Enabled = false;
      this.btnExp2Map.Location = new System.Drawing.Point(121, 219);
      this.btnExp2Map.Name = "btnExp2Map";
      this.btnExp2Map.Size = new System.Drawing.Size(75, 23);
      this.btnExp2Map.TabIndex = 10;
      this.btnExp2Map.Text = "Mapa";
      this.btnExp2Map.UseVisualStyleBackColor = true;
      this.btnExp2Map.Click += new System.EventHandler(this.btnExp2Map_Click_1);
      // 
      // btnExportarAExcel
      // 
      this.btnExportarAExcel.Enabled = false;
      this.btnExportarAExcel.Location = new System.Drawing.Point(40, 219);
      this.btnExportarAExcel.Name = "btnExportarAExcel";
      this.btnExportarAExcel.Size = new System.Drawing.Size(75, 23);
      this.btnExportarAExcel.TabIndex = 9;
      this.btnExportarAExcel.Text = "Excel";
      this.btnExportarAExcel.UseVisualStyleBackColor = true;
      this.btnExportarAExcel.Click += new System.EventHandler(this.btnExportarAExcel_Click_1);
      // 
      // FrmExportarResultados2
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(339, 255);
      this.Controls.Add(this.progressBar1);
      this.Controls.Add(this.lblCapaAsociada);
      this.Controls.Add(this.monthCalendar1);
      this.Controls.Add(this.btnCerrar);
      this.Controls.Add(this.btnExp2Map);
      this.Controls.Add(this.btnExportarAExcel);
      this.Name = "FrmExportarResultados2";
      this.Text = "FrmExportarResultados2";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.Label lblCapaAsociada;
    private System.Windows.Forms.MonthCalendar monthCalendar1;
    private System.Windows.Forms.Button btnCerrar;
    private System.Windows.Forms.Button btnExp2Map;
    private System.Windows.Forms.Button btnExportarAExcel;
  }
}