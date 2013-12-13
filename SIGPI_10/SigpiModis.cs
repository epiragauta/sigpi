using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Xml.Serialization;


namespace SIGPI_10
{
  public class SigpiModis : ESRI.ArcGIS.Desktop.AddIns.Button
  {

    private static String URL_MODIS = "http://e4ftl01.cr.usgs.gov/MODIS_Composites/MOLT/MOD13A1.005/";
    private static String HTML_A_TAG = "a";
    private static String REGEX_MODIS_FOLDER = "[0-9].[0-9].[0-9]";
    private static String MODIS_DIR = "modis";
    private static String MASK = "XXXX.XX.XX";
    private static String SIGPI_PROCESS_BAT = "sigpi_process.bat";
    private static String SIGPI_READ_HDF = "sigpi_read_hdf.py";
    private static String PARAMS_GEO_PRM = "params_geo.prm";
    private static String PATH_INPUT_MOSAIC_HDF = "PATH_INPUT_MOSAIC_HDF";
    private static String PATH_OUTPUT_MOSAIC_HDF = "PATH_OUTPUT_MOSAIC_HDF";
    private static String PROCESS_MODIS_NVI_AGS = "process_modis_nvi_ags.py";
    private static String PROCESS_MODIS_NVI_AGS_BASE_TMP_DIR = "PROCESS_MODIS_NVI_AGS_BASE_TMP_DIR";
    private static String PATH_MODIS_APP = "PATH_MODIS_APP";
    private static String NVI_RASTER_RESULT = "NVI_RASTER_RESULT";
    private static String PATH_SIGPI_NVI = "PATH_SIGPI_NVI";
    private static String MODIS_DIR_APP = "modis";
    private static String NVI = "nvi";
    private static String RESULT_NVI = "tmpMosaic.500m_16_days_NDVI_prj_magna_adjust.tif";
    

    WebBrowser mywebBrowser;


    public SigpiModis()
    {
    }

    protected override void OnClick()
    {
      mywebBrowser = new WebBrowser();
      mywebBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(mywebBrowser_DocumentCompleted);

      Uri address = new Uri(URL_MODIS);
      mywebBrowser.Navigate(address);
    }

    protected override void OnUpdate()
    {
    }

    private void mywebBrowser_DocumentCompleted(Object sender, WebBrowserDocumentCompletedEventArgs e)
    {
      //Until this moment the page is not completely loaded
      HtmlDocument doc = mywebBrowser.Document;
      HtmlElementCollection tagCollection;
      tagCollection = doc.GetElementsByTagName(HTML_A_TAG);
      String dateFolder = "";

      if (tagCollection.Count > 0)
      {
        HtmlElement htmlElement = tagCollection[tagCollection.Count - 1];

        dateFolder = htmlElement.InnerText;
        if (dateFolder == null)
        {
          MessageBox.Show("Existen problemas con la conexión al sitio de MODIS.", "SIGPI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return;
        }
        //Regex regex = new Regex(@"[0-9].[0-9].[0-9]");
        if (Regex.IsMatch(dateFolder, REGEX_MODIS_FOLDER))
        {
          dateFolder = dateFolder.Substring(0, dateFolder.Length - 1);
          dateFolder = dateFolder.Replace('.', '-');

          System.Console.WriteLine(dateFolder);

          String tempDir = System.IO.Path.GetTempPath(); //"d:\\tmp";
          String currentPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
          currentPath = currentPath.Replace("file:\\", "");
          String dateDir = tempDir + dateFolder;

          SIGPIParametros parametros;
          try
          {
            XmlSerializer serializer = new XmlSerializer(typeof(SIGPIParametros));
            System.IO.StreamReader r = new System.IO.StreamReader(currentPath + "\\parameters\\parametros.xml");
            parametros = (SIGPIParametros)serializer.Deserialize(r);
            r.Close();
            serializer = null;
            r = null;
            
          }
          catch (Exception ex)
          {
            MessageBox.Show(ex.Message, "SIGPI");
            return;
          }


          if (System.IO.File.Exists(parametros.RutaSIGPI + NVI + "\\" + RESULT_NVI) &&
            System.IO.File.Exists(parametros.RutaSIGPI + NVI + "\\" + dateFolder))
          {
            MessageBox.Show("El NVI se encuentra actualizado");
            return;
          }
          else
          {
            if (!System.IO.File.Exists(parametros.RutaSIGPI + NVI + "\\" + dateFolder))
            {
              String[] files = System.IO.Directory.GetFiles(parametros.RutaSIGPI + NVI);
              foreach (String file in files)
              {
                System.IO.File.Delete(file);
              }

            }
          }

          if (!System.IO.File.Exists(parametros.RutaSIGPI + NVI + "\\" + RESULT_NVI))
          {

            System.IO.Directory.CreateDirectory(dateDir);
            String[] files = System.IO.Directory.GetFiles(currentPath + "\\" + MODIS_DIR);
            foreach (String f in files)
            {

              System.IO.File.Copy(f, dateDir + "\\" + f.Substring(f.LastIndexOf("\\") + 1, f.Length - (f.LastIndexOf("\\") + 1)), true);
            }

            ////////////////////////////////////////////////////////////////////////
            StreamReader sr = new StreamReader(dateDir + "\\" + SIGPI_READ_HDF);
            String txtScript = sr.ReadToEnd();
            sr.Close();

            txtScript = txtScript.Replace(MASK, dateFolder.Replace("-", "."));

            StreamWriter sw = new StreamWriter(dateDir + "\\" + SIGPI_READ_HDF);
            sw.Write(txtScript);
            sw.Close();

            ////////////////////////////////////////////////////////////////////////
            sr = new StreamReader(dateDir + "\\" + PARAMS_GEO_PRM);
            txtScript = sr.ReadToEnd();
            sr.Close();

            txtScript = txtScript.Replace(PATH_INPUT_MOSAIC_HDF, dateDir).Replace(PATH_OUTPUT_MOSAIC_HDF, dateDir);

            sw = new StreamWriter(dateDir + "\\" + PARAMS_GEO_PRM);
            sw.Write(txtScript);
            sw.Close();
            ////////////////////////////////////////////////////////////////////////

            ////////////////////////////////////////////////////////////////////////
            sr = new StreamReader(dateDir + "\\" + PROCESS_MODIS_NVI_AGS);
            txtScript = sr.ReadToEnd();
            sr.Close();

            txtScript = txtScript.Replace(PROCESS_MODIS_NVI_AGS_BASE_TMP_DIR, dateDir);

            sw = new StreamWriter(dateDir + "//" + PROCESS_MODIS_NVI_AGS);
            sw.Write(txtScript.Replace("\\","/"));
            sw.Close();
            ////////////////////////////////////////////////////////////////////////

            //////////////////////////////////////////////////////////////////////// C:/software/modis/
            try
            {
              sr = new StreamReader(dateDir + "\\" + SIGPI_PROCESS_BAT);
              txtScript = sr.ReadToEnd();
              sr.Close();

              txtScript = txtScript.Replace(PROCESS_MODIS_NVI_AGS_BASE_TMP_DIR, dateDir);

              sw = new StreamWriter(dateDir + "//" + SIGPI_PROCESS_BAT);
              sw.Write(txtScript.Replace(PATH_MODIS_APP,parametros.RutaSIGPI + MODIS_DIR_APP)
                                .Replace(NVI_RASTER_RESULT, RESULT_NVI)
                                .Replace(PATH_SIGPI_NVI, parametros.RutaSIGPI + NVI)
                                .Replace("/", "\\"));
              sw.Close();

            }
            catch (Exception ex)
            {
              MessageBox.Show(ex.Message, "SIGPI");
              return;
            }
            
            ////////////////////////////////////////////////////////////////////////
            
            ExecuteCommand(dateDir + "\\" + SIGPI_PROCESS_BAT, dateDir, "");

            sw = new StreamWriter(parametros.RutaSIGPI + NVI + "\\" + dateFolder);
            sw.Close();


          }

        }
        else
        {
          MessageBox.Show(String.Format("El algoritmo se encuentra trabajando con la ultima información MODIS disponible. FEcha: {0}",dateFolder),
                          "SIGPI",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

      }
      else
      {
        MessageBox.Show("Existen problemas ingresando el sitio web de información MODIS","SIGPI",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        return;
      }

    }

    public void ExecuteCommand(String command, String dir, String args)
    {
      Process proc = new Process();
      proc.StartInfo.FileName = command;
      proc.StartInfo.Arguments = args;
      proc.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
      proc.StartInfo.ErrorDialog = false;
      proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(command);
      proc.Start();
      proc.WaitForExit();
      if (proc.ExitCode != 0)
        Console.WriteLine("Error executing process...");
    }

  }
}
