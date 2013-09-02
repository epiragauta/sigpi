using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace SIGPI_10
{
  /// <summary>
  /// Procesar Lecturas
  /// </summary>
  public class ProcesarLecturas
  {
    /// <summary>
    /// Calcular Resultados Precipitacion
    /// </summary>
    /// <param name="SigpiDao"></param>
    /// <param name="sTablaConsolidados"></param>
    /// <param name="sTablaLecturas"></param>
    /// <param name="FechaCalculos"></param>
    /// <returns></returns>
    public bool CalcularResultadosPrecipitacion(SIGPIDao SigpiDao, string sTablaConsolidados,
                                                string sTablaLecturas, DateTime FechaCalculos,
                                                int numDias)
    {
      string sSQL = "DELETE FROM " + sTablaConsolidados;
      try
      {
        SigpiDao.EjecutarSentenciaSinQuery(sSQL);
      }
      catch (Exception e)
      {
        // throw new Exception(e.Message + sSQL);
      }
      DateTime FechaCalculosInicial = FechaCalculos.AddDays(-1 * (numDias - 1));

      //sSQL = "SELECT CODIGO,LECTURA,FECHA From " + sTablaLecturas +
      //        " WHERE FECHA <=#" + FechaCalculos.ToString("MM/dd/yyyy") + "#" +
      //        " AND FECHA >=#" + FechaCalculosInicial.ToString("MM/dd/yyyy") + "#" +
      //        " ORDER BY CODIGO,FECHA";

      sSQL = "SELECT CODIGO,LECTURA,FECHA From " + sTablaLecturas +
              " WHERE FECHA <= ? AND FECHA >= ? ORDER BY CODIGO,FECHA";

      OleDbCommand command = SigpiDao.LocalDBConnection.CreateCommand();
      command.CommandText = sSQL;
      OleDbParameter paramFechaCalculos = command.CreateParameter();
      OleDbParameter paramFechaCalculosInicial = command.CreateParameter();
      command.Parameters.Add(paramFechaCalculos);
      command.Parameters.Add(paramFechaCalculosInicial);
      paramFechaCalculos.Value = FechaCalculos;
      paramFechaCalculosInicial.Value = FechaCalculosInicial;
      OleDbDataReader dataReader = null;
      try
      {
        dataReader = command.ExecuteReader();
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message + " :: " + sSQL);
      }

      Lectura lectura;
      List<Lectura> lecturas = new List<Lectura>();
      int dCodigo;
      int i = 0;
      PrecipitacionProcesada precipitacionProcesada = new PrecipitacionProcesada();
      double dAcumuladoPrecipitacion;
      int iNumeroDiasSinLluvia, iNumeroLecturasConsecutivas, iAmenazaXDiaSinLluvia;

      dAcumuladoPrecipitacion = 0;
      iNumeroDiasSinLluvia = 0;
      dCodigo = -1;

      OleDbCommand command2 = SigpiDao.LocalDBConnection.CreateCommand();
      sSQL = "INSERT INTO " + sTablaConsolidados + " VALUES (?,?,?,?,?,?,?)";
      command2.CommandText = sSQL;
      OleDbParameter paramCodigo = command2.CreateParameter();
      OleDbParameter paramP5 = command2.CreateParameter();
      OleDbParameter paramDSLL5 = command2.CreateParameter();
      OleDbParameter paramDSLLC5 = command2.CreateParameter();
      OleDbParameter paramFecha = command2.CreateParameter();
      OleDbParameter paramNoDias = command2.CreateParameter();
      OleDbParameter paramAmenazaXDSL = command2.CreateParameter();
      command2.Parameters.Add(paramCodigo);
      command2.Parameters.Add(paramP5);
      command2.Parameters.Add(paramDSLL5);
      command2.Parameters.Add(paramDSLLC5);
      command2.Parameters.Add(paramFecha);
      command2.Parameters.Add(paramNoDias);
      command2.Parameters.Add(paramAmenazaXDSL);

      paramFecha.Value = FechaCalculos;


      while (dataReader.Read())
      {
        lectura = new Lectura();
        if (i == 0)
        {
          dCodigo = dataReader.GetInt32(0);
        }
        else
        {
          if (dCodigo != dataReader.GetInt32(0))
          {
            foreach (Lectura lect in lecturas)
            {
              if (lect.Valor <= 0.5)
              {
                iNumeroDiasSinLluvia++;
              }
              //if (lect.Valor < 2)
              //{
              //  iAmenazaXDiaSinLluvia = 5;
              //}

            }
            iNumeroLecturasConsecutivas = LecturasConsecutivas(lecturas);
            iAmenazaXDiaSinLluvia = AmenazasXLluvia(lecturas);
            precipitacionProcesada.Codigo = System.Convert.ToInt32(dCodigo);
            precipitacionProcesada.P5 = dAcumuladoPrecipitacion;
            precipitacionProcesada.DSLL5 = iNumeroDiasSinLluvia;
            precipitacionProcesada.DSLLC5 = iNumeroLecturasConsecutivas;
            precipitacionProcesada.FEC_UL_LECTU = lecturas[0].Fecha;

            paramCodigo.Value = precipitacionProcesada.Codigo;
            paramP5.Value = dAcumuladoPrecipitacion;
            paramDSLL5.Value = precipitacionProcesada.DSLL5;
            paramDSLLC5.Value = precipitacionProcesada.DSLLC5;
            paramNoDias.Value = numDias;
            paramAmenazaXDSL.Value = iAmenazaXDiaSinLluvia;
            try
            {
              command2.ExecuteNonQuery();
            }
            catch (Exception e)
            {
              throw new Exception(e.Message + " :: " + command2.CommandText);
            }
            dCodigo = dataReader.GetInt32(0);
            dAcumuladoPrecipitacion = 0;
            iNumeroLecturasConsecutivas = 0;
            iNumeroDiasSinLluvia = 0;
            lecturas = new List<Lectura>();
            i = 0;
          }
        }
        lectura = new Lectura();
        lectura.Codigo = System.Convert.ToInt32(dataReader.GetInt32(0));
        lectura.Valor = dataReader.GetDouble(1);
        dAcumuladoPrecipitacion += lectura.Valor;
        lectura.Fecha = dataReader.GetDateTime(2);
        lecturas.Add(lectura);
        i++;
      }
      return true;
    }

    /// <summary>
    /// Calcula promedio de temperatura
    /// </summary>
    /// <param name="SigpiDao"></param>
    /// <param name="sTablaConsolidados"></param>
    /// <param name="sTablaLecturas"></param>
    /// <param name="FechaCalculos"></param>
    /// <param name="sCampoPromedioTemp"></param>
    /// <param name="NumDias"></param>
    /// <returns></returns>
    public bool CalcularResultadosTemperatura(SIGPIDao SigpiDao, string sTablaConsolidados,
                                                string sTablaLecturas, DateTime FechaCalculos,
                                                string sCampoPromedioTemp, int NumDias)
    {
      string sSQL = "DROP TABLE " + sTablaConsolidados;
      try
      {
        SigpiDao.EjecutarSentenciaSinQuery(sSQL);
      }
      catch (Exception)
      {
        //throw new Exception(e.Message);
      }
      DateTime FechaCalculosInicial = FechaCalculos.AddDays(-1 * (NumDias - 1));

      //sSQL = "SELECT CODIGO, Avg(LECTURA) AS T5, Max(LECTUS_TEMPE.FECHA) AS FECHA INTO " + 
      //        sTablaConsolidados + " From " + sTablaLecturas +
      //        " WHERE FECHA <=#" + FechaCalculos.ToString("MM/dd/yyyy") + "#" +
      //        " AND FECHA >=#" + FechaCalculosInicial.ToString("MM/dd/yyyy") + "#" +
      //        " GROUP BY CODIGO";

      sSQL = "SELECT CODIGO, Avg(LECTURA) AS " + sCampoPromedioTemp + ", Max(LECTUS_TEMPE.FECHA) AS FECHA, " + NumDias.ToString() + " AS NO_DIAS INTO " +
                sTablaConsolidados + " From " + sTablaLecturas +
                " WHERE FECHA <= ?" +
                " AND FECHA >= ?" +
                " GROUP BY CODIGO";


      OleDbCommand command = SigpiDao.LocalDBConnection.CreateCommand();
      command.CommandText = sSQL;
      OleDbParameter paramFechaCalculos = command.CreateParameter();
      OleDbParameter paramFechaCalculosInicial = command.CreateParameter();
      paramFechaCalculos.Value = FechaCalculos;
      paramFechaCalculosInicial.Value = FechaCalculosInicial;
      command.Parameters.Add(paramFechaCalculos);
      command.Parameters.Add(paramFechaCalculosInicial);

      try
      {
        command.ExecuteNonQuery();
      }
      catch (Exception e)
      {

        throw new Exception(e.Message);
      }

      return true;

    }

    private int LecturasConsecutivas(List<Lectura> lecturas)
    {
      DateTime fechaBase = lecturas[0].Fecha;
      int iNumLecturasConsecutivas = 1;
      if (lecturas.Count > 1)
      {
        if (lecturas[1].Fecha.Day - lecturas[0].Fecha.Day == 1)
        {
          iNumLecturasConsecutivas++;
        }
      }
      for (int i = 2; i < lecturas.Count; i++)
      {
        if (lecturas[i].Fecha.Day - lecturas[i - 1].Fecha.Day == 1)
        {
          iNumLecturasConsecutivas++;
        }
      }
      return iNumLecturasConsecutivas;
    }

    private int AmenazasXLluvia(List<Lectura> lecturas)
    {
      DateTime fechaBase = lecturas[0].Fecha;
      int iAmenazasXLluvia = -1;
      int iNumDiasConsecutivos = -1;
      //if (lecturas.Count > 1)
      //{
      //  if (lecturas[1].Fecha.Day - lecturas[0].Fecha.Day == 1)
      //  {
      //    iNumLecturasConsecutivas++;
      //  }
      //}
      for (int i = 0; i < lecturas.Count; i++)
      {
        if (iAmenazasXLluvia == -1)
        {
          iNumDiasConsecutivos++;
          if (lecturas[i].Valor < 2)
          {
            iAmenazasXLluvia = 5;
          }
          else if (lecturas[i].Valor < 8 & lecturas[i].Valor > 2)
          {
            iAmenazasXLluvia = 4;
          }
          else if (lecturas[i].Valor < 14 & lecturas[i].Valor > 8)
          {
            iAmenazasXLluvia = 3;
          }
          else if (lecturas[i].Valor < 24 & lecturas[i].Valor > 14)
          {
            iAmenazasXLluvia = 2;
          }
          else if (lecturas[i].Valor > 24)
          {
            iAmenazasXLluvia = 1;
          }
          else
          {
            iAmenazasXLluvia = 0;
          }
        }
        else
        {
          if (lecturas[i].Valor < 2)
          {
            if (iAmenazasXLluvia == 5)
            {
              iAmenazasXLluvia = 5;
            }

            if (iAmenazasXLluvia == 4 & iNumDiasConsecutivos <= 3)
            {
              iAmenazasXLluvia = 4;
            }
            else if (iAmenazasXLluvia == 4 & iNumDiasConsecutivos > 3)
            {
              iAmenazasXLluvia = 5;
            }

            if (iAmenazasXLluvia == 3 & iNumDiasConsecutivos <= 3)
            {
              iAmenazasXLluvia = 3;
            }
            else if (iAmenazasXLluvia == 3 & iNumDiasConsecutivos > 3)
            {
              iAmenazasXLluvia = 5;
            }

          }
          else if (lecturas[i].Valor < 8 & lecturas[i].Valor > 2)
          {
            iAmenazasXLluvia = 4;
          }
          else if (lecturas[i].Valor < 14 & lecturas[i].Valor > 8)
          {
            iAmenazasXLluvia = 3;
          }
          else if (lecturas[i].Valor < 24 & lecturas[i].Valor > 14)
          {
            iAmenazasXLluvia = 2;
          }
          else if (lecturas[i].Valor > 24)
          {
            iAmenazasXLluvia = 1;
          }
          else
          {
            iAmenazasXLluvia = 0;
          }
          iNumDiasConsecutivos++;
        }
      }

      return iNumDiasConsecutivos;
    }
  }
}
