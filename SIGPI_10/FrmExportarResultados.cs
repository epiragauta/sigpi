using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.Office.Interop.Excel;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.GeoDatabaseUI;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Display;

namespace SIGPI_10
{
  public partial class FrmExportarResultados : Form
  {
    public FrmExportarResultados()
    {
      InitializeComponent();
    }
    
    private SIGPICls _sigpi;
    private IApplication m_pApp;

    /// <summary>
    /// Constructor de la clase
    /// </summary>
    /// <param name="sigpi"></param>
    /// <param name="pApp"></param>
    public FrmExportarResultados(SIGPICls sigpi, IApplication pApp)
    {
      InitializeComponent();
      _sigpi = sigpi;
      m_pApp = pApp;
    }

    /// <summary>
    /// Propiedad SIGPI
    /// </summary>
    public SIGPICls Sigpi
    {
      get
      {
        return _sigpi;
      }
      set
      {
        _sigpi = value;
      }
    }

    private void btnExportarAExcel_Click(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;

      string sNombreCapa = lvwModelos.SelectedItems[0].Text;
      DateTime fechaExportacion = new DateTime(Convert.ToInt16("20" + sNombreCapa.Substring(9, 2)),
                                               Convert.ToInt16(sNombreCapa.Substring(1, 2)),
                                               Convert.ToInt16(sNombreCapa.Substring(4, 2)));
      string sFecha = fechaExportacion.ToString("dd DE MMMM DE yyyy").ToUpper();
      SIGPIProcesamiento procesamiento = new SIGPIProcesamiento(_sigpi.Parametros);
      Microsoft.Office.Interop.Excel.Application _excelApp = new Microsoft.Office.Interop.Excel.Application();
      string sRuta = "";
      if (radioBtnProbabilidad.Checked)
        sRuta = _sigpi.Parametros.RutaSIGPI + "\\" + _sigpi.Parametros.Resultado;
      else if (radioBtnModelo.Checked)
        sRuta = _sigpi.Parametros.RutaSIGPI + "\\" + _sigpi.Parametros.Raster;

      string sTitulo = "PROBABILIDAD PARA EL " + sFecha;
      try
      {
        Worksheet sheet = procesamiento.ExportarAExcel(sNombreCapa, sRuta, _sigpi, _excelApp);
        sheet.SaveAs(_sigpi.Parametros.RutaSIGPI + "Tablas\\" + sTitulo + ".xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
      }
      catch (Exception ex)
      {
        _excelApp.Quit();
        MessageBox.Show(ex.Message);
        this.Cursor = Cursors.Default;
      }

      this.Cursor = Cursors.Default;
      _excelApp.Visible = true;
      //_excelApp.Quit();

      //MessageBox.Show("Archivo Exportado");
    }

    private void radioBtnProbabilidad_CheckedChanged(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;
      CargarModelos(_sigpi.Parametros.RutaSIGPI + "\\" + _sigpi.Parametros.Resultado);
      btnExportarAExcel.Enabled = false;
      btnExp2Map.Enabled = false;
      this.Cursor = Cursors.Default;
    }

    private void radioBtnModelo_CheckedChanged(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;
      CargarModelos(_sigpi.Parametros.RutaSIGPI + "\\" + _sigpi.Parametros.Raster);
      btnExportarAExcel.Enabled = false;
      btnExp2Map.Enabled = false;
      this.Cursor = Cursors.Default;
    }

    private void CargarModelos(string sRuta)
    {
      IWorkspaceFactory pWF = new RasterWorkspaceFactoryClass();
      IWorkspace pWorkspace = pWF.OpenFromFile(sRuta, 0);

      IEnumDatasetName pEnumDS = pWorkspace.get_DatasetNames(esriDatasetType.esriDTRasterDataset);
      IDatasetName pDS = pEnumDS.Next();
      lvwModelos.Items.Clear();
      while (pDS != null)
      {
        lvwModelos.Items.Add(pDS.Name.ToUpper());
        pDS = pEnumDS.Next();
      }
    }

    private void lvwModelos_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (lvwModelos.SelectedItems.Count > 0)
      {
        btnExportarAExcel.Enabled = true;
        btnExp2Map.Enabled = true;
      }
    }

    private void btnExp2Map_Click(object sender, EventArgs e)
    {
      string sTipo = "";
      string sRuta = "";
      if (radioBtnProbabilidad.Checked)
      {
        sTipo = "Probabilidad";
        sRuta = _sigpi.Parametros.RutaSIGPI + _sigpi.Parametros.Resultado;
      }
      else if (radioBtnModelo.Checked)
      {
        sTipo = "Modelo";
        sRuta = _sigpi.Parametros.RutaSIGPI + _sigpi.Parametros.Raster;
      }

      if (lvwModelos.SelectedIndices.Count > 0)
      {
        string sModelo = lvwModelos.SelectedItems[0].Text;
        IMxDocument pMxDoc = m_pApp.Document as IMxDocument;
        IMap pMap = pMxDoc.FocusMap;
        IRasterLayer pRLayer = new RasterLayerClass();
        pRLayer.CreateFromFilePath(sRuta + "\\" + sModelo);
        AsignarSimbologiaProbabilidad(pRLayer);


        pMap.AddLayer(pRLayer);

        if (pMap.LayerCount > 0)
        {
          pMap.MoveLayer(pRLayer, pMap.LayerCount - 1);
        }

        pMxDoc.UpdateContents();
        pMxDoc.ActiveView.Refresh();
      }

      //FrmExportMap frmExportMap = new FrmExportMap(_sigpi, lvwModelos.SelectedItems[0].Text, sTipo);
      //frmExportMap.ShowDialog();
    }

    private void AsignarSimbologiaProbabilidad(IRasterLayer pRLayer)
    {
      IRasterUniqueValueRenderer pRUVRenderer = new RasterUniqueValueRendererClass();
      IRasterRenderer pRRenderer = pRUVRenderer as IRasterRenderer;
      string sLayerName = pRLayer.Name;
      int iNumDias = 0;
      try
      {
        iNumDias = System.Convert.ToInt32(sLayerName.Substring(sLayerName.Length - 1, 1));
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
      IRgbColor pColor = new RgbColorClass();

      pRRenderer.Raster = pRLayer.Raster;
      pRRenderer.ResamplingType = rstResamplingTypes.RSP_BilinearInterpolation;
      pRUVRenderer.HeadingCount = 1;
      pRUVRenderer.set_Heading(0, "Probabilidad para " + iNumDias.ToString() + " días");

      switch (iNumDias)
      {
        case 5:
          pRUVRenderer.set_ClassCount(0, 6);
          AsignarColorAClase(0, "", 255, 255, 255, pRUVRenderer);
          AsignarColorAClase(1, "Muy Baja", 0, 255, 0, pRUVRenderer);
          AsignarColorAClase(2, "Baja", 255, 240, 70, pRUVRenderer);
          AsignarColorAClase(3, "Moderada", 255, 220, 100, pRUVRenderer);
          AsignarColorAClase(4, "Alta", 255, 100, 20, pRUVRenderer);
          AsignarColorAClase(5, "Muy Alta", 255, 0, 0, pRUVRenderer);
          break;
        case 4:
          pRUVRenderer.set_ClassCount(0, 5);
          AsignarColorAClase(0, "", 255, 255, 255, pRUVRenderer);
          AsignarColorAClase(1, "Baja", 0, 255, 0, pRUVRenderer);
          AsignarColorAClase(2, "Moderada", 255, 240, 70, pRUVRenderer);
          AsignarColorAClase(3, "Alta", 255, 190, 0, pRUVRenderer);
          AsignarColorAClase(4, "Muy Alta", 255, 0, 0, pRUVRenderer);
          break;
        case 3:
          pRUVRenderer.set_ClassCount(0, 3);
          AsignarColorAClase(0, "", 255, 255, 255, pRUVRenderer);
          AsignarColorAClase(1, "Baja", 0, 255, 0, pRUVRenderer);
          AsignarColorAClase(2, "Moderada", 255, 170, 0, pRUVRenderer);
          AsignarColorAClase(3, "Alta", 255, 0, 0, pRUVRenderer);
          break;
      }

      pRUVRenderer.UseDefaultSymbol = false;
      pRRenderer.Update();
      pRLayer.Renderer = pRRenderer;

    }

    private void AsignarColorAClase(int iClase, string sEtiqueta, int red, int green, int blue, IRasterUniqueValueRenderer pRUVRenderer)
    {
      IColorSymbol pColorSymbol = new ColorSymbolClass();
      pRUVRenderer.AddValue(0, iClase, iClase);
      pRUVRenderer.set_Label(0, iClase, sEtiqueta);
      IRgbColor pColor = new RgbColorClass();
      pColor.Red = red;
      pColor.Green = green;
      pColor.Blue = blue;
      pColorSymbol.Color = pColor as IColor;
      pRUVRenderer.set_Symbol(0, iClase, pColorSymbol as ISymbol);
    }

    private void btnCerrar_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}
