using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleSceneManager : MonoBehaviour
{

    public Button btnOnlinePlay;
    public Button btnLeaderboard;

    // Use this for initialization
    void Start()
    {
        btnOnlinePlay.onClick.AddListener(() =>
        {
            OnLoadOnlinePlayMode();
        });
        btnLeaderboard.onClick.AddListener(() =>
        {
            OnLoadLeaderboard();
        });
    }    

    private void OnLoadOnlinePlayMode()
    {
        UIEventLoader.Instance.OnLoadScene("Lobby");
    }

    private void OnLoadLeaderboard()
    {

    }
}
