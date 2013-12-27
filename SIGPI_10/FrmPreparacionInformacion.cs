using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.esriSystem;
using Microsoft.Office.Interop.Excel;
using System.Data.OleDb;

namespace SIGPI_10
{
  /// <summary>
  /// 
  /// </summary>
  public partial class FrmPreparacionInformacion : Form
  {

    private static String LECTURAS_TEMPERATURA = "temperatura.xls";
    private static String LECTURAS_PRECIPITACION = "precipitacion.xls";
    private static String DIR_PARAMETROS = "parameters";
    private static String ARCHIVO_PARAMETROS = "parameteros.xml";
    private static String ARCHIVO_NO_ENCONTRADO = "No se encuentra en archivo";
    private static String SIGPI_TITULO = "SIGPI";
    private static String SIGPI_CELDA_MES_TEMPERATURA = "D1";
    private static String SIGPI_CELDA_MES_PRECIPITACION = "A1";
    private static String SIGPI_COLUMNA_ESTACIONES = "D";
    private static String SIGPI_COLUMNA_CODIGO_TEMPERATURA = "A";
    private static String SIGPI_COLUMNA_CODIGO_PRECIPITACION = "A";
    private static String SIGPI_TABLA_LECTURAS_TEMPERATURA = "LECTUS_TEMPE";
    private static String SIGPI_TABLA_LECTURAS_PRECIPITACION = "LECTUS_PRECI";
    private static Double SIGPI_TEMPERATURA_MIN_VAL = -20;
    private static Double SIGPI_TEMPERATURA_MAX_VAL = 50;
    private static Double SIGPI_PRECIPITACION_MIN_VAL = 0;
    private static Double SIGPI_PRECIPITACION_MAX_VAL = 500;
    private static Int32 SIGPI_COLUMNA_INICIO_DIA_TEMPERATURA = 5;
    private static String SIGPI_FILA_DIA_TEMPERATURA = "2";
    private static Int32 SIGPI_COLUMNA_INICIO_DIA_PRECIPITACION = 6;
    private static String SIGPI_FILA_DIA_PRECIPITACION = "2";



    private SIGPIParametros _parametros;
    private SIGPICls _sigpi;
    private SIGPIDao _sigpiDao;

    public FrmPreparacionInformacion()
    {
      InitializeComponent();
    }

    public FrmPreparacionInformacion(SIGPIParametros parametros, SIGPICls sigpi, SIGPIDao sigpiDao)
    {
      _parametros = parametros;
      _sigpi = sigpi;
      _sigpiDao = sigpiDao;
      InitializeComponent();
    }

    private void FrmPreparacionInformacion_Load(object sender, EventArgs e)
    {

      if (_sigpi != null)
      {
        dtPickerFechaAIncorporar.Value = _sigpi.FechaIncorporacion;
        txtUltimoDiaIncorporacion.Text = dtPickerFechaAIncorporar.Text;
        dtPickerFechaAIncorporar.Value = _sigpi.FechaIncorporacion.AddDays(1);
        dtPickerFechaAProcesar.Value = _sigpi.FechaProcesamiento;
        txtUltimaFechaProceso.Text = dtPickerFechaAProcesar.Text;
      }
    }

    private void readXMLParameters()
    {
      //string sCurrentDir = System.Reflection.Assembly.GetExecutingAssembly().Location;
      string sPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

      //MessageBox.Show(sPath);
      //string sPath = sCurrentDir;

      XmlDocument xmlDoc = new XmlDocument();
      XmlNodeList xmlNodeList;
      xmlDoc.Load(sPath + "\\" + DIR_PARAMETROS + "\\" + ARCHIVO_PARAMETROS);
      xmlNodeList = xmlDoc.SelectNodes("/SIGPI/RUTA_LECTURAS");
      string sRutaLectura = xmlNodeList.Item(0).InnerText;
      string sRutaDB = xmlDoc.SelectNodes("/SIGPI/RUTA_BD").Item(0).InnerText;

      if (!PrepararInformacion.ExistSIGPIFiles(sRutaLectura + LECTURAS_TEMPERATURA))
      {
        MessageBox.Show(String.Format(ARCHIVO_NO_ENCONTRADO + ": {0}", sRutaLectura +  LECTURAS_TEMPERATURA), SIGPI_TITULO);
        return;
      }


      if (!PrepararInformacion.ExistSIGPIFiles(sRutaLectura + LECTURAS_PRECIPITACION))
      {
        MessageBox.Show(String.Format(ARCHIVO_NO_ENCONTRADO + ": {0}", sRutaLectura + LECTURAS_PRECIPITACION), SIGPI_TITULO);
        return;
      }
        

      //MessageBox.Show(sRutaDB);
    }

    /// <summary>
    /// Incorpora lecturas desde los archivos descargados desde la intranet
    /// </summary>
    private void IncorporarLecturas2()
    {
      statusLblProcesando.Text = "Incorporando Lecturas...";

      IProgressDialogFactory pProDiaFac = new ProgressDialogFactoryClass();
      IStepProgressor pStepPro = pProDiaFac.Create(null, 0);
      pStepPro.MinRange = 1;
      pStepPro.MaxRange = 5;
      pStepPro.StepValue = 1;
      IProgressDialog2 pProDia = (IProgressDialog2)pStepPro;
      pProDia.Animation = esriProgressAnimationTypes.esriProgressGlobe;

      pProDia.Title = "Incorporando Lecturas";
      pProDia.ShowDialog();
      pStepPro.Step();
      pStepPro.Message = "Incorporando temperatura...";

      SIGPIParametros parametros = _parametros;

      PrepararInformacion preparar = new PrepararInformacion();
      string mesTemperatura, mesPrecipitacion;
      string sRuta = parametros.RutaSIGPI + "\\" + parametros.Lecturas + "\\" + LECTURAS_TEMPERATURA;
      if (!System.IO.File.Exists(sRuta))
      {
        MessageBox.Show("No existe el Archivo '" + sRuta + "' Verifique la ruta");
        return;
      }
      Microsoft.Office.Interop.Excel.Application _excelApp = new Microsoft.Office.Interop.Excel.Application();
      Workbook workBook = _excelApp.Workbooks.Open(sRuta, Type.Missing, Type.Missing, Type.Missing,
                          Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                          Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

      Worksheet sheet;
      try
      {
        sheet = (Worksheet)workBook.Sheets[1];

      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
        _excelApp.Workbooks.Close();
        pProDia.HideDialog();
        return;
      }
      mesTemperatura = "";
      try
      {
        mesTemperatura = preparar.VerificarFechasLecturas(sheet, SIGPI_CELDA_MES_TEMPERATURA);
      }
      catch (Exception ex)
      {
        _excelApp.Workbooks.Close();
        pProDia.HideDialog();
        MessageBox.Show(ex.Message);
        return;
      }
      DateTime dFechaAIncorporar = dtPickerFechaAIncorporar.Value;
      int iDay = dFechaAIncorporar.Day;
      if (dFechaAIncorporar.Month != ConversionMes.MesNumero(mesTemperatura.ToUpper()))
      {
        if (dFechaAIncorporar.Month != ConversionMes.MesNumero(mesTemperatura.ToUpper()) + 1)
        {
          MessageBox.Show("La fecha seleccionada no se encuentra en el archivo de lecturas. Periodo correspondiente al mes de: " + mesTemperatura);
          _excelApp.Workbooks.Close();
          pProDia.HideDialog();
          return;
        }
      }

      int iTotalEstaciones = preparar.TotalEstaciones(sheet, SIGPI_COLUMNA_ESTACIONES);
      string sColumnaDia = preparar.ColumnaLecturaDia(sheet, 8, iDay.ToString(), "3");
      if (sColumnaDia == "-99")
      {
        MessageBox.Show("No se encontro el dia de lectura en el archivo de Excel de temperaturas. Dia: " + iDay.ToString());
        _excelApp.Workbooks.Close();
        pProDia.HideDialog();
        return;
      }

      List<Lectura> listaTemperatura = preparar.ObtenerLecturas(sheet, _sigpiDao, SIGPI_COLUMNA_CODIGO_TEMPERATURA, sColumnaDia, 
                                                              SIGPI_TEMPERATURA_MIN_VAL, SIGPI_TEMPERATURA_MAX_VAL);
      //pStepPro.Step();
      DateTime dFechaIncorporacionActual = dtPickerFechaAIncorporar.Value; //_sigpi.FechaIncorporacion.AddDays(1);
      //string sFechaIncorporacionActual = dFechaIncorporacionActual.ToString("dd/MM/yyyy");

      bool bIncorporarTemperatura = preparar.IncorporarLecturas(_sigpiDao, SIGPI_TABLA_LECTURAS_TEMPERATURA, dFechaIncorporacionActual, listaTemperatura);

      pStepPro.Message = "Incorporando Precipitacion...";
      pStepPro.Step();
      pStepPro.StepValue = 3;

      sRuta = parametros.RutaSIGPI + "\\" + parametros.Lecturas + "\\" + LECTURAS_PRECIPITACION;

      _excelApp.Workbooks.Close();

      _excelApp = new Microsoft.Office.Interop.Excel.Application();
      workBook = _excelApp.Workbooks.Open(sRuta, Type.Missing, Type.Missing, Type.Missing,
                          Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                          Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

      sheet = (Worksheet)workBook.Sheets[1];

      try
      {
        mesPrecipitacion = preparar.VerificarFechasLecturas(sheet, SIGPI_CELDA_MES_PRECIPITACION);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
        _excelApp.Workbooks.Close();
        pProDia.HideDialog();
        return;
      }

      sColumnaDia = preparar.ColumnaLecturaDia(sheet, 8, iDay.ToString(), "2");
      if (sColumnaDia == "-99")
      {
        MessageBox.Show("No se encontro el dia de lectura en el archivo de Excel de precipitacion. Dia: " + iDay.ToString());
        _excelApp.Workbooks.Close();
        pProDia.HideDialog();
        return;
      }

      List<Lectura> listaPrecipitacion = preparar.ObtenerLecturas(sheet, _sigpiDao, SIGPI_COLUMNA_CODIGO_PRECIPITACION, sColumnaDia,
                                                                  SIGPI_PRECIPITACION_MIN_VAL, SIGPI_PRECIPITACION_MAX_VAL);

      bool bIncorporarPrecip = preparar.IncorporarLecturas(_sigpiDao, SIGPI_TABLA_LECTURAS_PRECIPITACION, dFechaIncorporacionActual, listaPrecipitacion);

      pStepPro.StepValue = 4;
      pProDia.HideDialog();

      if (bIncorporarTemperatura && bIncorporarPrecip)
      {
        txtUltimoDiaIncorporacion.Text = dtPickerFechaAIncorporar.Value.ToLongDateString();
        preparar.ActualizarFechaIncorporacion(dtPickerFechaAIncorporar.Value, _sigpiDao);
        _sigpi.FechaIncorporacion = dtPickerFechaAIncorporar.Value;
        dtPickerFechaAProcesar.Value = dtPickerFechaAIncorporar.Value;
        dtPickerFechaAIncorporar.Value = dtPickerFechaAIncorporar.Value.AddDays(1);
        MessageBox.Show("Incorporacion de lecturas terminada!");

      }
      _excelApp.Workbooks.Close();
    }

    private void btnIncorporar_Click_1(object sender, EventArgs e)
    {

      //IncorporarLecturas2();
      //return;

      statusLblProcesando.Text = "Incorporando Lecturas...";

      IProgressDialogFactory pProDiaFac = new ProgressDialogFactoryClass();
      IStepProgressor pStepPro = pProDiaFac.Create(null, 0);
      pStepPro.MinRange = 1;
      pStepPro.MaxRange = 5;
      pStepPro.StepValue = 1;
      IProgressDialog2 pProDia = (IProgressDialog2)pStepPro;
      pProDia.Animation = esriProgressAnimationTypes.esriProgressGlobe;

      pProDia.Title = "Incorporando Lecturas";
      pProDia.ShowDialog();
      pStepPro.Step();
      pStepPro.Message = "Incorporando temperatura...";

      SIGPIParametros parametros = _parametros;
      
      PrepararInformacion preparar = new PrepararInformacion();
      string mesTemperatura, mesPrecipitacion;
      string sRuta = parametros.RutaSIGPI + "\\" + parametros.Lecturas + "\\" + LECTURAS_TEMPERATURA;
      Microsoft.Office.Interop.Excel.Application _excelApp = null;
      Workbook workBook = null;
      Worksheet sheet = null;
      string sColumnaDia;
      DateTime dFechaIncorporacionActual;
      int iDay;
      bool bIncorporarTemperatura;

      try
      {
        _excelApp = new Microsoft.Office.Interop.Excel.Application();
        workBook = _excelApp.Workbooks.Open(sRuta, Type.Missing, Type.Missing, Type.Missing,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

        sheet = (Worksheet)workBook.Worksheets.get_Item(1);    //.Sheets["max"];
        mesTemperatura = "";
        try
        {
          mesTemperatura = preparar.VerificarFechasLecturas(sheet, SIGPI_CELDA_MES_TEMPERATURA);
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message);
          pProDia.HideDialog();
          _excelApp.Workbooks.Close();
          return;
        }
        DateTime dFechaAIncorporar = dtPickerFechaAIncorporar.Value;
        iDay = dFechaAIncorporar.Day;
        if (dFechaAIncorporar.Month != ConversionMes.MesNumero(mesTemperatura.ToUpper()))
        {
          if (dFechaAIncorporar.Month != ConversionMes.MesNumero(mesTemperatura.ToUpper()) + 1)
          {
            MessageBox.Show("La fecha seleccionada no se encuentra en el archivo de lecturas. Periodo correspondiente al mes de: " + mesTemperatura);
            pProDia.HideDialog();
            _excelApp.Workbooks.Close();
            return;
          }
        }

        int iTotalEstaciones = preparar.TotalEstaciones(sheet, SIGPI_COLUMNA_CODIGO_TEMPERATURA);
        sColumnaDia = preparar.ColumnaLecturaDia(sheet, SIGPI_COLUMNA_INICIO_DIA_TEMPERATURA, iDay.ToString(), SIGPI_FILA_DIA_TEMPERATURA);
        if (sColumnaDia == "-99")
        {
          MessageBox.Show("No se encontro el dia de lectura en el archivo de Excel de temperaturas. Dia: " + iDay.ToString());
          pProDia.HideDialog();
          _excelApp.Workbooks.Close();
          return;
        }

        List<Lectura> listaTemperatura = preparar.ObtenerLecturas(sheet, _sigpiDao, SIGPI_COLUMNA_CODIGO_TEMPERATURA,
                                                    sColumnaDia, SIGPI_TEMPERATURA_MIN_VAL, SIGPI_TEMPERATURA_MAX_VAL);
        pStepPro.Step();
        dFechaIncorporacionActual = dtPickerFechaAIncorporar.Value; //_sigpi.FechaIncorporacion.AddDays(1);
        //MessageBox.Show("lecturas a incorporar Temp. : " + listaTemperatura.Count.ToString());
        bIncorporarTemperatura = preparar.IncorporarLecturas(_sigpiDao, SIGPI_TABLA_LECTURAS_TEMPERATURA, dFechaIncorporacionActual, listaTemperatura);

        pStepPro.Message = "Incorporando Precipitacion...";
        pStepPro.Step();
        pStepPro.StepValue = 3;
                
      }
      finally 
      {
        _excelApp.Workbooks.Close();
        GC.Collect();
        GC.WaitForPendingFinalizers();

        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(workBook);
        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(_excelApp);

      }
      
      sRuta = parametros.RutaSIGPI + "\\" + parametros.Lecturas + "\\" + LECTURAS_PRECIPITACION;


      try
      {
        _excelApp = new Microsoft.Office.Interop.Excel.Application();
        workBook = _excelApp.Workbooks.Open(sRuta, Type.Missing, Type.Missing, Type.Missing,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

        sheet = (Worksheet)workBook.Worksheets.get_Item(1);   //Sheets["precipitacion"];

        try
        {
          mesPrecipitacion = preparar.VerificarFechasLecturas(sheet, SIGPI_CELDA_MES_PRECIPITACION);
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message);
          pProDia.HideDialog();
          _excelApp.Workbooks.Close();
          return;
        }

        sColumnaDia = preparar.ColumnaLecturaDia(sheet, SIGPI_COLUMNA_INICIO_DIA_PRECIPITACION, iDay.ToString(), SIGPI_FILA_DIA_PRECIPITACION);
        if (sColumnaDia == "-99")
        {
          MessageBox.Show("No se encontro el dia de lectura en el archivo de Excel de precipitacion. Dia: " + iDay.ToString());
          pProDia.HideDialog();
          _excelApp.Workbooks.Close();
          return;
        }

        List<Lectura> listaPrecipitacion = preparar.ObtenerLecturas(sheet, _sigpiDao, SIGPI_COLUMNA_CODIGO_PRECIPITACION,
                                                                sColumnaDia, SIGPI_PRECIPITACION_MIN_VAL, SIGPI_PRECIPITACION_MAX_VAL);
        //MessageBox.Show("lecturas a incorporar Prec. : " + listaPrecipitacion.Count.ToString());
        bool bIncorporarPrecip = preparar.IncorporarLecturas(_sigpiDao, SIGPI_TABLA_LECTURAS_PRECIPITACION, 
                                                            dFechaIncorporacionActual, listaPrecipitacion);

        pStepPro.StepValue = 4;
        pProDia.HideDialog();

        if (bIncorporarTemperatura && bIncorporarPrecip)
        {
          txtUltimoDiaIncorporacion.Text = dtPickerFechaAIncorporar.Value.ToLongDateString();
          preparar.ActualizarFechaIncorporacion(dtPickerFechaAIncorporar.Value, _sigpiDao);
          _sigpi.FechaIncorporacion = dtPickerFechaAIncorporar.Value;
          dtPickerFechaAProcesar.Value = dtPickerFechaAIncorporar.Value;
          dtPickerFechaAIncorporar.Value = dtPickerFechaAIncorporar.Value.AddDays(1);
          MessageBox.Show("Incorporacion de lecturas terminada!");
        }        
      }
      finally 
      {
        _excelApp.Workbooks.Close();
        
      }
      
    }

    private void btnProcesar_Click_1(object sender, EventArgs e)
    {
      statusLblProcesando.Text = "Procesando datos...";

      IProgressDialogFactory pProDiaFac = new ProgressDialogFactoryClass();
      IStepProgressor pStepPro = pProDiaFac.Create(null, 0);
      pStepPro.MinRange = 1;
      pStepPro.MaxRange = 5;
      pStepPro.StepValue = 1;
      IProgressDialog2 pProDia = (IProgressDialog2)pStepPro;
      pProDia.Animation = esriProgressAnimationTypes.esriProgressGlobe;

      pProDia.Title = "Incorporando Lecturas";
      pProDia.ShowDialog();
      pStepPro.Step();
      pStepPro.Message = "Procesando precipitacion...";


      ProcesarLecturas procesarLecturas = new ProcesarLecturas();
      try
      {
        procesarLecturas.CalcularResultadosPrecipitacion(_sigpiDao, "DEFI_PRECI", "LECTUS_PRECI", dtPickerFechaAProcesar.Value, 10);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error procesando las lecturas:\n" + ex.Message);
      }

      pStepPro.Step();
      pStepPro.Message = "Procesando Temperatura...";
      try
      {
        procesarLecturas.CalcularResultadosTemperatura(_sigpiDao, "DEFI_TEMPE", "LECTUS_TEMPE", dtPickerFechaAProcesar.Value, "T5", 10);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error procesando las lecturas:\n" + ex.Message);
      }

      string sSQL = "UPDATE FECHAS_PROCESO SET FEC_PROCE = #" + dtPickerFechaAProcesar.Value.ToString("MM/dd/yyyy") + "#";
      try
      {
        _sigpiDao.EjecutarSentenciaSinQuery(sSQL);
      }
      catch (Exception ex)
      {
        MessageBox.Show("No se pudo actualizar la fecha de incorporacion.\n" + ex.Message);
      }
      _sigpi.FechaProcesamiento = dtPickerFechaAProcesar.Value;
      txtUltimaFechaProceso.Text = dtPickerFechaAProcesar.Value.ToLongDateString();

      pProDia.HideDialog();
      MessageBox.Show("Datos Procesados");
    }

    private void btnCerrar_Click_1(object sender, EventArgs e)
    {
      this.Close();
    }


  }
}
