using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Framework;
using System.Xml.Serialization;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesRaster;
using NLog;

namespace SIGPI_10
{
  public partial class FrmSIGPIPrincipal : Form
  {
    private SIGPIParametros parametros;
    private SIGPIProcesamiento procesamiento;
    private SIGPICls sigpi;
    private SIGPIDao sigpiDao;

    private IApplication m_pApp;

    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pApp"></param>
    public FrmSIGPIPrincipal(IApplication pApp)
    {
      InitializeComponent();
      m_pApp = pApp;
    }

    private void FrmSIGPIPrincipal_Load(object sender, EventArgs e)
    {
      lblTitulo.Text = "SISTEMA DE INFORMACION GEOGRAFICA \nPARA LA PREVENCION DE INCENDIOS SIGPI";

      string sPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
      sPath = sPath + "\\" + "parameters";
      logger.Info("FrmSIGPIPrincipal  :: sPath :: {0}",sPath);
      //System.Console.WriteLine(sPath);
      //MessageBox.Show(sPath,"sPath");
      
      sPath = sPath.Replace("file:\\", "");
      //MessageBox.Show("parametros: " + sPath + "\\parametros.xml");
      try
      {
        XmlSerializer serializer = new XmlSerializer(typeof(SIGPIParametros));
        System.IO.StreamReader r = new System.IO.StreamReader(sPath + "\\parametros.xml");
        parametros = (SIGPIParametros)serializer.Deserialize(r);
        r.Close();
        serializer = null;
        r = null;
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "SIGPI");
        return;
      }

      //string sCurrentDir = System.Reflection.Assembly.GetExecutingAssembly().Location;
      //string sPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

      //try
      //{
      //  parametros = new SIGPIParametros(sPath + "\\parametros.xml");
      //}
      //catch (Exception ex)
      //{

      //  MessageBox.Show(ex.Message, "SIGPI 2010");
      //  return;
      //}

      procesamiento = new SIGPIProcesamiento(parametros);
      sigpi = new SIGPICls();
      sigpiDao = new SIGPIDao();
      sigpiDao.ConnectLocalDB(parametros.RutaBD);
      sigpiDao.UltimaFechaIncorporacion(sigpi);
      sigpi.Parametros = parametros;
      // Revisar que coincida la fecha de procesamiento de datos con la fecha de procesamiento del modelo
      txtFechaUltimoModelo.Text = sigpi.FechaProcesamiento.ToLongDateString();

      //if (!LicenseInitializer.InitializeApplication("arcview"))
      //{
      //  if (!LicenseInitializer.InitializeApplication("arcinfo"))
      //  {
      //    MessageBox.Show("No posee licencia de arcview o arcinfo");
      //  }
      //}
    }

    private void button1_Click(object sender, EventArgs e)
    {
      IProgressDialogFactory pProDiaFac = new ProgressDialogFactoryClass();
      IStepProgressor pStepPro = pProDiaFac.Create(null, 0);
      pStepPro.MinRange = 1;
      pStepPro.MaxRange = 5;
      pStepPro.StepValue = 1;
      IProgressDialog2 pProDia = (IProgressDialog2)pStepPro;
      pProDia.Animation = esriProgressAnimationTypes.esriProgressGlobe;

      pProDia.Title = "Generar Grids";
      pProDia.ShowDialog();
      pStepPro.Step();
      pStepPro.Message = "Generando Grids Meteorologicos...";

      IFeatureClass pFeatureClass;
      IWorkspaceFactory pShpWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
      IWorkspace pWorkspaceTemp = pShpWorkspaceFactory.OpenFromFile(parametros.RutaSIGPI + parametros.Temporal, 0);

      try
      {
        pFeatureClass = procesamiento.ConstruirFeatureClass(pWorkspaceTemp, "DEFI_PRECI", "estaciones", "CODIGO", "DEFI_PRECI.P5 >= 0", "JP");
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error generando el FeatureClass de Estaciones. Descripcion: \n" + ex.Message);
        return;
      }

      IWorkspaceFactory pWF = new AccessWorkspaceFactoryClass();
      IFeatureWorkspace pWSMask = (IFeatureWorkspace)pWF.OpenFromFile(parametros.RutaGBD, 0);
      IGeoDataset pFCMask = (IGeoDataset)pWSMask.OpenFeatureClass(parametros.Mascara);


      DateTime date = sigpi.FechaProcesamiento;
      string sMonth = date.ToString("MM");

      //P5
      pStepPro.Step();
      pStepPro.Message = "Generando Grids Meteorologicos: Precipitacion Total";
      IRaster pRaster1 = procesamiento.ConstruirGrid(pFeatureClass, "P5", parametros, pFCMask, "RP5", "", true);
      //DSLL5
      pStepPro.Step();
      pStepPro.Message = "Generando Grids Meteorologicos: Promedio ultimos 5 dias sin lluvia";
      IRaster pRaster2 = procesamiento.ConstruirGrid(pFeatureClass, "DSLL5", parametros, pFCMask, "DSLL5", "", true);
      pFeatureClass = procesamiento.ConstruirFeatureClass(pWorkspaceTemp, "DEFI_TEMPE", "estaciones", "CODIGO", "DEFI_TEMPE.T5 >= 0", "JT");
      //T5
      pStepPro.Step();
      pStepPro.Message = "Generando Grids Meteorologicos: Temperatura promedio";
      IRaster pRaster3 = procesamiento.ConstruirGrid(pFeatureClass, "T5", parametros, pFCMask, "RT5", "", true);
      //TMMMM
      IRaster pRaster4 = SIGPIUtils.AbrirRasterDesdeArchivo(parametros.RutaSIGPI + "\\" + parametros.TMMMM, "tmmx" + sMonth);
      //Susceptibilidad
      //IRaster pRaster5 = SIGPIUtils.AbrirRaster(parametros.RutaSIGPI + "\\" + parametros.Grids, parametros.Susceptibilidad);
      IRaster pRaster5 = SIGPIUtils.AbrirRasterDesdeGDB(parametros.RutaGBD, parametros.Susceptibilidad);
      //Asentamientos
      //IRaster pRaster6 = SIGPIUtils.AbrirRaster(parametros.RutaSIGPI + "\\" + parametros.Grids, parametros.Asentamientos);
      IRaster pRaster6 = SIGPIUtils.AbrirRasterDesdeGDB(parametros.RutaGBD, parametros.Asentamientos);
      IRaster[] pRasters = { pRaster1, pRaster2, pRaster3, pRaster4, pRaster5, pRaster6 };
      try
      {
        procesamiento.AlgoritmoCompleto(pRasters, sigpi);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }


      pProDia.HideDialog();
      MessageBox.Show("Algoritmo completo ejecutado");
    }

    private void button2_Click(object sender, EventArgs e)
    {
      FrmPreparacionInformacion frmPrepararInfo = new FrmPreparacionInformacion(parametros, sigpi, sigpiDao);
      frmPrepararInfo.ShowDialog();
      this.txtFechaUltimoModelo.Text = sigpi.FechaProcesamiento.ToLongDateString();
    }

    private void button6_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void button3_Click(object sender, EventArgs e)
    {
      FrmPassword frmPass = new FrmPassword();
      if (frmPass.ShowDialog() == DialogResult.Cancel)
        return;

      string sPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
      sPath = sPath.Replace("file:\\", "");
      try
      {
        XmlSerializer serializer = new XmlSerializer(typeof(SIGPIParametros));
        System.IO.StreamReader r = new System.IO.StreamReader(sPath + "\\parameters\\parametros.xml");
        parametros = (SIGPIParametros)serializer.Deserialize(r);
        r.Close();
        serializer = null;
        r = null;
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "SIGPI 2010");
        return;
      }
      FrmPropiedadesSIGPI frmPropiedades = new FrmPropiedadesSIGPI(parametros);
      frmPropiedades.ShowDialog();
    }

    private void button4_Click(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;
      DateTime fechaProcesamiento = sigpi.FechaProcesamiento;

      IWorkspaceFactory pWF = new RasterWorkspaceFactoryClass();
      IWorkspace pWorkspace = pWF.OpenFromFile(sigpi.Parametros.RutaSIGPI + "\\" + sigpi.Parametros.Raster, 0);

      IEnumDataset pEnumDS = pWorkspace.get_Datasets(esriDatasetType.esriDTRasterDataset);
      IDataset pDS = pEnumDS.Next();
      List<string> capas = new List<string>();
      while (pDS != null)
      {
        if (!pDS.Name.ToUpper().Contains("RASTER"))
          capas.Add(pDS.Name.ToUpper());
        pDS = pEnumDS.Next();
      }

      FrmProbabilidadCalculada frmProbabilidad = new FrmProbabilidadCalculada(capas.ToArray());
      this.Cursor = Cursors.Default;
      if (frmProbabilidad.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;
      int iNumDias = frmProbabilidad.NumeroDeDias;

      IProgressDialogFactory pProDiaFac = new ProgressDialogFactoryClass();
      IStepProgressor pStepPro = pProDiaFac.Create(null, 0);
      pStepPro.MinRange = 1;
      pStepPro.MaxRange = 2;
      pStepPro.StepValue = 1;
      IProgressDialog2 pProDia = (IProgressDialog2)pStepPro;
      pProDia.Animation = esriProgressAnimationTypes.esriProgressGlobe;

      pProDia.Title = "Calcular Probabilidad";
      pProDia.ShowDialog();
      pStepPro.Step();
      pStepPro.Message = "Calculando Probabilidad...";
      try
      {

        procesamiento.ProbabilidadCalculada(frmProbabilidad.ModeloBase, iNumDias, sigpi, frmProbabilidad.OpcionDeGuardar);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }

      pProDia.HideDialog();
      MessageBox.Show("Se ha calculado el modelo y la probabilidad para el dia:\n" + sigpi.FechaProcesamiento);
      txtFechaUltimoModelo.Text = sigpi.FechaProcesamiento.ToLongDateString();
    }

    private void button5_Click(object sender, EventArgs e)
    {

      //FrmExportarResultados frmExport = new FrmExportarResultados(sigpi, m_pApp);
      FrmExportarResultados2 frmExport = new FrmExportarResultados2(sigpi, m_pApp);
      frmExport.ShowDialog();
      return;

      //DateTime fechaProcesamiento = sigpi.FechaProcesamiento;

      //  IWorkspaceFactory pWF = new RasterWorkspaceFactoryClass();
      //  IWorkspace pWorkspace = pWF.OpenFromFile(sigpi.Parametros.RutaSIGPI + "\\" + sigpi.Parametros.Resultado, 0);

      //  IEnumDataset pEnumDS = pWorkspace.get_Datasets(esriDatasetType.esriDTRasterDataset);
      //  IDataset pDS = pEnumDS.Next();
      //  List<string> capas = new List<string>();
      //  while (pDS != null)
      //  {
      //      capas.Add(pDS.Name.ToUpper());
      //      pDS = pEnumDS.Next();
      //  }

      //  FrmProbabilidadCalculada frmProbabilidad = new FrmProbabilidadCalculada(capas.ToArray());
      //  if (frmProbabilidad.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;
      //  Excel.Application _excelApp = new Excel.Application();
      //  Worksheet sheet = procesamiento.ExportarAExcel(frmProbabilidad.ModeloBase, "", sigpi, _excelApp);
      //  sheet.SaveAs(sigpi.Parametros.RutaSIGPI + "Tablas\\" + "demo.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing,
      //                  Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
      //  _excelApp.Visible = true;
      //  _excelApp.Quit();

      //  MessageBox.Show("FinSS");
    }
    
    private void btnModelo2_Click(object sender, EventArgs e)
    {
      FrmAlgoritmoAmenazas frmAmenazas = new FrmAlgoritmoAmenazas(m_pApp, sigpi.FechaProcesamiento);
      frmAmenazas.ShowDialog();
    }

    private void GenerarModelo(string sTablaPrecipPromedio, string sConsultaTablaPrecipPromedio, string sPrefijo,
                              string sTablaTempPromedio, string sConsultaTablaTempPromedio)
    {
      ProcesarLecturas procesarLecturas = new ProcesarLecturas();

      try
      {
        procesarLecturas.CalcularResultadosPrecipitacion(sigpiDao, "DEFI_PRECI", "LECTUS_PRECI", sigpi.FechaProcesamiento, 5);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error procesando las lecturas:\n" + ex.Message);
      }

      //pStepPro.Step();
      //pStepPro.Message = "Procesando Temperatura...";
      try
      {
        procesarLecturas.CalcularResultadosTemperatura(sigpiDao, "DEFI_TEMPE", "LECTUS_TEMPE", sigpi.FechaProcesamiento, "T5", 5);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error procesando las lecturas:\n" + ex.Message);
      }

      string sSQL = "UPDATE FECHAS_PROCESO SET FEC_PROCE = #" + sigpi.FechaProcesamiento.ToString("MM/dd/yyyy") + "#";
      try
      {
        sigpiDao.EjecutarSentenciaSinQuery(sSQL);
      }
      catch (Exception ex)
      {
        MessageBox.Show("No se pudo actualizar la fecha de incorporacion.\n" + ex.Message);
      }

      IProgressDialogFactory pProDiaFac = new ProgressDialogFactoryClass();
      IStepProgressor pStepPro = pProDiaFac.Create(null, 0);
      pStepPro.MinRange = 1;
      pStepPro.MaxRange = 5;
      pStepPro.StepValue = 1;
      IProgressDialog2 pProDia = (IProgressDialog2)pStepPro;
      pProDia.Animation = esriProgressAnimationTypes.esriProgressGlobe;

      pProDia.Title = "Generar Grids";
      pProDia.ShowDialog();
      pStepPro.Step();
      pStepPro.Message = "Generando Grids Meteorologicos...";

      IFeatureClass pFeatureClass;
      IWorkspaceFactory pShpWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
      IWorkspace pWorkspaceTemp = pShpWorkspaceFactory.OpenFromFile(parametros.RutaSIGPI + parametros.Temporal, 0);

      // Crear FeatureClass a partir de los datos de la tabla de los promedios de la precipitacion
      try
      {
        //pFeatureClass = procesamiento.ConstruirFeatureClass(pWorkspaceTemp, "DEFI_PRECI", "estaciones", "CODIGO", "DEFI_PRECI.P5 >= 0", "JP");
        pFeatureClass = procesamiento.ConstruirFeatureClass(pWorkspaceTemp, sTablaPrecipPromedio, "estaciones", "CODIGO", sConsultaTablaPrecipPromedio, "JP");
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error generando el FeatureClass de Estaciones. Descripcion: \n" + ex.Message);
        return;
      }

      IWorkspaceFactory pWF = new AccessWorkspaceFactoryClass();
      IFeatureWorkspace pWSMask = (IFeatureWorkspace)pWF.OpenFromFile(parametros.RutaGBD, 0);
      IGeoDataset pFCMask = (IGeoDataset)pWSMask.OpenFeatureClass(parametros.Mascara);


      DateTime date = sigpi.FechaProcesamiento;
      string sMonth = date.ToString("MM");

      //P5
      pStepPro.Step();
      pStepPro.Message = "Generando Grids Meteorologicos: Precipitacion Total";
      IRaster pRaster1 = procesamiento.ConstruirGrid(pFeatureClass, "P5", parametros, pFCMask, "RP5", "", true);
      //DSLL5
      pStepPro.Step();
      pStepPro.Message = "Generando Grids Meteorologicos: Promedio ultimos 5 dias sin lluvia";
      IRaster pRaster2 = procesamiento.ConstruirGrid(pFeatureClass, "DSLL5", parametros, pFCMask, "DSLL5", "", true);
      //Crear FeatureClass a partir de la tabla de promedios de temperatura
      //pFeatureClass = procesamiento.ConstruirFeatureClass(pWorkspaceTemp, "DEFI_TEMPE", "estaciones", "CODIGO", "DEFI_TEMPE.T5 >= 0", "JT");
      pFeatureClass = procesamiento.ConstruirFeatureClass(pWorkspaceTemp, sTablaTempPromedio, "estaciones", "CODIGO", sConsultaTablaTempPromedio, "JT");
      //T5
      pStepPro.Step();
      pStepPro.Message = "Generando Grids Meteorologicos: Temperatura promedio";
      IRaster pRaster3 = procesamiento.ConstruirGrid(pFeatureClass, "T5", parametros, pFCMask, "RT5", "", true);
      //TMMMM
      IRaster pRaster4 = SIGPIUtils.AbrirRasterDesdeArchivo(parametros.RutaSIGPI + "\\" + parametros.TMMMM, "tmmx" + sMonth);
      //Susceptibilidad
      //IRaster pRaster5 = SIGPIUtils.AbrirRaster(parametros.RutaSIGPI + "\\" + parametros.Grids, parametros.Susceptibilidad);
      IRaster pRaster5 = SIGPIUtils.AbrirRasterDesdeGDB(parametros.RutaGBD, parametros.Susceptibilidad);
      //Asentamientos
      //IRaster pRaster6 = SIGPIUtils.AbrirRaster(parametros.RutaSIGPI + "\\" + parametros.Grids, parametros.Asentamientos);
      IRaster pRaster6 = SIGPIUtils.AbrirRasterDesdeGDB(parametros.RutaGBD, parametros.Asentamientos);
      IRaster[] pRasters = { pRaster1, pRaster2, pRaster3, pRaster4, pRaster5, pRaster6 };
      try
      {
        procesamiento.AlgoritmoCompleto(pRasters, sigpi);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }


      pProDia.HideDialog();
      MessageBox.Show("Algoritmo completo ejecutado");
    }

    private void btnAbout_Click(object sender, EventArgs e)
    {
      FrmAboutBox about = new FrmAboutBox();
      about.ShowDialog();
    }

    
  }
}
