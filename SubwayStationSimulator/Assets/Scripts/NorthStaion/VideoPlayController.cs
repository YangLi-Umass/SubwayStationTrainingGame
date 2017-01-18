using UnityEngine;
using System.Collections;
using UnityEditor;

[RequireComponent (typeof (AudioSource))]

public class VideoPlayController : MonoBehaviour {

	public int textureOrientation;
	public int desiredOrientation;
	public bool videoPlayable = false;

	private MovieTexture movie;
	private AudioSource audio;

	private static readonly string VIDEO_PATH = "Assets/Streaming Assets/";

	// Use this for initialization
	void Start () {
		setMovieTexture("NorthStation","6-8",1.0f,12);
		setVideoPlayable (true);

	}
	
	// Update is called once per frame
	void Update () {
		float v = Input.GetAxis("Vertical");
		if (movie == null)
			return;
		if (!videoPlayable && movie.isPlaying) {
			movie.Pause ();
			audio.Pause ();
		}

		if (v > 0) {
			if (!movie.isPlaying && videoPlayable) {
				movie.Play ();
				audio.Play ();
			} 
		} else {
			if (movie.isPlaying) {
				movie.Pause ();
				audio.Pause ();
			}
		}
	}

	//Utility
	public void setMovieTexture(string buildingName, string videoName, float videoSpeed, int playerOri) {
		//to do

		GetComponent<Renderer> ().material.mainTexture =  AssetDatabase.LoadAssetAtPath (VIDEO_PATH + buildingName + "/" + videoName + "," + textureOrientation + ".mp4", typeof (Texture)) as Texture;
		GetComponent<Renderer> ().material.SetTextureScale ("_MainTex", new Vector2(-1,-1));
		GetComponent<Renderer> ().material.SetTextureOffset ("_MainTex", new Vector2(1,1));
		movie = (MovieTexture)GetComponent<Renderer> ().material.mainTexture;
		if (movie == null) {
			Debug.Log ("Cannot find the video file.");
			return;
		}
		audio = (AudioSource)GetComponent<AudioSource> ();
		audio.clip = movie.audioClip;
		audio.pitch = videoSpeed;
		if (playerOri == desiredOrientation) {
			setVideoPlayable (true);
		} else {
			setVideoPlayable (false);
		}

	}

	public void setVideoPlayable(bool _b) {
		videoPlayable = _b;
	}


}
