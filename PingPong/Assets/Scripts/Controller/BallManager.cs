using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallManager : MonoBehaviour {

    #region private
    [SerializeField]
    private float speed = 8f;

    private Transform m_Transform;
    private Rigidbody m_Rigidbody;
    #endregion
	
	void Start () 
    {
		m_Transform = this.transform;
        m_Rigidbody = m_Transform.GetComponent<Rigidbody>();

        m_Rigidbody.velocity = Vector3.right * speed;
	}
	
	private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") {
            ChangeDirection(collision);
        }

        if (collision.gameObject.tag == "Goal") {
            Debug.Log("Add Score");
            UIInterface.Instance.UpdateScoreP1();
        }
    }

    private void ChangeDirection(Collision collision)
    {
        float newDirectionY = HitObject(m_Transform.position, collision.transform.position, collision.collider.bounds.size.y);
        float newDirectionX = (m_Rigidbody.velocity.x >= 0f) ? 1f: -1f;

        Vector3 direction = new Vector3(newDirectionX, newDirectionY, 0f).normalized;
        m_Rigidbody.velocity = direction * speed;
    }

    private float HitObject(Vector3 ballPosition, Vector3 paddlePosition, float paddleHeight)
    {
        return (ballPosition.y - paddlePosition.y) / paddleHeight;
    }
}
