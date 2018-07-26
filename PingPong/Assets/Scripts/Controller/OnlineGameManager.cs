using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OnlineGameManager : MonoBehaviour 
{
	public enum STATE {
		None = 0,
		Playing,
		Pause,
		EndGame
	}

	public STATE m_state = STATE.None;
	NetworkClient myClient;
	private bool isPause = false;

	private static OnlineGameManager _instance;
	public static OnlineGameManager Instance 
	{
		get { return _instance; }
	}

	// public class MsgClientType {
    //     public static short Msg = MsgType.Highest + 1;
    // };

	// public class SendMessage : MessageBase
    // {
    //     public string msg;
    // }

	// Use this for initialization
	private void Awake () {
		if ( _instance != null)
		{
			return;
		}
		_instance = this;
	}

	private void Start() 
	{
		SetupClient();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPauseGame()
	{
		isPause = !isPause;
		SendMessage msg = new SendMessage();		
        msg.msg = (isPause) ? "pause" : "unpause";

        NetworkServer.SendToAll(MsgClientType.Msg, msg);
	}

	// Create a client and connect to the server port
    public void SetupClient()
    {
        // myClient = new NetworkClient();
        // myClient.RegisterHandler(MsgType.Connect, OnConnected);  
		// myClient.RegisterHandler(MsgClientType.Msg, OnRecivedMSG);  
        // myClient.Connect("localhost", 7777);
    }	
}
