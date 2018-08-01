using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITitleScreen : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        OnLoadLeaderboard();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnLoadLeaderboard()
    {
        LeaderBoardData boardData = SaveLoadJSON.Instance.LoadLeaderboard();
        if(boardData != null)
        {              
            UILeaderboard.Instance.SetData(boardData);   
        }    
    }
}
