using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class BallManager : NetworkBehaviour
{
    #region private
    [SerializeField]
    private float speed = 8f;
    private float speedUp = 0.0f;
    [SyncVar(hook = "OnScaleChanged")]
    private float scale = 0.5f;
    private float scaleUp = 2;
    private float timeSpeed = 0.0f;
    private float timeScaleBall = 0.0f;

    [SyncVar(hook = "OnScoreChangedP1")]
    private int score_p1 = 0;
    [SyncVar(hook = "OnScoreChangedP2")]
    private int score_p2 = 0;

    private Transform m_Transform;
    private Rigidbody m_Rigidbody;    
    private Vector3 direction;

    private bool isSpeed = false;
    private bool isScale = false;
    private GameObject hitObject;
    #endregion

    [Command]
    private void CmdAddScale()
    {
        scale *= scaleUp;
    }

    [Command]
    private void CmdDefaultScale()
    {
        scale = 0.5f;
    }

    void Start()
    {
        m_Transform = this.transform;
        m_Transform.localScale = new Vector3(scale, scale, scale);
        m_Rigidbody = m_Transform.GetComponent<Rigidbody>();

        m_Rigidbody.velocity = Vector3.right * speed;
        direction = Vector3.right;
    }

    private void Update()
    {
        if (!OnlineGameManager.Instance.isStart ||            
            OnlineGameManager.Instance.m_state == OnlineGameManager.STATE.Pause || 
            OnlineGameManager.Instance.m_state == OnlineGameManager.STATE.EndGame)
        {
            m_Rigidbody.isKinematic = true;
        }
        else
        {
            if (m_Rigidbody.isKinematic)
            {
                m_Rigidbody.isKinematic = false;
                m_Rigidbody.velocity = direction * (speed + speedUp);
            }
        }

        if (isSpeed)
        {
            speedUp = (timeSpeed > 0) ? speed : 0;
            isSpeed = (timeSpeed > 0);
            timeSpeed -= Time.deltaTime;
            m_Rigidbody.velocity = direction * (speed + speedUp);
        }

        if (isScale)
        {           
            isScale = (timeScaleBall > 0);
            timeScaleBall -= Time.deltaTime;
            if(!isScale)
            {
                CmdDefaultScale();
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            Debug.Log("Hit item");
            switch (collision.gameObject.GetComponent<ItemEffect>().m_itemType)
            {
                case ItemEffect.EITEM.Speed:
                    isSpeed = true;
                    timeSpeed = 5.0f;
                    break;
                case ItemEffect.EITEM.ScaleBall:
                    isScale = true;
                    timeScaleBall = 5.0f;
                    CmdAddScale(); // scale up
                    break;
                default: break;
            }
            GameObject.Find("OnlineModeSceneManager").GetComponent<OnlineModeManager>().OnDestroyItem();            
        }  
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {            
            if (OnlineGameManager.Instance.isHost) 
            {
                if(collision.gameObject.transform.position.x == -6 )        
                {    
                    CmdAddScore1();            
                }
                else
                {
                    CmdAddScore2();            
                }
            }
            else 
            {
                if(collision.gameObject.transform.position.x == -6 )        
                {    
                    CmdAddScore2();            
                }
                else
                {
                    CmdAddScore1();            
                }
            }
        }

        if (collision.gameObject.tag == "Player")
        {
            ChangeDirection(collision);
        }
    }

    [Command]
    private void CmdAddScore1()
    {
        score_p1++;
    }

    [Command]
    private void CmdAddScore2()
    {
        score_p2++;
    }

    private void OnScoreChangedP1(int newScore)
    {               
        UIInterface.Instance.UpdateScoreP1(newScore);          
        Debug.Log("Add score to player 1");        
    }

    private void OnScoreChangedP2(int newScore)
    {               
        UIInterface.Instance.UpdateScoreP2(newScore);          
        Debug.Log("Add score to player 2");        
    }

    private void ChangeDirection(Collision collision)
    {
        float newDirectionY = HitObject(m_Transform.position, collision.transform.position, collision.collider.bounds.size.y);
        float newDirectionX = (m_Rigidbody.velocity.x >= 0f) ? 1f : -1f;

        direction = new Vector3(newDirectionX, newDirectionY, 0f).normalized;
        m_Rigidbody.velocity = direction * speed;
    }

    private float HitObject(Vector3 ballPosition, Vector3 paddlePosition, float paddleHeight)
    {
        return (ballPosition.y - paddlePosition.y) / paddleHeight;
    }

    private void OnScaleChanged(float _scale)
    {
        Debug.Log("OnScaleChanged " + _scale);
        m_Transform.localScale = new Vector3(_scale, _scale, _scale);
    }
}
