using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;


public class BS{
	
	
	/// <summary>
	/// Save the specified data in the file.
	/// </summary>
	/// <param name='data'>
	/// Data that needs to be saved.
	/// </param>
	/// <param name='fn'>
	/// The file name.
	/// </param>
	public static void Save(byte[] data, string fn){
		FileStream fs = new FileStream(Application.dataPath + "\\SFFields\\" + fn +".sffd", FileMode.Create);
		string saveS = BS.Compress(data);
		BinaryWriter bw = new BinaryWriter(fs);
		bw.Write(saveS);
		bw.Close();
		fs.Close();
		Debug.Log(saveS.Length);
	}
	
	public static void SaveToWeb(byte[] d, string fn, string un){
		string query = "\'INSERT INTO Fields(FieldName, UserName, CompressedString) VALUES (\""+fn+"\",\""+un+"\",\""+BS.Compress(d)+"\")\'";
		Application.ExternalCall("RunPHPSave",query);
	}
	
	
	public static void saveFarm(string cs){
		
	}
	
	/// <summary>
	/// Load the specified File.
	/// </summary>
	/// <param name='fn'>
	/// The file name that needs to be loaded.
	/// </param>
	public static byte[] Load(string fn){
            FileStream fs = new FileStream(Application.dataPath + "\\SFFields\\" + fn +".sffd", FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            string openS = "";
            openS = br.ReadString();
            fs.Close();
            return BS.Decompress(openS);
	}
	
	/// <summary> 
	/// Decompress the specified data.
	/// </summary>
	/// <param name='data'>
	/// Data.
	/// </param>
	static byte[] Decompress(string data){
		byte[] output = null;
		var m = new MemoryStream(Convert.FromBase64String(data.Substring(10)));
		var z = new InflaterInputStream(m);
		var br = new BinaryReader(m);
		var length = br.ReadInt32();
		output = new byte[length];
		z.Read(output, 0, length);
		z.Close();
		m.Close();
		return output;
	}
	
	public static string LoadFromWeb(string id){
		WWWForm form = new WWWForm();
		string query = "SELECT CompressedString FROM Fields WHERE PK_ID = "+id;
		form.AddField("q",query);
		string cs = "";
		form.AddField("cs",cs);
		return null;
	}
	
	public static void loadFarm(string cs){
		
	}
	
	/// <summary>
	/// Compress the specified data.
	/// </summary>
	/// <param name='data'>
	/// Data.
	/// </param>
	static string Compress(byte[] data){
		MemoryStream ms = new MemoryStream();
		BinaryWriter bw = new BinaryWriter(ms);
		DeflaterOutputStream zs = new DeflaterOutputStream(ms);
		bw.Write(data.Length);
		zs.Write(data,0,data.Length);
		zs.Flush();
		zs.Close ();
		bw.Close();
		return "ZipStream:" + Convert.ToBase64String(ms.GetBuffer());
	}
	
}
