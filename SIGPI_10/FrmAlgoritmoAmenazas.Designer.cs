namespace SIGPI_10
{
  partial class FrmAlgoritmoAmenazas
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAlgoritmoAmenazas));
      this.label1 = new System.Windows.Forms.Label();
      this.lblFechaAEjecutar = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.txtRutaNVI = new System.Windows.Forms.TextBox();
      this.txtRutaPrecipitacion = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.btnBuscarNVI = new System.Windows.Forms.Button();
      this.label5 = new System.Windows.Forms.Label();
      this.btnDelTemp = new System.Windows.Forms.Button();
      this.btnAbrirPrecipitacion = new System.Windows.Forms.Button();
      this.chkUtilizarImagenes = new System.Windows.Forms.CheckBox();
      this.btnEjecutar = new System.Windows.Forms.Button();
      this.chkVerResultadorIntermedios = new System.Windows.Forms.CheckBox();
      this.panelOpImagenes = new System.Windows.Forms.Panel();
      this.btnAbrirTemperatura = new System.Windows.Forms.Button();
      this.txtRutaTemperatura = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.btnCerrar = new System.Windows.Forms.Button();
      this.panelOpImagenes.SuspendLayout();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(158, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(251, 20);
      this.label1.TabIndex = 2;
      this.label1.Text = "Amenaza de Incendios Forestales";
      // 
      // lblFechaAEjecutar
      // 
      this.lblFechaAEjecutar.AutoSize = true;
      this.lblFechaAEjecutar.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.lblFechaAEjecutar.Location = new System.Drawing.Point(145, 32);
      this.lblFechaAEjecutar.Name = "lblFechaAEjecutar";
      this.lblFechaAEjecutar.Size = new System.Drawing.Size(37, 13);
      this.lblFechaAEjecutar.TabIndex = 12;
      this.lblFechaAEjecutar.Text = "Fecha";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(12, 32);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(127, 13);
      this.label4.TabIndex = 11;
      this.label4.Text = "Fecha a ejecutar modelo:";
      // 
      // richTextBox1
      // 
      this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.richTextBox1.Location = new System.Drawing.Point(12, 48);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.Size = new System.Drawing.Size(468, 113);
      this.richTextBox1.TabIndex = 10;
      this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
      // 
      // txtRutaNVI
      // 
      this.txtRutaNVI.Location = new System.Drawing.Point(94, 268);
      this.txtRutaNVI.Name = "txtRutaNVI";
      this.txtRutaNVI.Size = new System.Drawing.Size(345, 20);
      this.txtRutaNVI.TabIndex = 19;
      this.txtRutaNVI.Text = "C:\\SIGPI\\datos\\IMÁGENES\\01 NOVIEMBRE\\total24h.tif";
      this.txtRutaNVI.Visible = false;
      // 
      // txtRutaPrecipitacion
      // 
      this.txtRutaPrecipitacion.Location = new System.Drawing.Point(79, 35);
      this.txtRutaPrecipitacion.Name = "txtRutaPrecipitacion";
      this.txtRutaPrecipitacion.Size = new System.Drawing.Size(345, 20);
      this.txtRutaPrecipitacion.TabIndex = 4;
      this.txtRutaPrecipitacion.Text = "C:\\SIGPI\\datos\\NVDI\\r_20110401_20110410.img";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(3, 38);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(71, 13);
      this.label3.TabIndex = 3;
      this.label3.Text = "Precipitacion:";
      // 
      // btnBuscarNVI
      // 
      this.btnBuscarNVI.Location = new System.Drawing.Point(441, 265);
      this.btnBuscarNVI.Name = "btnBuscarNVI";
      this.btnBuscarNVI.Size = new System.Drawing.Size(32, 23);
      this.btnBuscarNVI.TabIndex = 21;
      this.btnBuscarNVI.UseVisualStyleBackColor = true;
      this.btnBuscarNVI.Visible = false;
      this.btnBuscarNVI.Click += new System.EventHandler(this.btnBuscarNVI_Click_1);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(18, 273);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(61, 13);
      this.label5.TabIndex = 17;
      this.label5.Text = "Indice Veg:";
      this.label5.Visible = false;
      // 
      // btnDelTemp
      // 
      this.btnDelTemp.Location = new System.Drawing.Point(12, 313);
      this.btnDelTemp.Name = "btnDelTemp";
      this.btnDelTemp.Size = new System.Drawing.Size(125, 23);
      this.btnDelTemp.TabIndex = 16;
      this.btnDelTemp.Text = "Borrar Temporales";
      this.btnDelTemp.UseVisualStyleBackColor = true;
      this.btnDelTemp.Click += new System.EventHandler(this.btnDelTemp_Click_1);
      // 
      // btnAbrirPrecipitacion
      // 
      this.btnAbrirPrecipitacion.Location = new System.Drawing.Point(426, 35);
      this.btnAbrirPrecipitacion.Name = "btnAbrirPrecipitacion";
      this.btnAbrirPrecipitacion.Size = new System.Drawing.Size(32, 23);
      this.btnAbrirPrecipitacion.TabIndex = 5;
      this.btnAbrirPrecipitacion.UseVisualStyleBackColor = true;
      this.btnAbrirPrecipitacion.Click += new System.EventHandler(this.btnAbrirPrecipitacion_Click_1);
      // 
      // chkUtilizarImagenes
      // 
      this.chkUtilizarImagenes.AutoSize = true;
      this.chkUtilizarImagenes.Location = new System.Drawing.Point(15, 167);
      this.chkUtilizarImagenes.Name = "chkUtilizarImagenes";
      this.chkUtilizarImagenes.Size = new System.Drawing.Size(162, 17);
      this.chkUtilizarImagenes.TabIndex = 14;
      this.chkUtilizarImagenes.Text = "Utilizar Imagenes de Satelite ";
      this.chkUtilizarImagenes.UseVisualStyleBackColor = true;
      this.chkUtilizarImagenes.Visible = false;
      // 
      // btnEjecutar
      // 
      this.btnEjecutar.Location = new System.Drawing.Point(317, 313);
      this.btnEjecutar.Name = "btnEjecutar";
      this.btnEjecutar.Size = new System.Drawing.Size(75, 23);
      this.btnEjecutar.TabIndex = 13;
      this.btnEjecutar.Text = "Ejecutar";
      this.btnEjecutar.UseVisualStyleBackColor = true;
      this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click_1);
      // 
      // chkVerResultadorIntermedios
      // 
      this.chkVerResultadorIntermedios.AutoSize = true;
      this.chkVerResultadorIntermedios.Location = new System.Drawing.Point(302, 167);
      this.chkVerResultadorIntermedios.Name = "chkVerResultadorIntermedios";
      this.chkVerResultadorIntermedios.Size = new System.Drawing.Size(174, 17);
      this.chkVerResultadorIntermedios.TabIndex = 20;
      this.chkVerResultadorIntermedios.Text = "Mostrar Resultados Intermedios";
      this.chkVerResultadorIntermedios.UseVisualStyleBackColor = true;
      // 
      // panelOpImagenes
      // 
      this.panelOpImagenes.Controls.Add(this.btnAbrirPrecipitacion);
      this.panelOpImagenes.Controls.Add(this.txtRutaPrecipitacion);
      this.panelOpImagenes.Controls.Add(this.label3);
      this.panelOpImagenes.Controls.Add(this.btnAbrirTemperatura);
      this.panelOpImagenes.Controls.Add(this.txtRutaTemperatura);
      this.panelOpImagenes.Controls.Add(this.label2);
      this.panelOpImagenes.Location = new System.Drawing.Point(15, 190);
      this.panelOpImagenes.Name = "panelOpImagenes";
      this.panelOpImagenes.Size = new System.Drawing.Size(461, 67);
      this.panelOpImagenes.TabIndex = 18;
      this.panelOpImagenes.Visible = false;
      // 
      // btnAbrirTemperatura
      // 
      this.btnAbrirTemperatura.Location = new System.Drawing.Point(426, 9);
      this.btnAbrirTemperatura.Name = "btnAbrirTemperatura";
      this.btnAbrirTemperatura.Size = new System.Drawing.Size(32, 23);
      this.btnAbrirTemperatura.TabIndex = 2;
      this.btnAbrirTemperatura.UseVisualStyleBackColor = true;
      this.btnAbrirTemperatura.Visible = false;
      this.btnAbrirTemperatura.Click += new System.EventHandler(this.btnAbrirTemperatura_Click_1);
      // 
      // txtRutaTemperatura
      // 
      this.txtRutaTemperatura.Location = new System.Drawing.Point(79, 9);
      this.txtRutaTemperatura.Name = "txtRutaTemperatura";
      this.txtRutaTemperatura.Size = new System.Drawing.Size(345, 20);
      this.txtRutaTemperatura.TabIndex = 1;
      this.txtRutaTemperatura.Visible = false;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(3, 12);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(70, 13);
      this.label2.TabIndex = 0;
      this.label2.Text = "Temperatura:";
      this.label2.Visible = false;
      // 
      // btnCerrar
      // 
      this.btnCerrar.Location = new System.Drawing.Point(398, 313);
      this.btnCerrar.Name = "btnCerrar";
      this.btnCerrar.Size = new System.Drawing.Size(75, 23);
      this.btnCerrar.TabIndex = 15;
      this.btnCerrar.Text = "&Cerrar";
      this.btnCerrar.UseVisualStyleBackColor = true;
      this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click_1);
      // 
      // FrmAlgoritmoAmenazas
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(492, 339);
      this.Controls.Add(this.txtRutaNVI);
      this.Controls.Add(this.btnBuscarNVI);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.btnDelTemp);
      this.Controls.Add(this.chkUtilizarImagenes);
      this.Controls.Add(this.btnEjecutar);
      this.Controls.Add(this.chkVerResultadorIntermedios);
      this.Controls.Add(this.panelOpImagenes);
      this.Controls.Add(this.btnCerrar);
      this.Controls.Add(this.lblFechaAEjecutar);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.richTextBox1);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "FrmAlgoritmoAmenazas";
      this.Text = "Algoritmo Amenazas";
      this.panelOpImagenes.ResumeLayout(false);
      this.panelOpImagenes.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lblFechaAEjecutar;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.TextBox txtRutaNVI;
    private System.Windows.Forms.TextBox txtRutaPrecipitacion;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button btnBuscarNVI;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Button btnDelTemp;
    private System.Windows.Forms.Button btnAbrirPrecipitacion;
    private System.Windows.Forms.CheckBox chkUtilizarImagenes;
    private System.Windows.Forms.Button btnEjecutar;
    private System.Windows.Forms.CheckBox chkVerResultadorIntermedios;
    private System.Windows.Forms.Panel panelOpImagenes;
    private System.Windows.Forms.Button btnAbrirTemperatura;
    private System.Windows.Forms.TextBox txtRutaTemperatura;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button btnCerrar;
  }
}