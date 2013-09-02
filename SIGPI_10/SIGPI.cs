using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SIGPI_10
{
  public class SIGPI : ESRI.ArcGIS.Desktop.AddIns.Button
  {
    public SIGPI()
    {
      FrmSIGPIPrincipal frmMain = new FrmSIGPIPrincipal(ArcMap.Application);
      frmMain.Show();
    }

    protected override void OnClick()
    {
      //
      //  TODO: Sample code showing how to access button host
      //
      ArcMap.Application.CurrentTool = null;
    }
    protected override void OnUpdate()
    {
      Enabled = ArcMap.Application != null;
    }
  }

}
