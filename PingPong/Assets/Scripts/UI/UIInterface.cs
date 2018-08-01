using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInterface : MonoBehaviour
{

    private static UIInterface _instance;
    public static UIInterface Instance
    {
        get { return _instance; }
    }

    #region public
    public Text txtScoreRight;
    public Text txtScoreLeft;
    public Text txtMsg;
    public Text txtHP_P1;
    public Text txtHP_P2;
    public Button btnPause;
    public GameObject panelLeaderboard;
    #endregion    

    private void Awake()
    {
        if (_instance != null)
        { return; }
        _instance = this;

        btnPause.onClick.AddListener(() =>
        {
            OnlineGameManager.Instance.OnPauseGame();
        });
    }

    private void Update()
    {
        if (OnlineGameManager.Instance.isReady && OnlineGameManager.Instance.isStart)
        {
            
            if (OnlineGameManager.Instance.timeStart > 0)
            {
                UpdateTime(OnlineGameManager.Instance.timeStart);
                OnlineGameManager.Instance.timeStart -= Time.deltaTime;                
            }
        }
    }

    public void UpdateScoreP1(int _score)
    {
        OnlineGameManager.Instance.score_p1 = _score;
        if (txtScoreLeft != null) txtScoreLeft.text = OnlineGameManager.Instance.score_p1.ToString("0");
    }

    public void UpdateScoreP2(int _score)
    {
        OnlineGameManager.Instance.score_p2 = _score;
        if (txtScoreRight != null) txtScoreRight.text = OnlineGameManager.Instance.score_p2.ToString("0");
    }

    public void UpdateHPP1(int _life)
    {        
        OnlineGameManager.Instance.hp_p1 = _life;
        if (txtHP_P1 != null) txtHP_P1.text = "HP "+_life.ToString("0");
    }

    public void UpdateHPP2(int _life)
    {        
        OnlineGameManager.Instance.hp_p2 = _life;
        if (txtHP_P2 != null) txtHP_P2.text = "HP "+_life.ToString("0");
    }

    public void UpdateTime(float _timer)
    {
        txtMsg.enabled = true;
        if (txtMsg != null) txtMsg.text = _timer.ToString("0");
        if (_timer <= 0) txtMsg.enabled = false;
    }

    public void SetTextDisplay(string _msg, bool _show)
    {
        txtMsg.enabled = _show;
        if (txtMsg != null) txtMsg.text = _msg;
    }

    public void ShowLeaderBoard()
    {
        panelLeaderboard.SetActive(true);
    }

    public void CloseLeaderBoard()
    {
        panelLeaderboard.SetActive(false);
        GameObject.Find("LobbyManager").GetComponent<LobbyManager>().OnDisconnect();
        // UIEventLoader.Instance.OnLoadScene("Lobby");
    }
}
