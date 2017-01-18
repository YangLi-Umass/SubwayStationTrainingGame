using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

	public UIPopupList BuildingNameList;
	public UIPopupList RealEnvironmentVideoList;
	public UIPopupList DisplayTypeList;
	public UIPopupList StartingPointList;
	public UIPopupList DestinationList;
	//public UIPopupList DestinationPreferList;
	//public UIPopupList DesiredDestinationList;

	public UIPopupList TransmitTypeList;
	public UIPopupList TrainingModeList;


	public UIButton StartButton;

	string buildingName = Tags.BuildingName.Arlington;
	string realEnvironmentVideo = Tags.RealEnvironmentVideo.True;
	string displayType = Tags.DispalyType.Scene;
	string startingPoint = Tags.NorthStationStartingPoint.Entrance544;
	string destination = Tags.NorthStaionDestination.Out;

	string transmitType = Tags.TransmitType.Elevator;
	string trainingMode = Tags.TrainingMode.SelfExploration;


	//string destinationPrefer = Tags.NorthStaionDestination.Out;
	//string desiredDestination = Tags.NorthStaionDestination.FareGate;

	// Use this for initialization
	void Start () {
		EventDelegate.Add(BuildingNameList.onChange, SetBuildingName);
		EventDelegate.Add(RealEnvironmentVideoList.onChange, SetRealEnvironmentVideo);
		EventDelegate.Add(DisplayTypeList.onChange, SetDisplayType);
		EventDelegate.Add(StartingPointList.onChange, SetStartPoint);
		EventDelegate.Add(DestinationList.onChange, SetDestination);
		EventDelegate.Add(TransmitTypeList.onChange, SetTransmitType);
		EventDelegate.Add(TrainingModeList.onChange, SetTrainingMode);
		//EventDelegate.Add(DesiredDestinationList.onChange, SetDesiredDestination);
		EventDelegate.Add(StartButton.onClick, OnStart);
	}

	void SetBuildingName(){
		buildingName = BuildingNameList.value;
		if (buildingName == Tags.BuildingName.Arlington) {
			StartingPointList.Clear();
			DestinationList.Clear();
			StartingPointList.value = Tags.NorthStationStartingPoint.Entrance544;
			StartingPointList.AddItem (Tags.NorthStationStartingPoint.Entrance544);
			StartingPointList.AddItem (Tags.NorthStationStartingPoint.Entrance562);
			StartingPointList.AddItem (Tags.NorthStationStartingPoint.Entrance575_1);
			StartingPointList.AddItem (Tags.NorthStationStartingPoint.Entrance575_2);
			StartingPointList.AddItem (Tags.NorthStationStartingPoint.Entrance582_1);
			StartingPointList.AddItem (Tags.NorthStationStartingPoint.Entrance582_2);
			DestinationList.value = Tags.NorthStaionDestination.In;
			DestinationList.AddItem (Tags.NorthStaionDestination.In);
			DestinationList.AddItem (Tags.NorthStaionDestination.Out);
			DestinationList.AddItem (Tags.NorthStaionDestination.FareGate);
			DestinationList.AddItem (Tags.NorthStaionDestination.InformationDesk);

		} else if (buildingName == Tags.BuildingName.KnowlesBuilding){
			StartingPointList.Clear();
			DestinationList.Clear();
			StartingPointList.value = Tags.KnowlesStartingPoint.EntranceEast;
			StartingPointList.AddItem (Tags.KnowlesStartingPoint.EntranceEast);
			StartingPointList.AddItem (Tags.KnowlesStartingPoint.EntranceNorth);
			StartingPointList.AddItem (Tags.KnowlesStartingPoint.EntranceWest);
			DestinationList.value = Tags.KnowlesDestination.FirstFloorElevator;
			DestinationList.AddItem (Tags.KnowlesDestination.FirstFloorElevator);
			DestinationList.AddItem (Tags.KnowlesDestination.SecondFloorRestroom);
			DestinationList.AddItem (Tags.KnowlesDestination.ThirdFloorRoom309);
		}
			
	}

	void SetRealEnvironmentVideo(){
		realEnvironmentVideo = RealEnvironmentVideoList.value;
	}

	void SetDisplayType(){
		displayType = DisplayTypeList.value;
	}

	void SetStartPoint(){
		startingPoint = StartingPointList.value;
	}

	void SetTransmitType(){
		transmitType = TransmitTypeList.value;
	}

	void SetDestination(){
		destination = DestinationList.value;
	}

	//void SetDestinationPrefer(){
	//	destinationPrefer = DestinationPreferList.value;
	//}

	void SetTrainingMode(){
		trainingMode = TrainingModeList.value;
	}

	//void SetDesiredDestination(){
	//	desiredDestination = DesiredDestinationList.value;
	//}

	void OnStart(){
		PlayerPrefs.SetString("BuildingName", buildingName);
		PlayerPrefs.SetString("RealEnvironmentVideo", realEnvironmentVideo);
		PlayerPrefs.SetString("DisplayType", displayType);
		PlayerPrefs.SetString("StartingPoint", startingPoint);
		PlayerPrefs.SetString("Destination", destination);
		PlayerPrefs.SetString("TransmitType", transmitType);
		PlayerPrefs.SetString("TrainingMode", trainingMode);

		//PlayerPrefs.SetString("DesiredDestination", desiredDestination);
		//PlayerPrefs.SetString("DestinationPrefer", destinationPrefer);

		if (buildingName == Tags.BuildingName.Arlington) {
			Application.LoadLevel(1);
		} else if (buildingName == Tags.BuildingName.KnowlesBuilding) {
			Application.LoadLevel(2);
		}
	}

}
