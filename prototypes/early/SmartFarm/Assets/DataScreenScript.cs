using UnityEngine;
using System;
using System.Collections.Generic;
//using System.Data;
//using System.Data.SQLite;
//using System.Data.Linq;
//using Mono.Data.Sqlite;
//using System.Windows.Forms;
using System.Xml;
using System.Collections;
using System.Threading;
using System.IO;
//using System.Drawing;
using System.IO.Compression;
using System.Text;
//using ICSharpCode.SharpZipLib.BZip2;
//This zip archive library was obtained from http://dotnetzip.codeplex.com/
using Ionic.Zip;
//This Tiff compression library was obtained from FWTools at http://trac.osgeo.org/gdal/wiki/FWTools
//using OSGeo;
//using OSGeo.GDAL;
//using System.Runtime.InteropServices;
//using System.Diagnostics;
//using Microsoft.Win32;

public class DataScreenScript : MonoBehaviour {
	string database = "epic";
	private float startTime;
    string textTime;
	
	string ReqOutput = "";
	
	string node = "";
	string url = "";
	string job = "";
	
	bool init_job = false;
	bool stat_ready = false;
	bool downloaded = false;
	bool clean = false;
	bool unzipped = false;
	bool toFile = false;
	bool decompressed = false;
	
	string toFtext = "";
	string stat_reply = "";
	string init_reply = "";
	string getData_reply = "";
	string cleanComp_reply = "";
	string unzip_stat = "";
	string toFileRep = "";
	
	// coordinate box around manhattan, ks
	string coordTOP = "39.32328";
	string coordBOTTOM = "39.04407";
	string coordLEFT = "-96.76656";
	string coordRIGHT = "-96.21642";
	
	// extra test coordinates (Silverthorne, CO)
	//string coordTOP = "39.66797";
	//string coordBOTTOM = "39.55248";
	//string coordLEFT = "-106.14075";
	//string coordRIGHT = "-105.97250";
	
	string TmpZipFile = "";
	string ReqDownInit = "";
	byte[] MapZipFile;
	
	//Dataset OrigImg;
	RequestValidationServiceService NedMapReq;
	// SQLiteDatabase db;
	// project scaner
	
	void Start () {		
        startTime = Time.time;  
    }

	// Update is called once per frame
	void Update () {
        int guiTime = (int)(Time.time - startTime);

 		int minutes = guiTime / 60;
        int seconds = guiTime % 60;
        int fraction = (guiTime * 100) % 100;
        int passTime = 10; // in seconds
		
		if(stat_ready && !downloaded){
			GetData(job);
		}
		if(!unzipped){
			if(downloaded){
				//unzip_stat = Zipper.UnzipBytes(MapZipFile);	
				//unzipped = true;
				if(!toFile){
					toFile = BytesToFile(TmpZipFile, MapZipFile);
					toFileRep = "saving to file " + TmpZipFile + " ... ";
					if(toFile) toFileRep = toFileRep + "success!";
					else toFileRep = toFileRep + "failed!";
					uzip(TmpZipFile);
				}
			}
		}
		if(toFile) toFtext = "exporting...";
	}

    void OnGUI()
    {
        
		if(!stat_ready) Status(job);
		
		GUI.Label(new Rect(20, 70, 90, 25), "TOP LAT");
		GUI.Label(new Rect(110, 70, 90, 25), "BOTTOM LAT");
		GUI.Label(new Rect(200, 70, 90, 25), "LEFT LONG");
		GUI.Label(new Rect(290, 70, 90, 25), "RIGHT LONG");
		
		coordTOP = GUI.TextField(new Rect(20, 90, 90, 25), coordTOP, 25);
		coordBOTTOM = GUI.TextField(new Rect(110, 90, 90, 25), coordBOTTOM, 25);
		coordLEFT = GUI.TextField(new Rect(200, 90, 90, 25), coordLEFT, 25);
		coordRIGHT = GUI.TextField(new Rect(290, 90, 90, 25), coordRIGHT, 25);
		
		if(GUI.Button(new Rect(385, 90, 80, 25), "GET MAP")){
			url = Request();
			InitDownload(url);
		}
		
		if(unzipped){
			if(GUI.Button(new Rect(475, 90, 80, 25), "RENDER!")){
				cleanComp_reply = TmpZipFile.Trim(".zip".ToCharArray());
				Decompress(TmpZipFile.Trim(".zip".ToCharArray()));
			}
		}
		
		GUI.TextArea(new Rect(20, 120, 450, 125), "url : \n" + url);
		GUI.TextArea(new Rect(20, 245, 450, 75), "init_reply : \n" + init_reply);
		GUI.TextArea(new Rect(20, 320, 450, 25), "job id : " + job);
		GUI.TextArea(new Rect(20, 345, 450, 60), "request status : \n" + stat_reply);
		GUI.TextArea(new Rect(20, 405, 450, 25), "get : " + getData_reply);
		toFtext = "";
		if(toFile) toFtext = "converting ...";
		GUI.TextArea(new Rect(20, 430, 450, 25), "export file : " + toFtext);
		GUI.TextArea(new Rect(20, 455, 450, 25), "unzip : " + unzip_stat);
		GUI.TextArea(new Rect(20, 480, 450, 80), "clean : \n" + cleanComp_reply);
    }
	
	// intial job request for NED data, return a url to reference a job id
	string Request(){
		string new_url = "";
		string XmlMapReq = 
			"<REQUEST_SERVICE_INPUT>" +
				"<AOI_GEOMETRY>" +
					"<EXTENT>" +
						"<TOP>" + coordTOP + "</TOP>" +
						"<BOTTOM>" + coordBOTTOM + "</BOTTOM>" +
						"<LEFT>" + coordLEFT + "</LEFT>" +
						"<RIGHT>" + coordRIGHT + "</RIGHT>" +
					"</EXTENT>" +
					"<SPATIALREFERENCE_WKID/>" +
				"</AOI_GEOMETRY>" +
				"<LAYER_INFORMATION>" +
				    "<LAYER_IDS>NED05XZ</LAYER_IDS>" +
			    "</LAYER_INFORMATION>" +
			    "<CHUNK_SIZE></CHUNK_SIZE>" +
			    "<ORIGINATOR/>" +
			    "<JSON> </JSON>" +
		    "</REQUEST_SERVICE_INPUT>";
		
		NedMapReq = new RequestValidationServiceService();
		ReqOutput = NedMapReq.processAOI(XmlMapReq);
		//XMLParser parser = new XMLParser();
		//node = parser.Parse(output);
		//url = node["DOWNLOAD_URL"];
		
		int start = ReqOutput.IndexOf("<DOWNLOAD_URL>", 0);
        int End = ReqOutput.LastIndexOf("</DOWNLOAD_URL>");
		new_url = ReqOutput.Substring(start + 14, End - start - 14);
		return new_url;
	}
	
	// request handler that probs for job id, returns whether or not data is ready in NED database
	public IEnumerator WaitForInitDown(WWW www){
		yield return www;
		init_reply = www.text;
		int start = init_reply.IndexOf("<ns:return>", 0);
        int End = init_reply.LastIndexOf("</ns:return>");
		job = init_reply.Substring(start + 11, End - start - 11);
		if(job != ""){
			TmpZipFile = "tmp/MAP_" + job + ".zip";
			init_job = true;
		}
	}
	
	// request to initiate download from NED database
	void InitDownload(string address){
		//string job = "";
		if (address == "") return;
		else{
			WWW addy = new WWW(address);
			StartCoroutine(WaitForInitDown(addy));
		}
		//init_reply = addy.text;
		//int start = init_reply.IndexOf("<ns:return>", 0);
        //int End = init_reply.LastIndexOf("</ns:return>");
		//job = init_reply.Substring(start + 11, End - start -12);
		//return job;
	}
	
	// request handler to collect current job status
	public IEnumerator WaitForStatRep(WWW www){
		yield return www;
		stat_reply = www.text;
		int start = stat_reply.IndexOf("<ns:return>", 0);
        int End = stat_reply.LastIndexOf("</ns:return>");
		stat_reply = stat_reply.Substring(start + 11, End - start - 11);
		if (stat_reply.Contains("400")) {
			getData_reply = "downloading data : starting";
			stat_ready = true;
		}
	}
	
	// GET request for job status
	bool Status(string job_id){
		bool ready = false;
		if (!init_job) return false;
		string ReqDownStat = 
			"http://extract.cr.usgs.gov/axis2/services/DownloadService/getDownloadStatus?downloadID="+job_id;
		WWW addy = new WWW(ReqDownStat);
		StartCoroutine(WaitForStatRep(addy));
		//yield(addy);
		//stat_reply = addy.text;
		ready = true;
		Thread.Sleep(1000);
		return ready;
	}
	
	// handler for download status
	public IEnumerator WaitForGetDataRep(WWW www){
		yield return www;
	}
	
	// actual download data retrieval via GET
	void GetData(string job_id){
		if(downloaded) return;
		string ReqGetData = "http://extract.cr.usgs.gov/axis2/services/DownloadService/getData?downloadID="+job_id;
		WWW addy = new WWW(ReqGetData);
		//OnGUI();
		StartCoroutine(WaitForGetDataRep(addy));
		int ts = 0;
		while(!addy.isDone){
			ts++;
			if (ts % 5000 == 0) getData_reply = "downloading data : progress %" + addy.progress.ToString();
		}
		if(addy.isDone) {
        	MapZipFile =  addy.bytes;
			getData_reply = "done downloading : size = " + addy.size;	
			cleanComp_reply = "Cleaning ... ";
			setDownComp(job);
			unzip_stat = "unzipping";
			toFileRep = "creating file ... \n";
			downloaded = true;
		}
	}
	
	// handler for download, waits for download complete
	public IEnumerator WaitForSetDownCompRep(WWW www){
		yield return www;
		cleanComp_reply = www.text;	
	}
	
	// Get request to set download complete
	void setDownComp(string job_id){
		string ReqSetComp = "http://extract.cr.usgs.gov/axis2/services/DownloadService/setDownloadComplete?downloadID="+job_id;
		WWW addy = new WWW(ReqSetComp);
		StartCoroutine(WaitForSetDownCompRep(addy));
	}
	
	// convert downloaded bytes to compressed zip
	public bool BytesToFile(string file, byte[] map)
    {
        try
        {
            System.IO.FileStream _FileStream = new System.IO.FileStream(file, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            _FileStream.Write(map, 0, map.Length);
            _FileStream.Close();
            return true;
        }
        catch (Exception _Exception)
        {
            toFileRep = "Exception caught in process: {0}" + _Exception.ToString();
        }
        return false;
    }

	// unzip function for donwloaded maps
	void uzip(string zipped){
		ZipFile zip = ZipFile.Read(zipped);
        foreach (ZipEntry e in zip)
        {
            e.Extract(zipped.Substring(0, zipped.Length - 4));
        }
		unzip_stat = "unzipped to " + zipped.Substring(0, zipped.Length - 4);
		unzipped = true;
	}
	
	// decompression function for unzipped maps from AsciiGrid format
	void Decompress(string path){
		Console.WriteLine("Decompression path : " + path);
		string[] files = Directory.GetFiles(path,  "*.*",
	    	SearchOption.AllDirectories);
		cleanComp_reply = "";
		string sendFile = "";
		foreach (string file in files)
		{	if(file.Contains(".hdr")){
				cleanComp_reply += file + " ";
				sendFile = file.Trim(".hdr".ToCharArray());
			}
		}
		HeightMapFromGridFloat.ApplyHeightmap(sendFile);
		
		//GeoTiffReader Reader = new GeoTiffReader(path);
	}
	
	void project_build(string basedir){
		
	}
}

//public class GeoTiff
//	{
//		// Variables used to store property values:
//		public const string Executable = "FWTools\\listgeo.exe";
//		public const string Bindirectory = "FWTools";
//
//		// The -t tabledir flag overrides the programs concept of how to file the EPSG CSV files, causing it to look in directory "tabledir". 
//		// -d makes corner coordinates appear as plain double numbers. 
//		public static string Args = "-d";
//
//		private string m_result = "";
//		public string ResultString { get { return m_result.Length == 0 && m_error.Length == 0 ? "OK: command succeded" : m_result; } }
//
//		private string m_error = "";
//		public string ErrorString { get { return m_error; } }
//
//		protected string m_mapName = "";
//		public string MapName { get { return m_mapName; } set { m_mapName = value; } }
//
//		private string m_fileName;
//		public string FileName { get { return m_fileName; } }
//
//		public double TopLeftLng = 0.0d;
//		public double TopLeftLat = 0.0d;
//		public double BottomLeftLng = 0.0d;
//		public double BottomLeftLat = 0.0d;
//		public double TopRightLng = 0.0d;
//		public double TopRightLat = 0.0d;
//		public double BottomRightLng = 0.0d;
//		public double BottomRightLat = 0.0d;
//
//		public bool isValid 
//		{
//			get
//			{
//				return m_error.Length == 0
//					&& TopLeftLng < TopRightLng && TopLeftLat > BottomLeftLat
//					&& BottomRightLat < TopRightLat && BottomRightLng > BottomLeftLng;
//			}
//		}
//
//		public GeoTiff(string fileName)
//		{
//			m_fileName = fileName;
//
//			// map name can be changed later if needed.
//			FileInfo fi = new FileInfo(fileName);
//			m_mapName = fi.Name;
//		}
//
//		// when making a GeoTIFF from image
//		public void initImageOnly()
//		{
//		}
//
//		/// <summary>
//		/// Execute the listgeo command, parse it's output filling this object's properties.
//		/// </summary>
//		public void init()
//		{
//			m_error = "";
//			Exception exc = null;
//
//			//CmdRunner runner = new CmdRunner();
//
//			string outputText = "";
////			try
////			{
////				runner.executeCommand(Path.Combine(Project.startupPath, Executable), Args + " \"" + m_fileName + "\"",
////										Path.Combine(Project.startupPath, Bindirectory), null, out outputText);
////				m_result = runner.OutputString;
////				m_error = runner.ErrorString;
////			}
////			catch (Exception ee)
////			{
////				m_error = ee.Message;
////				LibSys.StatusBar.Error(ee.Message);
////				exc = ee;
////			}
////
////			if(runner.exitcode == 0)
////			{
//				try
//				{
//					using (StreamReader sr = new StreamReader(m_filename))
//		            {
//		                outputText = sr.ReadToEnd();
//		                Console.WriteLine(outputText);
//		            }
//					
//					parseListgeo(outputText);
//					
//				}
//				catch (Exception ee)
//				{
//					m_error = ee.Message;
//					
//					exc = ee;
//				}
//			//}
//
//			if(exc != null)
//			{
//				throw exc;
//			}
//		}
//
//		/// <summary>
//		/// parses listgeo.exe output, filling in GeoTiff's object properties.
//		/// </summary>
//		/// <param name="libgeoOutput"></param>
//		private void parseListgeo(string libgeoOutput)
//		{
//			/*
//			Geotiff_Information:
//			   Version: 1
//			   Key_Revision: 0.1
//			   Tagged_Information:
//				  ModelTiepointTag (2,3):
//					 0                0                0                
//					 664354.283       4601368.61       0                
//				  ModelPixelScaleTag (1,3):
//					 10.16            10.16            0                
//				  End_Of_Tags.
//			   Keyed_Information:
//				  GTModelTypeGeoKey (Short,1): ModelTypeProjected
//				  GTRasterTypeGeoKey (Short,1): RasterPixelIsArea
//				  ProjectedCSTypeGeoKey (Short,1): PCS_WGS84_UTM_zone_17N
//				  PCSCitationGeoKey (Ascii,25): "UTM Zone 17 N with WGS84"
//				  End_Of_Keys.
//			   End_Of_Geotiff.
//			
//			PCS = 32617 (name unknown)
//			Projection = 16017 ()
//			Projection Method: CT_TransverseMercator
//			   ProjNatOriginLatGeoKey: 0.000000 (  0d 0' 0.00"N)
//			   ProjNatOriginLongGeoKey: -81.000000 ( 81d 0' 0.00"W)
//			   ProjScaleAtNatOriginGeoKey: 0.999600
//			   ProjFalseEastingGeoKey: 500000.000000 m
//			   ProjFalseNorthingGeoKey: 0.000000 m
//			GCS: 4326/WGS 84
//			Datum: 6326/World Geodetic System 1984
//			Ellipsoid: 7030/WGS 84 (6378137.00,6356752.31)
//			Prime Meridian: 8901/Greenwich (0.000000/  0d 0' 0.00"E)
//			
//			Corner Coordinates:
//			Upper Left    (  664354.283, 4601368.607)  ( -79.0294261W,  41.5471041N)
//			Lower Left    (  664354.283, 4537888.927)  ( -79.0465367W,  40.9756259N)
//			Upper Right   (  765151.643, 4601368.607)  ( -77.8223338W,  41.5200659N)
//			Lower Right   (  765151.643, 4537888.927)  ( -77.8498913W,  40.9491226N)
//			Center        (  714752.963, 4569628.767)  ( -78.4369247W,  41.2495544N)
//			*/
//
//			StringReader reader = new StringReader (libgeoOutput);
//				
//			int state = 0;
//			int cornerCount = 0;
//			string line;
//			bool hasTransformation = false;
//			while((line=reader.ReadLine()) != null) 
//			{
//				try
//				{
//					switch(state)
//					{
//						case 0:
//							if(line.StartsWith("Corner Coordinates:")) 
//							{
//								state = 10;
//							}
//							else if(line.IndexOf("ModelTransformationTag") != -1) 
//							{
//								hasTransformation = true;
//							} 
//							break;
//						case 10:
//							if(line.StartsWith("Upper Left")) 
//							{
//								parseCoordLine(line, out TopLeftLng, out TopLeftLat);
//								cornerCount++;
//							}
//							if(line.StartsWith("Lower Left")) 
//							{
//								parseCoordLine(line, out BottomLeftLng, out BottomLeftLat);
//								cornerCount++;
//							}
//							if(line.StartsWith("Upper Right")) 
//							{
//								parseCoordLine(line, out TopRightLng, out TopRightLat);
//								cornerCount++;
//							}
//							if(line.StartsWith("Lower Right")) 
//							{
//								parseCoordLine(line, out BottomRightLng, out BottomRightLat);
//								cornerCount++;
//							}
//							break;
//					}
//				}
//				catch {}
//			}
//
//			if(cornerCount == 4)
//			{
//				// all went well with the corners parsing; now process transformation tags appearing in some weird GeoTIFFs:
//
//				if(hasTransformation)
//				{
//					//Project.ErrorBox(Project.mainForm, "Sorry, GeoTIFF ModelTransformationTag not supported, image may be misplaced or missing.");
//				}
//			}
//			else
//			{
//				//Project.ErrorBox(Project.mainForm, "Sorry, GeoTIFF files not having corner coordinates not supported.");
//			}
//		}
//
//		private const string libTiffWin32Url = @"http://gnuwin32.sourceforge.net/packages/tiff.htm";
//		private const string libTiffMustInstallMessage = @"Please install LibTIFF/Win32 to read this type of GeoTiff files.
//The package can be found at {0} and must be installed in defalt location {1}";
//
//		public Bitmap FromNoncontigImage()
//		{
//			Bitmap bitmap = null;
//			m_error = "";
//			Exception exc = null;
//
//			string libTiffDir = Project.driveSystem + "Program Files\\GnuWin32";
//			string libtiffcp = Path.Combine(libTiffDir, "bin\\tiffcp.exe");
//			string cpArgs = "-p contig";
//
//			if(!File.Exists(libtiffcp))
//			{
//				Project.ErrorBox(Project.mainForm, String.Format(libTiffMustInstallMessage, libTiffWin32Url, libTiffDir));
//				return null;
//			}
//
//			CmdRunner runner = new CmdRunner();
//			string tempFile = Path.GetTempFileName();
//			Project.filesToDelete.Add(tempFile);
//
//			string outputText = "";
//			try
//			{
//				runner.executeCommand(libtiffcp, cpArgs + " \"" + m_fileName + "\" \"" + tempFile + "\"",
//					Path.Combine(Project.startupPath, Bindirectory), null, out outputText);
//				m_result = runner.OutputString;
//				m_error = runner.ErrorString;
//			}
//			catch (Exception ee)
//			{
//				m_error = ee.Message;
//				LibSys.StatusBar.Error(ee.Message);
//				exc = ee;
//			}
//
//			if(runner.exitcode == 0)
//			{
//				try
//				{
//					LibSys.StatusBar.Trace(outputText);
//					bitmap = new Bitmap(tempFile);
//					LibSys.StatusBar.Trace("OK: contig tiff read in");
//				}
//				catch (Exception ee)
//				{
//					m_error = ee.Message;
//					LibSys.StatusBar.Error(ee.Message);
//					exc = ee;
//				}
//			}
//
//			if(exc != null)
//			{
//				throw exc;
//			}
//			return bitmap;
//		}
//
//		/// <summary>
//		/// parses listgeo.exe output strings related to corners
//		/// </summary>
//		/// <param name="line"></param>
//		/// <param name="lng"></param>
//		/// <param name="lat"></param>
//		private void parseCoordLine(string line, out double lng, out double lat)
//		{
//			// Upper Left    (  664354.283, 4601368.607)  ( -79.0294261W,  41.5471041N)
//			lng = 0.0d;
//			lat = 0.0d;
//
//			line = line.Substring(line.IndexOf(")"));
//			line = line.Substring(line.IndexOf("(") + 1);
//			string lineLng = line.Substring(0, line.IndexOf(",") - 1).Trim();
//			string lineLat = line.Substring(line.IndexOf(",") + 1);
//			lineLat = lineLat.Substring(0, lineLat.IndexOf(")") - 1).Trim();
//
//			lng = Convert.ToDouble(lineLng);
//			lat = Convert.ToDouble(lineLat);
//		}
//}

//public class GeoTiffReader{
//	string tiffFile = "";
//	Dataset origImage;
//	Driver jp2Driver;
//	Dataset outImage;
//	
//	int width = 0;
//	int height = 0;
//	
//	ArrayList MapFiles = new ArrayList();
//	
//	public GeoTiffReader(string path){
//		tiffFile = path;
//		linkFile(path);
//	}
//	
//	public void linkFile(string fileName){
//		string tiffFile = "";
//		string[] files = Directory.GetFiles(fileName,  "*.*",
//	    	SearchOption.AllDirectories);
//		foreach (string file in files)
//		{
//			if(file.Contains(".tif")){
//				tiffFile = file;
//			}
//		}
//		
//		//if(MapFiles.Count == 0) return;
//		//else if (MapFiles.Count == 1){
//		Console.WriteLine(tiffFile);
//			origImage = Gdal.Open(tiffFile, OSGeo.GDAL.Access.GA_ReadOnly);
//			width = origImage.RasterXSize;
//			height = origImage.RasterXSize;
//			ColorTable colorTable = origImage.GetRasterBand(1).GetColorTable(); // Get the color table from the first band. If it is null then it is a greyscale image.
//			bool Grayscale = colorTable == null;
//			if (!Grayscale)
//            {
//				Console.WriteLine("Grayscale detection failed");
//			}
//			int[][] lookupTable = {
//                                  new int[256],
//                                  new int[256],
//                                  new int[256],
//                                };
//            if (!Grayscale)
//            {
//                 for (int i = 0; i < colorTable.GetCount(); i++)
//                 {
//                     ColorEntry entry = colorTable.GetColorEntry(i);
//                     for (int x = 0; x < 3; x++)
//                     {
//                         short value = 0;
//                         switch (x)
//                         {
//                             case 0:
//                                 value = entry.c1;
//                                 break;
//                             case 1:
//                                 value = entry.c2;
//                                 break;
//                             case 2:
//                                 value = entry.c3;
//                                 break;
//                         };
//                         lookupTable[x][i] = value;
//                     }
//                 }
//            }
//			int newLineIndex = 0;
//            for (int line = 0; line < height - 2; line++)
//            {
//                for (int bandNumber = 0; bandNumber < 3; bandNumber++)
//                {
//                    int bandIndex = origImage.RasterCount > 1 ? bandNumber + 1 : 1;
////                    //Read in the original band
//                    Band band = origImage.GetRasterBand(bandIndex);
////                    Band newBand = dsjp2.GetRasterBand(bandNumber + 1);
//					//band.ReadRaster(
//					byte[] oldbuffer = new byte[10];
//                    band.ReadRaster(0, line, width, 1, oldbuffer, width, 1, 1, 1);
////
//                    if (Grayscale)
//                    {	
//						Console.WriteLine(oldbuffer);
//						//newBand.WriteRaster(0, newLineIndex, newWidth, 1, oldbuffer, newWidth, 1, 1, 1);
//                    	
//					} else {
//						Console.WriteLine("grayscale detection failed");
////                        int[] bandTable = lookupTable[bandNumber];
////                        int newPixelIndex = 0;
////                        for (int pixel = TopBeginningPixel; pixel < BotEndingPixel - 1; pixel++)
////                        {
////                            newbuffer[newPixelIndex] = (Byte)bandTable[(short)oldbuffer[pixel]];
////                            newPixelIndex++;
////                        }
////                        newBand.WriteRaster(0, newLineIndex, newWidth, 1, newbuffer, newWidth, 1, 1, 1);
//                    }
//                }
//
//                newLineIndex++;
//                //If you are using a progress bar then update the progress here.
//            }
//			
//			//jp2Driver = Gdal.GetDriverByName("XYZ");
//			//Gdal.AllRegister();
//			//outImage = jp2Driver.Create(fileName+"out", width, height, 3, origImage.GetRasterBand(1).DataType, jp2ecwCreationOptions);
//	
//		//}
//	}
//}

//public class unZipper{
//	// Bytes array in which we're going to store the actual file to be decompressed
//    byte[] bufferWrite;
//    // Will open the file to be decompressed
//    FileStream fsSource;
//    // Will write the new decompressed file
//    FileStream fsDest;
//    // To hold the compressed file
//    GZipStream gzDecompressed;
//	
//	public bool unzip(string txtPath, string destFile){
//		bool done;
//		try{
//	    	fsSource = new FileStream(txtPath, FileMode.Open, FileAccess.Read, FileShare.Read);
//	    	// Will hold the compressed stream created from the destination stream
//	    	gzDecompressed = new GZipStream(fsSource, CompressionMode.Decompress, true);
//		    // Retrieve the size of the file from the compressed archive's footer
//		    bufferWrite = new byte[4];
//		    fsSource.Position = (int)fsSource.Length - 4;
//		    // Write the first 4 bytes of data from the compressed file into the buffer
//		    fsSource.Read(bufferWrite, 0, 4);
//		    // Set the position back at the start
//		    fsSource.Position = 0;
//		    int bufferLength = BitConverter.ToInt32(bufferWrite, 0);
//		
//		    byte[] buffer = new byte[bufferLength + 100];
//		    int readOffset = 0;
//		    int totalBytes = 0;
//		
//		    // Loop through the compressed stream and put it into the buffer
//		    while (true)
//		    {
//		        int bytesRead = gzDecompressed.Read(buffer, readOffset, 100);
//		        // If we reached the end of the data
//		        if (bytesRead == 0)
//		            break;
//		        readOffset += bytesRead;
//		        totalBytes += bytesRead;
//		    }
//		
//		    // Write the content of the buffer to the destination stream (file)
//		    fsDest = new FileStream(destFile, FileMode.Create);
//		    fsDest.Write(buffer, 0, totalBytes);
//		
//		    // Close the streams
//		    fsSource.Close();
//		    gzDecompressed.Close();
//		    fsDest.Close();
//			
//			done = true;
//		}
//		catch(Exception _Exception)
//        {
//			done = false;
//            Console.WriteLine("Exception caught in process: {0}" + _Exception.ToString());
//		}
//		return done;
//	}
//}

//[System.Serializable]
//public struct Zipper
//{
//    public static string UnzipBytes(byte[] compbytes)
//    {
//        string result;
//        MemoryStream m_msBZip2 = null;
//        BZip2InputStream m_isBZip2 = null;
//        try
//        {
//            m_msBZip2 = new MemoryStream(compbytes);
//            // read final uncompressed string size stored in first 4 bytes
//            //
//            using (BinaryReader reader = new BinaryReader(m_msBZip2, System.Text.Encoding.ASCII))
//            {
//                Int32 size = reader.ReadInt32();
//
//                m_isBZip2 = new BZip2InputStream(m_msBZip2);
//                byte[] bytesUncompressed = new byte[size];
//                m_isBZip2.Read(bytesUncompressed, 0, bytesUncompressed.Length);
//                m_isBZip2.Close();
//                m_msBZip2.Close();
//
//                result = Encoding.ASCII.GetString(bytesUncompressed);
//
//                reader.Close();
//            }
//        }
//        finally
//        {
//            if (m_isBZip2 != null)
//            {
//                m_isBZip2.Dispose();
//            }
//            if (m_msBZip2 != null)
//            {
//                m_msBZip2.Dispose();
//            }
//        }
//        return result;
//    }
//}

// the following classes are generate via wdsl as a service consumer, these
// interact as part of the SOAP protocol from the NED database and provide
// functionallity for requesting, downloading, and cleaning job requests
// from the NED database.
[System.Web.Services.WebServiceBinding(Name="RequestValidationService", Namespace="http://edc.usgs.gov")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class RequestValidationServiceService : System.Web.Services.Protocols.SoapHttpClientProtocol {
    
    private System.Threading.SendOrPostCallback processAOIOperationCompleted;
    
    private System.Threading.SendOrPostCallback processAOI2OperationCompleted;
    
    private System.Threading.SendOrPostCallback getTiledDataDirectURLsOperationCompleted;
    
    private System.Threading.SendOrPostCallback getTiledDataDirectURLs2OperationCompleted;
    
    private System.Threading.SendOrPostCallback validateChunksOperationCompleted;
    
    public RequestValidationServiceService() {
        this.Url = "http://igskmncngs046.cr.usgs.gov/requestValidationService/services/RequestValidationService";
    }
    
    public event processAOICompletedEventHandler processAOICompleted;
    
    public event processAOI2CompletedEventHandler processAOI2Completed;
    
    public event getTiledDataDirectURLsCompletedEventHandler getTiledDataDirectURLsCompleted;
    
    public event getTiledDataDirectURLs2CompletedEventHandler getTiledDataDirectURLs2Completed;
    
    public event validateChunksCompletedEventHandler validateChunksCompleted;
    
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://edc.usgs.gov", ResponseNamespace="http://edc.usgs.gov", ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Use=System.Web.Services.Description.SoapBindingUse.Literal)]
    [return: System.Xml.Serialization.XmlElementAttribute("processAOIReturn")]
    public string processAOI(string requestInfoXml) {
        object[] results = this.Invoke("processAOI", new object[] {
                    requestInfoXml});
        return ((string)(results[0]));
    }
    
    public System.IAsyncResult BeginprocessAOI(string requestInfoXml, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("processAOI", new object[] {
                    requestInfoXml}, callback, asyncState);
    }
    
    public string EndprocessAOI(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    public void processAOIAsync(string requestInfoXml) {
        this.processAOIAsync(requestInfoXml, null);
    }
    
    public void processAOIAsync(string requestInfoXml, object userState) {
        if ((this.processAOIOperationCompleted == null)) {
            this.processAOIOperationCompleted = new System.Threading.SendOrPostCallback(this.OnprocessAOICompleted);
        }
        this.InvokeAsync("processAOI", new object[] {
                    requestInfoXml}, this.processAOIOperationCompleted, userState);
    }
    
    private void OnprocessAOICompleted(object arg) {
        if ((this.processAOICompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.processAOICompleted(this, new processAOICompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://edc.usgs.gov", ResponseNamespace="http://edc.usgs.gov", ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Use=System.Web.Services.Description.SoapBindingUse.Literal)]
    [return: System.Xml.Serialization.XmlElementAttribute("processAOI2Return")]
    public string processAOI2(string requestInfoXml) {
        object[] results = this.Invoke("processAOI2", new object[] {
                    requestInfoXml});
        return ((string)(results[0]));
    }
    
    public System.IAsyncResult BeginprocessAOI2(string requestInfoXml, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("processAOI2", new object[] {
                    requestInfoXml}, callback, asyncState);
    }
    
    public string EndprocessAOI2(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    public void processAOI2Async(string requestInfoXml) {
        this.processAOI2Async(requestInfoXml, null);
    }
    
    public void processAOI2Async(string requestInfoXml, object userState) {
        if ((this.processAOI2OperationCompleted == null)) {
            this.processAOI2OperationCompleted = new System.Threading.SendOrPostCallback(this.OnprocessAOI2Completed);
        }
        this.InvokeAsync("processAOI2", new object[] {
                    requestInfoXml}, this.processAOI2OperationCompleted, userState);
    }
    
    private void OnprocessAOI2Completed(object arg) {
        if ((this.processAOI2Completed != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.processAOI2Completed(this, new processAOI2CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://edc.usgs.gov", ResponseNamespace="http://edc.usgs.gov", ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Use=System.Web.Services.Description.SoapBindingUse.Literal)]
    [return: System.Xml.Serialization.XmlElementAttribute("getTiledDataDirectURLsReturn")]
    public string getTiledDataDirectURLs(string requestInfoXml) {
        object[] results = this.Invoke("getTiledDataDirectURLs", new object[] {
                    requestInfoXml});
        return ((string)(results[0]));
    }
    
    public System.IAsyncResult BegingetTiledDataDirectURLs(string requestInfoXml, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("getTiledDataDirectURLs", new object[] {
                    requestInfoXml}, callback, asyncState);
    }
    
    public string EndgetTiledDataDirectURLs(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    public void getTiledDataDirectURLsAsync(string requestInfoXml) {
        this.getTiledDataDirectURLsAsync(requestInfoXml, null);
    }
    
    public void getTiledDataDirectURLsAsync(string requestInfoXml, object userState) {
        if ((this.getTiledDataDirectURLsOperationCompleted == null)) {
            this.getTiledDataDirectURLsOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetTiledDataDirectURLsCompleted);
        }
        this.InvokeAsync("getTiledDataDirectURLs", new object[] {
                    requestInfoXml}, this.getTiledDataDirectURLsOperationCompleted, userState);
    }
    
    private void OngetTiledDataDirectURLsCompleted(object arg) {
        if ((this.getTiledDataDirectURLsCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.getTiledDataDirectURLsCompleted(this, new getTiledDataDirectURLsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://edc.usgs.gov", ResponseNamespace="http://edc.usgs.gov", ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Use=System.Web.Services.Description.SoapBindingUse.Literal)]
    [return: System.Xml.Serialization.XmlElementAttribute("getTiledDataDirectURLs2Return")]
    public string getTiledDataDirectURLs2(string requestInfoXml) {
        object[] results = this.Invoke("getTiledDataDirectURLs2", new object[] {
                    requestInfoXml});
        return ((string)(results[0]));
    }
    
    public System.IAsyncResult BegingetTiledDataDirectURLs2(string requestInfoXml, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("getTiledDataDirectURLs2", new object[] {
                    requestInfoXml}, callback, asyncState);
    }
    
    public string EndgetTiledDataDirectURLs2(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    public void getTiledDataDirectURLs2Async(string requestInfoXml) {
        this.getTiledDataDirectURLs2Async(requestInfoXml, null);
    }
    
    public void getTiledDataDirectURLs2Async(string requestInfoXml, object userState) {
        if ((this.getTiledDataDirectURLs2OperationCompleted == null)) {
            this.getTiledDataDirectURLs2OperationCompleted = new System.Threading.SendOrPostCallback(this.OngetTiledDataDirectURLs2Completed);
        }
        this.InvokeAsync("getTiledDataDirectURLs2", new object[] {
                    requestInfoXml}, this.getTiledDataDirectURLs2OperationCompleted, userState);
    }
    
    private void OngetTiledDataDirectURLs2Completed(object arg) {
        if ((this.getTiledDataDirectURLs2Completed != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.getTiledDataDirectURLs2Completed(this, new getTiledDataDirectURLs2CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://edc.usgs.gov", ResponseNamespace="http://edc.usgs.gov", ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Use=System.Web.Services.Description.SoapBindingUse.Literal)]
    [return: System.Xml.Serialization.XmlElementAttribute("validateChunksReturn")]
    public string validateChunks(string requestInfoXml) {
        object[] results = this.Invoke("validateChunks", new object[] {
                    requestInfoXml});
        return ((string)(results[0]));
    }
    
    public System.IAsyncResult BeginvalidateChunks(string requestInfoXml, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("validateChunks", new object[] {
                    requestInfoXml}, callback, asyncState);
    }
    
    public string EndvalidateChunks(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    public void validateChunksAsync(string requestInfoXml) {
        this.validateChunksAsync(requestInfoXml, null);
    }
    
    public void validateChunksAsync(string requestInfoXml, object userState) {
        if ((this.validateChunksOperationCompleted == null)) {
            this.validateChunksOperationCompleted = new System.Threading.SendOrPostCallback(this.OnvalidateChunksCompleted);
        }
        this.InvokeAsync("validateChunks", new object[] {
                    requestInfoXml}, this.validateChunksOperationCompleted, userState);
    }
    
    private void OnvalidateChunksCompleted(object arg) {
        if ((this.validateChunksCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.validateChunksCompleted(this, new validateChunksCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
}
public partial class processAOICompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    private object[] results;
    internal processAOICompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }   
    public string Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((string)(this.results[0]));
        }
    }
}
public delegate void processAOICompletedEventHandler(object sender, processAOICompletedEventArgs args);
public partial class processAOI2CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    private object[] results;
    internal processAOI2CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    public string Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((string)(this.results[0]));
        }
    }
}
public delegate void processAOI2CompletedEventHandler(object sender, processAOI2CompletedEventArgs args);
public partial class getTiledDataDirectURLsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {    
    private object[] results;
    internal getTiledDataDirectURLsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    public string Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((string)(this.results[0]));
        }
    }
}
public delegate void getTiledDataDirectURLsCompletedEventHandler(object sender, getTiledDataDirectURLsCompletedEventArgs args);
public partial class getTiledDataDirectURLs2CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    private object[] results;
    internal getTiledDataDirectURLs2CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    public string Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((string)(this.results[0]));
        }
    }
}
public delegate void getTiledDataDirectURLs2CompletedEventHandler(object sender, getTiledDataDirectURLs2CompletedEventArgs args);
public partial class validateChunksCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs { 
    private object[] results;
    internal validateChunksCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    public string Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((string)(this.results[0]));
        }
    }
}
public delegate void validateChunksCompletedEventHandler(object sender, validateChunksCompletedEventArgs args);
// end wdsl generated service consumer

//public class DBQuery
// {
//    public static void query(string dataB)
//    {
//       string connectionString = "URI=file:" + dataB + ".db";
//       IDbConnection dbcon;
//       dbcon = (IDbConnection) new SqliteConnection(connectionString);
//       dbcon.Open();
//       IDbCommand dbcmd = dbcon.CreateCommand();
//       // requires a table to be created named employee
//       // with columns firstname and lastname
//       // such as,
//       //        CREATE TABLE employee (
//       //           firstname varchar(32),
//       //           lastname varchar(32));
//       string sql =
//          "SELECT firstname, lastname " +
//          "FROM employee";
//       dbcmd.CommandText = sql;
//       IDataReader reader = dbcmd.ExecuteReader();
//       while(reader.Read()) {
//            string FirstName = reader.GetString (0);
//            string LastName = reader.GetString (1);
//            Console.WriteLine("Name: " +
//                FirstName + " " + LastName);
//       }
//       // clean up
//       reader.Close();
//       reader = null;
//       dbcmd.Dispose();
//       dbcmd = null;
//       dbcon.Close();
//       dbcon = null;
//    }
// }

/// <summary>
/// This HelperClass was obtained from Brenny Doogle's Programming Blog:
/// http://brennydoogles.wordpress.com/2010/02/26/using-sqlite-with-csharp/
/// It is an adaptation of a tutorial by Mike Duncan from his blog which is now dead-linked here:
/// http://brennydoogles.wordpress.com/2010/02/26/using-sqlite-with-csharp/
/// </summary>
//class SQLiteDatabase
//{
//     String dbConnection;
//
//     ///
//     ///     Default Constructor for SQLiteDatabase Class.
//     ///
//     public SQLiteDatabase()
//     {
//          dbConnection = "Data Source=recipes.s3db";
//     }
//
//     ///
//     ///     Single Param Constructor for specifying the DB file.
//     ///
//     /// The File containing the DB
//     public SQLiteDatabase(String inputFile)
//     {
//          dbConnection = String.Format("Data Source={0}", inputFile);
//     }
//     
//     ///
//     ///     Single Param Constructor for specifying advanced connection options.
//     ///
//     /// A dictionary containing all desired options and their values
//     public SQLiteDatabase(Dictionary connectionOpts)
//     {
//          String str = "";
//          foreach (KeyValuePair row in connectionOpts)
//          {
//               str += String.Format("{0}={1}; ", row.Key, row.Value);
//          }
//          str = str.Trim().Substring(0, str.Length - 1);
//          dbConnection = str;
//     }
//
//     ///
//     ///     Allows the programmer to run a query against the Database.
//     ///
//     /// The SQL to run
//     /// A DataTable containing the result set.
//     public DataTable GetDataTable(string sql)
//     {
//          DataTable dt = new DataTable();
//          try
//          {
//               SQLiteConnection cnn = new SQLiteConnection(dbConnection);
//               cnn.Open();
//               SQLiteCommand mycommand = new SQLiteCommand(cnn);
//               mycommand.CommandText = sql;
//               SQLiteDataReader reader = mycommand.ExecuteReader();
//               dt.Load(reader);
//               reader.Close();
//               cnn.Close();
//          }
//          catch (Exception e)
//          {
//               throw new Exception(e.Message);
//          }
//          return dt;
//     }
//     ///
//     ///     Allows the programmer to interact with the database for purposes other than a query.
//     ///
//     /// The SQL to be run.
//     /// An Integer containing the number of rows updated.
//     public int ExecuteNonQuery(string sql)
//     {
//          SQLiteConnection cnn = new SQLiteConnection(dbConnection);
//          cnn.Open();
//          SQLiteCommand mycommand = new SQLiteCommand(cnn);
//          mycommand.CommandText = sql;
//          int rowsUpdated = mycommand.ExecuteNonQuery();
//          cnn.Close();
//          return rowsUpdated;
//     }
//     ///
//     ///     Allows the programmer to retrieve single items from the DB.
//     ///
//     /// The query to run.
//     /// A string.
//     public string ExecuteScalar(string sql)
//     {
//          SQLiteConnection cnn = new SQLiteConnection(dbConnection);
//          cnn.Open();
//          SQLiteCommand mycommand = new SQLiteCommand(cnn);
//          mycommand.CommandText = sql;
//          object value = mycommand.ExecuteScalar();
//          cnn.Close();
//          if (value != null)
//          {
//               return value.ToString();
//          }
//          return "";
//     }
//
//     ///
//     ///     Allows the programmer to easily update rows in the DB.
//     ///
//     /// The table to update.
//     /// A dictionary containing Column names and their new values.
//     /// The where clause for the update statement.
//     /// A boolean true or false to signify success or failure.
//     public bool Update(String tableName, Dictionary data, String where)
//     {
//          String vals = "";
//          Boolean returnCode = true;
//          if (data.Count >= 1)
//          {
//               foreach (KeyValuePair val in data)
//               {
//                    vals += String.Format(" {0} = '{1}',", val.Key.ToString(), val.Value.ToString());
//               }
//               vals = vals.Substring(0, vals.Length - 1);
//          }
//          try
//          {
//               this.ExecuteNonQuery(String.Format("update {0} set {1} where {2};", tableName, vals, where));
//          }
//          catch
//          {
//               returnCode = false;
//          }
//          return returnCode;
//     }
//
//     ///
//     ///     Allows the programmer to easily delete rows from the DB.
//     ///
//     /// The table from which to delete.
//     /// The where clause for the delete.
//     /// A boolean true or false to signify success or failure.
//     public bool Delete(String tableName, String where)
//     {
//          Boolean returnCode = true;
//          try
//          {
//               this.ExecuteNonQuery(String.Format("delete from {0} where {1};", tableName, where));
//          }
//          catch (Exception fail)
//          {
//               MessageBox.Show(fail.Message);
//               returnCode = false;
//          }
//          return returnCode;
//     }
//
//     ///
//     ///     Allows the programmer to easily insert into the DB
//     ///
//     /// The table into which we insert the data.
//     /// A dictionary containing the column names and data for the insert.
//     /// A boolean true or false to signify success or failure.
//     public bool Insert(String tableName, Dictionary data)
//     {
//          String columns = "";
//          String values = "";
//          Boolean returnCode = true;
//          foreach (KeyValuePair val in data)
//          {
//               columns += String.Format(" {0},", val.Key.ToString());
//               values += String.Format(" '{0}',", val.Value);
//          }
//          columns = columns.Substring(0, columns.Length - 1);
//          values = values.Substring(0, values.Length - 1);
//          try
//          {
//               this.ExecuteNonQuery(String.Format("insert into {0}({1}) values({2});", tableName, columns, values));
//          }
//          catch(Exception fail)
//          {
//               MessageBox.Show(fail.Message);
//               returnCode = false;
//          }
//          return returnCode;
//     }
//
//     ///
//     ///     Allows the programmer to easily delete all data from the DB.
//     ///
//     /// A boolean true or false to signify success or failure.
//     public bool ClearDB()
//     {
//          DataTable tables;
//          try
//          {
//               tables = this.GetDataTable("select NAME from SQLITE_MASTER where type='table' order by NAME;");
//               foreach (DataRow table in tables.Rows)
//               {
//                    this.ClearTable(table["NAME"].ToString());
//               }
//               return true;
//          }
//          catch
//          {
//               return false;
//          }
//     }
//
//     ///
//     ///     Allows the user to easily clear all data from a specific table.
//     ///
//     /// The name of the table to clear.
//     /// A boolean true or false to signify success or failure.
//     public bool ClearTable(String table)
//     {
//          try
//          {
//               this.ExecuteNonQuery(String.Format("delete from {0};", table));
//               return true;
//          }
//          catch
//          {
//               return false;
//          }
//     }
//}


// This class was obtained from http://code.google.com/p/kml-parser-csharp/
// as a part of the google code base. It is licensed under the open-source 
// MIT licensing agreement.
//public class KML
//{
//    //kml tags found
//    bool IS_POINT = false;
//    bool IS_LINESTRING = false;
//    bool IS_COORDINATE = false;
//
//    //kml geometry
//    private enum kmlGeometryType
//    {
//        POINT,
//        LINESTRING
//    }
//    //kml tags
//    private enum kmlTagType
//    {
//        POINT,
//        LINESTRING,
//        COORDINATES
//    }
//
//    //return types        
//    List<Hashtable> PointsCollection = new List<Hashtable>();//all parsed kml points
//    List<Hashtable> LinesCollection = new List<Hashtable>();//all parsed kml lines
//    Hashtable Point;//single point (part of PointsCollection)
//    Hashtable Line;//single line (part of LinesCollection)
//    Hashtable Coordinates;//object coordinate
//
//    Hashtable KMLCollection = new Hashtable();//parsed KML
//
//    private kmlGeometryType? currentGeometry = null;//currently parsed geometry object
//    private kmlTagType? currentKmlTag = null;//currently parsed kml tag        
//
//    private string lastError;
//
//    /// <summary>
//    /// parse kml, fill Points and Lines collections
//    /// </summary>
//    /// <param name="fileName">Full ABSOLUTE path to file.</param>
//    /// <returns>HashTable</returns>
//    public Hashtable KMLDecode (string fileName){
//        readKML(fileName);
//        if (PointsCollection != null) KMLCollection.Add("POINTS", PointsCollection);
//        if (LinesCollection != null) KMLCollection.Add("LINES", LinesCollection);
//        return KMLCollection;
//    }
//
//    /// <summary>
//    /// Open kml, loop it and check for tags.
//    /// </summary>
//    /// <param name="fileName">Full ABSOLUTE path to file.</param>        
//    private void readKML (string fileName){
//            using (XmlReader kmlread = XmlReader.Create(fileName))
//            {
//                while (kmlread.Read())//read kml node by node
//                {
//                    //select type of tag and object
//                    switch (kmlread.NodeType)
//                    {
//                        case XmlNodeType.Element:
//                            //in elements select kml type
//                            switch (kmlread.Name.ToUpper())
//                            {
//                                case "POINT":
//                                    currentGeometry = kmlGeometryType.POINT;
//                                    Point = new Hashtable();
//                                    break;
//                                case "LINESTRING":
//                                    currentGeometry = kmlGeometryType.LINESTRING;
//                                    Line = new Hashtable();
//                                    break;
//                                case "COORDINATES":
//                                    currentKmlTag = kmlTagType.COORDINATES;
//                                    break;
//                            }
//                            break;
//                        case XmlNodeType.EndElement:
//                            //check if any geometry is parsed in add it to collection
//                            switch (kmlread.Name.ToUpper())
//                            {
//                                case "POINT":
//                                    if (Point != null) PointsCollection.Add(Point);
//                                    //Reinit vars
//                                    Point = null;
//                                    currentGeometry = null;
//                                    currentKmlTag = null;
//                                    break;
//                                case "LINESTRING":
//                                    if (Line != null) LinesCollection.Add(Line);
//                                    //Reinit vars
//                                    Line = null;
//                                    currentGeometry = null;
//                                    currentKmlTag = null;
//                                    break;                                 
//                            }
//
//                            break;
//                        case XmlNodeType.Text:
//                        case XmlNodeType.CDATA:
//                        case XmlNodeType.Comment:
//                        case XmlNodeType.XmlDeclaration:
//                            //Parse inner object data
//                            switch (currentKmlTag)
//                            {
//                                case kmlTagType.COORDINATES:                                        
//                                    parseGeometryVal(kmlread.Value);//try to parse coordinates
//                                    break;
//                            }
//                            break;
//                    case XmlNodeType.DocumentType:
//                        break;
//                    default: break;
//                    }
//                }
//            }
//    }
//
//    /// <summary>
//    /// Parse selected geometry based on type
//    /// </summary>
//    /// <param name="tag_value">Value of geometry element.</param>        
//    protected void parseGeometryVal(string tag_value)
//    {
//        object value = null;
//        switch (currentGeometry)
//        {
//            case kmlGeometryType.POINT:
//                parsePoint(tag_value);
//                break;
//            case kmlGeometryType.LINESTRING:
//                parseLine(tag_value);
//                break;
//        }
//    }
//
//    /// <summary>
//    /// If geometry is point select element values:
//    ///     COORDINATES - add lat & lan to HashTable Point
//    /// </summary>
//    /// <param name="tag_value">Value of geometry element.</param>   
//    protected void parsePoint(string tag_value)
//    {
//        Hashtable value = null;
//        string[] coordinates; 
//        switch (currentKmlTag)
//        {
//            case kmlTagType.COORDINATES:
//                //kml point coordinates format is [lat,lan]
//                value = new Hashtable();
//                coordinates = tag_value.Split(',');
//                if (coordinates.Length < 2)lastError = "ERROR IN FORMAT OF POINT COORDINATES";
//                value.Add("LNG", coordinates[0].Trim());
//                value.Add("LAT", coordinates[1].Trim());
//                Point.Add("COORDINATES", value);
//                break;
//        }
//    }
//
//    /// <summary>
//    /// If geometry is line select element values:
//    ///     COORDINATES - add lat & lan to List
//    ///                   add list to HashTable Line
//    /// </summary>
//    /// <param name="tag_value">Value of geometry element.</param>   
//    protected void parseLine(string tag_value)
//    {
//        List<Hashtable> value = null;
//        Hashtable linePoint = null;
//        string[] vertex;
//        string[] coordinates;
//        int idx = 0;
//        switch (currentKmlTag)
//        {
//            case kmlTagType.COORDINATES:
//                //kml coordinates format is [lat,lan]
//                value = new List<Hashtable>();
//                vertex = tag_value.Trim().Split(' ');//Split linestring to vertexes
//
//                foreach (string point in vertex)
//                {
//                    coordinates = point.Split(',');
//                    if (coordinates.Length < 2) LastError = "ERROR IN FORMAT OF LINESTRING COORDINATES";
//                    foreach (string coordinate in coordinates)
//                    {
//                        linePoint = new Hashtable();
//                        linePoint.Add("LNG", coordinates[0]);
//                        linePoint.Add("LAT", coordinates[1]);
//                        idx++;//index of net point
//                        value.Add(linePoint);
//                    }
//                }
//                Line.Add("COORDINATES", value);//Add coordinates to line
//                break;
//        }
//    }
//
//    /// <summary>
//    /// Last returned error        
//    /// </summary>        
//    public string LastError
//    {
//        get { return lastError; }
//        set {
//            //remember error and promote it to caller
//            lastError = value;
//            throw new System.Exception(lastError);
//        }
//    }
//}    

