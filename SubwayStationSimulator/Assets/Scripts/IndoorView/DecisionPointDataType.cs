using UnityEngine;
using System.Collections;

public class DecisionPointDataType : MonoBehaviour {

	private int exitOrientation;
	private bool enable;
	private string videoName;
	private string describe;

	public DecisionPointDataType (string _videoName, int _desiredOrientation, string _describe) {
		this.exitOrientation = _desiredOrientation;
		this.enable = true;
		this.videoName = _videoName;
		this.describe = _describe;
	}

	public DecisionPointDataType (int _desiredOrientation) {
		this.exitOrientation = _desiredOrientation;
		this.enable = false;
	}

	public string getVideoName () {
		return this.videoName;
	}

	public int getExitOrientation () {
		return this.exitOrientation;
	}

	public string getDescribe () {
		return this.describe;
	}

	public bool getEnable () {
		return this.enable;
	}
}
