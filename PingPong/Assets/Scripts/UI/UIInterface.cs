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
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateScoreP1() {
		score_p1++;
		if (txtScoreRight!=null) txtScoreRight.text = score_p1.ToString("0");
	}

	public void UpdateScoreP2() {
		score_p2++;
		if (txtScoreLeft!=null) txtScoreLeft.text = score_p2.ToString("0");
	}
}
