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
  public partial class FrmPassword : Form
  {
    private int intentos;

    public FrmPassword()
    {
      InitializeComponent();
      intentos = 0;
    }

    private void btnAceptar_Click(object sender, EventArgs e)
    {
      if (txUsuario.Text.ToUpper() == "SIGPI" & txtClave.Text.ToUpper() == "SIGPI2010")
      {
        //MessageBox.Show("Clave OK");
        this.DialogResult = DialogResult.OK;
      }
      else
      {
        MessageBox.Show("Clave Errada");
        intentos++;
        if (intentos == 3)
        {
          this.DialogResult = DialogResult.Cancel;
        }
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Hide();
    }
  }
}
