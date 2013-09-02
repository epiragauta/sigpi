using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.GeoAnalyst;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GeoDatabaseUI;
using ESRI.ArcGIS.SpatialAnalyst;
using Microsoft.Office.Interop.Excel;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.SpatialAnalystTools;

namespace SIGPI_10
{
  /// <summary>
  /// Clase Procesamiento asociado al SIGPI
  /// </summary>
  public class SIGPIProcesamiento
  {
    #region Variables Privadas

    private String sOutputFormat;
    private String sOutputExtension;
    private SIGPIParametros _parametros;

    #endregion


    #region Metodos Publicos

    /// <summary>
    /// Constructor de la clase
    /// </summary>
    /// <param name="parametros">Parametros</param>
    public SIGPIProcesamiento(SIGPIParametros parametros)
    {
      _parametros = parametros;
      sOutputFormat = "IMAGINE Image";
      sOutputExtension = ".img";
    }

    /// <summary>
    /// Construir Grid Precipitacion
    /// </summary>
    /// <param name="pWorkspaceTemp">IWorkspace Temporal</param>
    public void ConstruirGRIDPrecipitacion(IWorkspace pWorkspaceTemp)
    {
      string sLayer = "estaciones";
      string sTablePrecipitacion = "DEFI_PRECI";
      IWorkspaceFactory pWF = new AccessWorkspaceFactoryClass();
      IWorkspace pWorkspace = pWF.OpenFromFile(_parametros.RutaGBD, 0);
      IFeatureWorkspace pFW = (IFeatureWorkspace)pWorkspace;
      IFeatureClass pFC = pFW.OpenFeatureClass(sLayer);
      IFeatureLayer pFLayer = new FeatureLayerClass();
      pFLayer.Name = sLayer;
      pFLayer.FeatureClass = pFC;
      System.Windows.Forms.MessageBox.Show(pFC.Fields.FieldCount.ToString());
      IWorkspace pWorkspace2 = pWF.OpenFromFile(_parametros.RutaBD, 0);
      IFeatureWorkspace pFW2 = (IFeatureWorkspace)pWorkspace2;
      ITable pTable = pFW2.OpenTable(sTablePrecipitacion);

      IDisplayRelationshipClass pDispRC = (IDisplayRelationshipClass)pFLayer;
      IMemoryRelationshipClassFactory pMemRC = new MemoryRelationshipClassFactoryClass();
      IRelationshipClass2 pRelationship = (IRelationshipClass2)pMemRC.Open("JOIN", (IObjectClass)pFC, "CODIGO", (IObjectClass)pTable, "CODIGO", "forward", "backward", esriRelCardinality.esriRelCardinalityOneToOne);
      System.Windows.Forms.MessageBox.Show(pFC.Fields.FieldCount.ToString());

      //IDisplayRelationshipClass pDispRC = (IDisplayRelationshipClass)pFLayer;
      pDispRC.DisplayRelationshipClass(pRelationship, esriJoinType.esriLeftOuterJoin);
      IFeatureLayerDefinition pFLDef = (IFeatureLayerDefinition)pFLayer;
      pFLDef.DefinitionExpression = "DEFI_PRECI.P5 >= 0";

      IInterpolationOp pIntOp = (IInterpolationOp)new RasterInterpolationOpClass();
      IRasterAnalysisEnvironment pRasterAnEnv = (IRasterAnalysisEnvironment)pIntOp;

      IEnvelope pEnv = new EnvelopeClass();
      pEnv.XMin = -80000;
      pEnv.YMin = -80000;
      pEnv.XMax = 2000000;
      pEnv.YMax = 2400000;

      ////object missing = Type.Missing;
      ////pRasterAnEnv.SetExtent(esriRasterEnvSettingEnum.esriRasterEnvValue, ref (object)pEnv, ref missing);
      //////pRasterAnEnv.SetExtent(esriRasterEnvSettingEnum.esriRasterEnvValue, (object)pEnv, missing);
      ////pRasterAnEnv.SetCellSize(esriRasterEnvSettingEnum.esriRasterEnvValue, 4000);        
      ////IRasterRadius pRadius = new RasterRadiusClass();
      ////pRadius.SetVariable(12, null);
      ////IRaster pOutRaster;
      ////pOutRaster = pIntOp.IDW((IGeoDataset)pFLayer, 2, pRadius, missing);
    }

    /// <summary>
    /// Genera un modelo digital a partir de una capa vectorial, indicando el campo, los parametros, la mascara y el prefijo
    /// del nombramiento de la capa
    /// </summary>
    /// <param name="pFC">Feature Class</param>
    /// <param name="field">Campo</param>
    /// <param name="parametros">Parametros</param>
    /// <param name="pMask">Mascara</param>
    /// <param name="prefijo">Prefijo</param>
    /// <param name="sQuery">Consulta</param>
    /// <param name="bPermanente">Permanente</param>
    /// <returns></returns>
    public IRaster ConstruirGrid(IFeatureClass pFC, string field, SIGPIParametros parametros,
                                  IGeoDataset pMask, string prefijo, string sQuery, bool bPermanente)
    {
      IFeatureClassDescriptor pFCDescriptor = new FeatureClassDescriptorClass();
      IQueryFilter pQF = new QueryFilterClass();
      if (sQuery.Length != 0)
      {
        pQF.WhereClause = sQuery;
      }

      pFCDescriptor.Create(pFC, pQF, field);

      IInterpolationOp3 pInterpolationOp = new RasterInterpolationOpClass();
      IRasterAnalysisEnvironment pRasterAnalysysEnv = (IRasterAnalysisEnvironment)pInterpolationOp;
      IEnvelope pEnvelope = new EnvelopeClass();
      pEnvelope.XMin = -80000;
      pEnvelope.YMin = -80000;
      pEnvelope.XMax = 2000000;
      pEnvelope.YMax = 2400000;

      object objEnvelope = (object)pEnvelope;
      object objSnap = Type.Missing;

      pRasterAnalysysEnv.SetExtent(esriRasterEnvSettingEnum.esriRasterEnvValue, ref objEnvelope, ref objSnap);

      object objCellSize = (object)4000;
      pRasterAnalysysEnv.SetCellSize(esriRasterEnvSettingEnum.esriRasterEnvValue, ref objCellSize);

      pRasterAnalysysEnv.Mask = pMask;

      IRasterRadius pRasterRadius = new RasterRadiusClass();
      //object objMaxDistance = (object)12;
      object objMaxDistance = Type.Missing;
      pRasterRadius.SetVariable(12, ref objMaxDistance);
      object objBarrier = Type.Missing;
      IGeoDataset pGeoDS;
      pGeoDS = pInterpolationOp.IDW((IGeoDataset)pFCDescriptor, 2, pRasterRadius, ref objBarrier);
      IRaster pOutRaster = (IRaster)pGeoDS;
      //IRaster pOutRaster = (IRaster)pInterpolationOp.IDW((IGeoDataset)pFC, 2, pRasterRadius, ref objBarrier);
      string sFormatLayers = DateTime.Now.ToString("yyyyMMdd-HHmmss");
      string sData = parametros.RutaSIGPI + parametros.Temporal + "\\" + prefijo + "-" + sFormatLayers + sOutputExtension;
      IRasterBandCollection pRasterBandColl = (IRasterBandCollection)pOutRaster;
      IDataset pDataset;
      IRasterDataset pRasterDataset;
      if (bPermanente)
      {
        pDataset = pRasterBandColl.SaveAs(sData, null, sOutputFormat);
        pRasterDataset = (IRasterDataset)pDataset;
        pFCDescriptor = null;
        pOutRaster = null;
        IRasterDataset pDS2 = (IRasterDataset)pGeoDS;


        return pRasterDataset.CreateDefaultRaster();
      }
      else
      {
        pFCDescriptor = null;
        pOutRaster = null;
        IDataset pDS3 = (IDataset)pGeoDS;
        pDS3.Delete();
        return null;
      }

    }

    /// <summary>
    /// Genera un FeatureClass en el espacio de trabajo (Workspace) especificado por el usuario, indicando el nombre de la tabla,
    /// capa, campo, filtro de consulta y prefijo
    /// </summary>
    /// <param name="pWorkspaceTemp"></param>
    /// <param name="sTable"></param>
    /// <param name="sCapaEstaciones"></param>
    /// <param name="sField"></param>
    /// <param name="sQuery"></param>
    /// <param name="sPrefijo"></param>
    /// <returns></returns>
    public IFeatureClass ConstruirFeatureClass(IWorkspace pWorkspaceTemp, string sTable, string sCapaEstaciones, string sField,
                                        string sQuery, string sPrefijo)
    {

      SIGPIParametros parametros = _parametros;

      IPropertySet propertySetTable = new PropertySetClass();
      propertySetTable.SetProperty("DATABASE", parametros.RutaBD);
      IWorkspace pWorkspace = SIGPIUtils.ConectarAGeodatabase(propertySetTable);
      IFeatureWorkspace pFW = (IFeatureWorkspace)pWorkspace;

      ITable pTable;

      try
      {
        pTable = pFW.OpenTable(sTable);
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }


      SIGPI sigpi = new SIGPI();


      IPropertySet propertySet = new PropertySetClass();
      //propertySet.SetProperty ("Server", "");
      //propertySet.SetProperty ("Instance", "");
      //propertySet.SetProperty ("user", "");
      //propertySet.SetProperty ("password", "" );
      //propertySet.SetProperty ("Database", "");
      //propertySet.SetProperty("version", "");

      //propertySet.SetProperty("DATABASE", @"C:\SIGPI\SIGPIGDB.mdb");
      propertySet.SetProperty("DATABASE", parametros.RutaGBD);
      //string sCapaEstaciones = "estaciones";

      IWorkspace pWorkspace2;
      try
      {
        pWorkspace2 = SIGPIUtils.ConectarAGeodatabase(propertySet);
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

      IFeatureWorkspace pFW2 = (IFeatureWorkspace)pWorkspace2;
      IFeatureClass pFC;
      try
      {
        pFC = pFW2.OpenFeatureClass(sCapaEstaciones);
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

      IFeatureLayer pFL = new FeatureLayerClass();
      pFL.FeatureClass = pFC;
      IFeatureSelection pFeatSel = (IFeatureSelection)pFL;


      IDisplayRelationshipClass pDispRelClass;
      pDispRelClass = (IDisplayRelationshipClass)pFL;
      pDispRelClass.DisplayRelationshipClass(null, esriJoinType.esriLeftInnerJoin);

      IMemoryRelationshipClassFactory pMemRelClassFac = new MemoryRelationshipClassFactoryClass();
      IRelationshipClass pRelClass;
      pRelClass = pMemRelClassFac.Open("JOIN", (IObjectClass)pFC, sField, (IObjectClass)pTable, sField, "forward", "backward", esriRelCardinality.esriRelCardinalityOneToOne);

      pDispRelClass.DisplayRelationshipClass(pRelClass, esriJoinType.esriLeftOuterJoin);

      IDisplayTable pDisplayTable = (IDisplayTable)pFL;
      IDataset pDS = (IDataset)pDisplayTable.DisplayTable;
      IDatasetName pDSName = (IDatasetName)pDS.FullName;

      //IWorkspaceFactory pShpWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
      //IWorkspace pShpWorkspace = pShpWorkspaceFactory.OpenFromFile(parametros.RutaSIGPI + parametros.Temporal, 0);
      //IDataset pDSShpWorkspace = (IDataset)pShpWorkspace;
      //IWorkspaceName pWorkspaceName = (IWorkspaceName)pDSShpWorkspace.FullName;
      IDataset pDSShpWorkspace = (IDataset)pWorkspaceTemp;
      IWorkspaceName pWorkspaceName = (IWorkspaceName)pDSShpWorkspace.FullName;
      string sFormatLayers = DateTime.Now.ToString("HHmmss");
      IDatasetName pDSName2 = new FeatureClassNameClass();

      string sShapeFileName = "E" + sFormatLayers + sPrefijo;
      pDSName2.Name = sShapeFileName;

      pDSName2.WorkspaceName = pWorkspaceName;

      IQueryFilter pQF = new QueryFilterClass();
      pQF.WhereClause = sQuery;  // "DEFI_PRECI.P5 >= 0";

      pFeatSel.SelectFeatures(pQF, esriSelectionResultEnum.esriSelectionResultNew, false);
      //System.Windows.Forms.MessageBox.Show("Exportando: " + pFeatSel.SelectionSet.Count.ToString());
      IExportOperation pExportOp = new ExportOperationClass();

      try
      {
        pExportOp.ExportFeatureClass(pDSName, pQF, null, null, (IFeatureClassName)pDSName2, 0);
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }


      //IFeatureWorkspace pShpFW = (IFeatureWorkspace)pShpWorkspace;
      IFeatureWorkspace pShpFW = (IFeatureWorkspace)pWorkspaceTemp;
      IFeatureClass pFCNew = pShpFW.OpenFeatureClass(sShapeFileName);

      return pFCNew;
    }
    /// <summary>
    /// Metodo para el calculo del algoritmo completo a partir del conjunto de capas Raster
    /// </summary>
    /// <param name="pRasters"></param>
    /// <param name="sigpi"></param>
    public void AlgoritmoCompleto(IRaster[] pRasters, SIGPICls sigpi)
    {
      if (pRasters.GetUpperBound(0) < 5)
      {
        throw new Exception("Definicion de modelos incompleta");
      }


      SIGPIParametros parametros = sigpi.Parametros;
      IMapAlgebraOp pMapAlgebraOp = new RasterMapAlgebraOpClass();
      IRasterAnalysisEnvironment pRasterAnalysisEnv = (IRasterAnalysisEnvironment)pMapAlgebraOp;

      IWorkspaceFactory pWorkspaceFactory;
      IWorkspace pWorkspace;
      pWorkspaceFactory = new RasterWorkspaceFactoryClass();
      pWorkspace = pWorkspaceFactory.OpenFromFile(parametros.RutaSIGPI + "\\" + parametros.Raster, 0);
      pRasterAnalysisEnv.OutWorkspace = pWorkspace;
      pMapAlgebraOp.BindRaster((IGeoDataset)pRasters[0], "R1");
      pMapAlgebraOp.BindRaster((IGeoDataset)pRasters[1], "R2");
      pMapAlgebraOp.BindRaster((IGeoDataset)pRasters[2], "R3");
      pMapAlgebraOp.BindRaster((IGeoDataset)pRasters[3], "R4");
      pMapAlgebraOp.BindRaster((IGeoDataset)pRasters[4], "R5");
      pMapAlgebraOp.BindRaster((IGeoDataset)pRasters[5], "R6");

      IRaster pOutRaster;

      pOutRaster = (IRaster)pMapAlgebraOp.Execute("[R1] < 2.5 & [R5] > 0 & ([R4] - [R3]) < 0 & [R2] > 2");
      string sPrefijo1 = sigpi.FechaProcesamiento.ToString("ddMMMyy").ToUpper();
      string sPrefijo2 = sigpi.FechaProcesamiento.ToString("MM");
      string sNombreRaster = "M" + sPrefijo2 + "-" + sPrefijo1;
      IRasterBandCollection pRasterBandColl = (IRasterBandCollection)pOutRaster;
      IRasterWorkspace pRasterWorkspace = (IRasterWorkspace)pWorkspace;
      bool bExiste = false;
      try
      {
        IRasterDataset pRasterDS = pRasterWorkspace.OpenRasterDataset(sNombreRaster);
        bExiste = true;
      }
      catch (Exception)
      {
        bExiste = false;
      }
      if (!bExiste)
      {
        pRasterBandColl.SaveAs(parametros.RutaSIGPI + "\\" + parametros.Raster + "\\" + sNombreRaster, null, "GRID");
      }
      else
      {
        throw new Exception("El modelo " + sNombreRaster + " ya existe");
      }
    }

    /// <summary>
    /// Metodo para el calculo del algoritmo completo a partir del conjunto de capas Raster
    /// </summary>
    /// <param name="pRasters"></param>
    /// <param name="sigpi"></param>
    public void AlgoritmoCompleto2(IRaster[] pRasters, SIGPICls sigpi)
    {
      if (pRasters.GetUpperBound(0) < 2)
      {
        throw new Exception("Definicion de modelos incompleta");
      }


      SIGPIParametros parametros = sigpi.Parametros;
      IMapAlgebraOp pMapAlgebraOp = new RasterMapAlgebraOpClass();
      IRasterAnalysisEnvironment pRasterAnalysisEnv = (IRasterAnalysisEnvironment)pMapAlgebraOp;
      // temperatura, precipitacion, accesibilidad, am_pendiente, am_radiacion, am_viento, frecuencia
      IWorkspaceFactory pWorkspaceFactory;
      IWorkspace pWorkspace;
      pWorkspaceFactory = new RasterWorkspaceFactoryClass();
      pWorkspace = pWorkspaceFactory.OpenFromFile(parametros.RutaSIGPI + "\\" + parametros.Raster, 0);
      pRasterAnalysisEnv.OutWorkspace = pWorkspace;
      pMapAlgebraOp.BindRaster((IGeoDataset)pRasters[0], "R1"); // Precipitacion
      pMapAlgebraOp.BindRaster((IGeoDataset)pRasters[1], "R2"); // Temperatura
      pMapAlgebraOp.BindRaster((IGeoDataset)pRasters[2], "R3"); // Amenazas parciales


      IRaster pOutRaster;

      pOutRaster = (IRaster)pMapAlgebraOp.Execute("([R1] * 0.29) + ([R2] * 0.24) + ([R3] * 0.47)");
      string sPrefijo1 = sigpi.FechaProcesamiento.ToString("ddMMMyy").ToUpper();
      string sPrefijo2 = sigpi.FechaProcesamiento.ToString("MM");
      string sNombreRaster = "M" + sPrefijo2 + "-" + sPrefijo1 + "_v2";
      IRasterBandCollection pRasterBandColl = (IRasterBandCollection)pOutRaster;
      IRasterWorkspace pRasterWorkspace = (IRasterWorkspace)pWorkspace;
      bool bExiste = false;
      try
      {
        IRasterDataset pRasterDS = pRasterWorkspace.OpenRasterDataset(sNombreRaster);
        bExiste = true;
      }
      catch (Exception)
      {
        bExiste = false;
      }
      if (!bExiste)
      {
        pRasterBandColl.SaveAs(parametros.RutaSIGPI + "\\" + parametros.Raster + "\\" + sNombreRaster + sOutputExtension, null, sOutputFormat);
      }
      else
      {
        throw new Exception("El modelo " + sNombreRaster + " ya existe");
      }
    }

    /// <summary>
    /// Genera el modelo de la probabilidad calculada, indicando el numero de dias
    /// </summary>
    /// <param name="NumeroDeDias"></param>
    /// <param name="sigpi"></param>
    public IRasterBandCollection ProbabilidadCalculada(string ModeloBase, int NumeroDeDias, SIGPICls sigpi, EnumGuardarProbabilidad pEnumGuardar)
    {

      DateTime date;
      IRaster[] pRasters = new IRaster[NumeroDeDias];
      IRaster pRasterTemp;
      string[] nombreCapas = new string[NumeroDeDias];
      string sMonth = ModeloBase.Substring(1, 2);  //sigpi.FechaProcesamiento.ToString("MM");
      DateTime fechaProbabilidad = new DateTime(System.Convert.ToInt16("20" + ModeloBase.Substring(ModeloBase.Length - 2, 2)),
                                    Convert.ToInt16(sMonth), Convert.ToInt16(ModeloBase.Substring(4, 2)));

      string sDirRaster = sigpi.Parametros.RutaSIGPI + "\\" + sigpi.Parametros.Resultado;
      string sNombreRasterFinal = "P" + sMonth + "-" + fechaProbabilidad.ToString("ddMMMyy").ToUpper() + "D" + NumeroDeDias.ToString() + "";
      string sRasterFinal = sDirRaster + "\\" + sNombreRasterFinal;

      for (int i = 0; i < NumeroDeDias; i++)
      {
        date = fechaProbabilidad.AddDays(-i);
        if (date.Month < 10)
        {
          sMonth = "0" + date.Month.ToString();
        }
        else
        {
          sMonth = date.Month.ToString();
        }

        nombreCapas[i] = "M" + sMonth + "-" + date.ToString("ddMMMyy").ToUpper();
        try
        {
          //System.Windows.Forms.MessageBox.Show(sigpi.Parametros.RutaSIGPI + "\\" + sigpi.Parametros.Raster, nombreCapas[i]);
          pRasterTemp = SIGPIUtils.AbrirRasterDesdeArchivo(sigpi.Parametros.RutaSIGPI + "\\" + sigpi.Parametros.Raster, nombreCapas[i]);
          if (pRasterTemp == null)
            System.Windows.Forms.MessageBox.Show(sigpi.Parametros.RutaSIGPI + "\\" + sigpi.Parametros.Raster, nombreCapas[i] + "\\" + "Raster no encontrado");

        }
        catch (Exception ex)
        {

          throw new Exception(ex.Message);
          return null;
        }
        pRasters[i] = pRasterTemp;

      }
      IMapAlgebraOp pMapAlgebraOp = new RasterMapAlgebraOpClass();
      IRasterAnalysisEnvironment pRasterAnalysisEnv = (IRasterAnalysisEnvironment)pMapAlgebraOp;


      string sExpresion;
      string[] sRasters = new string[NumeroDeDias];
      for (int i = 0; i < NumeroDeDias; i++)
      {
        pMapAlgebraOp.BindRaster((IGeoDataset)pRasters[i], "R" + i.ToString());
        sRasters[i] = "[R" + i.ToString() + "]";
      }
      sExpresion = String.Join(" + ", sRasters, 0, sRasters.GetLength(0));
      IRaster pOutRaster = (IRaster)pMapAlgebraOp.Execute(sExpresion);
      IRasterBandCollection pRasterBandColl = (IRasterBandCollection)pOutRaster;

      IWorkspaceFactory pWorkspaceFactory = new RasterWorkspaceFactoryClass();
      IRasterWorkspace pRasterWorkspace = (IRasterWorkspace)pWorkspaceFactory.OpenFromFile(sDirRaster, 0);
      bool bExiste = false;
      IRasterDataset pRasterDS = null;
      try
      {
        IWorkspace2 pWS = pRasterWorkspace as IWorkspace2;
        //if (pWS.get_NameExists(esriDatasetType.esriDTRasterDataset, sNombreRasterFinal))
        //{
        pRasterDS = pRasterWorkspace.OpenRasterDataset(sNombreRasterFinal);
        bExiste = true;
        //}
      }
      catch (Exception)
      {
        bExiste = false;
      }

      if (bExiste & pEnumGuardar == EnumGuardarProbabilidad.Sobreescribir)
      {
        IDataset pDataSet = pRasterDS as IDataset;
        try
        {
          pDataSet.Delete();
          bExiste = false;
        }
        catch (Exception ex)
        {

          throw new Exception("No se pudo eliminar el modelo de probabilidad existente. mensaje: " + ex.Message);
        }
      }
      else if (bExiste & pEnumGuardar == EnumGuardarProbabilidad.CargarExistente)
      {
        pRasterBandColl = (IRasterBandCollection)pRasterDS.CreateDefaultRaster();
      }

      if (!bExiste)
      {
        try
        {
          pRasterBandColl.SaveAs(sRasterFinal, null, "GRID");
        }
        catch (Exception ex)
        {
          throw new Exception("No se pudo crear la capa, verifique el directorio de almacenamiento. Mensaje: " + ex.Message);
        }
      }

      return pRasterBandColl;
    }

    /// <summary>
    /// Exporta a Excel
    /// </summary>
    /// <param name="ModeloBase"></param>
    /// <param name="sRutaModelos"></param>
    /// <param name="sigpi"></param>
    /// <param name="_excelApp"></param>
    /// <returns></returns>
    public Worksheet ExportarAExcel(string ModeloBase, string sRutaModelos, SIGPICls sigpi, Microsoft.Office.Interop.Excel.Application _excelApp)
    {
      string sMonth = ModeloBase.Substring(1, 2);  //sigpi.FechaProcesamiento.ToString("MM");
      DateTime fechaProbabilidad = new DateTime(System.Convert.ToInt16("20" + ModeloBase.Substring(9, 2)),
                                    Convert.ToInt16(sMonth), Convert.ToInt16(ModeloBase.Substring(4, 2)));

      string sDirRaster = sigpi.Parametros.RutaSIGPI + "\\" + sigpi.Parametros.Resultado;
      IWorkspaceFactory pWF = new RasterWorkspaceFactoryClass();
      IRasterWorkspace pRW;
      try
      {
        pRW = (IRasterWorkspace)pWF.OpenFromFile(sRutaModelos, 0);
      }
      catch (Exception ex)
      {

        throw new Exception(ex.Message); ;
      }

      IRasterDataset pRasterDS = pRW.OpenRasterDataset(ModeloBase);
      IRaster pRaster = pRasterDS.CreateDefaultRaster();
      IGeoDataset pGeoDS = (IGeoDataset)pRaster;

      double dXMin, dYMax, dX, dY, dCellSizeX, dCellSizeY;
      int iCellValue;

      IRasterProps pRasterProp = (IRasterProps)pRaster;

      IPnt pCellSize = pRasterProp.MeanCellSize();
      dCellSizeX = pCellSize.X;
      dCellSizeY = pCellSize.Y;

      dXMin = pGeoDS.Extent.XMin + (dCellSizeX / 2);
      dYMax = pGeoDS.Extent.YMax - (dCellSizeY / 2);

      ESRI.ArcGIS.Geometry.IPoint pPoint = new PointClass();

      IRasterCursor pRasterCursor = pRaster.CreateCursor();
      IPixelBlock pBlock;

      string sRuta = sigpi.Parametros.RutaSIGPI + "\\" + "plantillas" + "\\" + "plantilla presentacion datos probabilidad.xls";

      Workbook workBook = _excelApp.Workbooks.Open(sRuta, Type.Missing, Type.Missing, Type.Missing,
                          Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                          Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

      Worksheet sheet = (Worksheet)workBook.Sheets["DATOS_PROBABILIDAD"];
      object objVal;
      Microsoft.Office.Interop.Excel.Range range;
      object[,] objValues = new object[1, 6];
      int k = 2;

      IWorkspaceFactory pWF2 = new AccessWorkspaceFactoryClass();
      IWorkspace pWorkspace2 = pWF2.OpenFromFile(sigpi.Parametros.RutaGBD, 0);
      IFeatureWorkspace pFWorkspace = (IFeatureWorkspace)pWorkspace2;
      string sCapaMunicipios = "municipios";
      string sCapaCobertura = "cober2003";
      string sCapaParques = "parques_naturales";

      IFeatureClass pFCMunicipios = pFWorkspace.OpenFeatureClass(sCapaMunicipios);
      IFeatureClass pFCCobertura = pFWorkspace.OpenFeatureClass(sCapaCobertura);
      IFeatureClass pFCParques = pFWorkspace.OpenFeatureClass(sCapaParques);
      int lFieldMpio = pFCMunicipios.FindField("MUNICIPIO");
      int lFieldCabecera = pFCMunicipios.FindField("CABECERA");
      int lFieldDepto = pFCMunicipios.FindField("DEPARTAMEN");
      int lFieldCobertura = pFCCobertura.FindField("NOMBRE");
      int lFieldParque = pFCParques.FindField("NOMBRE");

      IFeatureCursor pFCursor1;
      IFeature pFeature1;

      IFeatureCursor pFCursor2;
      IFeature pFeature2;

      IFeatureCursor pFCursor3;
      IFeature pFeature3;

      ISpatialFilter pSpaFil = new SpatialFilterClass();
      pSpaFil.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
      pSpaFil.GeometryField = "SHAPE";
      dY = 0;
      if (pRasterCursor != null)
      {
        do
        {
          pBlock = pRasterCursor.PixelBlock;
          if (dY > 0)
          {
            dYMax = dY - dCellSizeY;
          }
          for (int i = 0; i < pBlock.Width; i++)
          {
            for (int j = 0; j < pBlock.Height; j++)
            {
              dY = dYMax - (j * dCellSizeY);
              dX = dXMin + (i * dCellSizeX);
              pPoint.PutCoords(dX, dY);
              pSpaFil.Geometry = (IGeometry)pPoint;
              if (pBlock.GetVal(0, i, j) != null)
              {
                objVal = pBlock.GetVal(0, i, j);
                iCellValue = System.Convert.ToInt32(objVal);
                if (iCellValue > 0)
                {
                  range = sheet.get_Range("A" + k.ToString(), "E" + k.ToString());
                  objValues[0, 0] = iCellValue;

                  pFCursor1 = pFCMunicipios.Search(pSpaFil, true);
                  pFeature1 = pFCursor1.NextFeature();
                  if (pFeature1 != null)
                  {
                    objValues[0, 1] = pFeature1.get_Value(lFieldMpio);
                    objValues[0, 2] = pFeature1.get_Value(lFieldCabecera);
                    objValues[0, 3] = pFeature1.get_Value(lFieldDepto);
                  }

                  pFCursor2 = pFCCobertura.Search(pSpaFil, true);
                  pFeature2 = pFCursor2.NextFeature();
                  if (pFeature2 != null)
                  {
                    objValues[0, 4] = pFeature2.get_Value(lFieldCobertura);
                  }

                  pFCursor3 = pFCParques.Search(pSpaFil, true);
                  pFeature3 = pFCursor3.NextFeature();
                  if (pFeature3 != null)
                  {
                    objValues[0, 5] = pFeature3.get_Value(lFieldParque);
                  }

                  range.set_Value(Type.Missing, objValues);
                  k++;
                  if (k % 30 == 0)
                    GC.Collect();
                  if (k > 1000)
                  {
                    return sheet;
                  }
                }
              }
            }
          }

        }
        while (pRasterCursor.Next());
      }

      return sheet;


    }

    /// <summary>
    /// Reporta a Excel
    /// </summary>
    /// <param name="pRaster"></param>
    /// <param name="sigpi"></param>
    /// <param name="_excelApp"></param>
    /// <returns></returns>
    public Worksheet ExportarAExcel2(IRaster pRaster, SIGPICls sigpi, Microsoft.Office.Interop.Excel.Application _excelApp)
    {

      string sDirRaster = sigpi.Parametros.RutaSIGPI + "\\" + sigpi.Parametros.Resultado;

      IGeoDataset pGeoDS = (IGeoDataset)pRaster;

      double dXMin, dYMax, dX, dY, dCellSizeX, dCellSizeY;
      int iCellValue;

      IRasterProps pRasterProp = (IRasterProps)pRaster;

      IPnt pCellSize = pRasterProp.MeanCellSize();
      dCellSizeX = pCellSize.X;
      dCellSizeY = pCellSize.Y;

      dXMin = pGeoDS.Extent.XMin + (dCellSizeX / 2);
      dYMax = pGeoDS.Extent.YMax - (dCellSizeY / 2);

      ESRI.ArcGIS.Geometry.IPoint pPoint = new PointClass();

      IRasterCursor pRasterCursor = pRaster.CreateCursor();
      IPixelBlock pBlock;

      string sRuta = sigpi.Parametros.RutaSIGPI + "\\" + "plantillas" + "\\" + "plantilla presentacion datos probabilidad.xls";
      System.Windows.Forms.MessageBox.Show("Plantilla: " + sRuta);
      Workbook workBook = _excelApp.Workbooks.Open(sRuta, Type.Missing, Type.Missing, Type.Missing,
                          Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                          Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

      Worksheet sheet = (Worksheet)workBook.Sheets["DATOS_PROBABILIDAD"];
      object objVal;
      Microsoft.Office.Interop.Excel.Range range;
      object[,] objValues = new object[1, 6];
      int k = 2;

      IWorkspaceFactory pWF2 = new AccessWorkspaceFactoryClass();
      IWorkspace pWorkspace2 = pWF2.OpenFromFile(sigpi.Parametros.RutaGBD, 0);
      IFeatureWorkspace pFWorkspace = (IFeatureWorkspace)pWorkspace2;
      string sCapaMpios_parques_cobertura = "mpios_parques_cobertura";


      IFeatureClass pFC = pFWorkspace.OpenFeatureClass(sCapaMpios_parques_cobertura);

      int lFieldMpio = pFC.FindField("MUNICIPIO");
      int lFieldCabecera = pFC.FindField("CABECERA");
      int lFieldDepto = pFC.FindField("DEPARTAMEN");
      int lFieldCobertura = pFC.FindField("COBERTURA");
      int lFieldParque = pFC.FindField("NOMBRE_PARQUE");

      IFeatureCursor pFCursor1;
      IFeature pFeature1 = null;

      ISpatialFilter pSpaFil = new SpatialFilterClass();
      pSpaFil.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
      pSpaFil.GeometryField = "SHAPE";
      IRelationalOperator pRelOp = null;
      dY = 0;

      if (pRasterCursor != null)
      {
        do
        {
          pBlock = pRasterCursor.PixelBlock;
          if (dY > 0)
          {
            dYMax = dY - dCellSizeY;
          }
          for (int i = 0; i < pBlock.Width; i++)
          {
            for (int j = 0; j < pBlock.Height; j++)
            {
              dY = dYMax - (j * dCellSizeY);
              dX = dXMin + (i * dCellSizeX);
              pPoint.PutCoords(dX, dY);
              pSpaFil.Geometry = (IGeometry)pPoint;
              if (pBlock.GetVal(0, i, j) != null)
              {
                objVal = pBlock.GetVal(0, i, j);
                iCellValue = System.Convert.ToInt32(objVal);
                if (iCellValue > 0)
                {
                  if (k == 2)
                  {
                    try
                    {
                      pFCursor1 = pFC.Search(pSpaFil, false);
                    }
                    catch (Exception ex)
                    {
                      throw new Exception(ex.Message);
                    }
                    pFeature1 = pFCursor1.NextFeature();
                    if (pFeature1 != null)
                    {
                      pRelOp = (IRelationalOperator)pFeature1.Shape;
                    }
                  }
                  if (pRelOp.Contains((IGeometry)pPoint))
                  {
                    if (k == 2)
                    {
                      range = sheet.get_Range("A" + k.ToString(), "E" + k.ToString());
                      objValues[0, 0] = iCellValue;
                      objValues[0, 1] = pFeature1.get_Value(lFieldMpio);
                      objValues[0, 2] = pFeature1.get_Value(lFieldCabecera);
                      objValues[0, 3] = pFeature1.get_Value(lFieldDepto);
                      objValues[0, 4] = pFeature1.get_Value(lFieldCobertura);
                      objValues[0, 5] = pFeature1.get_Value(lFieldParque);
                      range.set_Value(Type.Missing, objValues);
                      k++;
                    }
                    else
                    {
                      if (!((System.Convert.ToString(objValues[0, 1]) == System.Convert.ToString(pFeature1.get_Value(lFieldMpio))) &
                          ((System.Convert.ToString(objValues[0, 2])) == System.Convert.ToString(pFeature1.get_Value(lFieldCabecera).ToString())) &
                          ((System.Convert.ToString(objValues[0, 3])) == System.Convert.ToString(pFeature1.get_Value(lFieldDepto).ToString())) &
                          ((System.Convert.ToString(objValues[0, 4])) == System.Convert.ToString(pFeature1.get_Value(lFieldCobertura).ToString())) &
                          ((System.Convert.ToString(objValues[0, 5])) == System.Convert.ToString(pFeature1.get_Value(lFieldParque).ToString()))))
                      {
                        range = sheet.get_Range("A" + k.ToString(), "E" + k.ToString());
                        objValues[0, 0] = iCellValue;
                        objValues[0, 1] = pFeature1.get_Value(lFieldMpio);
                        objValues[0, 2] = pFeature1.get_Value(lFieldCabecera);
                        objValues[0, 3] = pFeature1.get_Value(lFieldDepto);
                        objValues[0, 4] = pFeature1.get_Value(lFieldCobertura);
                        objValues[0, 5] = pFeature1.get_Value(lFieldParque);
                        range.set_Value(Type.Missing, objValues);

                        k++;
                        Console.WriteLine(k.ToString());

                        if (k % 500 == 0)
                          GC.Collect();
                        if (k > 33000)
                        {
                          return sheet;
                        }
                      }
                    }
                  }
                  else
                  {
                    try
                    {
                      pFCursor1 = pFC.Search(pSpaFil, false);
                    }
                    catch (Exception ex)
                    {
                      throw new Exception(ex.Message);
                    }
                    pFeature1 = pFCursor1.NextFeature();
                    if (pFeature1 != null)
                    {
                      pRelOp = (IRelationalOperator)pFeature1.Shape;
                      if (pRelOp.Contains((IGeometry)pPoint))
                      {
                        if (k == 2)
                        {
                          range = sheet.get_Range("A" + k.ToString(), "E" + k.ToString());
                          objValues[0, 0] = iCellValue;
                          objValues[0, 1] = pFeature1.get_Value(lFieldMpio);
                          objValues[0, 2] = pFeature1.get_Value(lFieldCabecera);
                          objValues[0, 3] = pFeature1.get_Value(lFieldDepto);
                          objValues[0, 4] = pFeature1.get_Value(lFieldCobertura);
                          objValues[0, 5] = pFeature1.get_Value(lFieldParque);
                          range.set_Value(Type.Missing, objValues);
                          k++;
                        }
                        else
                        {
                          if (!((System.Convert.ToString(objValues[0, 1]) == System.Convert.ToString(pFeature1.get_Value(lFieldMpio))) &
                              ((System.Convert.ToString(objValues[0, 2])) == System.Convert.ToString(pFeature1.get_Value(lFieldCabecera).ToString())) &
                              ((System.Convert.ToString(objValues[0, 3])) == System.Convert.ToString(pFeature1.get_Value(lFieldDepto).ToString())) &
                              ((System.Convert.ToString(objValues[0, 4])) == System.Convert.ToString(pFeature1.get_Value(lFieldCobertura).ToString())) &
                              ((System.Convert.ToString(objValues[0, 5])) == System.Convert.ToString(pFeature1.get_Value(lFieldParque).ToString()))))
                          {
                            range = sheet.get_Range("A" + k.ToString(), "E" + k.ToString());
                            objValues[0, 0] = iCellValue;
                            objValues[0, 1] = pFeature1.get_Value(lFieldMpio);
                            objValues[0, 2] = pFeature1.get_Value(lFieldCabecera);
                            objValues[0, 3] = pFeature1.get_Value(lFieldDepto);
                            objValues[0, 4] = pFeature1.get_Value(lFieldCobertura);
                            objValues[0, 5] = pFeature1.get_Value(lFieldParque);
                            range.set_Value(Type.Missing, objValues);

                            k++;
                            Console.WriteLine(k.ToString());

                            if (k % 500 == 0)
                              GC.Collect();
                            if (k > 33000)
                            {
                              GC.Collect();
                              return sheet;
                            }
                          }
                        }

                      }
                    }
                  }
                }
              }
            }
          }

        }
        while (pRasterCursor.Next());
      }

      return sheet;


    }

    /// <summary>
    /// Exporta a Excel
    /// </summary>
    /// <param name="pFC"></param>
    /// <param name="sRuta"></param>
    /// <param name="_excelApp"></param>
    /// <returns></returns>
    public Worksheet ExportaAExcel3(IFeatureClass pFC, string sRuta, Microsoft.Office.Interop.Excel.Application _excelApp)
    {
      Workbook workBook = _excelApp.Workbooks.Open(sRuta, Type.Missing, Type.Missing, Type.Missing,
                          Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                          Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

      Worksheet sheet = (Worksheet)workBook.Sheets["DATOS_PROBABILIDAD"];
      object objVal;
      Microsoft.Office.Interop.Excel.Range range;
      object[,] objValues = new object[1, 6];
      int k = 2;

      int lFieldCelda = pFC.FindField("GRIDCODE");
      int lFieldMpio = pFC.FindField("MUNICIPIO");
      int lFieldCabecera = pFC.FindField("CABECERA");
      int lFieldDepto = pFC.FindField("DEPARTAMEN");
      int lFieldCobertura = pFC.FindField("COBERTURA");
      int lFieldParque = pFC.FindField("NOMBRE_PAR");

      IFeatureCursor pFCursor1;
      IFeature pFeature1 = null;

      pFCursor1 = pFC.Search(null, false);
      pFeature1 = pFCursor1.NextFeature();

      while (pFeature1 != null)
      {
        range = sheet.get_Range("A" + k.ToString(), "E" + k.ToString());
        objValues[0, 0] = pFeature1.get_Value(lFieldCelda);
        objValues[0, 1] = pFeature1.get_Value(lFieldMpio);
        objValues[0, 2] = pFeature1.get_Value(lFieldCabecera);
        objValues[0, 3] = pFeature1.get_Value(lFieldDepto);
        objValues[0, 4] = pFeature1.get_Value(lFieldCobertura);
        objValues[0, 5] = pFeature1.get_Value(lFieldParque);
        range.set_Value(Type.Missing, objValues);
        k++;
        pFeature1 = pFCursor1.NextFeature();
      }

      return sheet;
    }

    /// <summary>
    /// Reclasifica los raster generados con valores de 1 a 5
    /// </summary>
    /// <param name="pRaster"></param>
    /// <param name="iNumClases"></param>
    /// <param name="sPrefijo"></param>
    /// <param name="parametros"></param>
    /// <returns></returns>
    public IRaster Reclasificar(IRaster pRaster, int iNumClases, string sPrefijo, SIGPIParametros parametros, bool bInvertirValores)
    {
      IReclassOp pReclassOp = new RasterReclassOpClass();
      object missing = Type.Missing;
      IRaster pOutRaster = (IRaster)pReclassOp.Slice((IGeoDataset)pRaster, esriGeoAnalysisSliceEnum.esriGeoAnalysisSliceEqualInterval,
                            iNumClases, ref missing);
      IRasterBandCollection pRasterBandColl = null;
      if (bInvertirValores)
      {
        INumberRemap pNumberRemap = new NumberRemapClass();
        pNumberRemap.MapValue(1, 5);
        pNumberRemap.MapValue(2, 4);
        pNumberRemap.MapValue(3, 3);
        pNumberRemap.MapValue(4, 2);
        pNumberRemap.MapValue(5, 1);

        IRaster pOutRaster2 = (IRaster)pReclassOp.ReclassByRemap((IGeoDataset)pOutRaster, (IRemap)pNumberRemap, false);
        pRasterBandColl = (IRasterBandCollection)pOutRaster2;
      }
      else
      {
        pRasterBandColl = (IRasterBandCollection)pOutRaster;
      }


      string sFormatLayers = DateTime.Now.ToString("yyyyMMdd-HHmmss");
      string prefijo = "RECLASS_" + sPrefijo;
      string sData = parametros.RutaSIGPI + parametros.Temporal + "\\" + prefijo + "-" + sFormatLayers + sOutputExtension; ;
      pRasterBandColl.SaveAs(sData, null, sOutputFormat);
      return pOutRaster;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pRaster"></param>
    /// <param name="iNumClases"></param>
    /// <param name="sPrefijo"></param>
    /// <param name="parametros"></param>
    /// <param name="bInvertirValores"></param>
    /// <returns></returns>
    public IRaster ReclasificarDesdeTabla(IRaster pRaster, ITable pTable,
                                          string sFromField, string sToField,
                                          string sOutField, SIGPIParametros parametros,
                                          string sPrefijo, bool bPermanente)
    {


      IReclassOp pReclassOp = new RasterReclassOpClass();
      IRaster pOutRaster;
      IRasterBandCollection pRasterBandColl;
      pOutRaster = (IRaster)pReclassOp.Reclass((IGeoDataset)pRaster, pTable, sFromField, sToField, sOutField, true);


      string sFormatLayers = DateTime.Now.ToString("yyyyMMdd-HHmmss");
      string prefijo = "RECLASS_" + sPrefijo;
      string sData = parametros.RutaSIGPI + parametros.Temporal + "\\" + prefijo + "-" + sFormatLayers + sOutputExtension; ;
      pRasterBandColl = (IRasterBandCollection)pOutRaster;
      if (bPermanente)
        pRasterBandColl.SaveAs(sData, null, sOutputFormat);
      return pOutRaster;
    }

    /// <summary>
    /// Construye FeatureClass de Precipitaciones
    /// </summary>
    /// <param name="sNombreTabla"></param>
    /// <param name="spatialReference"></param>
    /// <returns></returns>
    public IFeatureClass FCPrecipitacion(string sNombreTabla, ISpatialReference spatialReference)
    {
      //string sNombreTabla = "TMPPR_" + sSufijo;
      //try
      //{
      //  sigpiDao.EjecutarSentenciaSinQuery("DROP TABLE " + sNombreTabla);          
      //}
      //catch (Exception ex)
      //{
      //  //System.Windows.Forms.MessageBox.Show(ex.Message);
      //}

      //string sSQL = "SELECT LECTUS_PRECI.CODIGO, LECTUS_PRECI.FECHA, ESTACIONES_1.X, ESTACIONES_1.[Y], " + 
      //              "IIf([LECTUS_PRECI]![LECTURA]<=2,5,IIf([LECTUS_PRECI]![LECTURA]<=8 And [LECTUS_PRECI]![LECTURA]>2,4,IIf([LECTUS_PRECI]![LECTURA]<=14 And [LECTUS_PRECI]![LECTURA]>8,3,IIf([LECTUS_PRECI]![LECTURA]<=24 And [LECTUS_PRECI]![LECTURA]>14,2,IIf([LECTUS_PRECI]![LECTURA]>24,1,0))))) AS LECTURAS " + 
      //              "INTO " + sNombreTabla + " " +
      //              "FROM LECTUS_PRECI INNER JOIN ESTACIONES_1 ON LECTUS_PRECI.CODIGO = ESTACIONES_1.CODIGO " +
      //              "WHERE (((LECTUS_PRECI.FECHA)=#" + Fecha.ToString("MM/dd/yyyy")  + "#))";

      //try
      //{
      //  sigpiDao.EjecutarSentenciaSinQuery(sSQL);    

      //}
      //catch (Exception ex)
      //{          
      //  throw new Exception (ex.Message);
      //}

      SIGPIParametros parametros = _parametros;

      IPropertySet propertySetTable = new PropertySetClass();
      propertySetTable.SetProperty("DATABASE", parametros.RutaBD);
      IWorkspace pWorkspace = SIGPIUtils.ConectarAGeodatabase(propertySetTable);

      IFeatureWorkspace pFW = (IFeatureWorkspace)pWorkspace;

      ITable pTable;

      try
      {
        pTable = pFW.OpenTable(sNombreTabla);
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

      // Create and populate the XY Event2Fields Properties object.  
      IXYEvent2FieldsProperties xyEvent2FieldsProperties = new XYEvent2FieldsPropertiesClass();
      xyEvent2FieldsProperties.XFieldName = "X";
      xyEvent2FieldsProperties.YFieldName = "Y";
      // xyEvent2FieldsProperties.ZFieldName = "Z"; // This property is optional.
      // Get a name object for the source table.  
      IDataset sourceDataset = (IDataset)pTable;
      ESRI.ArcGIS.esriSystem.IName sourceName = sourceDataset.FullName;
      // Create an event source name using the properties and the spatial reference.  
      IXYEventSourceName xyEventSourceName = new XYEventSourceNameClass();
      xyEventSourceName.EventProperties = xyEvent2FieldsProperties;
      xyEventSourceName.EventTableName = sourceName;
      xyEventSourceName.SpatialReference = spatialReference;
      // Create the XY event source.  
      ESRI.ArcGIS.esriSystem.IName name = (ESRI.ArcGIS.esriSystem.IName)xyEventSourceName;
      IXYEventSource xyEventSource = (IXYEventSource)name.Open();
      // Cast the event source to the IFeatureClass interface.  
      IFeatureClass featureClass = (IFeatureClass)xyEventSource;
      return featureClass;

    }


    /// <summary>
    /// Interpola utilizando el metodo IDW
    /// </summary>
    /// <param name="sInputFC"></param>
    /// <param name="sCampo"></param>
    /// <param name="sFCMask"></param>
    /// <param name="sOutputFC"></param>
    public void InterpolarIDW(string sInputFC, string sCampo,
                              string sFCMask, string sOutputFC)
    {
      Geoprocessor gp = new Geoprocessor();
      gp.SetEnvironmentValue("Mask", sFCMask);
      ESRI.ArcGIS.SpatialAnalystTools.Idw idw = new Idw();
      
      idw.in_point_features = sInputFC;
      idw.z_field = sCampo;
      idw.out_raster = sOutputFC;
      idw.cell_size = 4000;
      gp.Execute(idw, null);
    }


    #endregion


  }
}
