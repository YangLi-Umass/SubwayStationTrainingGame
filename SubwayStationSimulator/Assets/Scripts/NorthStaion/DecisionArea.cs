using UnityEngine;
using System.Collections;

public class DecisionArea : MonoBehaviour {

	//For test
	public bool test = false;

	//Attributes
	public int nodeId;
	private int nextNodeId;
	private int nextOrientation;

	//Setter
	public void setNextNodeId(int _id) {
		nextNodeId = _id;
	}

	public void setNextOrientation(int _o) {
		nextOrientation = _o;
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (test) {
			Debug.Log ("Test !!!");
			Debug.Log ("Enter decision area " + nodeId);
		} else {
			Debug.Log ("Enter decision area " + nodeId);
			NorthStationGameController gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<NorthStationGameController>();
			string buildingName = gameController.buildingName;
			string destination = gameController.destination;
			string result = readDecisionPointConfigInfoFromDB (buildingName + "DecisionArea.db", destination, nodeId);
			Debug.Log (result);
		}
	}

	void OnTriggerExit(Collider other) {
		if (test) {
			Debug.Log ("Test !!!");
			Debug.Log ("Exit decision area " + nodeId);
		} else {
			Debug.Log ("Exit decision area " + nodeId);
		}
	}

	public string readDecisionPointConfigInfoFromDB (string _dbName, string _tableName, int _decisionPointId) {

		SQLiteDBHelper sqLiteDBHelper = new SQLiteDBHelper (_dbName);
		sqLiteDBHelper.open ();
		string result = sqLiteDBHelper.readDecisionPointConfInfoAccordingToIdAndTableName (_decisionPointId, _tableName);
		sqLiteDBHelper.close ();
		return result;
	}
}
