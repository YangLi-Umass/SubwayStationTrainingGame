using UnityEngine;
using System.Collections;

public class VideoOnDecision : MonoBehaviour {
	//for test
	public bool test = false;

	//to do
	public int DECISION_POINT_ID;

	public string destination;
	public string buildingName;
	public string transmitType;		//Elevator or stair

	private DecisionPointDataType[] dataType;
	private string videoName;
	private int desiredOri;
	//li
	GameObject[] gameObjectsVideoTexture;


	// Use this for initialization
	void Start () {
		dataType = new DecisionPointDataType[4];
		dataType [0] = new DecisionPointDataType (3);
		dataType [1] = new DecisionPointDataType (6);
		dataType [2] = new DecisionPointDataType (9);
		dataType [3] = new DecisionPointDataType (12);
		gameObjectsVideoTexture = GameObject.FindGameObjectsWithTag ("VideoTexture");

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (test) {
			Debug.Log ("Enter decision area " + DECISION_POINT_ID);
			return;
		}
		if (other.gameObject.tag == "Player") {
			
			setDesBuiTran ();

			string result = readDecisionPointConfigInfoFromDB (buildingName + "DecisionPoint.db", "destination" + transmitType + destination, DECISION_POINT_ID);
			Debug.Log (result);
			string[] textTemp = result.Split (new char [] {','});
			desiredOri = int.Parse(textTemp [0]);
			videoName = textTemp [1];
			if(textTemp[0].Equals("3")){
				dataType [0] = new DecisionPointDataType (textTemp [1], 3, destination);
		
			} else if (textTemp[0].Equals("6")) {
				dataType [1] = new DecisionPointDataType (textTemp [1], 6, destination);
			
			} else if (textTemp[0].Equals("9")) {
				dataType [2] = new DecisionPointDataType (textTemp [1], 9, destination);

			} else if (textTemp[0].Equals("12")) {
				dataType [3] = new DecisionPointDataType (textTemp [1], 12, destination);

			}

			Debug.Log ("Enter decision point.");
			int currentPlayerOrientation = other.gameObject.GetComponent<PlayerController>().PlayerClockDirection;
			WindowsVoice.theVoice.speak ("Players enter decision areas.");
			WindowsVoice.theVoice.speak ("Right now the orientation is " + currentPlayerOrientation + " clock.");
			for (int i = 0; i < dataType.Length; i++) {
				if (dataType[i].getEnable()) {
					WindowsVoice.theVoice.speak ("if you want to go to " + dataType[i].getDescribe());
					int orientationDifference = currentPlayerOrientation - dataType [i].getExitOrientation ();
					if (orientationDifference > 0) {
						if (orientationDifference == 9) {
							WindowsVoice.theVoice.speak ("Please turn right " + 90 + " degrees.");
						} else {
							int degree = orientationDifference / 3 * 90;
							WindowsVoice.theVoice.speak ("Please turn left " + degree + " degrees.");
						}
					} else if (orientationDifference == 0) {
						WindowsVoice.theVoice.speak ("Please go forward.");
					} else {
						if (orientationDifference == -9) {
							WindowsVoice.theVoice.speak ("Please turn left " + 90 + " degrees.");
						} else {
							int degree = -orientationDifference / 3 * 90;
							WindowsVoice.theVoice.speak ("Please turn right " + degree + " degrees.");
						}
					}
				}
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (test) {
			Debug.Log ("Exit decision area " + DECISION_POINT_ID);
			return;
		}
		if (other.gameObject.tag == "Player") {
			
			int currentPlayerOrientation = other.gameObject.GetComponent<PlayerController>().PlayerClockDirection;

			for (int i = 0; i < dataType.Length; i++) {
				if (dataType[i].getExitOrientation() == currentPlayerOrientation) {
					Debug.Log ("OnTriggerExit, current direction: " + dataType[i].getEnable());
					if (dataType [i].getEnable()) {
						Debug.Log ("PlayerCurrentDirection: " + currentPlayerOrientation + " is valid.");
					} else {
						Debug.Log ("PlayerCurrentDirection: " + currentPlayerOrientation + " is NOT valid.");
					}
				}
			}

			for (int i = 0; i < gameObjectsVideoTexture.Length; i++) {
				VideoPlayController targetScript = gameObjectsVideoTexture[i].GetComponent<VideoPlayController>();
				targetScript.desiredOrientation = desiredOri;
				targetScript.setMovieTexture (buildingName, videoName, 1.0f, currentPlayerOrientation);

			}
		}

	}

	public string readDecisionPointConfigInfoFromDB (string _dbName, string _tableName, int _decisionPointId) {
		
		SQLiteDBHelper sqLiteDBHelper = new SQLiteDBHelper (_dbName);
		sqLiteDBHelper.open ();
		string result = sqLiteDBHelper.readDecisionPointConfInfoAccordingToIdAndTableName (_decisionPointId, _tableName);
		sqLiteDBHelper.close ();
		return result;

	}

	//Utility Get destination, buildingName, transmitType from PlayerPrefs
	private void setDesBuiTran(){
		NorthStationGameController gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<NorthStationGameController>();
		destination = gameController.destination;
		buildingName = gameController.buildingName;
		transmitType = gameController.transmitType;

		//destination = PlayerPrefs.GetString("Destination");
		//buildingName = PlayerPrefs.GetString("BuildingName");
		//transmitType = PlayerPrefs.GetString("TransmitType");
	}
		
}
