using UnityEngine;
using System.Collections;

public class CaneDetector : MonoBehaviour {

	public AudioClip wallhit;
	public AudioClip fareGatehit;
	public AudioClip pillarhit;
	public AudioClip fareMachinehit;
	public AudioClip metalGatehit;

	public GameObject RightAudio;
	public GameObject LeftAudio;

	private PlayerAudio rightAudioController;
	private PlayerAudio leftAudioController;
	private float DetectionDistance = 0.5f;
	private bool rightDectected = false;
	private bool leftDectected = false;

	void Awake(){
		rightAudioController = RightAudio.GetComponent<PlayerAudio>();
		leftAudioController = LeftAudio.GetComponent<PlayerAudio>();
	}
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Wall"){
			AudioSource.PlayClipAtPoint(wallhit, other.gameObject.transform.position);
		}
		else if(other.gameObject.tag == "FareGate"){
			AudioSource.PlayClipAtPoint(fareGatehit, other.gameObject.transform.position);
		}
		else if(other.gameObject.tag == "Pillar"){
			AudioSource.PlayClipAtPoint(pillarhit, other.gameObject.transform.position);
		}
		else if(other.gameObject.tag == "FareMachine"){
			AudioSource.PlayClipAtPoint(fareMachinehit, other.gameObject.transform.position);
		}
		else if(other.gameObject.tag == "MetalGate"){
			AudioSource.PlayClipAtPoint(metalGatehit, other.gameObject.transform.position);
		}
	}
}
