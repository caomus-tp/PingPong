using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class OnlinePlayerInpuController : NetworkBehaviour {

 	#region private
    [SerializeField]
    private float speed = 8f;
    [SerializeField]
    private string axis = "Vertical";

    private Rigidbody m_Rigidbody;
    #endregion

	// Use this for initialization
	void Start () 
	{
		if (!isLocalPlayer) {
            Destroy(this);
            return;
        }

        m_Rigidbody = this.transform.GetComponent<Rigidbody>();
	}
	
	private void FixedUpdate()
    {
        float currentVelocity = Input.GetAxisRaw("Vertical");
        m_Rigidbody.velocity = Vector3.up * speed * currentVelocity;
    }

    public override void OnStartLocalPlayer()
    {
        this.GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}
