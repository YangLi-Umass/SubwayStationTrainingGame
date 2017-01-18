using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TaskEvent : MonoBehaviour {

	TaskLinkedList<ExplorationTask> taskLinkedList;
	TaskNode<ExplorationTask> currentTask;
	Graph mapGraph;
	List<Edge> testList;
	Vector3 PlayerStartPosition = Vector3.zero;

	void Awake(){
		taskLinkedList = new TaskLinkedList<ExplorationTask>();
		mapGraph = new Graph();
	}

	public void TaskInitial(Landmark[] taskLandmarks){
		testList = mapGraph.FindWay(taskLandmarks);
		foreach(Edge c in testList){
			taskLinkedList.Add(new ExplorationTask(c.Instruction, c.StartPoint));
			Debug.Log(c.StartPoint.name);
		}
		currentTask = taskLinkedList.FindNode(1);
		currentTask.Data.TriggerWaiting = true;
	}
	
	// Update is called once per frame
	void Update () {
//		if(Input.GetKeyDown(KeyCode.Space)){
//			TriggerCurTask();
//		}
		if(Input.GetKeyDown(KeyCode.Escape)){
			BackToPrevTask();
		}
	}

	void TriggerCurTask(){
		if(currentTask.Data.TriggerWaiting){
			currentTask.Data.TriggerTask();

			currentTask.Data.ReadInstruction();
			if(currentTask.Next != null){
				currentTask = currentTask.Next;
				currentTask.Data.TriggerWaiting = true;
			}
		}
	}

	void BackToPrevTask(){
 		currentTask = currentTask.Prev;
		this.transform.position = currentTask.Data.BeginPosition;// sent the play to prev task position
		if(currentTask != null){
			currentTask.Data.TriggerWaiting = true;
		}
	}

	void OnTriggerEnter(Collider other){
//		Debug.Log(other.gameObject.name+" "+currentTask.Data.Id);
		if(currentTask != null){
			if(other.gameObject.name == currentTask.Data.Id.name){
				TriggerCurTask();
			}
		}
	
	}
}
