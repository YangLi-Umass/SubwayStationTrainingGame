using UnityEngine;
using System.Collections;
using Tags;

public class TerrainChange : MonoBehaviour {
	
	public AudioClip sound;

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player"){
			other.gameObject.GetComponent<PlayerController>().SetAudio(sound);
		}
	}

	void OnTriggerExit(Collider other){
		if(other.gameObject.tag == "Player"){
			other.gameObject.GetComponent<PlayerController>().ResetAudio();
		}
	}


}
