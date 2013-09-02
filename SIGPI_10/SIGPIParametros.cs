using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SIGPI_10
{
  public class SIGPIParametros
  {
    private string ruta_sigpi, lecturas, imagenes, temporal, resultado, raster,
        rutaBD, rutaGDB, tmmmm, susceptibilidad, grids, asentamientos, mascara,
        susceptibilidad_con_nino, porcentaje_efectivo_lecturas, fecha_ultimo_modelo,
        gdbServidor, gdbInstancia, gdbDB, gdbUsuario, gdbClave, susceptibilidadActiva;
    private double dTamanoCelda;
    private double dPesoPrecipitacion, dPesoTemperatura, dPesoAmenazasParciales;
    string sRutaTblReclassTemperatura, sRutaTblReclassPrecipitacion, sRutaTblReclassIncendios;
    string sTipoEstadistico;
    double[] dPesos;
    private string rutaMXD;

    /// <summary>
    /// Capa de Susceptibilidad Activa
    /// </summary>
    public string SusceptibilidadActiva
    {
      get { return susceptibilidadActiva; }
      set { susceptibilidadActiva = value; }
    }

    /// <summary>
    /// Directorio Base del SIGPI
    /// </summary>
    public string RutaSIGPI
    {
      get { return ruta_sigpi; }
      set { ruta_sigpi = value; }
    }

    /// <summary>
    /// Directorio de Lecturas
    /// </summary>
    public string Lecturas
    {
      get { return lecturas; }
      set { lecturas = value; }
    }

    /// <summary>
    /// Propiedad que indica el directorio donde almacenar las imagenes
    /// </summary>
    public string Imagenes
    {
      get { return imagenes; }
      set { imagenes = value; }
    }

    /// <summary>
    /// Directorio Temporal
    /// </summary>
    public string Temporal
    {
      get { return temporal; }
      set { temporal = value; }
    }

    /// <summary>
    /// Directorio de la Temperatura Maxima Media Mensual Multianual
    /// </summary>
    public string TMMMM
    {
      get { return tmmmm; }
      set { tmmmm = value; }
    }

    /// <summary>
    /// Directorio para los modelos de probabilidad
    /// </summary>
    public string Resultado
    {
      get { return resultado; }
      set { resultado = value; }
    }

    /// <summary>
    /// Directorio para los modelos de condiciones
    /// </summary>
    public string Raster
    {
      get { return raster; }
      set { raster = value; }
    }

    /// <summary>
    /// Localizacion de la base de datos de lecturas
    /// </summary>
    public string RutaBD
    {
      get { return rutaBD; }
      set { rutaBD = value; }
    }

    /// <summary>
    /// Localizacion de la Geodatabase Personal
    /// </summary>
    public string RutaGBD
    {
      get { return rutaGDB; }
      set { rutaGDB = value; }
    }

    /// <summary>
    /// Nombre de la Capa de Susceptibilidad Sin Nino
    /// </summary>
    public string Susceptibilidad
    {
      get
      {
        return susceptibilidad;
      }
      set
      {
        susceptibilidad = value;
      }
    }

    /// <summary>
    /// Nombre de la Capa de Susceptibilidad con Nino\
    /// </summary>
    public string SusceptibilidadConNino
    {
      get
      {
        return susceptibilidad_con_nino;
      }
      set
      {
        susceptibilidad_con_nino = value;
      }
    }

    /// <summary>
    /// Directorio donde se encuentran localizados los Grids
    /// </summary>
    public string Grids
    {
      get
      {
        return grids;
      }
      set
      {
        grids = value;
      }
    }

    /// <summary>
    /// Nombre de la capa de asentamientos
    /// </summary>
    public string Asentamientos
    {
      get
      {
        return asentamientos;
      }
      set
      {
        asentamientos = value;
      }
    }

    /// <summary>
    /// Mascara para la generacion de los resultados
    /// </summary>
    public string Mascara
    {
      get
      {
        return mascara;
      }
      set
      {
        mascara = value;
      }
    }

    /// <summary>
    /// Porcentaje Efectivo de Lecturas
    /// </summary>
    public string PorcentajeEfectivoLecturas
    {
      get
      {
        return porcentaje_efectivo_lecturas;
      }
      set
      {
        porcentaje_efectivo_lecturas = value;
      }
    }

    /// <summary>
    /// Tamano de la Celda
    /// </summary>
    public double TamanoCelda
    {
      get
      {
        return dTamanoCelda;
      }
      set
      {
        dTamanoCelda = value;
      }
    }

    /// <summary>
    /// Fecha del ultimo modelo procesado en la aplicacion
    /// </summary>
    public string FechaUltimoModelo
    {
      get
      {
        return fecha_ultimo_modelo;
      }
      set
      {
        fecha_ultimo_modelo = value;
      }
    }

    /// <summary>
    /// IP o Nombre del Servidor
    /// </summary>
    public string Servidor
    {
      get
      {
        return gdbServidor;
      }
      set
      {
        gdbServidor = value;
      }
    }

    /// <summary>
    /// Instancia de la Base de datos
    /// </summary>
    public string Instancia
    {
      get
      {
        return gdbInstancia;
      }
      set
      {
        gdbInstancia = value;
      }
    }

    /// <summary>
    /// Nombre de la base de datos alfanumerica
    /// </summary>
    public string BaseDeDatos
    {
      get
      {
        return gdbDB;
      }
      set
      {
        gdbDB = value;
      }
    }

    /// <summary>
    /// Nombre del Usuario
    /// </summary>
    public string Usuario
    {
      get
      {
        return gdbUsuario;
      }
      set
      {
        gdbUsuario = value;
      }
    }

    /// <summary>
    /// Clave de conexion a la GeoDatabase
    /// </summary>
    public string Clave
    {
      get
      {
        return gdbClave;
      }
      set
      {
        gdbClave = value;
      }
    }

    /// <summary>
    /// Directorio del documento MXD
    /// </summary>
    public string RutaMXD
    {
      get
      {
        return rutaMXD;
      }
      set
      {
        rutaMXD = value;
      }
    }

    /// <summary>
    /// Servidor de la Geodatabase
    /// </summary>
    public string GDBServidor
    {
      get
      {
        return gdbServidor;
      }
      set
      {
        gdbServidor = value;
      }
    }

    /// <summary>
    /// Instancia de la Geodatabase
    /// </summary>
    public string GDBInstancia
    {
      get
      {
        return gdbInstancia;
      }
      set
      {
        gdbInstancia = value;
      }
    }

    /// <summary>
    /// Nombre de la Geodatabase
    /// </summary>
    public string GDBDataBase
    {
      get
      {
        return gdbDB;
      }
      set
      {
        gdbDB = value;
      }
    }

    /// <summary>
    /// Usuario de la Geodatabase
    /// </summary>
    public string GDBUsuario
    {
      get
      {
        return gdbUsuario;
      }
      set
      {
        gdbUsuario = value;
      }
    }

    /// <summary>
    /// Clave de conexion a la GeoDatabase
    /// </summary>
    public string GDBClave
    {
      get
      {
        return gdbClave;
      }
      set
      {
        gdbClave = value;
      }
    }

    public double PesoPrecipitacion
    {
      get { return dPesoPrecipitacion; }
      set { dPesoPrecipitacion = value; }
    }

    public double PesoTemperatura
    {
      get { return dPesoTemperatura; }
      set { dPesoTemperatura = value; }
    }

    public double PesoAmenazasParciales
    {
      get { return dPesoAmenazasParciales; }
      set { dPesoAmenazasParciales = value; }
    }

    public string TablaReclasificacionTemperatura
    {
      get { return sRutaTblReclassTemperatura; }
      set { sRutaTblReclassTemperatura = value; }
    }

    public string TablaReclasificacionPrecipitacion
    {
      get { return sRutaTblReclassPrecipitacion; }
      set { sRutaTblReclassPrecipitacion = value; }
    }

    public string TablaReclasificacionIncendios
    {
      get { return sRutaTblReclassIncendios; }
      set { sRutaTblReclassIncendios = value; }
    }

    public string TipoEstadistico
    {
      get { return sTipoEstadistico; }
      set { sTipoEstadistico = value; }
    }

    public double[] Pesos
    {
      get { return dPesos; }
      set { dPesos = value; }
    }

    public SIGPIParametros()
    {

    }

    /// <summary>
    /// Constructor de la clase
    /// </summary>
    public SIGPIParametros(string sRutaArchivo)
    {
      XmlReader parameters = XmlReader.Create(sRutaArchivo);
      parameters.MoveToContent();

      while (parameters.Read())
      {
        if (parameters.NodeType == XmlNodeType.Element && parameters.Name == "RUTA_SIGPI")
          ruta_sigpi = parameters.ReadString();
        if (parameters.NodeType == XmlNodeType.Element && parameters.Name == "LECTURAS")
          lecturas = parameters.ReadString();
        if (parameters.NodeType == XmlNodeType.Element && parameters.Name == "IMAGENES")
          imagenes = parameters.ReadString();
        if (parameters.NodeType == XmlNodeType.Element && parameters.Name == "TEMPORAL")
          temporal = parameters.ReadString();
        if (parameters.NodeType == XmlNodeType.Element && parameters.Name == "RESULTADO")
          resultado = parameters.ReadString();
        if (parameters.NodeType == XmlNodeType.Element && parameters.Name == "RASTER")
          raster = parameters.ReadString();
        if (parameters.NodeType == XmlNodeType.Element && parameters.Name == "RUTA_BD")
          rutaBD = parameters.ReadString();
        if (parameters.NodeType == XmlNodeType.Element && parameters.Name == "RUTA_GDB")
          rutaGDB = parameters.ReadString();
        if (parameters.NodeType == XmlNodeType.Element && parameters.Name == "RUTA_MXD")
          rutaMXD = parameters.ReadString();
        if (parameters.NodeType == XmlNodeType.Element && parameters.Name == "SUSCEPTIBILIDAD")
          susceptibilidad = parameters.ReadString();
        if (parameters.NodeType == XmlNodeType.Element && parameters.Name == "SUSCEPTIBILIDAD_CON_NINO")
          susceptibilidad_con_nino = parameters.ReadString();
        if (parameters.NodeType == XmlNodeType.Element && parameters.Name == "TMMMM")
          tmmmm = parameters.ReadString();
        if (parameters.NodeType == XmlNodeType.Element && parameters.Name == "ASENTAMIENTOS")
          asentamientos = parameters.ReadString();
        if (parameters.NodeType == XmlNodeType.Element && parameters.Name == "GRIDS")
          grids = parameters.ReadString();
        if (parameters.NodeType == XmlNodeType.Element && parameters.Name == "MASCARA")
          mascara = parameters.ReadString();
        if (parameters.NodeType == XmlNodeType.Element && parameters.Name == "PORCENTAJE_EFECTIVO_LECTURAS")
          porcentaje_efectivo_lecturas = parameters.ReadString();
        if (parameters.NodeType == XmlNodeType.Element && parameters.Name == "TAMANO_CELDA")
          dTamanoCelda = System.Convert.ToDouble(parameters.ReadString());
        if (parameters.NodeType == XmlNodeType.Element && parameters.Name == "SERVIDOR")
          gdbServidor = parameters.ReadString();
        if (parameters.NodeType == XmlNodeType.Element && parameters.Name == "INSTANCIA")
          gdbInstancia = parameters.ReadString();
        if (parameters.NodeType == XmlNodeType.Element && parameters.Name == "BD")
          gdbDB = parameters.ReadString();
        if (parameters.NodeType == XmlNodeType.Element && parameters.Name == "USUARIO")
          gdbUsuario = parameters.ReadString();
        if (parameters.NodeType == XmlNodeType.Element && parameters.Name == "CLAVE")
          gdbClave = parameters.ReadString();
      }

      //ruta_sigpi = @"G:\SIGPI\";
      //rutaBD = @"G:\SIGPI\BASE_ACCESS\SIGPI_BD.MDB";
      //rutaGDB = @"G:\SIGPI\SIGPIGBD.MDB";
    }
  }
}
