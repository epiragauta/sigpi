using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.DataSourcesFile;
using Microsoft.Office.Interop.Excel;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.AnalysisTools;
using ESRI.ArcGIS.ConversionTools;
using ESRI.ArcGIS.Geoprocessor;

namespace SIGPI_10
{
  /// <summary>
  /// Clase exportar resultados
  /// </summary>
  public partial class FrmExportarResultados2 : Form
  {
    private SIGPICls _sigpi;
    private IApplication m_pApp;

    /// <summary>
    /// Constructor de la clase
    /// </summary>
    /// <param name="sigpi"></param>
    /// <param name="pApp"></param>
    public FrmExportarResultados2(SIGPICls sigpi, IApplication pApp)
    {
      InitializeComponent();
      _sigpi = sigpi;
      m_pApp = pApp;
      monthCalendar1.SelectionStart = _sigpi.FechaProcesamiento;
      monthCalendar1.SelectionEnd = _sigpi.FechaProcesamiento;
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

    private void btnExportarAExcel_Click_1(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;
      DateTime fechaExportacion = monthCalendar1.SelectionStart;
      string sMonth = fechaExportacion.ToString("MM");

      int iYear = fechaExportacion.Year;
      string sNombreFileGDB = iYear.ToString() + "-" + sMonth + "-Modelos.gdb";

      string sNombreCapa = "Amenaza_" + fechaExportacion.ToString("yyyy_MM_dd");

      Geoprocessor gp = new Geoprocessor();

      string sOutFC = sNombreCapa + ".shp";
      int i = 1;
      while (System.IO.File.Exists(System.IO.Path.GetTempPath() + sOutFC))
      {
        sOutFC = sNombreCapa + "_" + i.ToString() + ".shp";
        i++;
      }

      String sOutFC2;
      try
      {
        sOutFC2 = RasterAPuntos(Sigpi.Parametros.RutaSIGPI + "\\" + Sigpi.Parametros.Resultado + "\\" + sNombreFileGDB + "\\" + sNombreCapa,
                      System.IO.Path.GetTempPath() + sOutFC, gp);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "SIGPI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        this.Cursor = Cursors.Default;
        return;
      }
      

      IWorkspaceFactory pWF = new ShapefileWorkspaceFactoryClass();
      IFeatureWorkspace pFWorkspace = (IFeatureWorkspace)pWF.OpenFromFile(System.IO.Path.GetTempPath(), 0);
      string sFC = sOutFC2.Substring(sOutFC2.LastIndexOf("\\"),sOutFC2.Length-sOutFC2.LastIndexOf("\\")); // sOutFC2.Replace(System.IO.Path.GetTempPath(), "");
      IFeatureClass pFC = pFWorkspace.OpenFeatureClass(sFC);

      string sFecha = fechaExportacion.ToString("dd DE MMMM DE yyyy").ToUpper();

      SIGPIProcesamiento procesamiento = new SIGPIProcesamiento(_sigpi.Parametros);
      Microsoft.Office.Interop.Excel.Application _excelApp = new Microsoft.Office.Interop.Excel.Application();

      string sRuta = _sigpi.Parametros.RutaSIGPI + "\\" + "plantillas" + "\\" + "plantilla presentacion datos probabilidad.xls";
      string sTitulo = "PROBABILIDAD PARA EL " + sFecha;
      try
      {
        Workbook workBook = _excelApp.Workbooks.Open(sRuta, Type.Missing, Type.Missing, Type.Missing,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

        Worksheet sheet = (Worksheet)workBook.Sheets["DATOS_PROBABILIDAD"];
        object objVal;
        Microsoft.Office.Interop.Excel.Range range;
        object[,] objValues = new object[1, 6];
        int k = 2;
        int lFieldCelda = pFC.FindField("GRID_CODE");
        int lFieldMpio = pFC.FindField("MUNICIPIO");
        int lFieldCabecera = pFC.FindField("CABECERA");
        int lFieldDepto = pFC.FindField("DEPARTAMEN");
        int lFieldCobertura = pFC.FindField("COBERTURA");
        int lFieldParque = pFC.FindField("NOMBRE_PAR");

        IFeatureCursor pFCursor1;
        IFeature pFeature1 = null;


        pFCursor1 = pFC.Search(null, false);
        pFeature1 = pFCursor1.NextFeature();
        progressBar1.Minimum = 1;
        progressBar1.Maximum = 65537;
        progressBar1.Step = 100;
        progressBar1.Visible = true;
        System.Windows.Forms.Application.DoEvents();
        String currentValue = "";
        while (pFeature1 != null)
        {
          range = sheet.get_Range("A" + k.ToString(), "E" + k.ToString());
          objValues[0, 0] = pFeature1.get_Value(lFieldCelda);
          objValues[0, 1] = pFeature1.get_Value(lFieldMpio);
          objValues[0, 2] = pFeature1.get_Value(lFieldCabecera);
          objValues[0, 3] = pFeature1.get_Value(lFieldDepto);
          objValues[0, 4] = pFeature1.get_Value(lFieldCobertura);
          objValues[0, 5] = pFeature1.get_Value(lFieldParque);
          if (!currentValue.Equals(objValues[0, 0] + "-" + objValues[0, 1] + "-" + objValues[0, 2] + "-" + objValues[0, 3] + "-" + objValues[0, 4] + "-" + objValues[0, 5]))
          {
            range.set_Value(Type.Missing, objValues);
            k++;
          }
          currentValue = objValues[0, 0] + "-" + objValues[0, 1] + "-" + objValues[0, 2] + "-" + objValues[0, 3] + "-" + objValues[0, 4] + "-" + objValues[0, 5];
          
          
          if (k == 65537)
            break;
          if (k % 100 == 0)
          {
            progressBar1.PerformStep();
          }

          //Console.WriteLine(k.ToString());
          pFeature1 = pFCursor1.NextFeature();
        }
        progressBar1.Visible = false;
        System.Windows.Forms.Application.DoEvents();
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

      ////this.Cursor = Cursors.WaitCursor;
      ////DateTime fechaExportacion = monthCalendar1.SelectionStart;
      ////int iMonth = fechaExportacion.Month;
      ////string sMonth = (iMonth.ToString().Length == 1 ? "0" + iMonth.ToString() : iMonth.ToString() );

      ////int iYear = fechaExportacion.Year;
      ////string sNombreFileGDB = iYear.ToString() + "-" + sMonth + "-Modelos.gdb";
      ////IWorkspaceFactory pWF = new FileGDBWorkspaceFactoryClass();
      ////string sRutaFileGDB = Sigpi.Parametros.RutaSIGPI + "\\" + Sigpi.Parametros.Resultado + "\\" + sNombreFileGDB;
      ////IRasterWorkspaceEx pRWorkspace = (IRasterWorkspaceEx)pWF.OpenFromFile(sRutaFileGDB, 0);
      ////string sNombreCapa = "Amenaza_" + fechaExportacion.ToString("yyyy_MM_dd");

      ////IRasterDataset pRDataset = pRWorkspace.OpenRasterDataset(sNombreCapa);
      ////string sFecha = fechaExportacion.ToString("dd DE MMMM DE yyyy").ToUpper();

      ////SIGPIProcesamiento procesamiento = new SIGPIProcesamiento(_sigpi.Parametros);
      ////Microsoft.Office.Interop.Excel.Application _excelApp = new Microsoft.Office.Interop.Excel.Application();


      ////string sTitulo = "PROBABILIDAD PARA EL " + sFecha;
      ////try
      ////{
      ////  Worksheet sheet = procesamiento.ExportarAExcel2(pRDataset.CreateDefaultRaster(), _sigpi, _excelApp);
      ////  string sNombreArchivo = _sigpi.Parametros.RutaSIGPI + "Tablas\\" + sTitulo + ".xls";
      ////  MessageBox.Show("Archivo Generado: " + sNombreArchivo);
      ////  sheet.SaveAs(sNombreArchivo, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
      ////                  Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
      ////}
      ////catch (Exception ex)
      ////{
      ////  _excelApp.Quit();
      ////  MessageBox.Show(ex.Message);
      ////  this.Cursor = Cursors.Default;
      ////}

      ////this.Cursor = Cursors.Default;
      ////_excelApp.Visible = true;
      //////_excelApp.Quit();

      //////MessageBox.Show("Archivo Exportado");
    }



    private void btnExp2Map_Click_1(object sender, EventArgs e)
    {
      if (m_pApp != null)
      {
        IRasterLayer pRLayer = new RasterLayerClass();
        try
        {
          this.Cursor = Cursors.WaitCursor;
          DateTime fechaExportacion = monthCalendar1.SelectionStart;
          int iMonth = fechaExportacion.Month;
          string sMonth = (iMonth.ToString().Length == 1 ? "0" + iMonth.ToString() : iMonth.ToString());

          int iYear = fechaExportacion.Year;
          string sNombreFileGDB = iYear.ToString() + "-" + sMonth + "-Modelos.gdb";
          IWorkspaceFactory pWF = new FileGDBWorkspaceFactoryClass();
          string sRutaFileGDB = Sigpi.Parametros.RutaSIGPI + "\\" + Sigpi.Parametros.Resultado + "\\" + sNombreFileGDB;
          IRasterWorkspaceEx pRWorkspace = (IRasterWorkspaceEx)pWF.OpenFromFile(sRutaFileGDB, 0);
          string sNombreCapa = "Amenaza_" + fechaExportacion.ToString("yyyy_MM_dd");

          IRasterDataset pRDataset = pRWorkspace.OpenRasterDataset(sNombreCapa);
          pRLayer.CreateFromDataset(pRDataset);
          pRLayer.Name = sNombreCapa;
          IMxDocument pMxDoc = m_pApp.Document as IMxDocument;
          IMap pMap = pMxDoc.FocusMap;

          AsignarSimbologiaProbabilidad(pRLayer);
          pMap.AddLayer(pRLayer);
          if (pMap.LayerCount > 0)
          {
            pMap.MoveLayer(pRLayer, pMap.LayerCount - 1);
          }

          if (!(pMxDoc.ActiveView is IPageLayout))
          {
            pMxDoc.ActiveView = (IActiveView)pMxDoc.PageLayout;
          }

          IPageLayout pageLayout = pMxDoc.PageLayout;
          IGraphicsContainer pGraphicsContainer = (IGraphicsContainer)pageLayout;
          pGraphicsContainer.Reset();
          IElement pElement;
          pElement = pGraphicsContainer.Next();
          while (pElement != null)
          {
            if (pElement is ITextElement)
            {
              ITextElement pTxtElement = (ITextElement)pElement;
              IElementProperties3 pElemProperties = (IElementProperties3)pTxtElement;
              if (pElemProperties.Name.Equals("lblProbabilidad"))
              {
                pTxtElement.Text = "PROBABILIDAD PARA EL " + fechaExportacion.ToLongDateString();
              }
              if (pElemProperties.Name.Equals("lblFechaGeneracion"))
              {
                pTxtElement.Text = "Fecha Generación: " + DateTime.Now.ToLongTimeString();
              }
            }
            pElement = pGraphicsContainer.Next();
          }

          pMxDoc.UpdateContents();
          pMxDoc.ActiveView.Refresh();
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message, "Error cargando Capa!!!");
        }
      }
      this.Cursor = Cursors.Default;

      

      //string sTipo = "";
      //string sRuta = "";
      //if (radioBtnProbabilidad.Checked)
      //{
      //  sTipo = "Probabilidad";
      //  sRuta = _sigpi.Parametros.RutaSIGPI + _sigpi.Parametros.Resultado;
      //}
      //else if (radioBtnModelo.Checked)
      //{
      //  sTipo = "Modelo";
      //  sRuta = _sigpi.Parametros.RutaSIGPI + _sigpi.Parametros.Raster;
      //}

      //if (lvwModelos.SelectedIndices.Count > 0)
      //{
      //  string sModelo = lvwModelos.SelectedItems[0].Text;
      //  IMxDocument pMxDoc = m_pApp.Document as IMxDocument;
      //  IMap pMap = pMxDoc.FocusMap;
      //  IRasterLayer pRLayer = new RasterLayerClass();
      //  pRLayer.CreateFromFilePath(sRuta + "\\" + sModelo);
      //  AsignarSimbologiaProbabilidad(pRLayer);


      //  pMap.AddLayer(pRLayer);

      //  if (pMap.LayerCount > 0)
      //  {
      //    pMap.MoveLayer(pRLayer, pMap.LayerCount - 1);
      //  }

      //  pMxDoc.UpdateContents();
      //  pMxDoc.ActiveView.Refresh();
      //}

      //FrmExportMap frmExportMap = new FrmExportMap(_sigpi, lvwModelos.SelectedItems[0].Text, sTipo);
      //frmExportMap.ShowDialog();
    }

    private void AsignarSimbologiaProbabilidad(IRasterLayer pRLayer)
    {

      IRasterUniqueValueRenderer pRUVRenderer = new RasterUniqueValueRendererClass();
      IRasterRenderer pRRenderer = pRUVRenderer as IRasterRenderer;
      string sLayerName = pRLayer.Name;
      int iNumDias = 0;
      if (!sLayerName.ToUpper().Contains("AMENAZA"))
      {
        try
        {
          iNumDias = System.Convert.ToInt32(sLayerName.Substring(sLayerName.Length - 1, 1));
        }
        catch (Exception ex)
        {
          //MessageBox.Show(ex.Message);
          iNumDias = 5;
        }
      }
      else
      {
        iNumDias = 5;
      }
      IColor pColor = new RgbColorClass();

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

    private void btnCerrar_Click_1(object sender, EventArgs e)
    {
      this.Close();
    }

    private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
    {
      int iMonth = e.Start.Month;
      string sMonth = (iMonth.ToString().Length == 1 ? "0" + iMonth.ToString() : iMonth.ToString());

      int iYear = e.Start.Year;
      string sNombreFileGDB = iYear.ToString() + "-" + sMonth + "-Modelos.gdb";
      string sRutaFileGDB = Sigpi.Parametros.RutaSIGPI + "\\" + Sigpi.Parametros.Resultado + "\\" + sNombreFileGDB;
      if (!System.IO.Directory.Exists(sRutaFileGDB))
      {
        lblCapaAsociada.Text = "No existe Geodatabase de resultados: \n" + sRutaFileGDB;
        lblCapaAsociada.ForeColor = Color.Red;
        btnExportarAExcel.Enabled = false;
        btnExp2Map.Enabled = false;
        return;
      }
      try
      {
        IWorkspaceFactory pWF = new FileGDBWorkspaceFactoryClass();
        IWorkspace2 pWS = (IWorkspace2)pWF.OpenFromFile(sRutaFileGDB, 0);
        string sCapaResultado = "Amenaza_" + e.Start.ToString("yyyy_MM_dd");
        if (!pWS.get_NameExists(esriDatasetType.esriDTRasterDataset, sCapaResultado))
        {
          lblCapaAsociada.Text = "No existe Modelo: " + sCapaResultado;
          lblCapaAsociada.ForeColor = Color.Red;
          btnExportarAExcel.Enabled = false;
          btnExp2Map.Enabled = false;
        }
        else
        {
          lblCapaAsociada.Text = sCapaResultado;
          lblCapaAsociada.ForeColor = Color.Green;
          btnExportarAExcel.Enabled = true;
          btnExp2Map.Enabled = true;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }

    }

    private String RasterAPuntos(string sRasterLayer, string sOutputFC, Geoprocessor gp)
    {
      string sCapaMpios_parques_cobertura = "mpios_parques_cobertura";
      string sFCCruce = _sigpi.Parametros.RutaGBD + "\\" + sCapaMpios_parques_cobertura;
      try
      {
        RasterToPoint raster2Pnt = new RasterToPoint(sRasterLayer, sOutputFC);
        raster2Pnt.raster_field = "VALUE";

        gp.Execute(raster2Pnt, null);

        Intersect intersect = new Intersect();
        intersect.in_features = sOutputFC + " ; " + sFCCruce;
        intersect.out_feature_class = sOutputFC.Replace(".shp", "_intersect.shp");
        intersect.join_attributes = "NO_FID";
        intersect.output_type = "POINT";

        gp.Execute(intersect, null);
        return intersect.out_feature_class.ToString();
      }
      catch (Exception ex)
      {
        throw new Exception("Verifique que exista información para el día seleccionado");
      }
            
    }

    

  }
}
