using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using UnityEngine.Events;

[RequireComponent (typeof (AudioSource))]

public class MainUiVideo : MonoBehaviour {

	//2016.3.22.afternoon
	public int desiredOrientation;
	public bool videoPlayable = false;
	public string textureOrientation;

	private MovieTexture movie;
	private AudioSource audio;


	private RawImage rim;
	private static string videoPath = "Assets/Streaming Assets/";
	private string buildingName;

	// Use this for initialization
	void Start () {

		//audio = GetComponent<AudioSource> ();
		//audio.clip = movie.audioClip;
		//audio.pitch = 3.0f;

		buildingName = buildingName = PlayerPrefs.GetString("BuildingName");
		//Test 2016.03.21.night
		rim = (RawImage)GetComponent<RawImage>();
		rim.texture = AssetDatabase.LoadAssetAtPath (videoPath + buildingName + "/" + "106-105," + textureOrientation + ".mp4", typeof (Texture)) as Texture;
		Debug.Log (videoPath + buildingName + "/" + "106-105," + textureOrientation + ".mp4");
		movie = rim.texture as MovieTexture;
		//WindowsVoice.theVoice.speak ("It is sunny today.");
	}

	void Update() {
		float v = Input.GetAxis("Vertical");

		if (!videoPlayable && movie.isPlaying) {
			movie.Pause ();
			//audio.Pause ();
		}

		if (v > 0) {
			if (!movie.isPlaying && videoPlayable) {
				movie.Play ();
				//audio.Play ();
			} 
		} else {
			if (movie.isPlaying) {
				movie.Pause ();
				//audio.Pause ();
			}
		}
			
	}

	public void changeVideoTexture(string videoName) {

		rim.texture = AssetDatabase.LoadAssetAtPath ( videoPath + buildingName + "/" + videoName + ".mp4", typeof (Texture)) as Texture;
		if (rim.texture != null) {
			movie = rim.texture as MovieTexture;
		} 
		//audio = GetComponent<AudioSource> ();
		//audio.clip = movie.audioClip;
		//audio.pitch = 1.0f;
	}
}