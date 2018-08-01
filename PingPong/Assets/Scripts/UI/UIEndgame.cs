using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEndgame : MonoBehaviour {

	private static UIEndgame _instance;
	public static UIEndgame Instance
	{
		get { return _instance; }
	}

	public Button m_close;
	// Use this for initialization
	void Awake () {
		if (_instance != null) 
		{ return; }
		_instance = this;

		if (m_close != null) m_close.onClick.AddListener(()=>{UIInterface.Instance.CloseEndGame();});
	}
}
