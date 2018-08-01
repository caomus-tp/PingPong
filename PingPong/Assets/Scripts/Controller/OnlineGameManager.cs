using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OnlineGameManager : MonoBehaviour
{
    public enum STATE
    {
        None = 0,
        Playing,
        Pause,
        EndGame
    }

    public STATE m_state = STATE.None;
    public bool isReady;
    public bool isStart = true;
    public bool isHost = false;
    public float timeStart = 3.0f;
    private bool isPause = false;
    private string m_MachineName;

    public int hp_p1 = 3;
    public int hp_p2 = 3;
    public int score_p1 = 0;
    public int score_p2 = 0;


    private static OnlineGameManager _instance;
    public static OnlineGameManager Instance
    {
        get { return _instance; }
    }

    // Use this for initialization
    private void Awake()
    {
        if (_instance != null)
        {
            return;
        }
        _instance = this;
    }

    private void Start()
    {
        m_state = STATE.None;
        m_MachineName = System.Environment.MachineName + "|" + NetworkManager.singleton.networkAddress;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_state == STATE.None)
        {
            if (isReady && isStart)
            {
                if (timeStart <= 0)
                {
                    timeStart = 3.0f;
                    m_state = STATE.Playing;
                    isReady = false;
                    UIInterface.Instance.UpdateTime(0);
                }
            }
        }
        if (m_state == STATE.Playing)
        {
            if (hp_p1 <= 0 || hp_p2 <= 0)
            {
                m_state = STATE.EndGame;
                OnResult();
            }
        }
    }

    // server call 
    public void OnPauseGame()
    {        
        isPause = !isPause;
        GameStateMessage msg = new GameStateMessage();
        msg.gamestate = (isPause) ? "pause" : "unpause";
        m_state = (isPause) ? STATE.Pause : STATE.Playing;
        UIInterface.Instance.SetTextDisplay("Pause", isPause);

        if (isHost)
        {
            GameObject.Find("LobbyManager").GetComponent<LobbyManager>().ServerSendGameState(msg);
        }
        else
        {
            GameObject.Find("LobbyManager").GetComponent<LobbyManager>().ClientSendGameState(msg);
        }            
    }   

    public void OnResult()
    {
        UIInterface.Instance.ShowLeaderBoard();
        LeaderBoardData boardData = SaveLoadJSON.Instance.LoadLeaderboard();
        if(boardData == null)
        {      
            boardData = new LeaderBoardData()
            {
                dataList = new List<PlayerData>()
                {
                    new PlayerData() { id=0, name = m_MachineName, score =  (score_p1 > score_p2) ? score_p1 : score_p2}
                }
            };            
            SaveLoadJSON.Instance.SaveLeaderboard(boardData);
        }
        else
        {
            boardData.dataList.Sort((s1, s2) => s1.score.CompareTo(s2.score));            
            for (int i = 0; i < boardData.dataList.Count; i++)
            {
                if(boardData.dataList[i].name.Equals(m_MachineName))
                {
                    // is player 1 win!
                    if (score_p1 > score_p2)
                    {
                        if (boardData.dataList[i].score < score_p1)
                        {
                            boardData.dataList[i].score = score_p1;
                        }
                    }
                    else
                    {
                        if (boardData.dataList[i].score < score_p2)
                        {
                            boardData.dataList[i].score = score_p2;
                        }
                    }
                }
                else
                {
                    boardData.dataList.Add(new PlayerData() { id = 0, name = m_MachineName, score = (score_p1 > score_p2) ? score_p1 : score_p2 });
                    boardData.dataList.Sort((s1, s2) => s1.score.CompareTo(s2.score));                   
                }
            }
        } 

        UILeaderboard.Instance.SetData(boardData);         
    }
}
