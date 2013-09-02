using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGPI_10
{
  public class SIGPICls
  {
    #region Variables Privadas

    private DateTime fechaIncorporacion;
    private DateTime fechaProcesamiento;
    private SIGPIParametros parametros;

    #endregion

    #region Metodos Publicos

    /// <summary>
    /// Fecha de Incorporacion de lecturas
    /// </summary>
    public DateTime FechaIncorporacion
    {
      get
      {
        return fechaIncorporacion;
      }
      set
      {
        fechaIncorporacion = value;
      }
    }

    /// <summary>
    /// Fecha de procesamiento de los datos incorporados
    /// </summary>
    public DateTime FechaProcesamiento
    {
      get
      {
        return fechaProcesamiento;
      }
      set
      {
        fechaProcesamiento = value;
      }
    }

    /// <summary>
    /// Parametros asociados a la aplicacion
    /// </summary>
    public SIGPIParametros Parametros
    {
      get
      {
        return parametros;
      }
      set
      {
        parametros = value;
      }
    }

    #endregion
  }

  /// <summary>
  /// Tipo de estadistico habilitado
  /// </summary>
  public enum EnumTipoEstadistico
  {
    MAXIMUM,
    MINIMUM,
    MEAN
  }
}
