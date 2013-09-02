using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGPI_10
{
  public class Lectura
  {
    private int _codigo;
    private double _valor;
    private DateTime _fecha;
    double _dX, _dY;

    public int Codigo
    {
      get
      {
        return _codigo;
      }
      set
      {
        _codigo = value;
      }
    }

    public double Valor
    {
      get
      {
        return _valor;
      }
      set
      {
        _valor = value;
      }
    }

    public double X
    {
      get
      {
        return _dX;
      }
      set
      {
        _dX = value;
      }
    }

    public double Y
    {
      get
      {
        return _dY;
      }
      set
      {
        _dY = value;
      }
    }

    public DateTime Fecha
    {
      get
      {
        return _fecha;
      }
      set
      {
        _fecha = value;
      }

    }
  }
}
