using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEventLoader : MonoBehaviour {

	private static UIEventLoader _instance;
	public static UIEventLoader Instance 
	{
		get { return _instance; }
	}
	
	private void Awake() 
	{
		if (_instance != null) 
		{ return; }	
		_instance = this;
	}

	public void OnLoadScene(string _scene)
	{
		SceneManager.LoadScene(_scene, LoadSceneMode.Single);
	}
}
