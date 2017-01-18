using UnityEngine;
using System.Collections;

public class Landmark{

	public string name {set;get;}
	public Vector3 position {set; get;}
	public Landmark(string _name, Vector3 _position){
		this.name = _name;
		this.position = _position;
	}
}
