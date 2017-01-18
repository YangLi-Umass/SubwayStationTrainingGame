using UnityEngine;
using System.Collections;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class SQLiteDBHelper {

	public static readonly string DB_PATH = "URI=file:" + Application.dataPath + "/Database";

	private IDbConnection dbconn;

	private string dbPathInUnity;
	private string dbPathInWindows;

	public SQLiteDBHelper (string _dbName) {
		dbPathInUnity = DB_PATH + "/" + _dbName;
		dbPathInWindows = Application.dataPath + "/Database" + "/" + _dbName;
	}

	public bool open () {
		if (!System.IO.File.Exists (dbPathInWindows)) {
			Debug.Log ("Warning: the database does not exit.");
			return false;
		} else {
			dbconn = (IDbConnection) new SqliteConnection(dbPathInUnity);
			dbconn.Open();
			return true;
		}
	}

	public string readDecisionPointConfInfoAccordingToIdAndTableName (int _id, string _tableName) {
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = "SELECT PointInfo " + "FROM " + _tableName + " WHERE ID = " + _id;
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		string result = "default";
		while (reader.Read ()) {
			result = reader.GetString (0);
		}
		reader.Close();
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		return result;
	}

	public void close() {
		dbconn.Close();
		dbconn = null;
	}
}
