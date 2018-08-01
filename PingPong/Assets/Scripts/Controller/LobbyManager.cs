using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SendMsgType
{	    
    public static short GameState = MsgType.Highest + 1;
};

public class GameStateMessage : MessageBase
{
	public string gamestate;    
}

public class LobbyManager : NetworkManager 
{
    private NetworkClient m_client;
    private NetworkClient m_host;

    public override void OnServerConnect(NetworkConnection conn)
    {        
		if (conn.hostId != -1)
		{
            m_host = client;
            conn.RegisterHandler(SendMsgType.GameState, OnGameStateChange);            
            OnlineGameManager.Instance.isReady = true;
            Debug.Log("OnServerConnect " + conn.connectionId + " : hostId " + conn.hostId);
            Debug.Log("Host name " + System.Environment.MachineName);
        }		    
    }
	    
    public override void OnServerDisconnect(NetworkConnection conn)
    {        
        Debug.Log("OnServerDisconnect");	   
	    conn.UnregisterHandler(SendMsgType.GameState);        
        OnlineGameManager.Instance.isStart  = false;
        OnlineGameManager.Instance.isReady  = false;
        // OnlineGameManager.Instance.isHost   = false;
        if (OnlineGameManager.Instance.m_state != OnlineGameManager.STATE.EndGame)
        {
            UIInterface.Instance.ShowEndGame();
        }        
    }

	public override void OnClientConnect(NetworkConnection conn)
    {    		
		if (conn.hostId != -1 )
		{            
            m_client = client;
            conn.RegisterHandler(SendMsgType.GameState, OnGameStateChange);            
            OnlineGameManager.Instance.isReady = true;            
            Debug.Log("OnClientConnect " + conn.connectionId + " : hostId " + conn.hostId);
            Debug.Log("Client name " + System.Environment.MachineName);
        }		
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {        
		Debug.Log("OnClientDisconnect");
        conn.UnregisterHandler(SendMsgType.GameState);        
        OnlineGameManager.Instance.isStart = false;
        OnlineGameManager.Instance.isReady = false;

        OnDisconnect();
    }

    public override void OnServerError(NetworkConnection conn, int errorCode) 
    {
        Debug.Log("OnServerError "+ conn.connectionId +":"+conn.address);
    }

    public override void OnStopHost()
    {
        Debug.Log("OnStopHost");
    }

    public override void OnStopServer()
    {
        Debug.Log("OnStopServer");
    }
    
    public override void OnStopClient()
    {
        Debug.Log("OnStopClient");
    }

    public void ServerSendGameState(GameStateMessage _msg)
    {
        Debug.Log("ServerSendGameState");
        NetworkServer.SendToAll(SendMsgType.GameState, _msg);        
    }

    public void ClientSendGameState(GameStateMessage _msg)
    {
        Debug.Log("ClientSendGameState");
        m_client.Send(SendMsgType.GameState, _msg);
    }

    public void OnGameStateChange(NetworkMessage netMsg)
    {
        GameStateMessage _msg = netMsg.ReadMessage<GameStateMessage>();
        Debug.Log("OnGameStateChange ");
        if (_msg.gamestate.Equals("pause"))
        {
            OnlineGameManager.Instance.m_state = OnlineGameManager.STATE.Pause;
            UIInterface.Instance.SetTextDisplay("Pause", true);
        }
        else
        {
            OnlineGameManager.Instance.m_state = OnlineGameManager.STATE.Playing;
            UIInterface.Instance.SetTextDisplay("Pause", false);
        }
    }

    public void OnDisconnect() 
    {        
        if (OnlineGameManager.Instance.isHost)
        {
            OnDisconnectHost();
        }        
        else
        {
            Debug.Log("call StopClient");
            StopClient();            
            StopAllCoroutines();
        }
    }

    public void OnDisconnectHost()
    {
        Debug.Log("call StopServer");
        NetworkManager manager = GetComponent<NetworkManager>();
        manager.StopHost();
    }
}
