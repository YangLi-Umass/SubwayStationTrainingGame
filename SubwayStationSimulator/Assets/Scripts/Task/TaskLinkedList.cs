using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TaskLinkedList<T> : LinkedList<T> {
	private TaskNode<T> header;
	public TaskNode<T> Header{
		get{
			return header;
		}
		set{
			header = value;
		}
	}
	public TaskLinkedList(){
		header = new TaskNode<T>();
		header.Next = null;
		header.Prev = null;
	}

	public int Length(){
		TaskNode<T> t = header;
		int len = 0;
		while(t != null){
			++len;
			t = t.Next;
		}
		return len;
	}

	public bool isEmpty(){
		if(header == null){
			return true;
		}else{
			return false;
		}
	}

	public void Add(T taskItem){
		TaskNode<T> newTask = new TaskNode<T>(taskItem);
		TaskNode<T> p = header;
		if(header == null){
			header = newTask;
			return;
		}
		while(p.Next != null){
			p = p.Next;
		}
		p.Next = newTask;
		newTask.Prev = p;
	}

	public TaskNode<T> FindNode(int index){
		if(isEmpty()){
			Debug.LogError("TaskList is Empty");
			return null;
		}
		if(index < 1){
			Debug.LogError("Index is error");
			return null;
		}
		TaskNode<T> cur = new TaskNode<T>();
		cur = header;
		int j = 0;
		while (cur.Next != null && j < index){
			++j;
			cur = cur.Next;
		}
		return cur;
	}

}
