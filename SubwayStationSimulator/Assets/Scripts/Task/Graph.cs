using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Edge{
	public string Instruction{set; get;}
	public Landmark StartPoint{set; get;}
	public Edge(Landmark _sp,string _ins){
		this.StartPoint = _sp;
		this.Instruction = _ins;
	}
}

public class Graph{

	Dictionary<Landmark, int> VertexList = new Dictionary<Landmark, int>();
	Dictionary<string, Edge> EdgeList = new Dictionary<string, Edge>();
	
	public Graph(){
		VertexList.Add(FixedLandmark.Entrance544,1);
		VertexList.Add(FixedLandmark.Hallway,2);
		VertexList.Add(FixedLandmark.RightMetalGate,3);
		VertexList.Add(FixedLandmark.RightFareMachine,4);
		VertexList.Add(FixedLandmark.EndFareGate,5);
		VertexList.Add(FixedLandmark.EnterOutBound,6);
		VertexList.Add(FixedLandmark.RightWaitingLine,7);
		VertexList.Add(FixedLandmark.RightElevator,8);
		VertexList.Add(FixedLandmark.Entrance562,9);
		VertexList.Add(FixedLandmark.EnterInBound,10);
		VertexList.Add(FixedLandmark.LeftWaitingLine,11);
		VertexList.Add(FixedLandmark.LeftElevator,12);
		VertexList.Add(FixedLandmark.Entrance575_1,13);
		VertexList.Add(FixedLandmark.Entrance575_2,14);
		VertexList.Add(FixedLandmark.LeftMetalGate,15);
		VertexList.Add(FixedLandmark.LeftFareMachine,16);
		VertexList.Add(FixedLandmark.Entrance582_1,17);
		VertexList.Add(FixedLandmark.Entrance582_2,18);
		VertexList.Add(FixedLandmark.Entrance582_Lobby,19);

		EdgeList.Add("1-2", new Edge(FixedLandmark.Entrance544, 
		                             "Your current station is Arlington, you at the entrance, the first task is to find an intersecting hallway, go straight and turn left, tracking the wall on your right side, by end the wall turn right, you will arrive the intersceting hallway."));
		EdgeList.Add("2-3", new Edge(FixedLandmark.Hallway, 
		                             "you are arrived the hallway, This is a long hallway that will lead into Main Lobby. You will pass by Glass Window. go straight and following the wall on your right side, until you reach the Metal Gate."));
		EdgeList.Add("3-4", new Edge(FixedLandmark.RightMetalGate, 
		                             "you are arrived the metal gate, trail the metal gate, you will enter the lobby, the next task is to find the Charlie Card ticket machines."));
		EdgeList.Add("4-5", new Edge(FixedLandmark.RightFareMachine, 
		                             "there are fare machines, you can buy a ticket now. then Turn right and keeping the Charlie Card ticket machines on your right side. Continue until you reach the Fare Gate. You may hear the beeping, opening and closing of the Fare Gate. Go through the Fare Gate"));
		EdgeList.Add("5-6", new Edge(FixedLandmark.EndFareGate, 
		                             "find the stairs on your right side, Go down two flights of stairs to the platform below. At the bottom of the stairs, you will reach an opening."));
		EdgeList.Add("6-7", new Edge(FixedLandmark.EnterOutBound, 
		                             "trail the wall on your right side, You will pass by trash cans and a bench. The floor slopes down. Walk past the opening."));
		EdgeList.Add("7-7", new Edge(FixedLandmark.RightWaitingLine, 
		                             "ok, nice work. you can waiting the train here"));
		EdgeList.Add("5-8", new Edge(FixedLandmark.EndFareGate, 
		                             "Go straight until you reach the wall, turn right and go straight, you will find the elevator on your left side."));
		EdgeList.Add("8-6", new Edge(FixedLandmark.RightElevator, 
		                             "Face to the elevator and Touch the switcher button on your right side, enter the elevator you will reach the outbound"));
		EdgeList.Add("9-3", new Edge(FixedLandmark.Entrance562, 
		                             "Your current station is Arlington, go down two flights of stairs and trail the Wall on your right side until you reach an intersecting hallway. Cross the hallway until you reach the Metal Gate."));
		EdgeList.Add("5-10", new Edge(FixedLandmark.EndFareGate, 
		                             "find the stairs on your left side, Go down two flights of stairs to the platform below. At the bottom of the stairs, you will reach an opening."));
		EdgeList.Add("11-11", new Edge(FixedLandmark.LeftWaitingLine, 
		                             "ok, nice work. you can waiting the train here"));
		EdgeList.Add("5-12", new Edge(FixedLandmark.EndFareGate, 
		                             "Go straight until you reach the wall, turn left and go straight until you reach a opening, cross the opening and turn left, trail the wall, then you will find the elevator on your right side."));
		EdgeList.Add("12-10", new Edge(FixedLandmark.LeftElevator, 
		                             "Face to the elevator and Touch the switcher button on your right side, enter the elevator you will reach the outbound"));
		EdgeList.Add("10-11", new Edge(FixedLandmark.EnterInBound, 
		                             "trail the wall on your left side, You will pass by trash cans and a bench. The floor slopes down. Walk past the opening."));
		EdgeList.Add("13-15", new Edge(FixedLandmark.Entrance575_1, 
		                               "Your current station is Arlington, Go down one flight of stairs and trail the Wall on your right side. go down the stair until you reach the Metal Gate."));
		EdgeList.Add("14-15", new Edge(FixedLandmark.Entrance575_2, 
		                               "Your current station is Arlington, Go down one flight of stairs and turn left until you reach the wall, then turn left and go down the stair until you reach the Metal Gate."));
		EdgeList.Add("15-16", new Edge(FixedLandmark.LeftMetalGate, 
		                             "you are arrived the metal gate, trail the metal gate, you will enter the lobby, the next task is to find the Charlie Card ticket machines."));
		EdgeList.Add("16-5", new Edge(FixedLandmark.LeftFareMachine, 
		                             "there are fare machines, you can buy a ticket now. then Turn left and keeping the Charlie Card ticket machines on your left side. Continue until you reach the Fare Gate. You may hear the beeping, opening and closing of the Fare Gate. Go through the Fare Gate"));
		EdgeList.Add("17-19", new Edge(FixedLandmark.Entrance582_1, 
		                               "Your current station is Arlington, Go down one flight of stairs until you reach an intersecting hallway. Turn left, Go down one flight of stairs and trail the Wall on your left side until the wall angles out twice. You will enter the Lobby."));
		EdgeList.Add("18-19", new Edge(FixedLandmark.Entrance582_2, 
		                               "Your current station is Arlington, Go down one flight of stairs until you reach an intersecting hallway. Turn right, Go down one flight of stairs and trail the Wall on your right side until the wall angles out twice. You will enter the Lobby."));
		EdgeList.Add("19-16", new Edge(FixedLandmark.Entrance582_Lobby, 
		                               "There is Fare Machine to your ten oclock. Walk across to the Fare Machine."));
	}

	public List<Edge> FindWay(Landmark[] landmarks){
		List<int> _wayNum = new List<int>();
		foreach (Landmark l in landmarks){
			int n = 0;
			VertexList.TryGetValue(l, out n);
			_wayNum.Add(n);
		}
		int _start = 0; int _end = 0;
		List<Edge> way = new List<Edge>();
		for(int i = 0; i < _wayNum.Count-1; i++){
			_start = _wayNum[i];
			_end = _wayNum[i+1];
			string pair = _start + "-" + _end;
			Edge edge;
			EdgeList.TryGetValue(pair, out edge);
			way.Add(edge);
			if(i == (_wayNum.Count-2)){
				pair = _end + "-" + _end;
				EdgeList.TryGetValue(pair, out edge);
				way.Add(edge);
			}
		}

//		foreach (Edge e in way){
//			Debug.Log(e.StartPoint.name);
//		}
		return way;
	}
}
