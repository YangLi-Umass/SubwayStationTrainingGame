using UnityEngine;
using System.Collections;

public class VirtualTag : MonoBehaviour {
	// general tag
	public int SourceId;
	[HideInInspector]
	public int DestinationId;
	public long RFID;

	// virtual tag special
	public int TagOpening;
	
	private TriggerBody triggerBody;
	private RFTag rfTag;

	void Awake(){
		rfTag = this.GetComponentInChildren<RFTag>();
		triggerBody = this.GetComponentInChildren<TriggerBody>();
	}

	// Update is called once per frame
	void Update () {
		if(triggerBody.InArea == true){
			rfTag.PlaySound();
		}else{
			rfTag.StopSound();
		}
	}
}
