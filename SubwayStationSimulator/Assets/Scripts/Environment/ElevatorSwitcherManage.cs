using UnityEngine;
using System.Collections;

public class ElevatorSwitcherManage : MonoBehaviour {
	
	public GameObject elevator;
	public bool isupBtn;
	public AudioClip Deing;

	private ElevatorManage elevatorManage;
	
	// Use this for initialization
	void Start () {
		elevatorManage = elevator.GetComponent<ElevatorManage>();
	}

	void OnTriggerEnter(Collider other){
		this.GetComponent<AudioSource>().PlayOneShot(Deing);
		if(other.gameObject.tag == "Player"){
			Debug.Log ("Player collide with button.");
			if(isupBtn){
				elevatorManage.toUp = false;
				if(elevatorManage.isUp){
					elevatorManage.OpenFrontDoor();
					elevatorManage.toDown = true;
				}else{
					//elevatorManage.ElevatorUp();
					elevatorManage.OpenFrontDoor();
					elevatorManage.toDown = true;
				}
			}else{
				elevatorManage.toDown = false;
				if(!elevatorManage.isUp){
					elevatorManage.OpenBackDoor();
					elevatorManage.toUp = true;
				}else{
					elevatorManage.ElevatorDown();
					elevatorManage.OpenBackDoor();
					elevatorManage.toUp = true;
				}
			}

		}
	}
}
