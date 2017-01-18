using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tags;

public class TriggerBody : MonoBehaviour {

	public bool InArea = false;

	private VirtualTag virtualTagManage;
	private AudioSource scanTagSound;
	private int SourceID;
	private int DestinationID;
	private long RFID;
	private int TagOpening;
	private ServerGet serverGet;
	private PlayerController playerController;
	private bool movale = false; // movable when the direction is right
	private bool TagGetInstruction_Flag = true;
	private string instruction;
	private List<string> SubLandmarkers;
	private string instructionForLandmarker;
	private bool tagIsScanned = false;
	private int direct = 0;
	private int playerLastDirection; // for direction change detect
	private SignalRUnityController signalR;
	private bool tagIdSend = false;

	void Awake(){
		virtualTagManage = this.GetComponentInParent<VirtualTag>();
		scanTagSound = this.GetComponentInParent<AudioSource>();
		SourceID = virtualTagManage.SourceId;
		DestinationID = virtualTagManage.DestinationId;
		RFID = virtualTagManage.RFID;
		TagOpening = virtualTagManage.TagOpening;
		signalR = GameObject.FindGameObjectWithTag(UnityTag.SignalR).GetComponent<SignalRUnityController>();
	}

	// Use this for initialization
	void Start () {
		serverGet = new ServerGet();
	}
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == UnityTag.Player){
			signalR._subscription.Data += ScanTag;
			tagIsScanned = false;
			tagIdSend = false;
			InArea = true;
			//download the instruction
			StartCoroutine(serverGet.DownloadInstruction(SourceID,DestinationID));

			// adjust player to tag's center position
			playerController = GameObject.FindGameObjectWithTag(UnityTag.Player).GetComponent<PlayerController>();
			playerController.SetPosition(this.transform.position);
			playerController.DisableMovement();
			//System.Diagnostics.Process.Start("say", ("Please Scan the tag with smartphone"));
		}
	}

	void OnTriggerStay(Collider other){
		if(other.gameObject.tag == UnityTag.Player){
			if ((serverGet.instruction != null) && (TagGetInstruction_Flag == true)){
				instruction = StringProcess.GetInstructionPhase (serverGet.instruction);
				instruction = StringProcess.DeleteDescription (instruction);
				instructionForLandmarker = instruction.Substring(0,instruction.IndexOf(',') + 1);
				Debug.Log (instructionForLandmarker);
				int diff = TagOpening - playerController.PlayerClockDirection;
				playerLastDirection = playerController.PlayerClockDirection;
				direct = StringProcess.PlayerDirectionBasedonTags(TagOpening, instructionForLandmarker);
//				instructionForLandmarker = StringProcess.DirectConvertor(diff, "the tag", instructionForLandmarker);
//				Debug.Log(instructionForLandmarker);
				TagGetInstruction_Flag = false;
			} 
			if(tagIsScanned == true){
				if(tagIdSend == false){
					signalR.Send(RFID.ToString());
					scanTagSound.Play();
					tagIdSend = true;
				}
				playerController.SetLockDirection(direct);
				if(playerLastDirection != playerController.PlayerClockDirection){
					if(playerController.PlayerClockDirection != direct){
						//System.Diagnostics.Process.Start("say", ("Wrong direction, please " + instructionForLandmarker));
						Debug.Log("Wrong direction, please " + instructionForLandmarker);
					}
					else{
						//System.Diagnostics.Process.Start("say", ("Nice work, keep following the instruction with the smartphone to find next tag"));
						Debug.Log("Nice work, keep going");
					}
					playerLastDirection = playerController.PlayerClockDirection;
				}
			}
			if(Input.GetKeyDown( KeyCode.W ) && (tagIsScanned == false)){
 				Debug.Log("Please Scan the tag with smartphone");
				//System.Diagnostics.Process.Start("say", ("Please Scan the tag with smartphone"));
			}
		}

	}

	void OnTriggerExit(Collider other){
		if(other.gameObject.tag == UnityTag.Player){
			InArea = false;
			TagGetInstruction_Flag = true;
			tagIsScanned = false;
			tagIdSend = false;
			playerController.ResetLockDirection();
		}
	}
	void ScanTag(object[] data){
		
		tagIsScanned = true;
	}
	/************************************************************************************************/
	// for debug
	void Update(){
		if(Input.GetKeyDown(KeyCode.Space)){
			tagIsScanned = true;
		}
	}
	/************************************************************************************************/
	
}
