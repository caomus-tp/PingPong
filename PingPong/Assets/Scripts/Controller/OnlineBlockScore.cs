using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OnlineBlockScore : NetworkBehaviour {

	#region private
    [SerializeField]
    private string player = "P1";

    [SyncVar(hook = "OnScoreChanged")]
    private int score = 0;
    #endregion
	
	private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            CmdAddScore();
        }
    }

    [Command]
    private void CmdAddScore()
    {
        score++;
    }

    private void OnScoreChanged(int newScore)
    {
         OnlineModeManager.Instance.OnDestroyBall();
         if (player.Equals("P1")) {
             UIInterface.Instance.UpdateScoreP1(newScore);
         }
         else if(player.Equals("P2")) {
             UIInterface.Instance.UpdateScoreP2(newScore);
         }
    }

    public override void OnStartClient()
    {                
        UIInterface.Instance.UpdateScoreP1(0);
        UIInterface.Instance.UpdateScoreP2(0);        
    }
}
