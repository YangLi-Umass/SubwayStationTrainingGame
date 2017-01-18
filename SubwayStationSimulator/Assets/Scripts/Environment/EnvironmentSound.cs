using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource)) ]
public class EnvironmentSound : MonoBehaviour {

	public AudioClip sound;
	public bool isContinueSound;
	// Use this for initialization
	void Start () {
		GetComponent<AudioSource>().clip = sound;
		GetComponent<AudioSource>().playOnAwake = false;
		GetComponent<AudioSource>().volume = 0.5f;
		GetComponent<AudioSource>().rolloffMode = AudioRolloffMode.Linear;
		GetComponent<AudioSource>().priority = 216;
		GetComponent<AudioSource>().spatialBlend = 1;
		GetComponent<AudioSource>().reverbZoneMix = 0.61374f;
		GetComponent<AudioSource>().dopplerLevel = 1;
		GetComponent<AudioSource>().spread = 10;
		GetComponent<AudioSource>().maxDistance = 5;
		if(!isContinueSound)
			StartCoroutine(wait());
	}

	// Update is called once per frame
	void Update () {
		if(isContinueSound){
			if(!GetComponent<AudioSource>().isPlaying){
				PlaySoundContinue();
			}
		}
	}

	void PlaySoundRondom(){
		if(!GetComponent<AudioSource>().isPlaying)
		GetComponent<AudioSource>().Play();
	}

	void PlaySoundContinue(){
		if(!GetComponent<AudioSource>().isPlaying)
			GetComponent<AudioSource>().Play();
	}

	IEnumerator wait(){
		while(true){
			int second=Random.Range(10,30);
			yield return new WaitForSeconds(second);
			PlaySoundRondom();
		}
	}
}
