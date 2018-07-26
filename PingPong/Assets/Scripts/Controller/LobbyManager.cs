using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MsgClientType {
	public static short Msg = MsgType.Highest + 1;
};

public class SendMessage : MessageBase
{
	public string msg;
}

public class LobbyManager : NetworkManager 
{
	public override void OnServerConnect(NetworkConnection conn)
    {        
		if (conn.hostId != -1)
		{
			if (OnlineModeManager.Instance != null) OnlineModeManager.Instance.isReady = true;
			conn.RegisterHandler(MsgClientType.Msg, OnRecivedMSG);			
		}
		Debug.Log("OnServerConnect "+ conn.connectionId+" : hostId "+conn.hostId );        
    }
	    
    public override void OnServerDisconnect(NetworkConnection conn)
    {        
       Debug.Log("OnServerDisconnect");
	   if (OnlineModeManager.Instance != null) OnlineModeManager.Instance.isReady = false;
	   conn.UnregisterHandler(MsgClientType.Msg);
    }

	public override void OnClientConnect(NetworkConnection conn)
    {    		
		if (conn.hostId != -1 )
		{
			if (OnlineModeManager.Instance != null) OnlineModeManager.Instance.isReady = true;
			conn.RegisterHandler(MsgClientType.Msg, OnRecivedMSG);
		}
		Debug.Log("OnClientConnect "+ conn.connectionId+" : hostId "+conn.hostId );  
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {        
		Debug.Log("OnClientDisconnect");
		if (OnlineModeManager.Instance != null) OnlineModeManager.Instance.isReady = false;
		conn.UnregisterHandler(MsgClientType.Msg);
    }

	public void OnRecivedMSG(NetworkMessage netMsg) 
	{
		SendMessage _msg = netMsg.ReadMessage<SendMessage>();
        Debug.Log("OnRecivedMSG " + _msg.msg);
		if (_msg.msg.Equals("pause")) {			
			UIInterface.Instance.SetTextDisplay("Pause", true);
		}
		else {			
			UIInterface.Instance.SetTextDisplay("Pause", false);
		}
	}
}
