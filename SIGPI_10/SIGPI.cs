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
      
    }

    protected override void OnClick()
    {

      ArcMap.Application.CurrentTool = null;
      FrmSIGPIPrincipal frmMain = new FrmSIGPIPrincipal(ArcMap.Application);
      IntPtr pIntPtr = new IntPtr(ArcMap.Application.hWnd);
      frmMain.Show((System.Windows.Forms.Form)System.Windows.Forms.Form.FromHandle(pIntPtr));
    }
    protected override void OnUpdate()
    {
      Enabled = ArcMap.Application != null;
    }
  }

}
