using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geoprocessing;
using System.Xml.Serialization;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.GeoDatabaseUI;
using ESRI.ArcGIS.SpatialAnalystTools;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DataManagementTools;
using ESRI.ArcGIS.Geoprocessor;
using System.Data.OleDb;

namespace SIGPI_10
{
  /// <summary>
  /// Formulario Algoritmo Amenazas Incendios Forestales
  /// </summary>
  public partial class FrmAlgoritmoAmenazas : Form
  {
    #region Variables Privadas

    IApplication m_pApp;

    #endregion

    #region Constructor de la clase
    /// <summary>
    /// Constructor de la Clase
    /// </summary>
    /// <param name="pApp">Aplication ArcGIS Desktop</param>
    /// <param name="dFechaEjecucion">Fecha de Ejecucion</param>
    public FrmAlgoritmoAmenazas(IApplication pApp, DateTime dFechaEjecucion)
    {
      InitializeComponent();
      m_pApp = pApp;
      lblFechaAEjecutar.Text = dFechaEjecucion.ToLongDateString();
    }
    #endregion

    #region Eventos Privados
    /// <summary>
    /// Evento Clic del boton Ejecutar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnEjecutar_Click_1(object sender, EventArgs e)
    {
      //if (!LicenseInitializer.InitializeApplication("arcinfo"))
      //{
      //  if (!LicenseInitializer.InitializeApplication("arcview"))
      //  {
      //    MessageBox.Show("No posee licencia de arcview o arcinfo");
      //  }
      //}

      IGeoProcessor gp = new GeoProcessor();


      // || txtRutaTemperatura.Text.Trim().Length == 0
      if (chkUtilizarImagenes.Checked & (txtRutaPrecipitacion.Text.Trim().Length == 0))
      {

        MessageBox.Show("Busque las capa de precipitacion solicitadas");
        return;
      }
      
      //string sRasterPrecipitacionSatelite = txtRutaPrecipitacion.Text;
      //string sFecha = txtRutaPrecipitacion.Text.Substring(txtRutaPrecipitacion.Text.IndexOf('_'), 20).Replace("_satelite_", "");
      //String[] sFecha2 = sFecha.Split('_');
      //DateTime dFechaPrecipitacionSatelite = new DateTime(Convert.ToInt32(sFecha2[0]), Convert.ToInt32(sFecha2[1]), Convert.ToInt32(sFecha2[2]));
      String[] sRastersPrecipitacionX10 = new String[10];
      //DateTime dTemp;
      //String sTemp;
      //for (int i = 0; i < 10; i++)
      //{
      //  dTemp = dFechaPrecipitacionSatelite.AddDays(-i);
      //  sTemp = dTemp.ToString("yyyy_MM_dd"); //  +"_" + dTemp.ToString("MM") + "_" + dTemp.ToString("dd");
      //  sRastersPrecipitacionX10[i] = sRasterPrecipitacionSatelite.Replace(sFecha, sTemp);
      //}

      GenerarModelo("PRECIPITACION_PROMEDIO", "PRECIPITACION_PROMEDIO.SUM_PREC >= 0", "",
                      "TEMPERATURA_PROMEDIO", "TEMPERATURA_PROMEDIO.TEMP_PROM >= 0",
                      chkUtilizarImagenes.Checked, chkVerResultadorIntermedios.Checked, sRastersPrecipitacionX10);

    }

    /// <summary>
    /// Evento del Boton Cerrar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnCerrar_Click_1(object sender, EventArgs e)
    {
      this.Close();
    }

    /// <summary>
    /// Evento clic del boton borrar temporales
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnDelTemp_Click_1(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;
      BorrarTemporales();
      this.Cursor = Cursors.Default;
    }

    /// <summary>
    /// Evento Clic del boton Abrir Temperatura. Busca el raster de 
    /// temperaturas generado a partir de imagenes de satelite
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnAbrirTemperatura_Click_1(object sender, EventArgs e)
    {
      txtRutaTemperatura.Text = BuscarRaster("Buscar Raster Temperatura");
    }

    /// <summary>
    /// Evento Clic del boton Abrir Precipitacion. Busca el raster de 
    /// precipitacion generado a partir de imagenes de satelite
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnAbrirPrecipitacion_Click_1(object sender, EventArgs e)
    {
      txtRutaPrecipitacion.Text = BuscarRaster("Buscar Raster Precipitacion");
    }

    /// <summary>
    /// Evento Checked para activar la opcion que indica
    /// si se van a utilizar imagenes de satelite
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void chkUtilizarImagenes_CheckedChanged(object sender, EventArgs e)
    {
      if (chkUtilizarImagenes.Checked)
        panelOpImagenes.Visible = true;
      else
      {
        panelOpImagenes.Visible = false;
        txtRutaPrecipitacion.Text = "";
        txtRutaTemperatura.Text = "";
      }
    }

    /// <summary>
    /// Evento Clic del boton Abrir Raster NVI. Busca el raster del 
    /// NVI generado a partir de imagenes de satelite
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnBuscarNVI_Click_1(object sender, EventArgs e)
    {
      txtRutaNVI.Text = BuscarRaster("Buscar Raster NVI");
    }

    #endregion

    #region Metodos Privados
    /// <summary>
    /// Ejecuta el modelo de amenazas de incendios
    /// </summary>
    /// <param name="sTablaPrecipPromedio">Tabla Precipitacion Promedio</param>
    /// <param name="sConsultaTablaPrecipPromedio">SQL para la table precipitacion promedio</param>
    /// <param name="sPrefijo">Prefijo para los nombres de las capas</param>
    /// <param name="sTablaTempPromedio">Nombre de la tabla temporal de promedios</param>
    /// <param name="sConsultaTablaTempPromedio">SQL para la tabla temporal de promedios</param>
    /// <param name="bUsarSatelite">Indica si se han de utilizar las imagenes de satelita</param>
    /// <param name="bMostrarIntermedios">Indica si se mostraran los resultados intermedios en el mapa activo</param>
    private void GenerarModelo(string sTablaPrecipPromedio, string sConsultaTablaPrecipPromedio, string sPrefijo,
                                string sTablaTempPromedio, string sConsultaTablaTempPromedio, bool bUsarSatelite,
                                bool bMostrarIntermedios, String[] sRastersPrecipitacion)
    {
      string sPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

      sPath = sPath.Replace("file:\\", "");
      SIGPIParametros parametros = new SIGPIParametros();
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

      SIGPIProcesamiento procesamiento = new SIGPIProcesamiento(parametros);
      SIGPICls sigpi = new SIGPICls();
      SIGPIDao sigpiDao = new SIGPIDao();
      sigpiDao.ConnectLocalDB(parametros.RutaBD);
      sigpiDao.UltimaFechaIncorporacion(sigpi);
      sigpi.Parametros = parametros;

      OleDbCommand command = sigpiDao.LocalDBConnection.CreateCommand();
      OleDbParameter param = command.CreateParameter();
      OleDbParameter param1 = command.CreateParameter();
      param.ParameterName = "fecProce";
      param.Value = sigpi.FechaProcesamiento;
      
      command.CommandText = "UPDATE FECHAS_PROCESO SET FEC_PROCE = @fecProce";
      command.Parameters.Add(param);

      string sSQL = ""; // "UPDATE FECHAS_PROCESO SET FEC_PROCE = #" + sigpi.FechaProcesamiento.ToString("MM/dd/yyyy") + "#";
      try
      {
        sigpiDao.EjecutarSentenciaSinQuery(command);
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

      pProDia.Title = "Generar Modelo Amenazas";
      pProDia.ShowDialog();
      pStepPro.Step();
      pStepPro.Message = "Generando Grids Meteorologicos...";

      IFeatureClass pFeatureClass;
      IWorkspaceFactory pShpWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
      IWorkspaceFactory pFileGDBWorkspaceFactory = new FileGDBWorkspaceFactoryClass();

      string sFormatTmp = "gdb_" + sigpi.FechaProcesamiento.ToString("yyyyMMdd") + "_" +
                          DateTime.Now.ToString("HHmmss");

      string sRutaFileGDB = System.IO.Path.GetTempPath() + sFormatTmp + ".gdb";

      if (System.IO.Directory.Exists(sRutaFileGDB))
      {
        try
        {
          System.IO.Directory.Delete(sRutaFileGDB);
        }
        catch (Exception ex)
        {
          MessageBox.Show("No se pudo borrar la File Geodatabase Temporal: " +
                          sRutaFileGDB +
                          " Intente Borrarla Manualmente. " + ex.Message);
          return;
        }
      }

      Geoprocessor gp = new Geoprocessor();
      IWorkspaceName pWSName;
      string sCapaResultado = "Amenaza_" + sigpi.FechaProcesamiento.ToString("yyyy_MM_dd");

      string sRutaGdbResultados = parametros.RutaSIGPI + parametros.Resultado + "\\" +
                                  sigpi.FechaProcesamiento.Year.ToString() + "-" +
                                  sigpi.FechaProcesamiento.ToString("MM") + "-Modelos.gdb";

      if (System.IO.Directory.Exists(sRutaGdbResultados))
      {
        GPUtilitiesClass gpUtilClass = new GPUtilitiesClass();
        try
        {

          Delete del = new Delete();
          del.in_data = sRutaGdbResultados + "\\" + sCapaResultado;
          gp.Execute(del, null);
        }
        catch (Exception)
        {

        }
      }
      else
      {
        try
        {
          pWSName = pFileGDBWorkspaceFactory.Create(parametros.RutaSIGPI + parametros.Resultado + "\\",
                                  sigpi.FechaProcesamiento.Year.ToString() + "-" +
                                  sigpi.FechaProcesamiento.ToString("MM") + "-Modelos.gdb", null, 0);
        }
        catch (Exception ex)
        {
          MessageBox.Show("No se pudo crear la Geodatabase de resultados: \n" + sRutaGdbResultados);
          pStepPro.Hide();
        }
      }

      pWSName = pFileGDBWorkspaceFactory.Create(System.IO.Path.GetTempPath(), sFormatTmp, null, 0);
      ESRI.ArcGIS.esriSystem.IName name = (ESRI.ArcGIS.esriSystem.IName)pWSName;

      IFeatureWorkspace pWorkspaceTemp;
      try
      {
        pWorkspaceTemp = (IFeatureWorkspace)name.Open();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
        return;
      }

      IWorkspaceFactory pWF = new AccessWorkspaceFactoryClass();
      IFeatureWorkspace pWSMask = (IFeatureWorkspace)pWF.OpenFromFile(parametros.RutaGBD, 0);
      IGeoDataset pFCMask = (IGeoDataset)pWSMask.OpenFeatureClass(parametros.Mascara);
      ISpatialReference pSpaRef = pFCMask.SpatialReference;
      IEnvelope pEnv = pFCMask.Extent;

      string sNombreTabla = "TMPPR_";

      DateTime fechaProcesamiento = sigpi.FechaProcesamiento;
      fechaProcesamiento = fechaProcesamiento.Date;


      for (int i = 0; i < 10; i++)
      {
        
        try
        {
          sigpiDao.EjecutarSentenciaSinQuery("DROP TABLE " + sNombreTabla + i.ToString());
        }
        catch (Exception ex)
        {
          System.Console.WriteLine(ex.Message);
        }
        // "IIf([LECTUS_PRECI]![LECTURA]<=2,5,IIf([LECTUS_PRECI]![LECTURA]<=8 And [LECTUS_PRECI]![LECTURA]>2,4,IIf([LECTUS_PRECI]![LECTURA]<=14 And [LECTUS_PRECI]![LECTURA]>8,3,IIf([LECTUS_PRECI]![LECTURA]<=24 And [LECTUS_PRECI]![LECTURA]>14,2,IIf([LECTUS_PRECI]![LECTURA]>24,1,0))))) AS LECTURAS " +
        //sSQL = "SELECT CODIGO, FECHA, X, Y, LECTURA AS RASTERVALU " +
        //              "INTO " + sNombreTabla + i.ToString() + " " +
        //              "FROM LECTUS_PRECI " +
        //              "WHERE (((FECHA)=#" + sigpi.FechaProcesamiento.AddDays(-i).ToString("MM/dd/yyyy") + "#))";
        command = sigpiDao.LocalDBConnection.CreateCommand();
        command.CommandText = "SELECT CODIGO, FECHA, X, Y, LECTURA AS RASTERVALU " +
                      "INTO " + sNombreTabla + i.ToString() + " " +
                      "FROM LECTUS_PRECI " +
                      "WHERE (((FECHA) >=@fecha) and ((FECHA) <@fecha1))";
        param = command.CreateParameter();
        param.ParameterName = "fecha";
        param.Value = sigpi.FechaProcesamiento.AddDays(-i);
        command.Parameters.Add(param);
        param1 = command.CreateParameter();
        param1.ParameterName = "fecha1";
        param1.Value = sigpi.FechaProcesamiento.AddDays(-i+1);
        command.Parameters.Add(param1);

        try
        {
          sigpiDao.EjecutarSentenciaSinQuery(command);
        }
        catch (Exception ex)
        {
          MessageBox.Show("Error generando las tablas temporales de precipitaciones. Descripcion: \n" + ex.Message);
          //pProDia.HideDialog();
          return;
        }
      }

      try
      {
        sigpiDao.EjecutarSentenciaSinQuery("DROP TABLE TEMPERATURA_PROMEDIO");
      }
      catch (Exception ex)
      {
      }

      //"IIf(Avg(LECTUS_TEMPE.LECTURA)<=6,1,IIf(Avg(LECTUS_TEMPE.LECTURA) <=12 And Avg(LECTUS_TEMPE.LECTURA)>6,2," + 
      //"IIf(Avg(LECTUS_TEMPE.LECTURA)<=18 And Avg(LECTUS_TEMPE.LECTURA)>12,3,IIf(Avg(LECTUS_TEMPE.LECTURA)<=24 And " +
      //"Avg(LECTUS_TEMPE.LECTURA)>12,4,IIf(Avg(LECTUS_TEMPE.LECTURA)>24,5,0))))) AS LECTURAS, " +
      
      //sSQL = "SELECT CODIGO, Max(LECTUS_TEMPE.FECHA) AS FECHA, 10 AS Num_Dias, X, Y, AVG(LECTURA) AS RASTERVALU " +
      //        "INTO TEMPERATURA_PROMEDIO " +
      //        "FROM LECTUS_TEMPE " +
      //        "WHERE (((FECHA)>=#" + sigpi.FechaProcesamiento.AddDays(-10).ToString("MM/dd/yyyy") + "# " +
      //        "And (FECHA)<=#" + sigpi.FechaProcesamiento.ToString("MM/dd/yyyy") + "#)) " +
      //        "GROUP BY CODIGO, X, Y";
      command = sigpiDao.LocalDBConnection.CreateCommand();
      command.CommandText = "SELECT CODIGO, Max(LECTUS_TEMPE.FECHA) AS FECHA, 10 AS Num_Dias, X, Y, AVG(LECTURA) AS RASTERVALU " +
              "INTO TEMPERATURA_PROMEDIO " +
              "FROM LECTUS_TEMPE " +
              "WHERE (((FECHA)>= @fecha1 " +
              "And (FECHA)<= @fecha)) " +
              "GROUP BY CODIGO, X, Y";
      param = command.CreateParameter();
      param.ParameterName = "fecha";
      param.Value = sigpi.FechaProcesamiento;
      param1 = command.CreateParameter();
      param1.ParameterName = "fecha1";
      param1.Value = sigpi.FechaProcesamiento.AddDays(-10);
      command.Parameters.Add(param1);
      command.Parameters.Add(param);
      
      try
      {
        sigpiDao.EjecutarSentenciaSinQuery(command);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error Generando Tabla: TEMPERATURA_PROMEDIO " + ex.Message);
        pProDia.HideDialog();
        return;
      }

      string sExpression = "";
      string sOutGrid;
      double dPeso = 0;
      string sAmenazaXPrecipitacion = sRutaFileGDB + "\\amenaza_x_precipitacion";
      string sTablaTemperaturaPromedio = "TEMPERATURA_PROMEDIO";
      string sFCTempPromedio = "TEMP_PROMEDIO";
      string sPrefijoIDW = "idw";
      string sAmenazaXTemperatura = sRutaFileGDB + "\\" + "amenaza_x_temperatura";
      string sAmenazasParciales = parametros.RutaGBD + "\\" + "amenazas_parciales_2";
      string sAmenazaBruta = sRutaFileGDB + "\\amenaza_incendio_bruta";
      string sAmenazaFinal = sRutaFileGDB + "\\amenaza_incendio_final";
      string sTablaReclassIncendios = parametros.TablaReclasificacionIncendios; //"tbl_reclass_amenaza_incendios";
      string sTablaReclassTemp = parametros.TablaReclasificacionTemperatura; //"tbl_reclass_temperatura";
      string sTablaReclassPrecip = parametros.TablaReclasificacionPrecipitacion; //"tbl_reclass_precipitacion";
      string sAmenazaXTemperaturaCombinada = sRutaFileGDB + "\\" + "amenaza_x_temperatura_combinada";
      string sEstVirtualesPrecipitacion = sRutaFileGDB + "\\" + "estaciones_virtuales_precipitacion";
      string sAmenazaXPrecipitacionCombinada = sRutaFileGDB + "\\" + "amenaza_x_precipitacion_combinada";
      string sAmenazaFinalBrutaNVI = sRutaFileGDB + "\\" + "amenaza_incendio_bruta_nvi";
      string sNVITempReclass = sRutaFileGDB + "\\" + "nvi_reclass_temp";
      string sFromField = "FROM_";
      string sToField = "TO_";
      string sOutField = "OUT";
      string sNoData = "NODATA";
      string sFieldLecturas = "RASTERVALU";  //LECTURAS";
      string sTipoEstadistico = parametros.TipoEstadistico; //"MAXIMUM";      
      string sAmenazaXPrecipReclass = sAmenazaXPrecipitacion + "_reclass";
      string sAmenazaXTempReclass = sAmenazaXTemperatura + "_reclass";

      double dPesoPrecipitacion = parametros.PesoPrecipitacion;  //0.29;
      double dPesoTemperatura = parametros.PesoTemperatura;  //0.24;
      double dPesoAmenazasParciales = parametros.PesoAmenazasParciales;  //0.47;

      IDataset pDS;
      IDatasetName pDSName;
      IDatasetName pDSName2 = new FeatureClassNameClass();
      IExportOperation pExportOp = new ExportOperationClass();

      string sEstacionesVirtuales = @"C:\SIGPI\datos\ESTACIONES VIRTUALES\" + "EstacionesVirtuales.shp";
      ExtractValuesToPoints extractValPoint = new ExtractValuesToPoints();
      extractValPoint.in_point_features = sEstacionesVirtuales;
      extractValPoint.interpolate_values = "NONE";
      extractValPoint.add_attributes = "VALUE_ONLY";


      //'0.037037037;
      // 1" = 1/3600  
      double dCellSize = parametros.TamanoCelda / (3600 * 30);
      double[] iPesos = parametros.Pesos; //{ 30, 20, 10, 9, 8, 7, 6, 5, 4, 3 };
      double iTotalPesos = 0; //102;
      foreach (double dP in iPesos)
      {
        iTotalPesos += dP;
      }

      gp.AddOutputsToMap = bMostrarIntermedios;
      gp.SetEnvironmentValue("Mask", parametros.RutaGBD + "\\" + parametros.Mascara);
      gp.SetEnvironmentValue("Extent", pEnv.XMin.ToString() + " " + pEnv.YMin.ToString() + " " +
                                       pEnv.XMax.ToString() + " " + pEnv.YMax.ToString());

      ESRI.ArcGIS.SpatialAnalystTools.Idw idw = new Idw();
      idw.z_field = sFieldLecturas;
      idw.cell_size = dCellSize;

      for (int i = 0; i < 10; i++)
      {
        pFeatureClass = procesamiento.FCPrecipitacion(sNombreTabla + i.ToString(), pSpaRef);
        pDS = (IDataset)pFeatureClass;
        pDSName = (IDatasetName)pDS.FullName;
        pDSName2.Name = sNombreTabla + i.ToString();
        pDSName2.WorkspaceName = pWSName;
        try
        {
          pExportOp.ExportFeatureClass(pDSName, null, null, null, (IFeatureClassName)pDSName2, 0);
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message);
          pProDia.HideDialog();
          return;
        }

        if (bUsarSatelite)
        {
          //Geoprocessor gp1 = new Geoprocessor();
          //extractValPoint.in_raster = sRastersPrecipitacion[i];  // txtRutaPrecipitacion.Text;
          //extractValPoint.out_point_features = sEstVirtualesPrecipitacion + i.ToString();

          //try
          //{
          //  gp1.Execute(extractValPoint, null);
          //}
          //catch (Exception ex)
          //{
          //  MessageBox.Show(ex.Message);
          //}

          //Merge merge = new Merge();
          //string sInputMerge = sRutaFileGDB + "\\" + sNombreTabla + i.ToString() + ";" + sEstVirtualesPrecipitacion + i.ToString();
          //merge.inputs = sInputMerge;
          ////"[" + sRutaFileGDB + "\\" + sNombreTabla + i.ToString() + ";" + sEstVirtualesPrecipitacion + i.ToString() + "]" ;
          //merge.output = sRutaFileGDB + "\\" + "est_precip_temporal" + i.ToString();
          //try
          //{
          //  gp.Execute(merge, null);
          //}
          //catch (Exception ex)
          //{
          //  MessageBox.Show(ex.Message);
          //}

          //idw.in_point_features = sRutaFileGDB + "\\" + "est_precip_temporal" + i.ToString();

          idw.in_point_features = sRutaFileGDB + "\\" + sNombreTabla + i.ToString();
        }
        else
        {
          idw.in_point_features = sRutaFileGDB + "\\" + sNombreTabla + i.ToString();
        }

        sOutGrid = sRutaFileGDB + "\\" + sPrefijoIDW + "_" + sNombreTabla + i.ToString();
        idw.out_raster = sOutGrid;
        try
        {
          gp.Execute(idw, null);
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message);
        }

        dPeso = iPesos[i] / iTotalPesos;
        sExpression += "( Raster(r'" + sOutGrid + "') * " + dPeso.ToString().Replace(",",".") + ")";
        if (i < 9)
        {
          sExpression += " + ";
        }

      }


      pStepPro.Step();

      ////for (int i = 0; i < 10; i++)
      ////{
      ////  //pStepPro.Message = "Generando Modelo Precipitacion: " + i.ToString();
      ////  sOutGrid = sRutaFileGDB + "\\" + sPrefijoIDW + "_" + sNombreTabla + i.ToString();
      ////  idw.in_point_features = sRutaFileGDB + "\\" + sNombreTabla + i.ToString();
      ////  idw.out_raster = sOutGrid;
      ////  gp.Execute(idw, null);
      ////  dPeso = iPesos[i] / iTotalPesos;
      ////  sExpression += "(" + sOutGrid + " * " + dPeso.ToString() + ")";
      ////  if (i < 9)
      ////    sExpression += " + ";
      ////}
      //gp.AddMessage("Expresion: " + sExpression);
      //SingleOutputMapAlgebra mapAlgebra = new SingleOutputMapAlgebra();

      ESRI.ArcGIS.SpatialAnalystTools.RasterCalculator mapAlgebra = new RasterCalculator();
      //mapAlgebra.expression_string = sExpression;
      // mapAlgebra.out_raster = sAmenazaXPrecipitacion;

      mapAlgebra.expression = sExpression;

      mapAlgebra.output_raster = sAmenazaXPrecipitacion;
      pStepPro.Message = "Generando Amenaza Precipitacion...";
      try
      {
        gp.Execute(mapAlgebra, null);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error Generando Amenaza x Precipitacion. " + ex.Message);
        pProDia.HideDialog();
        return;
      }

      //if (bUsarSatelite)  // Para la combinacion de la precipitacion x estaciones y satelite
      //{

      //  //CellStatistics cellStatisticsP = new CellStatistics();
      //  //cellStatisticsP.in_rasters_or_constants = sAmenazaXPrecipitacion + ";" + txtRutaPrecipitacion.Text;
      //  //cellStatisticsP.out_raster = sAmenazaXPrecipitacionCombinada;
      //  //cellStatisticsP.statistics_type = sTipoEstadistico;
      //  try
      //  {
      //    gp.Execute(extractValPoint, null);
      //    //sAmenazaXPrecipitacion = sAmenazaXPrecipitacionCombinada;
      //  }
      //  catch (Exception ex)
      //  {
      //    MessageBox.Show("Error generando estaciones virtuales con precipitacion." + ex.Message);
      //    return;
      //  }

      //  Merge merge = new Merge();
      //  merge.inputs = 

      //}

      DateTime date = sigpi.FechaProcesamiento;
      string sMonth = date.ToString("MM");

      pStepPro.Step();
      pStepPro.Message = "Generando Amenaza x Temperatura";
      pFeatureClass = procesamiento.FCPrecipitacion(sTablaTemperaturaPromedio, pSpaRef);
      pDS = (IDataset)pFeatureClass;
      pDSName = (IDatasetName)pDS.FullName;
      pDSName2.Name = sFCTempPromedio;
      pDSName2.WorkspaceName = pWSName;
      try
      {
        pExportOp.ExportFeatureClass(pDSName, null, null, null, (IFeatureClassName)pDSName2, 0);
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

      idw.in_point_features = sRutaFileGDB + "\\" + sFCTempPromedio;
      idw.out_raster = sAmenazaXTemperatura;
      try
      {
        gp.Execute(idw, null);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }


      //if (bUsarSatelite)
      //{
      //  Geoprocessor gp1 = new Geoprocessor();
      //  Con pContmp = new Con();
      //  pContmp.in_conditional_raster = txtRutaTemperatura.Text;
      //  pContmp.in_true_raster_or_constant = txtRutaTemperatura.Text;
      //  pContmp.in_false_raster_or_constant = -99;
      //  string sRasterTTemp = sAmenazaXTemperaturaCombinada + "_ajs";
      //  pContmp.out_raster = sRasterTTemp;
      //  pContmp.where_clause = "VALUE < 55";

      //  try
      //  {
      //    gp1.Execute(pContmp, null);
      //  }
      //  catch (Exception)
      //  {
      //    sRasterTTemp = txtRutaTemperatura.Text; ;
      //  }
      //  gp1 = null;

      //  CellStatistics cellStatistics = new CellStatistics();
      //  cellStatistics.in_rasters_or_constants = sAmenazaXTemperatura + ";" + sRasterTTemp;
      //  cellStatistics.out_raster = sAmenazaXTemperaturaCombinada;
      //  cellStatistics.statistics_type = sTipoEstadistico;
      //  try
      //  {
      //    gp.Execute(cellStatistics, null);
      //    sAmenazaXTemperatura = sAmenazaXTemperaturaCombinada;
      //  }
      //  catch (Exception)
      //  {
      //  }
      //}

      ReclassByTable rbt = new ReclassByTable();

      rbt.in_raster = sAmenazaXPrecipitacion;
      rbt.in_remap_table = sTablaReclassPrecip;  // parametros.RutaGBD + "\\" + sTablaReclassPrecip;
      rbt.from_value_field = sFromField;
      rbt.to_value_field = sToField;
      rbt.output_value_field = sOutField;
      rbt.missing_values = sNoData;
      rbt.out_raster = sAmenazaXPrecipReclass;
      //pStepPro.Message = "Generando Amenaza X Precipitacion Reclasificada";
      try
      {
        gp.Execute(rbt, null);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
        pProDia.HideDialog();
        return;
      }

      rbt.in_raster = sAmenazaXTemperatura;
      rbt.in_remap_table = sTablaReclassTemp;  // parametros.RutaGBD + "\\" + sTablaReclassTemp;
      rbt.out_raster = sAmenazaXTempReclass;
      //pStepPro.Message = "Generando Amenaza X Temperatura Reclasificada";
      try
      {
        gp.Execute(rbt, null);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
        pProDia.HideDialog();
        return;
      }

      if (txtRutaNVI.Text.Trim() != "")
      {
        //Geoprocessor gp2 = new Geoprocessor();
        //Con pCon = new Con();
        //pCon.in_conditional_raster = txtRutaNVI.Text;
        //pCon.in_true_raster_or_constant = 0;
        //pCon.in_false_raster_or_constant = -1;
        //pCon.where_clause = "VALUE <= 0";
        //pCon.out_raster = sNVITempReclass;
        //try
        //{
        //  gp2.Execute(pCon, null);
        //}
        //catch (Exception ex)
        //{
        //  MessageBox.Show(ex.Message);
        //}
        //gp2 = null;

        //sExpression = "( Raster('" + sAmenazasParciales + "') + Raster('" + sNVITempReclass + "'))";
        //sExpression = sExpression.Replace("\\\\", "/").Replace("\\", "/");
        //mapAlgebra.expression = sExpression;
        //mapAlgebra.output_raster = sAmenazaFinalBrutaNVI;

        //try
        //{
        //  gp.Execute(mapAlgebra, null);
        //  sAmenazasParciales = sAmenazaFinalBrutaNVI;
        //}
        //catch (Exception ex)
        //{
        //  MessageBox.Show(ex.Message);
        //}
      }


      sExpression = "( Raster(r'" + sAmenazaXPrecipReclass + "') * " + dPesoPrecipitacion.ToString().Replace(",", ".") + ") + " +
                    "( Raster(r'" + sAmenazaXTempReclass + "') * " + dPesoTemperatura.ToString().Replace(",", ".") + ") + " +
                    "( Raster(r'" + sAmenazasParciales + "') * " + dPesoAmenazasParciales.ToString().Replace(",", ".") + ")";

      //mapAlgebra.expression_string = sExpression;
      //mapAlgebra.out_raster = sAmenazaBruta;

      sExpression = sExpression.Replace("\\\\", "/").Replace("\\", "/");
      mapAlgebra.expression = sExpression;
      mapAlgebra.output_raster = sAmenazaBruta;

      pStepPro.Message = "Generando Amenaza Final Bruta";
      try
      {
        gp.Execute(mapAlgebra, null);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }



      rbt.in_raster = sAmenazaBruta;
      rbt.in_remap_table = sTablaReclassIncendios; // parametros.RutaGBD + "\\" + sTablaReclassIncendios;
      rbt.from_value_field = sFromField;
      rbt.to_value_field = sToField;
      rbt.output_value_field = sOutField;
      rbt.missing_values = sNoData;
      rbt.out_raster = sAmenazaFinal;
      pStepPro.Message = "Generando Amenaza Final Reclasificada";
      gp.AddOutputsToMap = true;
      try
      {
        gp.Execute(rbt, null);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }

      Copy copy = new Copy();
      copy.in_data = sAmenazaFinal;

      copy.out_data = sRutaGdbResultados + "\\" + sCapaResultado;
      try
      {
        gp.Execute(copy, null);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error Copiando el resultado. \n " + ex.Message);
      }



      MessageBox.Show("Algoritmo completo ejecutado: " + sRutaGdbResultados + "\\" + sCapaResultado);

      if (m_pApp != null)
      {
        IRasterLayer pRLayer = new RasterLayerClass();
        try
        {
          IWorkspaceFactory pWF2 = new FileGDBWorkspaceFactoryClass();
          IRasterWorkspaceEx pRW = (IRasterWorkspaceEx)pWF2.OpenFromFile(sRutaGdbResultados, 0);
          IRasterDataset pRDataset = pRW.OpenRasterDataset(sCapaResultado);
          pRLayer.CreateFromDataset(pRDataset);
          pRLayer.Name = sCapaResultado;
          IMxDocument pMxDoc = m_pApp.Document as IMxDocument;
          IMap pMap = pMxDoc.FocusMap;
          AsignarSimbologiaProbabilidad(pRLayer);
          pMap.AddLayer(pRLayer);
          if (pMap.LayerCount > 0)
          {
            pMap.MoveLayer(pRLayer, pMap.LayerCount - 1);
          }

          pMxDoc.UpdateContents();
          pMxDoc.ActiveView.Refresh();
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message, "Error cargando Capa!!!");
        }
      }

      pProDia.HideDialog();
    }

    /// <summary>
    /// Borra los archivos raster del directorio temporal
    /// </summary>
    private void BorrarTemporales()
    {

      Geoprocessor gp = new Geoprocessor();
      gp.SetEnvironmentValue("workspace", System.IO.Path.GetTempPath());
      IGpEnumList rasters = gp.ListRasters("*", "All");
      Delete del = new Delete();
      string sRaster = rasters.Next();
      del.data_type = "RasterDataset";
      while (sRaster != "")
      {
        del.in_data = System.IO.Path.GetTempPath() + "\\" + sRaster;
        gp.Execute(del, null);
        sRaster = rasters.Next();
      }

      try
      {
        IGpEnumList ws = gp.ListWorkspaces("*", "FileGDB");
        string sRutaWs = ws.Next();
        del.data_type = "";
        while (sRutaWs != "")
        {
          del.in_data = sRutaWs;
          gp.Execute(del, null);
          sRutaWs = ws.Next();
        } MessageBox.Show("Archivos Temporales Borrados");
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }

    }

    /// <summary>
    /// Busca una Capa Raster
    /// </summary>
    /// <returns></returns>
    private string BuscarRaster(string titulo)
    {
      IGxDialog pGxDialog = new GxDialogClass();
      pGxDialog.AllowMultiSelect = false;
      IGxObjectFilter pGxFilter = (IGxObjectFilter)new GxFilterRasterDatasetsClass();
      pGxDialog.ObjectFilter = pGxFilter;
      pGxDialog.Title = titulo;
      IEnumGxObject pEnumGxObj;
      if (pGxDialog.DoModalOpen(0, out pEnumGxObj))
      {
        return pEnumGxObj.Next().FullName;
      }
      return "";
    }

    /// <summary>
    /// Asigna la simbologia establecida a la capa de probabilidades
    /// </summary>
    /// <param name="pRLayer">Objeto RasterLayer a Simbolizar</param>
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

      pRUVRenderer.set_ClassCount(0, 6);
      AsignarColorAClase(0, "", 255, 255, 255, pRUVRenderer);
      AsignarColorAClase(1, "Muy Baja", 0, 255, 0, pRUVRenderer);
      AsignarColorAClase(2, "Baja", 255, 240, 70, pRUVRenderer);
      AsignarColorAClase(3, "Moderada", 255, 220, 100, pRUVRenderer);
      AsignarColorAClase(4, "Alta", 255, 100, 20, pRUVRenderer);
      AsignarColorAClase(5, "Muy Alta", 255, 0, 0, pRUVRenderer);

      pRUVRenderer.UseDefaultSymbol = false;
      pRRenderer.Update();
      pRLayer.Renderer = pRRenderer;

    }

    /// <summary>
    /// Asigna Colores a una case
    /// </summary>
    /// <param name="iClase">Clase</param>
    /// <param name="sEtiqueta">Etiqueta</param>
    /// <param name="red">Rojo</param>
    /// <param name="green">Verde</param>
    /// <param name="blue">Azul</param>
    /// <param name="pRUVRenderer">Raster Unique Value Renderer</param>
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

    #endregion

              
  }
}
