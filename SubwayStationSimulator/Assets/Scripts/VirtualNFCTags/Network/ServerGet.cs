using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PERCEPT.Web.Data.Models;
using PERCEPT.Web.Data.Models.DTO;

public class ServerGet{

	string baseURL = "http://percept.ecs.umass.edu/PERCEPTSIRI/";
	public string instruction{ get; set;}

	HashSet<int> sourcelist = new HashSet<int>(); 
	 
	public IEnumerator DownloadInstruction(int SourceID, int DestinationID){
		string entireURL = baseURL + GetNavigationInstructionURL(SourceID, DestinationID, 123);
//		Debug.Log(entireURL);
		WWW checkurl = new WWW(entireURL);
		yield return checkurl;
		Debug.Log(checkurl.text);
		IEnumerable<DTO_BaseNavInstructionUnitTest> node = 
			JsonConvert.DeserializeObject<IEnumerable<DTO_BaseNavInstructionUnitTest>>(checkurl.text);

		foreach (DTO_BaseNavInstructionUnitTest Instruction in node){
			instruction = " "+Instruction.BaseDirections;
		}

//		DTO_BaseNavInstructionUnitTest firstItem = First<DTO_BaseNavInstructionUnitTest>(node);
//		string txt = firstItem.BaseDirections;
//		Debug.Log (txt);

//		DTO_BaseNavInstructionUnitTest Item = ElementAt<DTO_BaseNavInstructionUnitTest>(node, 0);
//		Tag.GetPosition(Item.SourceLandmarkId);
//		playerPosition = new Vector2( Tag.GetPosition(Item.SourceLandmarkId).x,Tag.GetPosition(Item.SourceLandmarkId).y);

	}

//	static T First<T>(IEnumerable<T> items)
//	{
//		using(IEnumerator<T> iter = items.GetEnumerator())
//		{
//			iter.MoveNext();
//			return iter.Current;
//		}
//	}
//
//	static T ElementAt<T>(IEnumerable<T> items, int index)
//	{
//		using(IEnumerator<T> iter = items.GetEnumerator())
//		{
//			for (int i = 0; i <= index; i++, iter.MoveNext()) ;
//			return iter.Current;
//		}
//	}

	private string GetNavigationInstructionURL(long sourceId, long destinationId, long buildingId){
		return "api/building/" + buildingId + "/source/"+ sourceId + "/destination/" + destinationId + "/BaseNavInstructionUnitTest";
	}

//	private string GetLandmarksForBuildingURL(long buildingId){
//		return "api/" + buildingId + "/building/LandmarkNode/forOfflineD";
//	}

//	private string GetLandmarksForBuildingURL(long buildingId){
//		return "api/building/" + buildingId + "/BaseNavInstructionUnitTest";
//	}

}