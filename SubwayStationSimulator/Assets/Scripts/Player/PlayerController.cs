using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public bool isOculusV = false;

	public float playerHeight = 2f;
	public float Acceleration = 0.1f;
	public float Damping = 0.3f;
	public float BackAndSideDampen = 0.5f;
	public float RotationAmount = 1.5f;
	public float RotationRatchet = 45.0f;
	public float moveSpeed;
	public bool HmdRotatesY = true;
	public Vector3 euler = Vector3.zero;
	public Quaternion ort;
	public Vector3 moveDirection = Vector3.zero;
	public float DetectionDistance = 1f;
	public int PlayerClockDirection = 12; // clock 1 - 12
	
	public AudioClip CollisionWarning;
	public AudioClip StepSound;
	public AudioSource playAudio;
	public AudioSource collisionAudio;
	
	public GameObject RightAudio;
	public GameObject LeftAudio;

	private float YRotation = 0.0f;
	private float MoveScale = 1.0f;
	private Vector3 MoveThrottle = Vector3.zero;
	private bool  HaltUpdateMovement = false;

	private float SimulationRate = 60f;
	private Rigidbody player;
	private bool Nomove = false;
	
	private PlayerAudio rightAudioController;
	private PlayerAudio leftAudioController;

	private bool Movable = true;
	private int LockPlayerDirection = 0;

	protected OVRCameraRig CameraController = null;
	
	private float MoveScaleMultiplier = 1.0f;
	private float RotationScaleMultiplier = 1.0f;
	private bool prevHatLeft = false;
	private bool prevHatRight = false;


	//li
	GameObject[] gameObjectsVideoTexture;
	void Awake()
	{
		player = this.GetComponent<Rigidbody>();
		OVRCameraRig[] CameraControllers;
		CameraControllers = gameObject.GetComponentsInChildren<OVRCameraRig>();
		
		if(CameraControllers.Length == 0)
			Debug.LogWarning("OVRPlayerController: No OVRCameraRig attached.");
		else if (CameraControllers.Length > 1)
			Debug.LogWarning("OVRPlayerController: More then 1 OVRCameraRig attached.");
		else
			CameraController = CameraControllers[0];

		YRotation = transform.rotation.eulerAngles.y;
		rightAudioController = RightAudio.GetComponent<PlayerAudio>();
		leftAudioController = LeftAudio.GetComponent<PlayerAudio>();
	}

	void Start(){
		playAudio.clip = StepSound;
		playAudio.loop = true;
		gameObjectsVideoTexture = GameObject.FindGameObjectsWithTag ("VideoTexture");
	}

    void Update()
	{
		// move speed adjust change speed
		//if(Input.GetKey(KeyCode.N)){
		//	moveSpeed = 8f;
		//}else{
		//	moveSpeed = 2f;
		//}
		if(!Nomove)
			player.MovePosition(player.position + moveDirection *  moveSpeed);
		else
			player.MovePosition(player.position + moveDirection * 0.2f);
		// info btn detection
		RayDetection(transform);
		if(Input.GetKeyDown(KeyCode.I)){
			ReadInformation(transform);
		}
		/**************************************************************************************************************/
		if(isOculusV){
			UpdateMovement();
			
			float motorDamp = (1.0f + (Damping * SimulationRate * Time.deltaTime));
			
			MoveThrottle.x /= motorDamp;
			MoveThrottle.y = (MoveThrottle.y > 0.0f) ? (MoveThrottle.y / motorDamp) : MoveThrottle.y;
			MoveThrottle.z /= motorDamp;
			
			moveDirection += MoveThrottle * SimulationRate * Time.deltaTime;
			
			Vector3 predictedXZ = Vector3.Scale((transform.localPosition + moveDirection), new Vector3(1, 0, 1));
			// Move contoller
			player.MovePosition(player.position + moveDirection);
			
			Vector3 actualXZ = Vector3.Scale(transform.localPosition, new Vector3(1, 0, 1));
			
			if (predictedXZ != actualXZ)
				MoveThrottle += (actualXZ - predictedXZ) / (SimulationRate * Time.deltaTime);
		}else{
			Movement();
		}


	}

	public void SetLockDirection(int direction){
		LockPlayerDirection = direction;
	}

	public void ResetLockDirection(){
		LockPlayerDirection = 0;
	}

	public void SetPosition(Vector3 position){
		this.transform.position = position;
	}
	public void SetAudio(AudioClip audioClip){
		playAudio.clip = audioClip;
		playAudio.loop = true;
	}
	public void ResetAudio(){
		playAudio.clip = StepSound;
		playAudio.loop = true;
	}

	void RayDetection(Transform trans){
		Ray forwardRay = new Ray(trans.position+new Vector3(0,0.5f,0), trans.forward);
		Ray rightRay = new Ray(trans.position, trans.right);
		Ray leftRay = new Ray(trans.position, -trans.right);
		RaycastHit righthit;
		RaycastHit lefthit;
		RaycastHit forwardhit;

		if(Physics.Raycast(rightRay, out righthit, DetectionDistance)){
			Debug.DrawLine(rightRay.origin,righthit.point, Color.yellow);
			rightAudioController.Play();
			leftAudioController.Stop();
		}
		else if(Physics.Raycast(leftRay, out lefthit, DetectionDistance)){
			Debug.DrawLine(leftRay.origin,lefthit.point, Color.blue);
			leftAudioController.Play();
			rightAudioController.Stop();
		}else{
			leftAudioController.Stop();
			rightAudioController.Stop();
		}
		Physics.Raycast(forwardRay, out forwardhit);
		Debug.DrawLine(forwardRay.origin,forwardhit.point,Color.red);

		if(forwardhit.distance < 0.5f){
			Nomove = true;
			if((forwardhit.collider != null) && (forwardhit.collider.gameObject.tag == "Wall")){
				collisionAudio.clip = CollisionWarning;
				collisionAudio.loop = false;
				collisionAudio.Play();
			}
		}else{
			Nomove = false;
			collisionAudio.Stop();
		}
	}

	void ReadInformation(Transform trans){
		string[] Information = new string[3];
		Ray rightRay = new Ray(trans.position, trans.right);
		Ray leftRay = new Ray(trans.position, -trans.right);
		Ray forwardRay = new Ray(trans.position+new Vector3(0,0.5f,0), trans.forward);
		RaycastHit righthit;
		RaycastHit lefthit;
		RaycastHit forwardhit;
		if(Physics.Raycast(rightRay, out righthit, DetectionDistance)){
			Debug.DrawLine(rightRay.origin,righthit.point);
			Information[1] = "You right side is the " + righthit.collider.gameObject.tag;
			//			Debug.Log(righthit.collider.gameObject.name);
		}
		if(Physics.Raycast(leftRay, out lefthit, DetectionDistance)){
			Debug.DrawLine(leftRay.origin,lefthit.point);
			Information[2] = "You left side is the " + lefthit.collider.gameObject.tag;
			//			Debug.Log(lefthit.collider.gameObject.name);
		}
		if(Physics.Raycast(forwardRay, out forwardhit, DetectionDistance)){
			Debug.DrawLine(forwardRay.origin,forwardhit.point);
			Information[0] = "You are in front to the " + forwardhit.collider.gameObject.tag;
		}
		string info = "";
		foreach (string i in Information){
			Debug.Log(i);
			info += i + ";  ";
		}
		System.Diagnostics.Process.Start("say", info);

	}

	public void Movement(){

		if(LockPlayerDirection != 0){
			if(LockPlayerDirection == PlayerClockDirection){
				Movable = true;
			}else{
				Movable = false;
			}
		}

		//forward
		if(Movable == true){
			float v = Input.GetAxis("Vertical");
			if(v > 0){
				if(!playAudio.isPlaying){
					playAudio.Play();
				}
				player.MovePosition (transform.position + transform.forward * moveSpeed * Time.deltaTime);
			}else{
				playAudio.Stop();
			}
		}
		//turn

		if(Input.GetKeyDown(KeyCode.J)){
			
			TurnToInverse(3);
			Debug.Log("PlayerCurrentDirection: " + PlayerClockDirection);
			checkDesiredOrientation ();

		}
		if(Input.GetKeyDown(KeyCode.K)){
			
			TurnToWise(3);
			Debug.Log("PlayerCurrentDirection: " + PlayerClockDirection);
			checkDesiredOrientation ();

		}
	}

	private void checkDesiredOrientation(){


		Debug.Log(gameObjectsVideoTexture.Length);
		for (int i = 0; i < gameObjectsVideoTexture.Length; i++) {
			VideoPlayController targetScript = gameObjectsVideoTexture[i].GetComponent<VideoPlayController>();

			if (PlayerClockDirection == targetScript.desiredOrientation) {
				targetScript.videoPlayable = true;

			} else {
				targetScript.videoPlayable = false;
			}

			if (targetScript.textureOrientation == PlayerClockDirection) {
				gameObjectsVideoTexture [i].GetComponent<Renderer> ().enabled = true;

			} else {
				gameObjectsVideoTexture [i].GetComponent<Renderer> ().enabled = false;
			}
		}

	}

	public void TurnToWise(int index){

		PlayerClockDirection=PlayerClockDirection+index;
		if(PlayerClockDirection > 11){
			PlayerClockDirection = PlayerClockDirection % 12;
		}
		if (PlayerClockDirection == 0) {
			PlayerClockDirection = 12;
		}
		Vector3 ortEuler = transform.rotation.eulerAngles;
		ortEuler.z = ortEuler.x = 0;
		ortEuler.y =  Mathf.RoundToInt((PlayerClockDirection) * 30f);
		transform.rotation = Quaternion.Euler(ortEuler);
	}
	
	public void TurnToInverse(int index){
		
		PlayerClockDirection=PlayerClockDirection-index;
		
		if(PlayerClockDirection < 0){
			PlayerClockDirection = PlayerClockDirection + 12;
		}
		if (PlayerClockDirection == 0) {
			PlayerClockDirection = 12;
		}
		Vector3 ortEuler = transform.rotation.eulerAngles;
		ortEuler.z = ortEuler.x = 0;
		ortEuler.y =  Mathf.RoundToInt((PlayerClockDirection) * 30f);
		transform.rotation = Quaternion.Euler(ortEuler);
	}

	public void DisableMovement(){
		Movable = false;
	}

	public void EnableMovement(){
		Movable = true;
	}
	
	public void UpdateMovement()
	{
		if (HaltUpdateMovement)
			return;

		bool moveForward = Input.GetKey(KeyCode.W);
		bool moveLeft = Input.GetKey(KeyCode.A);
		bool moveRight = Input.GetKey(KeyCode.D);
		bool moveBack = Input.GetKey(KeyCode.S);
		
		bool dpad_move = false;
		
		if (OVRGamepadController.GPC_GetButton(OVRGamepadController.Button.Up))
		{
			moveForward = true;
			dpad_move   = true;
			
		}
		if (OVRGamepadController.GPC_GetButton(OVRGamepadController.Button.Down))
		{
			moveBack  = true;
			dpad_move = true;
		}
		
		MoveScale = 1.0f;
		
		if ( (moveForward && moveLeft) || (moveForward && moveRight) ||
		    (moveBack && moveLeft)    || (moveBack && moveRight) )
			MoveScale = 0.70710678f;
		
		MoveScale *= SimulationRate * Time.deltaTime;
		
		// Compute this for key movement
		float moveInfluence = Acceleration * 0.1f * MoveScale * MoveScaleMultiplier;
		moveInfluence *= 0.5f;
		
		Quaternion ort = transform.rotation;
		Vector3 ortEuler = ort.eulerAngles;
		ortEuler.z = ortEuler.x = 0f;
		ort = Quaternion.Euler(ortEuler);
		
		if (moveForward)
			MoveThrottle += ort * (transform.lossyScale.z * moveInfluence * Vector3.forward);
		if (moveBack)
			MoveThrottle += ort * (transform.lossyScale.z * moveInfluence * BackAndSideDampen * Vector3.back);
		if (moveLeft)
			MoveThrottle += ort * (transform.lossyScale.x * moveInfluence * BackAndSideDampen * Vector3.left);
		if (moveRight)
			MoveThrottle += ort * (transform.lossyScale.x * moveInfluence * BackAndSideDampen * Vector3.right);
		
		bool curHatLeft = OVRGamepadController.GPC_GetButton(OVRGamepadController.Button.LeftShoulder);
		
		Vector3 euler = transform.rotation.eulerAngles;
		
		if (curHatLeft && !prevHatLeft)
			euler.y -= RotationRatchet;
		
		prevHatLeft = curHatLeft;
		
		bool curHatRight = OVRGamepadController.GPC_GetButton(OVRGamepadController.Button.RightShoulder);
		
		if(curHatRight && !prevHatRight)
			euler.y += RotationRatchet;
		
		prevHatRight = curHatRight;
		
		//Use keys to ratchet rotation
		if (Input.GetKeyDown(KeyCode.Q))
			euler.y -= RotationRatchet;
		
		if (Input.GetKeyDown(KeyCode.E))
			euler.y += RotationRatchet;
		
		float rotateInfluence = SimulationRate * Time.deltaTime * RotationAmount * RotationScaleMultiplier;

		euler.y += Input.GetAxis("Mouse X") * rotateInfluence * 3.25f;
		
		moveInfluence = SimulationRate * Time.deltaTime * Acceleration * 0.1f * MoveScale * MoveScaleMultiplier;
		
		#if !UNITY_ANDROID // LeftTrigger not avail on Android game pad
		moveInfluence *= 1.0f + OVRGamepadController.GPC_GetAxis(OVRGamepadController.Axis.LeftTrigger);
		#endif
		
		float leftAxisX = OVRGamepadController.GPC_GetAxis(OVRGamepadController.Axis.LeftXAxis);
		float leftAxisY = OVRGamepadController.GPC_GetAxis(OVRGamepadController.Axis.LeftYAxis);
		
		if(leftAxisY > 0.0f)
			MoveThrottle += ort * (leftAxisY * moveInfluence * Vector3.forward);
		
		if(leftAxisY < 0.0f)
			MoveThrottle += ort * (Mathf.Abs(leftAxisY) * moveInfluence * BackAndSideDampen * Vector3.back);
		
		if(leftAxisX < 0.0f)
			MoveThrottle += ort * (Mathf.Abs(leftAxisX) * moveInfluence * BackAndSideDampen * Vector3.left);
		
		if(leftAxisX > 0.0f)
			MoveThrottle += ort * (leftAxisX * moveInfluence * BackAndSideDampen * Vector3.right);
		
		float rightAxisX = OVRGamepadController.GPC_GetAxis(OVRGamepadController.Axis.RightXAxis);
		
		euler.y += rightAxisX * rotateInfluence;
		
		transform.rotation = Quaternion.Euler(euler);
	}

//	public void Stop()
//	{
//		player.MovePosition(player.position + Vector3.zero);
//		MoveThrottle = Vector3.zero;
//	}

//	public void GetHaltUpdateMovement(ref bool haltUpdateMovement)
//	{
//		haltUpdateMovement = HaltUpdateMovement;
//	}
//
//	public void SetHaltUpdateMovement(bool haltUpdateMovement)
//	{
//		HaltUpdateMovement = haltUpdateMovement;
//	}

	public void ResetOrientation()
	{
		Vector3 euler = transform.rotation.eulerAngles;
		euler.y = YRotation;
		transform.rotation = Quaternion.Euler(euler);
	}
	
}

