using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInterface : MonoBehaviour {

	private static UIInterface _instance;
	public static UIInterface Instance 
	{
		get { return _instance; }
	}
	
	#region public
	public Text txtScoreRight;
	public Text txtScoreLeft;
	public Text txtMsg;
	public Button btnPause;
	#endregion

	#region private
    private int score_p1 = 0;
	private int score_p2 = 0;
    #endregion

	private void Awake() 
	{
		if (_instance != null) 
		{ return; }	
		_instance = this;

		btnPause.onClick.AddListener(()=>{
			OnlineGameManager.Instance.OnPauseGame();
		});
	}

	private void Update()
    {
        if (OnlineModeManager.Instance.isReady && OnlineModeManager.Instance.isStart) {
            if (OnlineModeManager.Instance.timeStart > 0) {
                OnlineModeManager.Instance.timeStart -= Time.deltaTime;
            }
            UpdateTime(OnlineModeManager.Instance.timeStart);            
        }    
    }

	public void UpdateScoreP1(int _score) 
	{
		score_p1 = _score;
		if (txtScoreRight!=null) txtScoreRight.text = score_p1.ToString("0");
	}

	public void UpdateScoreP2(int _score) 
	{
		score_p2 = _score;
		if (txtScoreLeft!=null) txtScoreLeft.text = score_p2.ToString("0");
	}

	public void UpdateTime(float _timer) 
	{		
		txtMsg.enabled = true;
		if (txtMsg!=null) txtMsg.text = _timer.ToString("0");
		if (_timer <= 0) txtMsg.enabled = false;
	}

	public void SetTextDisplay(string _msg, bool _show) 
	{
		txtMsg.enabled = _show;
		if (txtMsg!=null) txtMsg.text = _msg;
	}
}
