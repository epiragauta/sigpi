using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesRaster;

namespace SIGPI_10
{
  public class SIGPIUtils
  {

    public static IWorkspace VerificarGDBTemporal(string sRuta)
    {
      string sFile = "t_" + DateTime.Now.ToString("yyyyMMdd");
      if (System.IO.File.Exists(sRuta + @"\" + sFile + ".mdb"))
      {
        try
        {
          System.IO.File.Delete(sRuta + @"\" + sFile + ".mdb");
        }
        catch (Exception)
        {
          sFile += "_2.mdb";
        }
      }

      IWorkspaceFactory pFW = new AccessWorkspaceFactoryClass();
      IWorkspaceName pWName = pFW.Create(sRuta, sFile, null, 0);
      IName pName = (IName)pWName;
      IWorkspace pWorkspace = (IWorkspace)pName.Open();

      return pWorkspace;

    }

    public static IWorkspace ConectarAGeodatabase(IPropertySet propertySet)
    {
      IWorkspaceFactory pFW = new AccessWorkspaceFactoryClass();
      IWorkspace pWorkspace;
      try
      {
        pWorkspace = pFW.Open(propertySet, 0);
      }
      catch (Exception e)
      {
        throw e;
      }
      return pWorkspace;
    }

    public static IRaster AbrirRasterDesdeGDB(string sDir, string sNombre)
    {
      try
      {
        IWorkspaceFactory2 pWorkspaceFactory2 = (IWorkspaceFactory2)new AccessWorkspaceFactoryClass();
        IRasterWorkspaceEx pRasterWorkspaceEx = (IRasterWorkspaceEx)pWorkspaceFactory2.OpenFromFile(sDir, 0);
        //IWorkspace pWorkspace = (IWorkspace)pRasterWorkspaceEx;
        //IEnumDataset pEnumDS = pWorkspace.get_Datasets(esriDatasetType.esriDTRasterDataset);
        //IDataset pDS = pEnumDS.Next();
        //while (pDS != null)
        //{
        //  if (pDS.Name.ToUpper().Equals(sNombre.ToUpper()))
        //  {
        //    IRasterDataset pRDS = (IRasterDataset)pDS;

        //  }
        //  pDS = pEnumDS.Next();
        //}
        IRasterDataset pRDS2 = pRasterWorkspaceEx.OpenRasterDataset(sNombre);
        return pRDS2.CreateDefaultRaster();
        //IWorkspaceFactory wsFactory = new RasterWorkspaceFactoryClass();
        //IRasterWorkspace ws = (IRasterWorkspace)wsFactory.OpenFromFile(sDir, 0);
        //IRasterDataset rasterDataset = ws.OpenRasterDataset(sNombre);
        //return rasterDataset.CreateDefaultRaster();
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine(ex.Message);
        return null;
      }
    }

    public static ITable AbrirTablaDesdeGDB(string sDir, string sNombre)
    {
      try
      {
        IWorkspaceFactory2 pWorkspaceFactory2 = (IWorkspaceFactory2)new AccessWorkspaceFactoryClass();
        IFeatureWorkspace pFWorkspace = (IFeatureWorkspace)pWorkspaceFactory2.OpenFromFile(sDir, 0);

        return pFWorkspace.OpenTable(sNombre);

      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine(ex.Message);
        return null;
      }
    }

    public static IRaster AbrirRasterDesdeArchivo(string path, string name)
    {
      try
      {
        IWorkspaceFactory wsFactory = new RasterWorkspaceFactoryClass();
        IRasterWorkspace ws = (IRasterWorkspace)wsFactory.OpenFromFile(path, 0);
        IRasterDataset rasterDataset = ws.OpenRasterDataset(name);
        return rasterDataset.CreateDefaultRaster();
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine(ex.Message);
        return null;
      }
    }
  }

  public class ConversionMes
  {
    public static int MesNumero(string sMesTexto)
    {
      switch (sMesTexto)
      {
        case "ENERO":
          return 1;
        case "FEBRERO":
          return 2;
        case "MARZO":
          return 3;
        case "ABRIL":
          return 4;
        case "MAYO":
          return 5;
        case "JUNIO":
          return 6;
        case "JULIO":
          return 7;
        case "AGOSTO":
          return 8;
        case "SEPTIEMBRE":
          return 9;
        case "OCTUBRE":
          return 10;
        case "NOVIEMBRE":
          return 11;
        case "DICIEMBRE":
          return 12;
        default:
          return -1;
      }
    }
  }
}
