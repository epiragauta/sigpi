%PYTHONHOME%\python sigpi_read_hdf.py
%PYTHONHOME%\Scripts\modis_mosaic.py -m "PATH_MODIS_APP" -o tmpMosaic list.txt
SET PATH=%PATH%;PATH_MODIS_APP\bin
SET MRT_DATA_DIR=PATH_MODIS_APP\data
resample -p params_geo.prm
%PYTHONHOME%\python process_modis_nvi_ags.py
copy NVI_RASTER_RESULT PATH_SIGPI_NVI