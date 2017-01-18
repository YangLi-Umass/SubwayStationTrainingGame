using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using uSignalR.Hubs;
using JetBrains.Annotations;
using uTasks;
using UnityEngine.UI;

public class Message{
	public string username {get;set;}
	public string usermessage {get;set;}
}

public class SignalRUnityController : MonoBehaviour {
//	[SerializeField] private Text _resultText;

	private CancellationTokenSource _tokenSource;
	
	bool useSignalR = true;
	//	string signalRUrl = "http://percept.ecs.umass.edu/perceptsiri";
	string signalRUrl = "http://sis2.ecs.umass.edu/SignalRChat/";
	
	string result = null ;
	
	private HubConnection _hubConnection = null;
	private IHubProxy _hubProxy;
	
	public Subscription _subscription;

	void Awake(){
		MainThread.Current = new UnityMainThread();
	}
	
	void Start()
	{
		_tokenSource = new CancellationTokenSource();
		TaskFactory.StartNew(() => StartSignalR(_tokenSource.Token))
			.CompleteWithAction(task =>
			                    {
				if (task.IsCanceled)
				{
					return;
				}
				Debug.Log(task.ToString());

			});
	}

	void Update(){
		if(result != null){
			//			_resultText.text = result;
			result = null;
		}
	}

	void StartSignalR(CancellationToken token)
	{
		if (_hubConnection == null)
		{
			_hubConnection = new HubConnection(signalRUrl);
			
			//			_hubProxy = _hubConnection.CreateProxy("SmartphoneHub");
			//			_subscription = _hubProxy.Subscribe("test");
			_hubProxy = _hubConnection.CreateProxy("ChatHub");
			_subscription = _hubProxy.Subscribe("broadcastMessage");
			_subscription.Data += data =>
			{
				Debug.Log("signalR called us back");
			};
			
			_subscription.Data += OnData;
			_hubConnection.Start();
			
			//			_hubProxy.Invoke("PairDevices", groupName);
			//			Tag tag = new Tag() {
			//				Id = "testing123",
			//				DeviceId = groupName
			//			};
			//			_hubProxy.Invoke("ScanTagVirtual",tag);
			//			Message msg = new Message(){
			//				username = "Unity",
			//				usermessage = "hello"
			//			};
			//			_hubProxy.Invoke("send", msg);
			
		}
		else
			Debug.Log("Signalr already connected...");
		
	}

	void OnData(object[] data)
	{
		result = data[0]+":"+data[1];
		Debug.Log(data[0]+":"+data[1]);
	} 
	
	public void Send(string message)
	{
		if (!useSignalR)
			return;
		_hubProxy.Invoke("send", "Unity", 
		                 message);
	}
	
}

