using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using Microsoft.Office.Interop.Excel;

namespace SIGPI_10
{
  /// <summary>
  /// Clase PrepararInformacion
  /// </summary>
  public class PrepararInformacion
  {
    private string _mesPrecipitacion, _mesTemperatura;

    public string MesPrecipitacion
    {
      get
      {
        return _mesPrecipitacion;
      }
      set
      {
        _mesPrecipitacion = value;
      }
    }

    public string MesTemperatura
    {
      get
      {
        return _mesTemperatura;
      }
      set
      {
        _mesTemperatura = value;
      }
    }

    private void ConnectDB()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sFile"></param>
    /// <returns></returns>
    public static bool ExistSIGPIFiles(string sFile)
    {
      if (System.IO.File.Exists(sFile))
        return true;
      else
        return false;

    }

    /// <summary>
    /// Incorporar Lecturas
    /// </summary>
    /// <param name="sigpiDao"></param>
    /// <param name="Tabla"></param>
    /// <param name="FechaIncorporacion"></param>
    /// <param name="Lecturas"></param>
    /// <returns></returns>
    public bool IncorporarLecturas(SIGPIDao sigpiDao, string Tabla, DateTime FechaIncorporacion, List<Lectura> Lecturas)
    {
      string sSqlDelete = "DELETE FROM " + Tabla + " WHERE FECHA = ?";

      OleDbCommand command = new OleDbCommand(sSqlDelete, sigpiDao.LocalDBConnection);
      OleDbParameter paramFecha = command.CreateParameter();
      paramFecha.Value = FechaIncorporacion;
      command.Parameters.Add(paramFecha);
      try
      {
        command.ExecuteNonQuery();
      }
      catch (Exception e)
      {
        System.Windows.Forms.MessageBox.Show(e.Message);
        return false;
      }
      //command = sigpiDao.LocalDBConnection.CreateCommand();
      command.Parameters.Clear();
      command.CommandText = "INSERT INTO " + Tabla + " (codigo, lectura, fecha,x,y) VALUES (?,?,?,?,?)";
      OleDbParameter paramCodigo = command.CreateParameter();
      OleDbParameter paramValor = command.CreateParameter();
      OleDbParameter paramX = command.CreateParameter();
      OleDbParameter paramY = command.CreateParameter();


      command.Parameters.Add(paramCodigo);
      command.Parameters.Add(paramValor);
      command.Parameters.Add(paramFecha);
      command.Parameters.Add(paramX);
      command.Parameters.Add(paramY);

      foreach (Lectura lectura in Lecturas)
      {
        //command.CommandText = "INSERT INTO " + Tabla + " (codigo, lectura, fecha) VALUES (" + lectura.Codigo
        //                        + "," + lectura.Valor.ToString().Replace(',','.') + ",'" + FechaIncorporacion + "')";
        //System.Windows.Forms.MessageBox.Show(command.CommandText + "\n" + lectura.Codigo.ToString() + " :: " + lectura.Valor.ToString() + " :: " + FechaIncorporacion.ToShortDateString());
        paramCodigo.Value = lectura.Codigo;
        paramValor.Value = lectura.Valor;
        paramFecha.Value = FechaIncorporacion;
        paramX.Value = lectura.X;
        paramY.Value = lectura.Y;

        try
        {
          command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
          System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + command.CommandText + "\n" + lectura.Codigo.ToString() + " :: " + lectura.Valor.ToString() + " :: " + FechaIncorporacion.ToShortDateString(), "IncorporarLecturas");
          return false;
        }

      }

      return true;
    }

    public string VerificarFechasLecturas(Worksheet sheet, string Celda)
    {
      string valor = sheet.get_Range(Celda, Celda).Value2.ToString();
      string[] temp = valor.Split(' ');
      return temp[0];
    }

    public int TotalEstaciones(Worksheet sheet, string Columna)
    {
      int iLimite, iTotalEstaciones, i;
      i = 1;
      iLimite = 0;
      iTotalEstaciones = 0;
      string sCelda;
      while (iLimite < 100)
      {
        sCelda = Columna + i.ToString();
        if (isNumeric(sheet.get_Range(sCelda, sCelda).Value2))
        {
          iTotalEstaciones += 1;
          iLimite = 0;
        }
        else
        {
          iLimite += 1;
        }
        i += 1;
      }
      return iTotalEstaciones;
    }

    /// <summary>
    ///  Verifica si el objeto es un numero
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool isNumeric(object value)
    {
      if (value == null)
        return false;
      try
      {
        double d = System.Double.Parse(value.ToString(), System.Globalization.NumberStyles.Any);
        return true;
      }
      catch (System.FormatException)
      {
        return false;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sheet"></param>
    /// <param name="inicio"></param>
    /// <param name="dia"></param>
    /// <param name="fila"></param>
    /// <returns></returns>
    public string ColumnaLecturaDia(Worksheet sheet, int inicio, string dia, string fila)
    {
      // 26 corresponde al numero de celdas alfabeticas en excel

      int iNumCelda = inicio; // +System.Convert.ToInt32(dia);
      string letra1, letra2, sDiaFila;
      bool bExiste = false;

      letra1 = Convert.ToChar(64 + iNumCelda).ToString();
      letra2 = sheet.get_Range(letra1 + fila, letra1 + fila).Value2.ToString().Trim();

      if (letra2 == dia)
      {
        return letra1;
      }

      while (letra2 != dia)
      {
        iNumCelda++;
        if ((64 + iNumCelda) < 91) // Debe llegar hasta 'Z'. si es superior, se da otro tratamiento para 'AA'
        {
          letra1 = Convert.ToChar(64 + iNumCelda).ToString();
          letra2 = sheet.get_Range(letra1 + fila, letra1 + fila).Value2.ToString().Trim();
          if (dia == letra2)
            return letra1;
        }
        else
        {
          int iPosicion = 64 + iNumCelda;
          string letra0 = Convert.ToChar(64 + (iPosicion / 64)).ToString();
          letra1 = Convert.ToChar(64 + (iPosicion % 90)).ToString();
          letra2 = sheet.get_Range(letra0 + letra1 + fila, letra0 + letra1 + fila).Value2.ToString().Trim();
          if (dia == letra2)
            return letra0 + letra1;
        }
      }

      if (!bExiste)
      {
        return "-99";
      }

      // System.Diagnostics.Debug.Write(sheet.get_Range("E5", "E5").Value2.ToString());
      //return Convert.ToChar(64 + iNumCelda).ToString();




      return "-99";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sheet"></param>
    /// <param name="sigpiDao"></param>
    /// <param name="ColumnaCodigo"></param>
    /// <param name="ColumnaLectura"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public List<Lectura> ObtenerLecturas(Worksheet sheet, SIGPIDao sigpiDao,
                                        string ColumnaCodigo, string ColumnaLectura,
                                        double min, double max)
    {

      int iLimite, i;
      iLimite = 0;
      List<Lectura> lista = new List<Lectura>();

      Lectura lectura; // = new Lectura();
      object objCodigo, objLectura;
      i = 1;
      OleDbConnection conn = sigpiDao.LocalDBConnection;
      //OleDbCommand command = new OleDbCommand();
      //command.Connection = conn;
      int iNumRegistros;
      double dLectura;
      double dX, dY;

      OleDbDataReader reader = sigpiDao.EjecutarSentenciaLectura("SELECT codigo, longitud, latitud FROM ESTACIONES");
      Dictionary<Int32, EstacionIdeam> dictEstaciones = new Dictionary<Int32, EstacionIdeam>();

      while (reader.Read())
      {
        EstacionIdeam estacion = new EstacionIdeam(reader.GetInt32(0), reader.GetDouble(1), reader.GetDouble(2));
        if (!dictEstaciones.ContainsKey(estacion.Codigo)) { 
          dictEstaciones.Add(estacion.Codigo, estacion);
        }
      }

      while (iLimite < 100)
      {
        objCodigo = sheet.get_Range(ColumnaCodigo + i.ToString(), ColumnaCodigo + i.ToString()).Value2;
        if (isNumeric(objCodigo))
        {
          //command.CommandText = "select count(*) from estaciones where codigo = " + objCodigo.ToString();
          //iNumRegistros = (Int32)command.ExecuteScalar();
          //if (iNumRegistros != 0)
          //{
          objLectura = sheet.get_Range(ColumnaLectura + i.ToString(), ColumnaLectura + i.ToString()).Value2;
          if (isNumeric(objLectura))
          {
            dLectura = System.Double.Parse(objLectura.ToString(), System.Globalization.NumberStyles.Any);
            if (dLectura >= min && dLectura <= max)
            {
              lectura = new Lectura();
              lectura.Codigo = System.Convert.ToInt32(objCodigo);
              lectura.Valor = dLectura;
              if (!ColumnaCodigo.Equals("A"))
              {
                try
                {
                  lectura.X = Convert.ToDouble(sheet.get_Range("A" + i.ToString(), "A" + i.ToString()).Value2);
                  lectura.Y = Convert.ToDouble(sheet.get_Range("B" + i.ToString(), "B" + i.ToString()).Value2);
                }
                catch (Exception)
                {
                }
              }
              else
              {
                if (dictEstaciones.ContainsKey(lectura.Codigo))
                {
                  lectura.X = dictEstaciones[lectura.Codigo].Longitud;
                  lectura.Y = dictEstaciones[lectura.Codigo].Latitud;
                }
              }
              if (lectura.X != 0)
              {
                lista.Add(lectura);
              }
              
            }
          }
          //}
          iLimite = 0;
        }
        else
        {
          iLimite += 1;
        }
        i += 1;
      }
      return lista;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="FechaIncorporacion"></param>
    /// <param name="SigpiDao"></param>
    /// <returns></returns>
    public bool ActualizarFechaIncorporacion(DateTime FechaIncorporacion, SIGPIDao SigpiDao)
    {
      string sSQL = "UPDATE FECHAS_PROCESO SET FEC_INCO = #" + FechaIncorporacion.ToString("MM/dd/yyyy") + "#";
      OleDbCommand command = new OleDbCommand(sSQL, SigpiDao.LocalDBConnection);
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

  }
}
