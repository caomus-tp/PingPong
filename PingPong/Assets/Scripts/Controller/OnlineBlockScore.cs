using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OnlineBlockScore : NetworkBehaviour
{
    #region private
    [SerializeField]
    private string player = "P1";
    [SyncVar(hook = "OnHPChanged")]
    private int hp = 3;
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {            
            CmdChangeHP();
        }
    }


    [Command]
    private void CmdChangeHP()
    {
        hp--;
    }

    private void OnHPChanged(int newHP)
    {
        if (OnlineGameManager.Instance.isHost) { GameObject.Find("OnlineModeSceneManager").GetComponent<OnlineModeManager>().OnDestroyBall(); }        
        if (player.Equals("P1"))
        {
            UIInterface.Instance.UpdateHPP1(newHP);
            Debug.Log("Delete life player 1");
        }
        /// client
        if (player.Equals("P2"))
        {
            UIInterface.Instance.UpdateHPP2(newHP);
            Debug.Log("Delete life player 2");
        }
    }

    public override void OnStartClient()
    {
        UIInterface.Instance.UpdateScoreP1(0);
        UIInterface.Instance.UpdateScoreP2(0);
    }
}
