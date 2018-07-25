using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SinglePlayerInpuController : MonoBehaviour {

	#region private
    [SerializeField]
    private float speed = 8f;
    [SerializeField]
    private string axis = "Vertical";

    private Rigidbody m_Rigidbody;
    #endregion

	private void Start () {
		m_Rigidbody = this.transform.GetComponent<Rigidbody>();
	}
	
	
	private void FixedUpdate() {
		float currentVelocity = Input.GetAxisRaw(axis);
        m_Rigidbody.velocity = Vector3.up * speed * currentVelocity;	
	} 
}
