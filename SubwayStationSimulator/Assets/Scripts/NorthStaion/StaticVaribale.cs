using UnityEngine;
using System.Collections;

namespace StaticVariable{
	
	public class UnityTag{
		public static readonly string PLAYER = "Player";
		public static readonly string MAIN_CAMERA = "MainCamera";
		public static readonly string VIDEO_DISPLAY_TEXTURE = "VideoDisplayTexture";
		public static readonly string GAME_CONTROLLER = "GameController";
	}

	public class BuildingName{
		public static readonly string NORTH_STATION = "NorthStation";
	}

	public class RealEnvironmentVideo{
		public const string True = "True";
		public const string False = "False"; 
	}

	public class DispalyType{
		public const string Oculus = "Oculus Mode";
		public const string Scene = "Scene Mode";
	}

	public class TransmitType{
		public const string Stair = "Stair";
		public const string Elevator = "Elevator";
		public const string Escalator = "Escalator";
	}

	public class TrainingMode{
		public const string SelfExploration = "Self Exploration";
		public const string PerceptApp = "Application UI";
	}

	public class NorthStationStartingPoint{
		public const string StartPoint1 = "StartPoint1";
	}

	public class NorthStationDestination{
		public const string InBoundOrangeLine = "InBoundOrangeLine";
	}
}
