using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tags;

public class GameController : MonoBehaviour {

	public static GameController _instance;

	public GameObject ScenePlayerPrefab;
	public GameObject OculusPlayerPrefab;
	public GameObject SceneObjects;
	public GameObject OculusObjects;

	public GameObject VirtualTags;
	public GameObject decisionArea;

	public delegate void OnInitialEvent();
	public event OnInitialEvent OnInitial;

	private GameObject player;
	private PlayerController playerController;
	private Vector3 PlayerStartPosition = Vector3.zero;
	private Landmark[] taskLandmarks;

	public string buildingName = BuildingName.Arlington;
	public string realEnvironmentVideo = RealEnvironmentVideo.True;
	public string displayType = DispalyType.Scene;
	public string strartingPoint = NorthStationStartingPoint.Entrance544;
	public string destination = NorthStaionDestination.Out;
	public string transmitType = TransmitType.Stair;
	public string trainingMode = TrainingMode.SelfExploration;

	void Awake(){
		_instance = this;

	}
	// Use this for initialization
	void Start () {
		GetModeSetting();
		if (buildingName == Tags.BuildingName.Arlington){
			Debug.Log ("Arlington Station.");
			taskLandmarks = TaskGenerater._instance.SetNewTask(strartingPoint, transmitType, destination);
			PlayerStartPosition = taskLandmarks[0].position;
			if(trainingMode == TrainingMode.SelfExploration){
				VirtualTags.SetActive(false);
			}
			else if(trainingMode == TrainingMode.PerceptApp){

			}

		} else {
			Debug.Log ("Knowles Building.");
			if (strartingPoint == Tags.KnowlesStartingPoint.EntranceEast) {
				PlayerStartPosition = KnowlesFixedLandmark.EntranceEast.position;
			} else if (strartingPoint == Tags.KnowlesStartingPoint.EntranceNorth) {
				PlayerStartPosition = KnowlesFixedLandmark.EntranceNorth.position;
			} else if (strartingPoint == Tags.KnowlesStartingPoint.EntranceWest) {
				PlayerStartPosition = KnowlesFixedLandmark.EntranceWest.position;
			}
			VirtualTags.SetActive(false);
		}

		if(displayType == DispalyType.Oculus){
			GameObject oculusgo = Instantiate(OculusPlayerPrefab, PlayerStartPosition, Quaternion.identity) as GameObject;
			oculusgo.transform.parent = GameObject.FindGameObjectWithTag(UnityTag.OculusObjects).transform;
			SceneObjects.SetActive(false);
		}else if(displayType == DispalyType.Scene){
			GameObject scenego = Instantiate(ScenePlayerPrefab,  PlayerStartPosition, Quaternion.identity) as GameObject;
			scenego.transform.parent = GameObject.FindGameObjectWithTag(UnityTag.SceneObjects).transform;
			OculusObjects.SetActive(false);
		}	
		this.OnInitial += OnPlayerInitial;
		OnInitial();
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

	void OnPlayerInitial(){
		player = GameObject.FindGameObjectWithTag(UnityTag.Player);
		if (buildingName == Tags.BuildingName.Arlington) {
			if (trainingMode == TrainingMode.SelfExploration) {
				player.GetComponent<TaskEvent> ().TaskInitial (taskLandmarks);
			} else if (trainingMode == TrainingMode.PerceptApp) {
				player.GetComponent<TaskEvent> ().enabled = false;
			}
		} else if (buildingName == Tags.BuildingName.KnowlesBuilding) {
			player.GetComponent<TaskEvent> ().enabled = false;
		}

	}
}
