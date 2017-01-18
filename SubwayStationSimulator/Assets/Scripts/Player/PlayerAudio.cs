using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class HitAudios
{
	public string collisionName;
	public AudioClip audio;
}


public class PlayerAudio : MonoBehaviour {
	
	private AudioClip audioClip;
	private AudioSource audioSource;
	private Dictionary<string, AudioClip> ad;
	
	public HitAudios[] hitAudios; 
	
	void Awake(){
		ad = new Dictionary<string, AudioClip>();
		foreach (var a in hitAudios){
			ad.Add(a.collisionName,a.audio);
		}
		audioSource = this.gameObject.GetComponent<AudioSource>();
		audioClip = audioSource.clip;
	}
	
	public void SetAudioClip(AudioClip clip){
		this.audioClip = clip;
	}
	
	public void PlayOneShot(string name){
		if(ad.ContainsKey(name)){
			ad.TryGetValue(name, out audioClip);
		}
		audioSource.PlayOneShot(audioClip);
	}
	
	public void PlayOneShot(){
		audioSource.PlayOneShot(audioClip);
	}
	public void Play(){
		audioSource.loop = true;
		if(!audioSource.isPlaying)
			audioSource.Play();
	}
	public void Stop(){
		audioSource.loop = false;
		audioSource.Stop();
	}
}