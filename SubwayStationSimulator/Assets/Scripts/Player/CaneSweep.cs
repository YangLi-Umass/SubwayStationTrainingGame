using UnityEngine;
using System.Collections;

public class CaneSweep : MonoBehaviour {

	// Update is called once per frame
	public float rotationSpeed = 0;
	public float maxRotation = 80;
	public float yRotation = 0;
	public Transform player;
	public Vector3 ortEuler;
	public AudioSource caneAudio;
	public AudioClip canewallSound;
	public float swapSpeed = 3f;

	private float radian = 0;

	[SerializeField]
	private float RotationAccuracy = 3f;
	
	private float RotationAngleLeft = 0;
	private float RotationAngleRight = 0;
	private PlayerController playerController;
	private Quaternion currentForward;
	private Quaternion playerForward;

	void Awake(){
		playerController = player.GetComponent<PlayerController>();
	}

	void Start(){
		RotationAngleLeft = 90 - maxRotation/2;
		RotationAngleRight = maxRotation/2 + 90; 

		ortEuler = playerController.ort.eulerAngles;
		ortEuler.y = ortEuler.y + 90f;
		ortEuler.z = ortEuler.x = 0f;
		playerForward = Quaternion.Euler(ortEuler);

		transform.rotation = playerForward;
		currentForward = playerForward;

		ResetAudio();
	}
	
	void Update () {
//		float h = Input.GetAxis("Horizontal");
		radian += (Time.deltaTime * swapSpeed);
		float h = Mathf.Cos(radian);

//		if(h != 0){
//			if(!caneAudio.isPlaying)
//			caneAudio.Play();
//		}else{
//			caneAudio.Stop();
//		}
		CaneTurning(h);

		ortEuler = playerController.ort.eulerAngles;
		ortEuler.y = ortEuler.y + 90f;
		ortEuler.z = ortEuler.x = 0f;
		playerForward = Quaternion.Euler(ortEuler);

		if(playerForward != currentForward){
			float angle = Quaternion.Angle(playerForward, currentForward);
			if(angle >= RotationAccuracy){
				transform.rotation = playerForward;
				currentForward = playerForward;
			}
		}
	}

	public void SetAudioClip(AudioClip audioClip){
		caneAudio.clip = audioClip;
		caneAudio.loop = true;
	}
	public void ResetAudio(){
		caneAudio.clip = canewallSound;
		caneAudio.loop = true;
	}
	
	void CaneTurning(float h){
			yRotation = transform.localEulerAngles.y;
			yRotation += h * rotationSpeed;
			if((yRotation > RotationAngleLeft)&&(yRotation < RotationAngleRight)){
				transform.localEulerAngles = new Vector3(0, yRotation, 0);
		}else if(yRotation <= RotationAngleLeft){
			yRotation = RotationAngleLeft;
			transform.localEulerAngles = new Vector3(0, yRotation, 0);
		}else if(yRotation >= RotationAngleRight){
			yRotation = RotationAngleRight;
			transform.localEulerAngles = new Vector3(0, yRotation, 0);
		}
	}
}
