using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallManager : MonoBehaviour {

    #region private
    [SerializeField]
    private float speed = 8f;
    private float speedUp = 0.0f;
    private float scale = 0.5f;
    private float scaleUp = 2;
    private float timeSpeed = 0.0f;
    private float timeScaleBall = 0.0f;

    private Transform m_Transform;
    private Rigidbody m_Rigidbody;
    private int score = 0;
    private Vector3 direction;

    private bool isSpeed = false;
    private bool isScale = false;
    #endregion
	
	void Start () 
    {
		m_Transform = this.transform;
        m_Transform.localScale = new Vector3(scale, scale, scale);
        m_Rigidbody = m_Transform.GetComponent<Rigidbody>();

        m_Rigidbody.velocity = Vector3.right * speed;
	}

    private void Update()
    {
        if (OnlineGameManager.Instance.m_state == OnlineGameManager.STATE.Pause) {
            m_Rigidbody.isKinematic = true;
        }
        else {
            if (m_Rigidbody.isKinematic) {
                m_Rigidbody.isKinematic = false;
                m_Rigidbody.velocity = direction * speed * speedUp;
            }            
        }

        if(isSpeed)
        {
            speedUp = (timeSpeed > 0) ? speed : 0;
            isSpeed = (timeSpeed > 0);
            timeSpeed -= Time.deltaTime;
            m_Rigidbody.velocity = direction * (speed + speedUp);
        }

        if(isScale)
        {
            m_Transform.localScale = (timeScaleBall > 0) ? new Vector3(scale * scaleUp, scale * scaleUp, scale * scaleUp) : new Vector3(scale, scale, scale);            
            isScale = (timeScaleBall > 0);
            timeScaleBall -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider collision) 
    {
        if (collision.gameObject.tag == "Item") {
            Debug.Log("Hit item");
            switch(collision.gameObject.GetComponent<ItemEffect>().m_itemType)
            {
                case ItemEffect.EITEM.Speed:  
                    isSpeed = true;
                    timeSpeed = 5.0f;
                break;
                case ItemEffect.EITEM.ScaleBall: 
                    isScale = true;
                    timeScaleBall = 5.0f;
                break;
                default: break;
            }
            OnlineModeManager.Instance.OnDestroyItem();
        }
    }
	
	private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") {
            ChangeDirection(collision);
        }

        // add score single player
        if (collision.gameObject.tag == "Goal") {
            Debug.Log("Add Score");
            score++;
            UIInterface.Instance.UpdateScoreP1(score);
        }
    }

    private void ChangeDirection(Collision collision)
    {
        float newDirectionY = HitObject(m_Transform.position, collision.transform.position, collision.collider.bounds.size.y);
        float newDirectionX = (m_Rigidbody.velocity.x >= 0f) ? 1f: -1f;

        direction = new Vector3(newDirectionX, newDirectionY, 0f).normalized;
        m_Rigidbody.velocity = direction * speed;
    }

    private float HitObject(Vector3 ballPosition, Vector3 paddlePosition, float paddleHeight)
    {
        return (ballPosition.y - paddlePosition.y) / paddleHeight;
    }
}
