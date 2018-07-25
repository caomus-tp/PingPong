using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OnlineModeManager : NetworkBehaviour {
	
	public GameObject m_ballPrefab;
    public GameObject m_leftWallPrefab;
    public GameObject m_rightWallPrefab;

	public override void OnStartServer()
    {
        GameObject ball = Instantiate<GameObject>(m_ballPrefab, new Vector3(0f, 0f, -9f), Quaternion.identity);
        GameObject leftWall = Instantiate<GameObject>(m_leftWallPrefab, new Vector3(9.38f, 0f, -9f), Quaternion.identity);
        GameObject rightWall = Instantiate<GameObject>(m_rightWallPrefab, new Vector3(-9.38f, 0f, -9f), Quaternion.identity);

        NetworkServer.Spawn(ball);
        NetworkServer.Spawn(leftWall);
        NetworkServer.Spawn(rightWall);
    }
}
