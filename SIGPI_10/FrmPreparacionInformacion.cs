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

namespace SIGPI_10
{
  /// <summary>
  /// 
  /// </summary>
  public partial class FrmPreparacionInformacion : Form
  {
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
      xmlDoc.Load(sPath + "\\" + "parametros.xml");
      xmlNodeList = xmlDoc.SelectNodes("/SIGPI/RUTA_LECTURAS");
      string sRutaLectura = xmlNodeList.Item(0).InnerText;
      string sRutaDB = xmlDoc.SelectNodes("/SIGPI/RUTA_BD").Item(0).InnerText;

      if (PrepararInformacion.ExistSIGPIFiles(sRutaLectura + "temperatura.xls") == false)
        return;

      if (PrepararInformacion.ExistSIGPIFiles(sRutaLectura + "precipitacion.xls") == false)
        return;

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
      string sRuta = parametros.RutaSIGPI + "\\" + parametros.Lecturas + "\\" + "temperaturamax.xls";
      if (!System.IO.File.Exists(sRuta))
      {
        MessageBox.Show("El Archivo '" + sRuta + "' Verifique la ruta");
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
        mesTemperatura = preparar.VerificarFechasLecturas(sheet, "E2");
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

      int iTotalEstaciones = preparar.TotalEstaciones(sheet, "D");
      string sColumnaDia = preparar.ColumnaLecturaDia(sheet, 8, iDay.ToString(), "3");
      if (sColumnaDia == "-99")
      {
        MessageBox.Show("No se encontro el dia de lectura en el archivo de Excel de temperaturas. Dia: " + iDay.ToString());
        _excelApp.Workbooks.Close();
        pProDia.HideDialog();
        return;
      }

      List<Lectura> listaTemperatura = preparar.ObtenerLecturas(sheet, _sigpiDao, "D", sColumnaDia, -20, 50);
      //pStepPro.Step();
      DateTime dFechaIncorporacionActual = dtPickerFechaAIncorporar.Value; //_sigpi.FechaIncorporacion.AddDays(1);
      //string sFechaIncorporacionActual = dFechaIncorporacionActual.ToString("dd/MM/yyyy");

      bool bIncorporarTemperatura = preparar.IncorporarLecturas(_sigpiDao, "LECTUS_TEMPE", dFechaIncorporacionActual, listaTemperatura);

      pStepPro.Message = "Incorporando Precipitacion...";
      pStepPro.Step();
      pStepPro.StepValue = 3;

      sRuta = parametros.RutaSIGPI + "\\" + parametros.Lecturas + "\\" + "precipitacion.xls";

      _excelApp.Workbooks.Close();

      _excelApp = new Microsoft.Office.Interop.Excel.Application();
      workBook = _excelApp.Workbooks.Open(sRuta, Type.Missing, Type.Missing, Type.Missing,
                          Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                          Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

      sheet = (Worksheet)workBook.Sheets[1];

      try
      {
        mesPrecipitacion = preparar.VerificarFechasLecturas(sheet, "D1");
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

      List<Lectura> listaPrecipitacion = preparar.ObtenerLecturas(sheet, _sigpiDao, "D", sColumnaDia, 0, 500);
      bool bIncorporarPrecip = preparar.IncorporarLecturas(_sigpiDao, "LECTUS_PRECI", dFechaIncorporacionActual, listaPrecipitacion);

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
      string sRuta = parametros.RutaSIGPI + "\\" + parametros.Lecturas + "\\" + "temperatura.xls";
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

        sheet = (Worksheet)workBook.Sheets["Hoja1"];
        mesTemperatura = "";
        try
        {
          mesTemperatura = preparar.VerificarFechasLecturas(sheet, "D1");
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

        int iTotalEstaciones = preparar.TotalEstaciones(sheet, "A");
        sColumnaDia = preparar.ColumnaLecturaDia(sheet, 5, iDay.ToString(), "2");
        if (sColumnaDia == "-99")
        {
          MessageBox.Show("No se encontro el dia de lectura en el archivo de Excel de temperaturas. Dia: " + iDay.ToString());
          pProDia.HideDialog();
          _excelApp.Workbooks.Close();
          return;
        }

        List<Lectura> listaTemperatura = preparar.ObtenerLecturas(sheet, _sigpiDao, "A", sColumnaDia, -20, 50);
        pStepPro.Step();
        dFechaIncorporacionActual = dtPickerFechaAIncorporar.Value; //_sigpi.FechaIncorporacion.AddDays(1);
        //MessageBox.Show("lecturas a incorporar Temp. : " + listaTemperatura.Count.ToString());
        bIncorporarTemperatura = preparar.IncorporarLecturas(_sigpiDao, "LECTUS_TEMPE", dFechaIncorporacionActual, listaTemperatura);

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
      
      sRuta = parametros.RutaSIGPI + "\\" + parametros.Lecturas + "\\" + "precipitacion.xls";


      try
      {
        _excelApp = new Microsoft.Office.Interop.Excel.Application();
        workBook = _excelApp.Workbooks.Open(sRuta, Type.Missing, Type.Missing, Type.Missing,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

        sheet = (Worksheet)workBook.Sheets["Hoja1"];

        try
        {
          mesPrecipitacion = preparar.VerificarFechasLecturas(sheet, "A1");
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message);
          pProDia.HideDialog();
          _excelApp.Workbooks.Close();
          return;
        }

        sColumnaDia = preparar.ColumnaLecturaDia(sheet, 5, iDay.ToString(), "2");
        if (sColumnaDia == "-99")
        {
          MessageBox.Show("No se encontro el dia de lectura en el archivo de Excel de precipitacion. Dia: " + iDay.ToString());
          pProDia.HideDialog();
          _excelApp.Workbooks.Close();
          return;
        }

        List<Lectura> listaPrecipitacion = preparar.ObtenerLecturas(sheet, _sigpiDao, "A", sColumnaDia, 0, 500);
        //MessageBox.Show("lecturas a incorporar Prec. : " + listaPrecipitacion.Count.ToString());
        bool bIncorporarPrecip = preparar.IncorporarLecturas(_sigpiDao, "LECTUS_PRECI", dFechaIncorporacionActual, listaPrecipitacion);

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
        procesarLecturas.CalcularResultadosPrecipitacion(_sigpiDao, "DEFI_PRECI", "LECTUS_PRECI", dtPickerFechaAProcesar.Value, 5);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error procesando las lecturas:\n" + ex.Message);
      }

      pStepPro.Step();
      pStepPro.Message = "Procesando Temperatura...";
      try
      {
        procesarLecturas.CalcularResultadosTemperatura(_sigpiDao, "DEFI_TEMPE", "LECTUS_TEMPE", dtPickerFechaAProcesar.Value, "T5", 5);
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
