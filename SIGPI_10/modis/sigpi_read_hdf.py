# script para descargar imagenes MODIS para Colombia
#
#  (c) Copyright IDEAM 2013
#  Authors: Edwin Piragauta Vargas
#  Email: edwin dot piragauta at gmail dot com
#
##################################################################
#
#  This MODIS Python script is licensed under the terms of GNU GPL 2.
#  This program is free software; you can redistribute it and/or
#  modify it under the terms of the GNU General Public License as
#  published by the Free Software Foundation; either version 2 of
#  the License, or (at your option) any later version.
#  This program is distributed in the hope that it will be useful,
#  but WITHOUT ANY WARRANTY; without even implied warranty of
#  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
#  See the GNU General Public License for more details.
#
##################################################################

import urllib2
from bs4 import BeautifulSoup

def downloadfile(url):
	file_name = url.split('/')[-1]
	u = urllib2.urlopen(url)
	f = open(file_name, 'wb')
	meta = u.info()
	file_size = int(meta.getheaders("Content-Length")[0])
	print "Descargando: %s Bytes: %s" % (file_name, file_size)

	file_size_dl = 0
	block_sz = 8192
	while True:
		buffer = u.read(block_sz)
		if not buffer:
			break

		file_size_dl += len(buffer)
		f.write(buffer)
		status = r"%10d  [%3.2f%%]" % (file_size_dl, file_size_dl * 100. / file_size)
		status = status + chr(8)*(len(status)+1)
		print status,

	f.close()
	return;
	
url = 'http://e4ftl01.cr.usgs.gov/MODIS_Composites/MOLT/MOD13A1.005/XXXX.XX.XX/'
response = urllib2.urlopen(url)
html=response.read()
soup=BeautifulSoup(html)
#  hdf_files =["h10v07","h11v07","h10v08","h11v08","h10v09","h11v09"]
hdf_files =["h10v08","h11v08"]

fo = open("list.txt","wb")
for a in soup.findAll("a"):
	for h in hdf_files:
		if h in a["href"]:
			href_file = a["href"]
			downloadfile(url +"/" + href_file)
			if (href_file[-3:] == "hdf"):
				fo.write(href_file + "\n")
			
		
fo.close()		

		
