using UnityEngine;
using System.Collections;

public class EscalatorManage : MonoBehaviour {

	public bool isUp;

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player"){
			other.gameObject.GetComponent<AudioSource>().enabled = false;
			other.gameObject.GetComponent<PlayerController>().enabled = false;
		}
	}

	void OnTriggerStay(Collider other){
		if(other.gameObject.tag == "Player"){
			if(isUp)
			{
				other.transform.position += Vector3.Cross(transform.up, transform.forward).normalized * Time.deltaTime * 2f;
			}else{
				other.transform.position += (-Vector3.Cross(transform.up, transform.forward).normalized) * Time.deltaTime * 2f;
			}
		}
	}

	void OnTriggerExit(Collider other){
		if(other.gameObject.tag == "Player"){
			other.gameObject.GetComponent<AudioSource>().enabled = true;
			other.gameObject.GetComponent<PlayerController>().enabled = true;
		}
	}
}
