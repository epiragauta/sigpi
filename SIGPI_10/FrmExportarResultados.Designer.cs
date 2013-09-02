namespace SIGPI_10
{
  partial class FrmExportarResultados
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
      this.btnCerrar = new System.Windows.Forms.Button();
      this.btnExp2Map = new System.Windows.Forms.Button();
      this.radioBtnModelo = new System.Windows.Forms.RadioButton();
      this.radioBtnProbabilidad = new System.Windows.Forms.RadioButton();
      this.lvwModelos = new System.Windows.Forms.ListView();
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.btnExportarAExcel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // btnCerrar
      // 
      this.btnCerrar.Location = new System.Drawing.Point(196, 274);
      this.btnCerrar.Name = "btnCerrar";
      this.btnCerrar.Size = new System.Drawing.Size(75, 23);
      this.btnCerrar.TabIndex = 11;
      this.btnCerrar.Text = "&Cerrar";
      this.btnCerrar.UseVisualStyleBackColor = true;
      this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
      // 
      // btnExp2Map
      // 
      this.btnExp2Map.Enabled = false;
      this.btnExp2Map.Location = new System.Drawing.Point(196, 237);
      this.btnExp2Map.Name = "btnExp2Map";
      this.btnExp2Map.Size = new System.Drawing.Size(75, 23);
      this.btnExp2Map.TabIndex = 10;
      this.btnExp2Map.Text = "Mapa";
      this.btnExp2Map.UseVisualStyleBackColor = true;
      this.btnExp2Map.Click += new System.EventHandler(this.btnExp2Map_Click);
      // 
      // radioBtnModelo
      // 
      this.radioBtnModelo.AutoSize = true;
      this.radioBtnModelo.Location = new System.Drawing.Point(127, 6);
      this.radioBtnModelo.Name = "radioBtnModelo";
      this.radioBtnModelo.Size = new System.Drawing.Size(60, 17);
      this.radioBtnModelo.TabIndex = 9;
      this.radioBtnModelo.TabStop = true;
      this.radioBtnModelo.Text = "Modelo";
      this.radioBtnModelo.UseVisualStyleBackColor = true;
      this.radioBtnModelo.CheckedChanged += new System.EventHandler(this.radioBtnModelo_CheckedChanged);
      // 
      // radioBtnProbabilidad
      // 
      this.radioBtnProbabilidad.AutoSize = true;
      this.radioBtnProbabilidad.Location = new System.Drawing.Point(12, 6);
      this.radioBtnProbabilidad.Name = "radioBtnProbabilidad";
      this.radioBtnProbabilidad.Size = new System.Drawing.Size(83, 17);
      this.radioBtnProbabilidad.TabIndex = 8;
      this.radioBtnProbabilidad.TabStop = true;
      this.radioBtnProbabilidad.Text = "Probabilidad";
      this.radioBtnProbabilidad.UseVisualStyleBackColor = true;
      this.radioBtnProbabilidad.CheckedChanged += new System.EventHandler(this.radioBtnProbabilidad_CheckedChanged);
      // 
      // lvwModelos
      // 
      this.lvwModelos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
      this.lvwModelos.FullRowSelect = true;
      this.lvwModelos.GridLines = true;
      this.lvwModelos.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this.lvwModelos.Location = new System.Drawing.Point(12, 29);
      this.lvwModelos.MultiSelect = false;
      this.lvwModelos.Name = "lvwModelos";
      this.lvwModelos.Size = new System.Drawing.Size(175, 268);
      this.lvwModelos.TabIndex = 6;
      this.lvwModelos.UseCompatibleStateImageBehavior = false;
      this.lvwModelos.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Capas";
      this.columnHeader1.Width = 171;
      // 
      // btnExportarAExcel
      // 
      this.btnExportarAExcel.Enabled = false;
      this.btnExportarAExcel.Location = new System.Drawing.Point(196, 208);
      this.btnExportarAExcel.Name = "btnExportarAExcel";
      this.btnExportarAExcel.Size = new System.Drawing.Size(75, 23);
      this.btnExportarAExcel.TabIndex = 7;
      this.btnExportarAExcel.Text = "Excel";
      this.btnExportarAExcel.UseVisualStyleBackColor = true;
      this.btnExportarAExcel.Click += new System.EventHandler(this.btnExportarAExcel_Click);
      // 
      // FrmExportarResultados
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(283, 308);
      this.Controls.Add(this.btnCerrar);
      this.Controls.Add(this.btnExp2Map);
      this.Controls.Add(this.radioBtnModelo);
      this.Controls.Add(this.radioBtnProbabilidad);
      this.Controls.Add(this.lvwModelos);
      this.Controls.Add(this.btnExportarAExcel);
      this.Name = "FrmExportarResultados";
      this.Text = "FrmExportarResultados";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnCerrar;
    private System.Windows.Forms.Button btnExp2Map;
    private System.Windows.Forms.RadioButton radioBtnModelo;
    private System.Windows.Forms.RadioButton radioBtnProbabilidad;
    private System.Windows.Forms.ListView lvwModelos;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.Button btnExportarAExcel;
  }
}