using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace SIGPI_10
{
  public class SIGPIDao
  {
    private OleDbConnection pConn;

    public void ConnectLocalDB(string path)
    {
      //Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\SIGPI\BASE_ACCESS\sigpi_bd.mdb;Persist Security Info=False
      string sConn = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Persist Security Info=False";
      pConn = new OleDbConnection(sConn);
      pConn.Open();

    }

    public OleDbConnection LocalDBConnection
    {
      get
      {
        return pConn;
      }
    }

    public OleDbDataReader UltimaFechaIncorporacion(SIGPICls sigpi)
    {
      OleDbDataReader dataReader = null;
      OleDbCommand command = null;
      string sSQL = "SELECT fec_inco,fec_proce FROM fechas_proceso";
      command = new OleDbCommand(sSQL, pConn);
      dataReader = command.ExecuteReader();
      while (dataReader.Read())
      {
        sigpi.FechaIncorporacion = dataReader.GetDateTime(0);
        sigpi.FechaProcesamiento = dataReader.GetDateTime(1);
      }
      return null;
    }

    public void EjecutarSentenciaSinQuery(string sSQL)
    {
      OleDbCommand command = new OleDbCommand(sSQL, pConn);
      try
      {
        command.ExecuteNonQuery();
      }
      catch (Exception e)
      {

        throw new Exception(e.Message);
      }
      //EjecutarSentenciaSinQuery = true;
    }

    public void EjecutarSentenciaSinQuery(OleDbCommand command)
    {
      
      try
      {
        command.ExecuteNonQuery();
      }
      catch (Exception e)
      {

        throw new Exception(e.Message);
      }
      //EjecutarSentenciaSinQuery = true;
    }

    public OleDbDataReader EjecutarSentenciaLectura(string sSQL)
    {
      OleDbCommand command = new OleDbCommand(sSQL, pConn);
      OleDbDataReader dataReader;
      try
      {
        dataReader = command.ExecuteReader();
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
      return dataReader;
    }
  }
}
