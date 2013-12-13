using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGPI_10
{
  public class EstacionIdeam
  {
    private Int32 codigo;
    private Double longitud;
    private Double latitud;

    public EstacionIdeam(Int32 _codigo, Double _longitud, Double _latitud)
    {
      codigo = _codigo;
      longitud = _longitud;
      latitud = _latitud;

    }
    public Double Longitud
    {
      get { return longitud; }
      set { longitud = value; }
    }
    

    public Double Latitud
    {
      get { return latitud; }
      set { latitud = value; }
    }
    
    public Int32 Codigo
    {
      get { return codigo; }
      set { codigo = value; }
    }


  }
}
