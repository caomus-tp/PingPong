using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemsList : MonoBehaviour 
{
	private static UIItemsList _instance;
	public static UIItemsList Instance 
	{
		get { return _instance; }
	}

	public Text txt_name;
	public Text txt_score;
	
	void Awake () {
		if (_instance != null) 
		{ return; }
		_instance = this;
	}

	public void SetLabel(string _name, int _score)
	{		
		txt_name.text = _name;
		txt_score.text = _score.ToString("0");
	}
}
