using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILeaderboard : MonoBehaviour 
{
	private static UILeaderboard _instance;
	public static UILeaderboard Instance
	{
		get { return _instance; }
	}

	public Transform m_canvs;
	public GameObject m_item;
	public Button m_close;
	// Use this for initialization
	void Awake () {
		if (_instance != null) 
		{ return; }
		_instance = this;

		if (m_close != null) m_close.onClick.AddListener(()=>{UIInterface.Instance.CloseLeaderBoard();});
	}
	
	public void SetData(LeaderBoardData _dataLeader)
	{
		for (int i =0 ; i< _dataLeader.dataList.Count; i++) 
		{
			GameObject _item = (GameObject)Instantiate(m_item);			
        	_item.transform.SetParent(m_canvs, false);
        	_item.SetActive(true);
        	_item.GetComponent<UIItemsList>().SetLabel(_dataLeader.dataList[i].name, _dataLeader.dataList[i].score);  
		}
	}
}
