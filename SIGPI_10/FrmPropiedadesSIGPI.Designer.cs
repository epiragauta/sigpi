namespace SIGPI_10
{
  partial class FrmPropiedadesSIGPI
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
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabParametrosIniciales = new System.Windows.Forms.TabPage();
      this.txtPorcentajeLecturas = new System.Windows.Forms.TextBox();
      this.label11 = new System.Windows.Forms.Label();
      this.txtDirCapasTMMM = new System.Windows.Forms.TextBox();
      this.label10 = new System.Windows.Forms.Label();
      this.txtDirCapasRaster = new System.Windows.Forms.TextBox();
      this.label9 = new System.Windows.Forms.Label();
      this.txtDirLecturas = new System.Windows.Forms.TextBox();
      this.label8 = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.txtGDBClave = new System.Windows.Forms.TextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.txtGDBUsuario = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.txtGDB = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.txtGDBInstancia = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.txtGDBServidor = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.button1 = new System.Windows.Forms.Button();
      this.txtBDLocal = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.btnDirBase = new System.Windows.Forms.Button();
      this.txtDirBase = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.btnFolderOpen = new System.Windows.Forms.Button();
      this.txtDirectorioResultados = new System.Windows.Forms.TextBox();
      this.lblDirectorio = new System.Windows.Forms.Label();
      this.cboCapaSusceptibilidad = new System.Windows.Forms.ComboBox();
      this.lblCapaSusceptibilidad = new System.Windows.Forms.Label();
      this.txtTamanoCelda = new System.Windows.Forms.TextBox();
      this.lblCellSize = new System.Windows.Forms.Label();
      this.tabParamNuevoAlgoritmo = new System.Windows.Forms.TabPage();
      this.btnAbriGeoDB = new System.Windows.Forms.Button();
      this.txtRutaGDB = new System.Windows.Forms.TextBox();
      this.label24 = new System.Windows.Forms.Label();
      this.cboTipoEstadistico = new System.Windows.Forms.ComboBox();
      this.label23 = new System.Windows.Forms.Label();
      this.btnAbrirTablaReclassIncendios = new System.Windows.Forms.Button();
      this.txtRutaTableReclassIncendios = new System.Windows.Forms.TextBox();
      this.label22 = new System.Windows.Forms.Label();
      this.btnAbrirTablaReclassPrecip = new System.Windows.Forms.Button();
      this.txtRutaTblReclassPrecipitacion = new System.Windows.Forms.TextBox();
      this.label21 = new System.Windows.Forms.Label();
      this.btnAbrirTablaReclassTemp = new System.Windows.Forms.Button();
      this.txtRutaTblReclassTemperatura = new System.Windows.Forms.TextBox();
      this.label20 = new System.Windows.Forms.Label();
      this.txtPesoAmenazasParciales = new System.Windows.Forms.TextBox();
      this.label15 = new System.Windows.Forms.Label();
      this.txtPesoTemperatura = new System.Windows.Forms.TextBox();
      this.label14 = new System.Windows.Forms.Label();
      this.txtPesoPrecipitacion = new System.Windows.Forms.TextBox();
      this.label13 = new System.Windows.Forms.Label();
      this.label12 = new System.Windows.Forms.Label();
      this.dgvPesos = new System.Windows.Forms.DataGridView();
      this.colDia0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colDia1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colDia2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colDia3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colDia4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colDia5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colDia6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colDia7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colDia8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colDia9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.btnAceptar = new System.Windows.Forms.Button();
      this.btnCancelar = new System.Windows.Forms.Button();
      this.folderDlg = new System.Windows.Forms.FolderBrowserDialog();
      this.openDlg = new System.Windows.Forms.OpenFileDialog();
      this.tabControl1.SuspendLayout();
      this.tabParametrosIniciales.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.tabParamNuevoAlgoritmo.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvPesos)).BeginInit();
      this.SuspendLayout();
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabParametrosIniciales);
      this.tabControl1.Controls.Add(this.tabParamNuevoAlgoritmo);
      this.tabControl1.Location = new System.Drawing.Point(8, 8);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(418, 423);
      this.tabControl1.TabIndex = 25;
      // 
      // tabParametrosIniciales
      // 
      this.tabParametrosIniciales.Controls.Add(this.txtPorcentajeLecturas);
      this.tabParametrosIniciales.Controls.Add(this.label11);
      this.tabParametrosIniciales.Controls.Add(this.txtDirCapasTMMM);
      this.tabParametrosIniciales.Controls.Add(this.label10);
      this.tabParametrosIniciales.Controls.Add(this.txtDirCapasRaster);
      this.tabParametrosIniciales.Controls.Add(this.label9);
      this.tabParametrosIniciales.Controls.Add(this.txtDirLecturas);
      this.tabParametrosIniciales.Controls.Add(this.label8);
      this.tabParametrosIniciales.Controls.Add(this.groupBox1);
      this.tabParametrosIniciales.Controls.Add(this.button1);
      this.tabParametrosIniciales.Controls.Add(this.txtBDLocal);
      this.tabParametrosIniciales.Controls.Add(this.label2);
      this.tabParametrosIniciales.Controls.Add(this.btnDirBase);
      this.tabParametrosIniciales.Controls.Add(this.txtDirBase);
      this.tabParametrosIniciales.Controls.Add(this.label1);
      this.tabParametrosIniciales.Controls.Add(this.btnFolderOpen);
      this.tabParametrosIniciales.Controls.Add(this.txtDirectorioResultados);
      this.tabParametrosIniciales.Controls.Add(this.lblDirectorio);
      this.tabParametrosIniciales.Controls.Add(this.cboCapaSusceptibilidad);
      this.tabParametrosIniciales.Controls.Add(this.lblCapaSusceptibilidad);
      this.tabParametrosIniciales.Controls.Add(this.txtTamanoCelda);
      this.tabParametrosIniciales.Controls.Add(this.lblCellSize);
      this.tabParametrosIniciales.Location = new System.Drawing.Point(4, 22);
      this.tabParametrosIniciales.Name = "tabParametrosIniciales";
      this.tabParametrosIniciales.Padding = new System.Windows.Forms.Padding(3);
      this.tabParametrosIniciales.Size = new System.Drawing.Size(410, 397);
      this.tabParametrosIniciales.TabIndex = 0;
      this.tabParametrosIniciales.Text = "Parametros Iniciales";
      this.tabParametrosIniciales.UseVisualStyleBackColor = true;
      // 
      // txtPorcentajeLecturas
      // 
      this.txtPorcentajeLecturas.Location = new System.Drawing.Point(188, 209);
      this.txtPorcentajeLecturas.Name = "txtPorcentajeLecturas";
      this.txtPorcentajeLecturas.Size = new System.Drawing.Size(68, 20);
      this.txtPorcentajeLecturas.TabIndex = 39;
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(6, 212);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(177, 13);
      this.label11.TabIndex = 47;
      this.label11.Text = "Porcentaje efectivo de lecturas (%): ";
      // 
      // txtDirCapasTMMM
      // 
      this.txtDirCapasTMMM.Location = new System.Drawing.Point(135, 183);
      this.txtDirCapasTMMM.Name = "txtDirCapasTMMM";
      this.txtDirCapasTMMM.Size = new System.Drawing.Size(244, 20);
      this.txtDirCapasTMMM.TabIndex = 38;
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(6, 186);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(124, 13);
      this.label10.TabIndex = 46;
      this.label10.Text = "Directorio capas TMMM:";
      // 
      // txtDirCapasRaster
      // 
      this.txtDirCapasRaster.Location = new System.Drawing.Point(135, 157);
      this.txtDirCapasRaster.Name = "txtDirCapasRaster";
      this.txtDirCapasRaster.Size = new System.Drawing.Size(244, 20);
      this.txtDirCapasRaster.TabIndex = 37;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(6, 160);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(116, 13);
      this.label9.TabIndex = 45;
      this.label9.Text = "Directorio capas raster:";
      // 
      // txtDirLecturas
      // 
      this.txtDirLecturas.Location = new System.Drawing.Point(135, 131);
      this.txtDirLecturas.Name = "txtDirLecturas";
      this.txtDirLecturas.Size = new System.Drawing.Size(244, 20);
      this.txtDirLecturas.TabIndex = 35;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(6, 134);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(95, 13);
      this.label8.TabIndex = 42;
      this.label8.Text = "Directorio lecturas:";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.txtGDBClave);
      this.groupBox1.Controls.Add(this.label7);
      this.groupBox1.Controls.Add(this.txtGDBUsuario);
      this.groupBox1.Controls.Add(this.label6);
      this.groupBox1.Controls.Add(this.txtGDB);
      this.groupBox1.Controls.Add(this.label5);
      this.groupBox1.Controls.Add(this.txtGDBInstancia);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.txtGDBServidor);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Location = new System.Drawing.Point(9, 235);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(398, 152);
      this.groupBox1.TabIndex = 41;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Parametros de conexion a la Geodatabase";
      // 
      // txtGDBClave
      // 
      this.txtGDBClave.Location = new System.Drawing.Point(126, 120);
      this.txtGDBClave.Name = "txtGDBClave";
      this.txtGDBClave.PasswordChar = '*';
      this.txtGDBClave.Size = new System.Drawing.Size(244, 20);
      this.txtGDBClave.TabIndex = 17;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(8, 122);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(64, 13);
      this.label7.TabIndex = 25;
      this.label7.Text = "Contraseña:";
      // 
      // txtGDBUsuario
      // 
      this.txtGDBUsuario.Location = new System.Drawing.Point(126, 94);
      this.txtGDBUsuario.Name = "txtGDBUsuario";
      this.txtGDBUsuario.Size = new System.Drawing.Size(244, 20);
      this.txtGDBUsuario.TabIndex = 16;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(8, 96);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(46, 13);
      this.label6.TabIndex = 23;
      this.label6.Text = "Usuario:";
      // 
      // txtGDB
      // 
      this.txtGDB.Location = new System.Drawing.Point(126, 68);
      this.txtGDB.Name = "txtGDB";
      this.txtGDB.Size = new System.Drawing.Size(244, 20);
      this.txtGDB.TabIndex = 15;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(8, 70);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(78, 13);
      this.label5.TabIndex = 21;
      this.label5.Text = "Base de datos:";
      // 
      // txtGDBInstancia
      // 
      this.txtGDBInstancia.Location = new System.Drawing.Point(126, 42);
      this.txtGDBInstancia.Name = "txtGDBInstancia";
      this.txtGDBInstancia.Size = new System.Drawing.Size(244, 20);
      this.txtGDBInstancia.TabIndex = 14;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(8, 44);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(50, 13);
      this.label4.TabIndex = 19;
      this.label4.Text = "Instancia";
      // 
      // txtGDBServidor
      // 
      this.txtGDBServidor.Location = new System.Drawing.Point(126, 16);
      this.txtGDBServidor.Name = "txtGDBServidor";
      this.txtGDBServidor.Size = new System.Drawing.Size(244, 20);
      this.txtGDBServidor.TabIndex = 13;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(8, 18);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(49, 13);
      this.label3.TabIndex = 17;
      this.label3.Text = "Servidor:";
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(385, 26);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(22, 23);
      this.button1.TabIndex = 30;
      this.button1.UseVisualStyleBackColor = true;
      // 
      // txtBDLocal
      // 
      this.txtBDLocal.Location = new System.Drawing.Point(135, 26);
      this.txtBDLocal.Name = "txtBDLocal";
      this.txtBDLocal.Size = new System.Drawing.Size(244, 20);
      this.txtBDLocal.TabIndex = 28;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 30);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(78, 13);
      this.label2.TabIndex = 40;
      this.label2.Text = "Base de datos:";
      // 
      // btnDirBase
      // 
      this.btnDirBase.Location = new System.Drawing.Point(385, 0);
      this.btnDirBase.Name = "btnDirBase";
      this.btnDirBase.Size = new System.Drawing.Size(22, 23);
      this.btnDirBase.TabIndex = 27;
      this.btnDirBase.UseVisualStyleBackColor = true;
      // 
      // txtDirBase
      // 
      this.txtDirBase.Location = new System.Drawing.Point(135, 0);
      this.txtDirBase.Name = "txtDirBase";
      this.txtDirBase.Size = new System.Drawing.Size(244, 20);
      this.txtDirBase.TabIndex = 25;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 3);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(81, 13);
      this.label1.TabIndex = 36;
      this.label1.Text = "Directorio base:";
      // 
      // btnFolderOpen
      // 
      this.btnFolderOpen.Location = new System.Drawing.Point(385, 104);
      this.btnFolderOpen.Name = "btnFolderOpen";
      this.btnFolderOpen.Size = new System.Drawing.Size(22, 23);
      this.btnFolderOpen.TabIndex = 34;
      this.btnFolderOpen.UseVisualStyleBackColor = true;
      // 
      // txtDirectorioResultados
      // 
      this.txtDirectorioResultados.Location = new System.Drawing.Point(135, 104);
      this.txtDirectorioResultados.Name = "txtDirectorioResultados";
      this.txtDirectorioResultados.Size = new System.Drawing.Size(244, 20);
      this.txtDirectorioResultados.TabIndex = 33;
      // 
      // lblDirectorio
      // 
      this.lblDirectorio.AutoSize = true;
      this.lblDirectorio.Location = new System.Drawing.Point(6, 108);
      this.lblDirectorio.Name = "lblDirectorio";
      this.lblDirectorio.Size = new System.Drawing.Size(121, 13);
      this.lblDirectorio.TabIndex = 29;
      this.lblDirectorio.Text = "Directorio de resultados:";
      // 
      // cboCapaSusceptibilidad
      // 
      this.cboCapaSusceptibilidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboCapaSusceptibilidad.FormattingEnabled = true;
      this.cboCapaSusceptibilidad.Location = new System.Drawing.Point(135, 77);
      this.cboCapaSusceptibilidad.Name = "cboCapaSusceptibilidad";
      this.cboCapaSusceptibilidad.Size = new System.Drawing.Size(121, 21);
      this.cboCapaSusceptibilidad.TabIndex = 32;
      // 
      // lblCapaSusceptibilidad
      // 
      this.lblCapaSusceptibilidad.AutoSize = true;
      this.lblCapaSusceptibilidad.Location = new System.Drawing.Point(6, 81);
      this.lblCapaSusceptibilidad.Name = "lblCapaSusceptibilidad";
      this.lblCapaSusceptibilidad.Size = new System.Drawing.Size(122, 13);
      this.lblCapaSusceptibilidad.TabIndex = 26;
      this.lblCapaSusceptibilidad.Text = "Capa de susceptibilidad:";
      // 
      // txtTamanoCelda
      // 
      this.txtTamanoCelda.Location = new System.Drawing.Point(135, 51);
      this.txtTamanoCelda.Name = "txtTamanoCelda";
      this.txtTamanoCelda.Size = new System.Drawing.Size(121, 20);
      this.txtTamanoCelda.TabIndex = 31;
      // 
      // lblCellSize
      // 
      this.lblCellSize.AutoSize = true;
      this.lblCellSize.Location = new System.Drawing.Point(6, 54);
      this.lblCellSize.Name = "lblCellSize";
      this.lblCellSize.Size = new System.Drawing.Size(93, 13);
      this.lblCellSize.TabIndex = 24;
      this.lblCellSize.Text = "Tamaño de celda:";
      // 
      // tabParamNuevoAlgoritmo
      // 
      this.tabParamNuevoAlgoritmo.Controls.Add(this.btnAbriGeoDB);
      this.tabParamNuevoAlgoritmo.Controls.Add(this.txtRutaGDB);
      this.tabParamNuevoAlgoritmo.Controls.Add(this.label24);
      this.tabParamNuevoAlgoritmo.Controls.Add(this.cboTipoEstadistico);
      this.tabParamNuevoAlgoritmo.Controls.Add(this.label23);
      this.tabParamNuevoAlgoritmo.Controls.Add(this.btnAbrirTablaReclassIncendios);
      this.tabParamNuevoAlgoritmo.Controls.Add(this.txtRutaTableReclassIncendios);
      this.tabParamNuevoAlgoritmo.Controls.Add(this.label22);
      this.tabParamNuevoAlgoritmo.Controls.Add(this.btnAbrirTablaReclassPrecip);
      this.tabParamNuevoAlgoritmo.Controls.Add(this.txtRutaTblReclassPrecipitacion);
      this.tabParamNuevoAlgoritmo.Controls.Add(this.label21);
      this.tabParamNuevoAlgoritmo.Controls.Add(this.btnAbrirTablaReclassTemp);
      this.tabParamNuevoAlgoritmo.Controls.Add(this.txtRutaTblReclassTemperatura);
      this.tabParamNuevoAlgoritmo.Controls.Add(this.label20);
      this.tabParamNuevoAlgoritmo.Controls.Add(this.txtPesoAmenazasParciales);
      this.tabParamNuevoAlgoritmo.Controls.Add(this.label15);
      this.tabParamNuevoAlgoritmo.Controls.Add(this.txtPesoTemperatura);
      this.tabParamNuevoAlgoritmo.Controls.Add(this.label14);
      this.tabParamNuevoAlgoritmo.Controls.Add(this.txtPesoPrecipitacion);
      this.tabParamNuevoAlgoritmo.Controls.Add(this.label13);
      this.tabParamNuevoAlgoritmo.Controls.Add(this.label12);
      this.tabParamNuevoAlgoritmo.Controls.Add(this.dgvPesos);
      this.tabParamNuevoAlgoritmo.Location = new System.Drawing.Point(4, 22);
      this.tabParamNuevoAlgoritmo.Name = "tabParamNuevoAlgoritmo";
      this.tabParamNuevoAlgoritmo.Padding = new System.Windows.Forms.Padding(3);
      this.tabParamNuevoAlgoritmo.Size = new System.Drawing.Size(410, 397);
      this.tabParamNuevoAlgoritmo.TabIndex = 1;
      this.tabParamNuevoAlgoritmo.Text = "Parametros Algoritmo Amenazas";
      this.tabParamNuevoAlgoritmo.UseVisualStyleBackColor = true;
      // 
      // btnAbriGeoDB
      // 
      this.btnAbriGeoDB.Location = new System.Drawing.Point(376, 18);
      this.btnAbriGeoDB.Name = "btnAbriGeoDB";
      this.btnAbriGeoDB.Size = new System.Drawing.Size(22, 23);
      this.btnAbriGeoDB.TabIndex = 46;
      this.btnAbriGeoDB.UseVisualStyleBackColor = true;
      // 
      // txtRutaGDB
      // 
      this.txtRutaGDB.Location = new System.Drawing.Point(9, 21);
      this.txtRutaGDB.Name = "txtRutaGDB";
      this.txtRutaGDB.Size = new System.Drawing.Size(361, 20);
      this.txtRutaGDB.TabIndex = 45;
      // 
      // label24
      // 
      this.label24.AutoSize = true;
      this.label24.Location = new System.Drawing.Point(6, 5);
      this.label24.Name = "label24";
      this.label24.Size = new System.Drawing.Size(164, 13);
      this.label24.TabIndex = 44;
      this.label24.Text = "Geodatabase Información Basica";
      // 
      // cboTipoEstadistico
      // 
      this.cboTipoEstadistico.FormattingEnabled = true;
      this.cboTipoEstadistico.Location = new System.Drawing.Point(247, 355);
      this.cboTipoEstadistico.Name = "cboTipoEstadistico";
      this.cboTipoEstadistico.Size = new System.Drawing.Size(151, 21);
      this.cboTipoEstadistico.TabIndex = 43;
      // 
      // label23
      // 
      this.label23.AutoSize = true;
      this.label23.Location = new System.Drawing.Point(9, 360);
      this.label23.Name = "label23";
      this.label23.Size = new System.Drawing.Size(235, 13);
      this.label23.TabIndex = 42;
      this.label23.Text = "Estadistico Utilizado para Combinacion Modelos:";
      // 
      // btnAbrirTablaReclassIncendios
      // 
      this.btnAbrirTablaReclassIncendios.Location = new System.Drawing.Point(376, 329);
      this.btnAbrirTablaReclassIncendios.Name = "btnAbrirTablaReclassIncendios";
      this.btnAbrirTablaReclassIncendios.Size = new System.Drawing.Size(22, 23);
      this.btnAbrirTablaReclassIncendios.TabIndex = 41;
      this.btnAbrirTablaReclassIncendios.UseVisualStyleBackColor = true;
      // 
      // txtRutaTableReclassIncendios
      // 
      this.txtRutaTableReclassIncendios.Location = new System.Drawing.Point(12, 329);
      this.txtRutaTableReclassIncendios.Name = "txtRutaTableReclassIncendios";
      this.txtRutaTableReclassIncendios.Size = new System.Drawing.Size(361, 20);
      this.txtRutaTableReclassIncendios.TabIndex = 40;
      // 
      // label22
      // 
      this.label22.AutoSize = true;
      this.label22.Location = new System.Drawing.Point(9, 313);
      this.label22.Name = "label22";
      this.label22.Size = new System.Drawing.Size(161, 13);
      this.label22.TabIndex = 39;
      this.label22.Text = "Tabla Reclasificacion Incendios:";
      // 
      // btnAbrirTablaReclassPrecip
      // 
      this.btnAbrirTablaReclassPrecip.Location = new System.Drawing.Point(376, 290);
      this.btnAbrirTablaReclassPrecip.Name = "btnAbrirTablaReclassPrecip";
      this.btnAbrirTablaReclassPrecip.Size = new System.Drawing.Size(22, 23);
      this.btnAbrirTablaReclassPrecip.TabIndex = 38;
      this.btnAbrirTablaReclassPrecip.UseVisualStyleBackColor = true;
      // 
      // txtRutaTblReclassPrecipitacion
      // 
      this.txtRutaTblReclassPrecipitacion.Location = new System.Drawing.Point(12, 290);
      this.txtRutaTblReclassPrecipitacion.Name = "txtRutaTblReclassPrecipitacion";
      this.txtRutaTblReclassPrecipitacion.Size = new System.Drawing.Size(361, 20);
      this.txtRutaTblReclassPrecipitacion.TabIndex = 37;
      // 
      // label21
      // 
      this.label21.AutoSize = true;
      this.label21.Location = new System.Drawing.Point(9, 274);
      this.label21.Name = "label21";
      this.label21.Size = new System.Drawing.Size(176, 13);
      this.label21.TabIndex = 36;
      this.label21.Text = "Tabla Reclasificacion Precipitacion:";
      // 
      // btnAbrirTablaReclassTemp
      // 
      this.btnAbrirTablaReclassTemp.Location = new System.Drawing.Point(376, 248);
      this.btnAbrirTablaReclassTemp.Name = "btnAbrirTablaReclassTemp";
      this.btnAbrirTablaReclassTemp.Size = new System.Drawing.Size(22, 23);
      this.btnAbrirTablaReclassTemp.TabIndex = 35;
      this.btnAbrirTablaReclassTemp.UseVisualStyleBackColor = true;
      // 
      // txtRutaTblReclassTemperatura
      // 
      this.txtRutaTblReclassTemperatura.Location = new System.Drawing.Point(9, 251);
      this.txtRutaTblReclassTemperatura.Name = "txtRutaTblReclassTemperatura";
      this.txtRutaTblReclassTemperatura.Size = new System.Drawing.Size(361, 20);
      this.txtRutaTblReclassTemperatura.TabIndex = 34;
      // 
      // label20
      // 
      this.label20.AutoSize = true;
      this.label20.Location = new System.Drawing.Point(6, 235);
      this.label20.Name = "label20";
      this.label20.Size = new System.Drawing.Size(175, 13);
      this.label20.TabIndex = 33;
      this.label20.Text = "Tabla Reclasificacion Temperatura:";
      // 
      // txtPesoAmenazasParciales
      // 
      this.txtPesoAmenazasParciales.Location = new System.Drawing.Point(144, 203);
      this.txtPesoAmenazasParciales.Name = "txtPesoAmenazasParciales";
      this.txtPesoAmenazasParciales.Size = new System.Drawing.Size(100, 20);
      this.txtPesoAmenazasParciales.TabIndex = 32;
      // 
      // label15
      // 
      this.label15.AutoSize = true;
      this.label15.Location = new System.Drawing.Point(6, 206);
      this.label15.Name = "label15";
      this.label15.Size = new System.Drawing.Size(132, 13);
      this.label15.TabIndex = 31;
      this.label15.Text = "Peso &Amenazas Parciales:";
      // 
      // txtPesoTemperatura
      // 
      this.txtPesoTemperatura.Location = new System.Drawing.Point(144, 177);
      this.txtPesoTemperatura.Name = "txtPesoTemperatura";
      this.txtPesoTemperatura.Size = new System.Drawing.Size(100, 20);
      this.txtPesoTemperatura.TabIndex = 30;
      // 
      // label14
      // 
      this.label14.AutoSize = true;
      this.label14.Location = new System.Drawing.Point(6, 180);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(97, 13);
      this.label14.TabIndex = 29;
      this.label14.Text = "Peso &Temperatura:";
      // 
      // txtPesoPrecipitacion
      // 
      this.txtPesoPrecipitacion.Location = new System.Drawing.Point(144, 149);
      this.txtPesoPrecipitacion.Name = "txtPesoPrecipitacion";
      this.txtPesoPrecipitacion.Size = new System.Drawing.Size(100, 20);
      this.txtPesoPrecipitacion.TabIndex = 28;
      // 
      // label13
      // 
      this.label13.AutoSize = true;
      this.label13.Location = new System.Drawing.Point(6, 152);
      this.label13.Name = "label13";
      this.label13.Size = new System.Drawing.Size(98, 13);
      this.label13.TabIndex = 27;
      this.label13.Text = "Peso &Precipitacion:";
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Location = new System.Drawing.Point(5, 52);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(39, 13);
      this.label12.TabIndex = 26;
      this.label12.Text = "Pesos:";
      // 
      // dgvPesos
      // 
      this.dgvPesos.AllowUserToAddRows = false;
      this.dgvPesos.AllowUserToDeleteRows = false;
      this.dgvPesos.AllowUserToResizeColumns = false;
      this.dgvPesos.AllowUserToResizeRows = false;
      this.dgvPesos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvPesos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDia0,
            this.colDia1,
            this.colDia2,
            this.colDia3,
            this.colDia4,
            this.colDia5,
            this.colDia6,
            this.colDia7,
            this.colDia8,
            this.colDia9});
      this.dgvPesos.Location = new System.Drawing.Point(8, 69);
      this.dgvPesos.Name = "dgvPesos";
      this.dgvPesos.RowHeadersWidth = 5;
      this.dgvPesos.Size = new System.Drawing.Size(395, 74);
      this.dgvPesos.TabIndex = 25;
      // 
      // colDia0
      // 
      this.colDia0.HeaderText = "Dia 0";
      this.colDia0.Name = "colDia0";
      this.colDia0.Width = 55;
      // 
      // colDia1
      // 
      this.colDia1.HeaderText = "Dia 0-1";
      this.colDia1.Name = "colDia1";
      this.colDia1.Width = 65;
      // 
      // colDia2
      // 
      this.colDia2.HeaderText = "Dia 0-2";
      this.colDia2.Name = "colDia2";
      this.colDia2.Width = 65;
      // 
      // colDia3
      // 
      this.colDia3.HeaderText = "Dia 0-3";
      this.colDia3.Name = "colDia3";
      this.colDia3.Width = 65;
      // 
      // colDia4
      // 
      this.colDia4.HeaderText = "Dia 0-4";
      this.colDia4.Name = "colDia4";
      this.colDia4.Width = 65;
      // 
      // colDia5
      // 
      this.colDia5.HeaderText = "Dia 0-5";
      this.colDia5.Name = "colDia5";
      this.colDia5.Width = 65;
      // 
      // colDia6
      // 
      this.colDia6.HeaderText = "Dia 0-6";
      this.colDia6.Name = "colDia6";
      this.colDia6.Width = 65;
      // 
      // colDia7
      // 
      this.colDia7.HeaderText = "Dia 0-7";
      this.colDia7.Name = "colDia7";
      this.colDia7.Width = 65;
      // 
      // colDia8
      // 
      this.colDia8.HeaderText = "Dia 0-8";
      this.colDia8.Name = "colDia8";
      this.colDia8.Width = 65;
      // 
      // colDia9
      // 
      this.colDia9.HeaderText = "Dia 0-9";
      this.colDia9.Name = "colDia9";
      this.colDia9.Width = 65;
      // 
      // btnAceptar
      // 
      this.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnAceptar.Location = new System.Drawing.Point(120, 437);
      this.btnAceptar.Name = "btnAceptar";
      this.btnAceptar.Size = new System.Drawing.Size(75, 23);
      this.btnAceptar.TabIndex = 45;
      this.btnAceptar.Text = "&Aceptar";
      this.btnAceptar.UseVisualStyleBackColor = true;
      // 
      // btnCancelar
      // 
      this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancelar.Location = new System.Drawing.Point(219, 437);
      this.btnCancelar.Name = "btnCancelar";
      this.btnCancelar.Size = new System.Drawing.Size(75, 23);
      this.btnCancelar.TabIndex = 46;
      this.btnCancelar.Text = "&Cancelar";
      this.btnCancelar.UseVisualStyleBackColor = true;
      // 
      // openDlg
      // 
      this.openDlg.FileName = "openFileDialog1";
      // 
      // FrmPropiedadesSIGPI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(430, 469);
      this.Controls.Add(this.btnAceptar);
      this.Controls.Add(this.btnCancelar);
      this.Controls.Add(this.tabControl1);
      this.Name = "FrmPropiedadesSIGPI";
      this.Text = "FrmPropiedadesSIGPI";
      this.tabControl1.ResumeLayout(false);
      this.tabParametrosIniciales.ResumeLayout(false);
      this.tabParametrosIniciales.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.tabParamNuevoAlgoritmo.ResumeLayout(false);
      this.tabParamNuevoAlgoritmo.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvPesos)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabParametrosIniciales;
    private System.Windows.Forms.TextBox txtPorcentajeLecturas;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.TextBox txtDirCapasTMMM;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.TextBox txtDirCapasRaster;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox txtDirLecturas;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox txtGDBClave;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox txtGDBUsuario;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtGDB;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtGDBInstancia;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtGDBServidor;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.TextBox txtBDLocal;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button btnDirBase;
    private System.Windows.Forms.TextBox txtDirBase;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnFolderOpen;
    private System.Windows.Forms.TextBox txtDirectorioResultados;
    private System.Windows.Forms.Label lblDirectorio;
    private System.Windows.Forms.ComboBox cboCapaSusceptibilidad;
    private System.Windows.Forms.Label lblCapaSusceptibilidad;
    private System.Windows.Forms.TextBox txtTamanoCelda;
    private System.Windows.Forms.Label lblCellSize;
    private System.Windows.Forms.TabPage tabParamNuevoAlgoritmo;
    private System.Windows.Forms.Button btnAbriGeoDB;
    private System.Windows.Forms.TextBox txtRutaGDB;
    private System.Windows.Forms.Label label24;
    private System.Windows.Forms.ComboBox cboTipoEstadistico;
    private System.Windows.Forms.Label label23;
    private System.Windows.Forms.Button btnAbrirTablaReclassIncendios;
    private System.Windows.Forms.TextBox txtRutaTableReclassIncendios;
    private System.Windows.Forms.Label label22;
    private System.Windows.Forms.Button btnAbrirTablaReclassPrecip;
    private System.Windows.Forms.TextBox txtRutaTblReclassPrecipitacion;
    private System.Windows.Forms.Label label21;
    private System.Windows.Forms.Button btnAbrirTablaReclassTemp;
    private System.Windows.Forms.TextBox txtRutaTblReclassTemperatura;
    private System.Windows.Forms.Label label20;
    private System.Windows.Forms.TextBox txtPesoAmenazasParciales;
    private System.Windows.Forms.Label label15;
    private System.Windows.Forms.TextBox txtPesoTemperatura;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.TextBox txtPesoPrecipitacion;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.DataGridView dgvPesos;
    private System.Windows.Forms.DataGridViewTextBoxColumn colDia0;
    private System.Windows.Forms.DataGridViewTextBoxColumn colDia1;
    private System.Windows.Forms.DataGridViewTextBoxColumn colDia2;
    private System.Windows.Forms.DataGridViewTextBoxColumn colDia3;
    private System.Windows.Forms.DataGridViewTextBoxColumn colDia4;
    private System.Windows.Forms.DataGridViewTextBoxColumn colDia5;
    private System.Windows.Forms.DataGridViewTextBoxColumn colDia6;
    private System.Windows.Forms.DataGridViewTextBoxColumn colDia7;
    private System.Windows.Forms.DataGridViewTextBoxColumn colDia8;
    private System.Windows.Forms.DataGridViewTextBoxColumn colDia9;
    private System.Windows.Forms.Button btnAceptar;
    private System.Windows.Forms.Button btnCancelar;
    private System.Windows.Forms.FolderBrowserDialog folderDlg;
    private System.Windows.Forms.OpenFileDialog openDlg;
  }
}