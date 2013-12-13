using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.Catalog;

namespace SIGPI_10
{
  public partial class FrmPropiedadesSIGPI : Form
  {
    private SIGPIParametros _parametros;

    public FrmPropiedadesSIGPI(SIGPIParametros parametros)
    {
      _parametros = parametros;
      InitializeComponent();
    }

    public FrmPropiedadesSIGPI()
    {
      InitializeComponent();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Hide();
    }

      
    private bool IsNumeric(object Expression)
    {
      bool isNum;
      double retNum;

      isNum = Double.TryParse(Convert.ToString(Expression),
        System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

      return isNum;
    }
    void dgvPesos_CellValueChanged(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
    {
      if (e.RowIndex == -1) return;
      DataGridViewCell cell = dgvPesos.Rows[e.RowIndex].Cells[e.ColumnIndex];

      if (!IsNumeric(cell.Value))
      {
        MessageBox.Show("Valor no Valido!!");
        cell.Value = "0";
      }

      //this.Text = dataGridView
    }

    private void btnAbrirTablaReclassIncendios_Click(object sender, EventArgs e)
    {
      txtRutaTableReclassIncendios.Text = BuscarTablaReclasificacion("Reclasificacion Incendios");
    }

    private string BuscarTablaReclasificacion(string TipoTabla)
    {
      IGxDialog pGxDialog = new GxDialogClass();
      IGxObjectFilter filter = new GxFilterTablesClass();
      pGxDialog.Title = "Buscar Tabla " + TipoTabla;
      pGxDialog.ObjectFilter = filter;
      IEnumGxObject pEnumObj;
      if (pGxDialog.DoModalOpen(0, out pEnumObj))
      {
        return pEnumObj.Next().FullName;
      }
      else
      {
        return "";
      }
    }

    private void btnAbrirTablaReclassPrecip_Click(object sender, EventArgs e)
    {
      txtRutaTblReclassPrecipitacion.Text = BuscarTablaReclasificacion("Reclasificacion Precipitacion");
    }

    private void btnAbrirTablaReclassTemp_Click(object sender, EventArgs e)
    {
      txtRutaTblReclassTemperatura.Text = BuscarTablaReclasificacion("Reclasificacion Temperatura");
    }

    private void btnAbriGeoDB_Click(object sender, EventArgs e)
    {
      txtRutaGDB.Text = BuscarGeodatabase();
    }

    private string BuscarGeodatabase()
    {
      IGxDialog pGxDialog = new GxDialogClass();
      IGxObjectFilter filter = new GxFilterPersonalGeodatabasesClass();
      pGxDialog.Title = "Buscar Geodatabase ";
      pGxDialog.ObjectFilter = filter;
      IEnumGxObject pEnumObj;
      if (pGxDialog.DoModalOpen(0, out pEnumObj))
      {
        return pEnumObj.Next().FullName;
      }
      else
      {
        return "";
      }
    }

   

    private void FrmPropiedadesSIGPI_Load(object sender, EventArgs e)
    {
      if (_parametros != null)
      {
        SIGPIParametros parametros = _parametros;
        txtDirBase.Text = parametros.RutaSIGPI;
        txtBDLocal.Text = parametros.RutaBD;
        txtTamanoCelda.Text = parametros.TamanoCelda.ToString();
        txtDirectorioResultados.Text = parametros.Resultado;
        txtDirLecturas.Text = parametros.Lecturas;
        txtDirCapasRaster.Text = parametros.Raster;
        txtDirCapasTMMM.Text = parametros.TMMMM;
        txtPorcentajeLecturas.Text = parametros.PorcentajeEfectivoLecturas;
        txtGDBServidor.Text = parametros.GDBServidor;
        txtGDBInstancia.Text = parametros.GDBInstancia;
        txtGDB.Text = parametros.GDBDataBase;
        txtGDBClave.Text = parametros.GDBClave;
        txtGDBUsuario.Text = parametros.GDBUsuario;
        cboCapaSusceptibilidad.Items.Add(parametros.Susceptibilidad);
        if (parametros.SusceptibilidadActiva == parametros.Susceptibilidad)
          cboCapaSusceptibilidad.SelectedIndex = 0;
        cboCapaSusceptibilidad.Items.Add(parametros.SusceptibilidadConNino);
        if (parametros.SusceptibilidadActiva == parametros.SusceptibilidadConNino)
          cboCapaSusceptibilidad.SelectedIndex = 1;

        txtRutaTblReclassTemperatura.Text = parametros.TablaReclasificacionTemperatura;
        txtRutaTblReclassPrecipitacion.Text = parametros.TablaReclasificacionPrecipitacion;
        txtRutaTableReclassIncendios.Text = parametros.TablaReclasificacionIncendios;

        txtPesoPrecipitacion.Text = parametros.PesoPrecipitacion.ToString(); ;
        txtPesoTemperatura.Text = parametros.PesoTemperatura.ToString(); ;
        txtPesoAmenazasParciales.Text = parametros.PesoAmenazasParciales.ToString(); ;
        txtRutaGDB.Text = parametros.RutaGBD;
        txtDirTemp.Text = parametros.TempDir;

        DataGridViewRow row = new DataGridViewRow();
        DataGridViewCell cell;
        double[] pesos = parametros.Pesos;
        if (pesos == null)
        {
          pesos = new double[10];
          pesos[0] = 30;
          pesos[1] = 20;
          pesos[2] = 10;
          pesos[3] = 9;
          pesos[4] = 8;
          pesos[5] = 7;
          pesos[6] = 6;
          pesos[7] = 5;
          pesos[8] = 4;
          pesos[9] = 3;
        }
        for (int i = 0; i < 10; i++)
        {
          cell = new DataGridViewTextBoxCell();
          cell.Value = pesos[i];
          row.Cells.Add(cell);
        }
        dgvPesos.Rows.Add(row);


        Type tipoEstadistico = typeof(EnumTipoEstadistico);
        int j = 0;
        foreach (string s in Enum.GetNames(tipoEstadistico))
        {
          cboTipoEstadistico.Items.Add(s);
          if (s.Equals(parametros.TipoEstadistico))
            cboTipoEstadistico.SelectedIndex = j;
          j++;
        }

      }
    }

    private void btnDirBase_Click_1(object sender, EventArgs e)
    {
      if (folderDlg.ShowDialog() == DialogResult.OK)
      {
        txtDirBase.Text = folderDlg.SelectedPath;
      }
    }

    private void button1_Click_1(object sender, EventArgs e)
    {
      openDlg.Filter = "Base de Datos MsAccess *.mdb|*.mdb";
      if (openDlg.ShowDialog() == DialogResult.OK)
      {
        txtBDLocal.Text = openDlg.FileName;
      }
    }

    
    private void btnAceptar_Click_1(object sender, EventArgs e)
    {
      _parametros.RutaSIGPI = txtDirBase.Text;
      _parametros.RutaBD = txtBDLocal.Text;
      _parametros.TamanoCelda = System.Convert.ToInt32(txtTamanoCelda.Text);
      _parametros.Resultado = txtDirectorioResultados.Text;
      _parametros.Lecturas = txtDirLecturas.Text;
      _parametros.Raster = txtDirCapasRaster.Text;
      _parametros.TMMMM = txtDirCapasTMMM.Text;
      _parametros.PorcentajeEfectivoLecturas = txtPorcentajeLecturas.Text;
      _parametros.GDBServidor = txtGDBServidor.Text;
      _parametros.GDBInstancia = txtGDBInstancia.Text;
      _parametros.GDBDataBase = txtGDB.Text;
      _parametros.GDBUsuario = txtGDBUsuario.Text;
      _parametros.GDBClave = txtGDBClave.Text;
      _parametros.SusceptibilidadActiva = cboCapaSusceptibilidad.Text;

      _parametros.PesoPrecipitacion = System.Convert.ToDouble(txtPesoPrecipitacion.Text);
      _parametros.PesoTemperatura = System.Convert.ToDouble(txtPesoTemperatura.Text);
      _parametros.PesoAmenazasParciales = System.Convert.ToDouble(txtPesoAmenazasParciales.Text);
      _parametros.TablaReclasificacionTemperatura = txtRutaTblReclassTemperatura.Text;
      _parametros.TablaReclasificacionPrecipitacion = txtRutaTblReclassPrecipitacion.Text;
      _parametros.TablaReclasificacionIncendios = txtRutaTableReclassIncendios.Text;
      _parametros.TipoEstadistico = cboTipoEstadistico.Text;
      _parametros.RutaGBD = txtRutaGDB.Text;
      _parametros.TempDir = txtDirTemp.Text;

      double[] pesos = new double[10];
      for (int i = 0; i < 10; i++)
      {
        pesos[i] = Double.Parse(dgvPesos.Rows[0].Cells[i].Value.ToString());

      }
      _parametros.Pesos = pesos;
      string sPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
      sPath = sPath.Replace("file:\\", "");
      try
      {
        XmlSerializer serializer = new XmlSerializer(typeof(SIGPIParametros));
        System.IO.StreamWriter writer = new System.IO.StreamWriter(sPath + "\\parameters\\parametros.xml");
        serializer.Serialize(writer, _parametros);
        writer.Close();
        writer = null;
        serializer = null;
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }

      this.DialogResult = DialogResult.OK;
      this.Hide();
    }

    private void btnCancelar_Click_1(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnFolderOpen_Click(object sender, EventArgs e)
    {
      if (folderDlg.ShowDialog() == DialogResult.OK)
      {
        txtDirectorioResultados.Text = folderDlg.SelectedPath;
      }
    }

    

  }
}
