using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OnlineModeManager : NetworkBehaviour {

    private static OnlineModeManager _instance;
    public static OnlineModeManager Instance
    {
        get { return _instance; }
    }

    #region public
	public bool isReady;
    public bool isStart = true;
	public GameObject m_ballPrefab;
    public GameObject m_leftWallPrefab;
    public GameObject m_rightWallPrefab;
    public GameObject m_item;
    public float timeStart = 3.0f;    
    #endregion

    private GameObject ball = null;
    private GameObject item = null;

	public override void OnStartServer()
    {        
        GameObject leftWall = Instantiate<GameObject>(m_leftWallPrefab, new Vector3(9.38f, 0f, -9f), Quaternion.identity);
        GameObject rightWall = Instantiate<GameObject>(m_rightWallPrefab, new Vector3(-9.38f, 0f, -9f), Quaternion.identity);

        NetworkServer.Spawn(leftWall);
        NetworkServer.Spawn(rightWall);
    }

    private void Awake() 
    {
        if (_instance != null) {
            return;
        }
        _instance = this;
    }

    private void Update()
    {
        if (isReady && isStart) {
            
            if (timeStart <= 0) {
                OnSpawnBall();
                StartCoroutine("OnSpawnItem");
            }            
        }    
    }

    public void OnSpawnBall()
    {
        isStart = false;
        ball = Instantiate<GameObject>(m_ballPrefab, new Vector3(0f, 0f, -9f), Quaternion.identity);
        NetworkServer.Spawn(ball);        
    }

    public void OnDestroyBall()
    {
        if (ball != null) {
            ball.transform.position = new Vector3(0f, 0f, -9f);            
        }
    }

    IEnumerator OnSpawnItem() 
    {
        yield  return new WaitForSeconds(5);
        if (item == null) {
            item = Instantiate<GameObject>(m_item, new Vector3(Random.Range(-3, 3), Random.Range(-4, 3), -9f), Quaternion.identity);
            item.GetComponent<ItemEffect>().m_itemType = (ItemEffect.EITEM)Random.Range((int)ItemEffect.EITEM.Speed, (int)ItemEffect.EITEM.Freeze + 1);
            NetworkServer.Spawn(item);  
        }
        OnSpawnItem();
    }

    public void OnDestroyItem()
    {
        if (item != null) {
            Destroy(item);
            item = null;         
        }
    }
}
