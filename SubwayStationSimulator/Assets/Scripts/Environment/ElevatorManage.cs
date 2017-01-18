using UnityEngine;
using System.Collections;

public class ElevatorManage : MonoBehaviour {

	public bool isUp;
	public bool toUp;
	public bool toDown;
	public float Speed = 1f;
	public GameObject FrontDoor;
	public GameObject BackDoor;
	public float UpBound;
	public float DownBound;
	public AudioClip openDoorSound;

	private Animator FrontDoorAnim;
	private Animator BackDoorAnim;
	private AudioSource elevatorSounds;

	// Use this for initialization
	void Start () {
		elevatorSounds = this.GetComponent<AudioSource>();
		FrontDoorAnim = FrontDoor.GetComponent<Animator>();
		BackDoorAnim = BackDoor.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(FrontDoorAnim.GetBool("Open")){
			StartCoroutine("DelayCloseFrontDoor");
		}else{
			StopCoroutine("DelayCloseFrontDoor");
		}
		if(BackDoorAnim.GetBool("Open")){
			StartCoroutine("DelayCloseBackDoor");
		}else{
			StopCoroutine("DelayCloseBackDoor");
		}
	}

	void OnTriggerStay(Collider other){
		if(other.gameObject.tag == "Player"){
			if((isUp)&&(toDown)){
				ElevatorDownWithPlayer(other.gameObject);
			}
			if((!isUp)&&(toUp)){
				ElevatorUpWithPlayer(other.gameObject);
			}
		}
	}


	public void OpenFrontDoor(){
		FrontDoorAnim.SetBool("Open", true);
		AudioSource.PlayClipAtPoint(openDoorSound,FrontDoor.transform.position);
	}
	public void CloseFrontDoor(){
		FrontDoorAnim.SetBool("Open", false);
		AudioSource.PlayClipAtPoint(openDoorSound,FrontDoor.transform.position);
	}
	public void OpenBackDoor(){
		BackDoorAnim.SetBool("Open", true);
		AudioSource.PlayClipAtPoint(openDoorSound,BackDoor.transform.position);
	}
	public void CloseBackDoor(){
		BackDoorAnim.SetBool("Open", false);
		AudioSource.PlayClipAtPoint(openDoorSound,BackDoor.transform.position);
	}

	void ElevatorDownWithPlayer(GameObject lockedObject){
		if(this.transform.localPosition.y <= DownBound){
			elevatorSounds.Stop();
			lockedObject.GetComponent<PlayerController>().enabled = true;
			lockedObject.GetComponent<AudioSource>().enabled = true;
			StartCoroutine("DelayOpenBackDoor");
			this.transform.localPosition = new Vector3(0,DownBound,0);
			isUp = false; //set elevator at down place
			toDown = false;
		}else{
			CloseFrontDoor();
			lockedObject.GetComponent<PlayerController>().enabled = false;
			lockedObject.GetComponent<AudioSource>().enabled = false;
			this.transform.position += (-Vector3.up) * Time.deltaTime * Speed;
			if(!elevatorSounds.isPlaying){
				elevatorSounds.Play();
			}
		}
	}

	void ElevatorUpWithPlayer(GameObject lockedObject){
		if(this.transform.localPosition.y >= UpBound){
			elevatorSounds.Stop();
			lockedObject.GetComponent<PlayerController>().enabled = true;
			lockedObject.GetComponent<AudioSource>().enabled = true;
			StartCoroutine("DelayOpenFrontDoor");
			this.transform.localPosition = new Vector3(0,UpBound,0);
			isUp = true; //set elevator at up place
			toUp = false;
		}else{
			CloseBackDoor();
			lockedObject.GetComponent<PlayerController>().enabled = false;
			lockedObject.GetComponent<AudioSource>().enabled = false;
			this.transform.position += Vector3.up * Time.deltaTime * Speed;
			if(!elevatorSounds.isPlaying){
				elevatorSounds.Play();
			}
		}
	}

	public void ElevatorDown(){
		while(isUp){
			if(this.transform.localPosition.y <= DownBound){
				elevatorSounds.Stop();
				StartCoroutine("DelayOpenBackDoor");
				this.transform.localPosition = new Vector3(0,DownBound,0);
				isUp = false; //set elevator at down place
			}else{
				CloseFrontDoor();
				this.transform.position += (-Vector3.up) * Time.deltaTime * Speed;
				if(!elevatorSounds.isPlaying){
					elevatorSounds.Play();
				}
			}
		}
	}
	
	public void ElevatorUp(){
		while(!isUp){
			if(this.transform.localPosition.y >= UpBound){
				elevatorSounds.Stop();
				StartCoroutine("DelayOpenFrontDoor");
				this.transform.localPosition = new Vector3(0,UpBound,0);
				isUp = true; //set elevator at up place
			}else{
				CloseBackDoor();
				this.transform.position += Vector3.up * Time.deltaTime * Speed;
				if(!elevatorSounds.isPlaying){
					elevatorSounds.Play();
				}
			}
		}
	}

	IEnumerator DelayOpenFrontDoor(){
		yield return new WaitForSeconds(2);
		OpenFrontDoor();
	}
	IEnumerator DelayOpenBackDoor(){
		yield return new WaitForSeconds(2);
		OpenBackDoor();
	}

	IEnumerator DelayCloseFrontDoor(){
		yield return new WaitForSeconds(10);
		CloseFrontDoor();
	}
	IEnumerator DelayCloseBackDoor(){
		yield return new WaitForSeconds(10);
		CloseBackDoor();
	}

}
