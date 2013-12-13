import arcpy
from arcpy import env
from arcpy.sa import *



base_tmp_dir = "PROCESS_MODIS_NVI_AGS_BASE_TMP_DIR\"
in_raster = "PROCESS_MODIS_NVI_AGS_BASE_TMP_DIR\" + "tmpMosaic.500m_16_days_NDVI.tif"
in_raster_copy = "PROCESS_MODIS_NVI_AGS_BASE_TMP_DIR\" + "tmpMosaic.500m_16_days_NDVI_.tif"
out_raster = base_tmp_dir + "c500m_16_days_NDVI_ProjectRa2.tif"
out_prj = "GEOGCS['GCS_MAGNA',DATUM['D_MAGNA',SPHEROID['GRS_1980',6378137.0,298.257222101]],PRIMEM['Greenwich',0.0],UNIT['Degree',0.0174532925199433],METADATA['Colombia',-84.77,-4.24,-66.87,15.5,0.0,0.0174532925199433,0.0,1070]]"
out_prj_wgs84="GEOGCS['GCS_WGS_1984',DATUM['D_WGS_1984',SPHEROID['WGS_1984',6378137.0,298.257223563]],PRIMEM['Greenwich',0.0],UNIT['Degree',0.0174532925199433],METADATA['World',-180.0,-90.0,180.0,90.0,0.0,0.0174532925199433,0.0,1262]]"
resampling = "NEAREST"
cell_size = "4,67096359025706E-03"
geo_transform = "MAGNA_To_WGS_1984_1"
reg_point = "#"
in_prj = "PROJCS['SINUSOIDAL_Unspecified_Datum_Semi_major_axis_6371007_181000_Semi_minor_axis_0_0',GEOGCS['',DATUM['D_unknown',SPHEROID['Unknown',6371007.181,0.0]],PRIMEM['Greenwich',0.0],UNIT['Degree',0.0174532925199433]],PROJECTION['Sinusoidal'],PARAMETER['false_easting',0.0],PARAMETER['false_northing',0.0],PARAMETER['central_meridian',0.0],UNIT['Meter',1.0]]"

ndvi_factor = "10000"
out_float_raster = base_tmp_dir + "raster_float.tif"

web_mercator_aux_sphere_prj = "PROJCS['WGS_1984_Web_Mercator_Auxiliary_Sphere',GEOGCS['GCS_WGS_1984',DATUM['D_WGS_1984',SPHEROID['WGS_1984',6378137.0,298.257223563]],PRIMEM['Greenwich',0.0],UNIT['Degree',0.0174532925199433]],PROJECTION['Mercator_Auxiliary_Sphere'],PARAMETER['False_Easting',0.0],PARAMETER['False_Northing',0.0],PARAMETER['Central_Meridian',0.0],PARAMETER['Standard_Parallel_1',0.0],PARAMETER['Auxiliary_Sphere_Type',0.0],UNIT['Meter',1.0]]"
out_raster2= base_tmp_dir + "tmpMosaic.500m_16_days_NDVI_geo_magna.tif"
out_raster3= base_tmp_dir + "tmpMosaic.500m_16_days_NDVI_prj_magna.tif"
out_prj2="PROJCS['MAGNA_Colombia_Bogota',GEOGCS['GCS_MAGNA',DATUM['D_MAGNA',SPHEROID['GRS_1980',6378137.0,298.257222101]],PRIMEM['Greenwich',0.0],UNIT['Degree',0.0174532925199433]],PROJECTION['Transverse_Mercator'],PARAMETER['False_Easting',1000000.0],PARAMETER['False_Northing',1000000.0],PARAMETER['Central_Meridian',-74.07750791666666],PARAMETER['Scale_Factor',1.0],PARAMETER['Latitude_Of_Origin',4.596200416666666],UNIT['Meter',1.0]]"
cell_size2="528,066843467686"
prj_sinusoidal="PROJCS['Sphere_Sinusoidal',GEOGCS['GCS_Sphere',DATUM['D_Sphere',SPHEROID['Sphere',6371000.0,0.0]],PRIMEM['Greenwich',0.0],UNIT['Degree',0.0174532925199433]],PROJECTION['Sinusoidal'],PARAMETER['False_Easting',0.0],PARAMETER['False_Northing',0.0],PARAMETER['Central_Meridian',0.0],UNIT['Meter',1.0]]"

#arcpy.Delete_management(in_raster_copy,"#")
arcpy.CopyRaster_management(in_raster,in_raster_copy,"#","#","#","NONE","NONE","#","NONE","NONE")

arcpy.DefineProjection_management(in_raster,prj_sinusoidal)
arcpy.ProjectRaster_management(in_raster,out_raster,out_prj_wgs84,"NEAREST","0,004670969987915","#","#",prj_sinusoidal)

arcpy.ProjectRaster_management(out_raster,out_raster2,out_prj,resampling,cell_size,geo_transform,reg_point,out_prj_wgs84)

arcpy.ProjectRaster_management(out_raster2,out_raster3,out_prj2,resampling,cell_size2,"#","#",out_prj)

arcpy.CheckOutExtension("Spatial")

out_raster4= base_tmp_dir + "tmpMosaic.500m_16_days_NDVI_prj_magna_adjust.tif"
arcpy.gp.RasterCalculator_sa("Float('" + out_raster3 + "') / " + ndvi_factor,out_raster4)

arcpy.Delete_management(out_raster,"#")
arcpy.Delete_management(out_raster2,"#")
arcpy.Delete_management(out_raster3,"#")
arcpy.Delete_management(in_raster_copy,"#")
