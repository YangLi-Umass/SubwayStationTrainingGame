using UnityEngine;
using System.Collections;
using Tags;

public class MiniMapCamera : MonoBehaviour {

	public float moveSpeed = 4f;

	private Transform target;

	public Vector3 Offset = new Vector3(-8.9f, 44.5f, -1.4f);
	
	void Start(){
		GameController._instance.OnInitial += this.OnInitial;
	}

	void OnInitial(){
		target = GameObject.FindGameObjectWithTag(UnityTag.Player).transform;
	}

	// Update is called once per frame
	void LateUpdate () {
		Vector3 targetPos = target.position + Offset;
		this.transform.position = Vector3.Lerp(this.transform.position, targetPos, moveSpeed * Time.deltaTime);
	}
	
}
