using UnityEngine;
using System.Collections;

public class TaskNode<T>{

	private T data;
	private TaskNode<T> next;
	private TaskNode<T> prev;

	public T Data{
		get{return data;}
		set{data = value;}
	}

	public TaskNode<T> Next{
		get{return next;}
		set{next = value;}
	}
	public TaskNode<T> Prev{
		get{return prev;}
		set{prev = value;}
	}
	public TaskNode(){
		data = default(T);
		prev = null;
		next = null;
	}
	public TaskNode(T val){
		data = val;
		next = null;
		prev = null;
	}
}
