using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGPI_10
{
  class PrecipitacionProcesada
  {
    private int _codigo;
    private double _p5;
    private int _dsll5;
    private int _dsllc5;
    private DateTime _fec_ul_lectu;

    public PrecipitacionProcesada(int codigo, double p5, int dsll5, int dsllc5, DateTime fec_ul_lectu)
    {
      _codigo = codigo;
      _p5 = p5;
      _dsll5 = dsll5;
      _dsllc5 = dsllc5;
      _fec_ul_lectu = fec_ul_lectu;

    }

    public PrecipitacionProcesada()
    {
      _codigo = -1;
      _dsll5 = -1;
      _dsllc5 = -1;
      _fec_ul_lectu = DateTime.Now;

    }

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

    public double P5
    {
      get
      {
        return _p5;
      }
      set
      {
        _p5 = value;
      }
    }

    public int DSLL5
    {
      get
      {
        return _dsll5;
      }
      set
      {
        _dsll5 = value;
      }
    }

    public int DSLLC5
    {
      get
      {
        return _dsllc5;
      }
      set
      {
        _dsllc5 = value;
      }
    }

    public DateTime FEC_UL_LECTU
    {
      get
      {
        return _fec_ul_lectu;
      }
      set
      {
        _fec_ul_lectu = value;
      }
    }
  }
}
