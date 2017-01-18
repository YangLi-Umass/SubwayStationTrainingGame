using UnityEngine;
using System.Collections;
using StaticVariable;

public class NorthStationGameController : MonoBehaviour {

	public GameObject ScenePlayerPrefab;

	public string buildingName = BuildingName.NORTH_STATION;
	public string realEnvironmentVideo = RealEnvironmentVideo.True;
	public string displayType = DispalyType.Scene;
	public string strartingPoint = NorthStationStartingPoint.StartPoint1;
	public string destination = NorthStationDestination.InBoundOrangeLine;
	public string transmitType = TransmitType.Stair;
	public string trainingMode = TrainingMode.SelfExploration;

	private Vector3 PlayerStartPosition = Vector3.zero;

	// Use this for initialization
	void Start () {
		//GetModeSetting ();
		if (strartingPoint == NorthStationStartingPoint.StartPoint1) {
			PlayerStartPosition = NorthStationFixedLandmark.StartPoint1.position;
		} 
		GameObject scenego  = Instantiate(ScenePlayerPrefab,  PlayerStartPosition, Quaternion.identity) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GetModeSetting(){
		buildingName = PlayerPrefs.GetString("BuildingName");
		realEnvironmentVideo = PlayerPrefs.GetString("RealEnvironmentVideo");
		displayType = PlayerPrefs.GetString("DisplayType");
		strartingPoint = PlayerPrefs.GetString("StartingPoint");
		destination = PlayerPrefs.GetString("Destination");
		transmitType = PlayerPrefs.GetString("TransmitType");
		trainingMode = PlayerPrefs.GetString("TrainingMode");
	}
}
