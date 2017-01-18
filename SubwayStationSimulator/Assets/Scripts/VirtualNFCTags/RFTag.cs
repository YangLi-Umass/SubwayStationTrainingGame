using UnityEngine;
using System.Collections;

public class RFTag : MonoBehaviour {

	private AudioSource audioSource;

	void Awake(){
		audioSource = this.GetComponent<AudioSource>();
	}

	public void PlaySound(){
		if(!audioSource.isPlaying)
			audioSource.Play();
	}

	public void StopSound(){
		if(audioSource.isPlaying)
			audioSource.Stop();
	}


}
