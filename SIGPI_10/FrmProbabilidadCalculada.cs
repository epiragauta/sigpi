using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SIGPI_10
{
  public partial class FrmProbabilidadCalculada : Form
  {
    private int iNumDias;
    private EnumGuardarProbabilidad pEnumGuardarProbabilidad;
    private string _sModeloBase;

    public FrmProbabilidadCalculada()
    {
      InitializeComponent();
    }

    public FrmProbabilidadCalculada(string[] capas)
    {

      InitializeComponent();
      listModelos.Items.AddRange(capas);

    }

    private void FrmProbabilidadCalculada_Load(object sender, EventArgs e)
    {
      // iniciado el numero de dias a 5
      iNumDias = 5;

      pEnumGuardarProbabilidad = EnumGuardarProbabilidad.Sobreescribir;
    }

    private void btnCerrar_Click_1(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Hide();
    }

    private void btnProbabilidad_Click_1(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
      this.Hide();
    }

    private void radioBtnDias3_CheckedChanged_1(object sender, EventArgs e)
    {
      iNumDias = 3;

    }

    private void radioBtnDias4_CheckedChanged_1(object sender, EventArgs e)
    {
      iNumDias = 4;
    }

    private void radioBtnDias5_CheckedChanged_1(object sender, EventArgs e)
    {
      iNumDias = 5;
    }

    private void radioBtoSobreescribirProbabilidad_CheckedChanged_1(object sender, EventArgs e)
    {
      pEnumGuardarProbabilidad = EnumGuardarProbabilidad.Sobreescribir;
    }

    private void radioBtnCargarArchivoExistente_CheckedChanged_1(object sender, EventArgs e)
    {
      pEnumGuardarProbabilidad = EnumGuardarProbabilidad.CargarExistente;
    }

    public int NumeroDeDias
    {
      get
      {
        return iNumDias;
      }
    }

    public string ModeloBase
    {
      get
      {
        return _sModeloBase;
      }
    }

    public EnumGuardarProbabilidad OpcionDeGuardar
    {
      get
      {
        return pEnumGuardarProbabilidad;
      }
    }

    private void listModelos_SelectedIndexChanged(object sender, EventArgs e)
    {
      _sModeloBase = listModelos.Text;
    }

    
    
  }
}
