using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows;


namespace SIGPI_10
{
  public class SigpiHelp : ESRI.ArcGIS.Desktop.AddIns.Button
  {
    public SigpiHelp()
    {
    }

    protected override void OnClick()
    {
      MessageBox.Show("Help in construction");
    }

    protected override void OnUpdate()
    {
    }
  }
}
