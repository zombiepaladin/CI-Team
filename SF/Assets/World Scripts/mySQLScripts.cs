using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using MySql.Data.MySqlClient;


public class mySQLScripts{
	
	string constr = "Server=mysql.cis.ksu.edu;Database=borzen;User ID=borzen;Password=gam3er";
	MySqlConnection con = null;
	MySqlCommand cmd = new MySqlCommand();
	MySqlDataReader rdr = null;
	MySqlError er = null;
	
	//Dictionary<int,string> playersFields = new Dictionary<int, string>();
	
	public bool connected = false;
	
	public void Connect(){
		try{
		con = new MySqlConnection(constr);		
		con.Open();
		connected = true;
		cmd.Connection = con;
		}
		catch(MySqlException ex){
			
			Debug.Log (ex.ToString());
		}

	}
	
	
	void OnApplicationQuit(){
		if(con != null){
			if(con.State != System.Data.ConnectionState.Closed){
				con.Close();
			}
			con.Dispose();
		}	
	}
	
	public void AddData(string data, string fn, string un){
		string query = "INSERT INTO Fields(FieldName, UserName, CompressedString) VALUES (?FN,?UN,?CS)";
		try{

			cmd.CommandText = query;
			MySqlParameter cd = cmd.Parameters.Add("?CS",MySqlDbType.LongText);
			cd.Value = data;
			MySqlParameter f = cmd.Parameters.Add("?FN",MySqlDbType.VarChar);
			fn.Value = fn;
			MySqlParameter u = cmd.Parameters.Add("?UN",MySqlDbType.VarChar);
			u.Value = un;
			cmd.ExecuteNonQuery();
		}
		catch(MySqlException ex){
			Debug.Log (ex.ToString());
		}
	}
}
